using System.Threading.Tasks;
using DG.Polly.Business.Documents.Queries.GetMetadata;
using DG.Polly.Business.Documents.Queries.GetStatus;
using DG.Polly.Contracts;
using DG.Polly.Domain.Enums;

namespace DG.Polly.Business.Documents.Queries.Get
{
    public class GetDocumentQuery
        : IGetDocumentQuery
    {
        private readonly IGetDocumentMetadateQuery _getDocumentMetadateQuery;
        private readonly IGetDocumentStatusQuery _getDocumentStatusQuery;

        public GetDocumentQuery(
            IGetDocumentMetadateQuery getDocumentMetadateQuery,
            IGetDocumentStatusQuery getDocumentStatusQuery)
        {
            _getDocumentMetadateQuery = getDocumentMetadateQuery;
            _getDocumentStatusQuery = getDocumentStatusQuery;
        }

        public async Task<DocumentContract> ExecuteAsync(string id)
        {
            //if status is 'created' then call for metadata
            //get the data and used it further

            var apiResponse = await _getDocumentStatusQuery.ExecuteAsync(id, default);

            if (apiResponse.Status == DocumentStatusEnum.Created)
            {
               var metadata = await _getDocumentMetadateQuery.ExecuteAsync(id);

                return new DocumentContract {
                        MessageId = metadata.MessageId,
                        Name = metadata.Name
                };
            }

            return new DocumentContract();
        }
    }
}
