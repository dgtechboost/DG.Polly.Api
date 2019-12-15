using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using DG.Polly.Contracts;
using DG.Polly.Domain.Enums;
using Newtonsoft.Json.Linq;
using Polly;

namespace DG.Polly.Business.Documents.Queries.GetStatus
{
    public class GetDocumentStatusQuery
        : IGetDocumentStatusQuery
    {
        private readonly IHttpClientFactory _clientFactory;
        private HttpClient _httpClient;
        private AsyncPolicy<DocumentStatusContract> _circuitBreakerPolicy;
        private AsyncPolicy<DocumentStatusContract> _retryPolicy;
        private Uri _baseUri;

        public GetDocumentStatusQuery(
            IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<DocumentStatusContract> ExecuteAsync(string id, CancellationToken cancelToken)
        {
            _baseUri = new Uri("https://ow-interview-exercise-dev.azurewebsites.net/documents");

            InitClient();

            _circuitBreakerPolicy = Policy
               .HandleResult<DocumentStatusContract>(x => x.Status != DocumentStatusEnum.Created)
               .CircuitBreakerAsync(5, TimeSpan.FromMinutes(1));

            _retryPolicy = Policy
                .HandleResult<DocumentStatusContract>(x => x.Status != DocumentStatusEnum.Created)
                .RetryForeverAsync(ex => Debug.WriteLine("Retrying connection"));//TODO: add logger

            var apiResponse = await RetryDocumentsService(id, cancelToken);

            return apiResponse;
        }

        private async Task<DocumentStatusContract> RetryDocumentsService(string id, CancellationToken cancelToken)
        {
            while (true && !cancelToken.IsCancellationRequested)
            {
                try
                {
                    Debug.WriteLine("Calling documents api");

                    var apiResponse = await _retryPolicy.WrapAsync(_circuitBreakerPolicy).ExecuteAsync(() =>
                    {
                        return GetValueAsync(id);
                    }
                    );

                    if (apiResponse.Status == DocumentStatusEnum.Created)
                    {
                        return apiResponse;
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error occured : " + ex.Message);
                }
            }

            return new DocumentStatusContract();
        }

        private void InitClient()
        {
            _httpClient = _clientFactory.CreateClient();
            _httpClient.BaseAddress = _baseUri;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("username", "testuser");
        }

        private async Task<DocumentStatusContract> GetValueAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{_baseUri}/{id}/status");

            var responseObject = await response.Content.ReadAsAsync<JToken>();
            var responseObjectMapped = responseObject?.ToObject<DocumentStatusContract>();
            return responseObjectMapped;
        }
    }
}
