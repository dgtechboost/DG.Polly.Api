using System.Threading;
using System.Threading.Tasks;
using DG.Polly.Contracts;

namespace DG.Polly.Business.Documents.Queries.GetStatus
{
    public interface IGetDocumentStatusQuery
    {
        Task<DocumentStatusContract> ExecuteAsync(string id, CancellationToken cancelToken);
    }
}
