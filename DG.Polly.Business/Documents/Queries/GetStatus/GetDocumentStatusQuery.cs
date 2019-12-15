using System.Threading.Tasks;
using DG.Polly.Contracts;

namespace DG.Polly.Business.Documents.Queries.GetStatus
{
    public class GetDocumentStatusQuery
        : IGetDocumentStatusQuery
    {
        private IDocumentsService _documentsService;

        public GetDocumentStatusQuery(
            IDocumentsService documentsService)
        {
            _documentsService = documentsService;
        }

        public async Task<DocumentStatusContract> ExecuteAsync(int id)
        {
            return await _documentsService.GetDocumentStatusAsync(id);
        }
    }
}
