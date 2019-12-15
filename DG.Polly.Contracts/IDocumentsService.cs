using System.Threading.Tasks;
using Refit;

namespace DG.Polly.Contracts
{
    public interface IDocumentsService
    {
        [Get("/{messageId}/status")]
        Task<DocumentStatusContract> GetDocumentStatusAsync(int messageId);
    }
}
