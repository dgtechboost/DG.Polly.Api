using System.Threading.Tasks;
using DG.Polly.Contracts;

namespace DG.Polly.Business.Documents.Queries.GetMetadata
{
    public class GetDocumentMetadateQuery
        : IGetDocumentMetadateQuery
    {
        private readonly IDocumentsService _documentsService;

        public GetDocumentMetadateQuery(
            IDocumentsService documentsService)
        {
            _documentsService = documentsService;
        }

        public async Task<DocumentMetadataContract> ExecuteAsync(string id)
        {
            return await _documentsService.GetDocumentMetadataAsync(id);
        }
    }
}
