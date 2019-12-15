using System.Threading.Tasks;
using Refit;

namespace DG.Polly.Contracts
{
    public interface IDocumentsService
    {
        [Get("/{messageId}")]
        Task<DocumentMetadataContract> GetDocumentMetadataAsync(string messageId);
    }
}
