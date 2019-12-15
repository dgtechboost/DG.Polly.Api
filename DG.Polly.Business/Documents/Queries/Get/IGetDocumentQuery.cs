using System.Threading.Tasks;
using DG.Polly.Contracts;

namespace DG.Polly.Business.Documents.Queries.Get
{
    public interface IGetDocumentQuery
    {
        Task<DocumentContract> ExecuteAsync(string id);
    }
}
