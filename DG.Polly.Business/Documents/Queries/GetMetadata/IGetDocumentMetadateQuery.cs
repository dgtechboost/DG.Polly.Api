using System.Threading.Tasks;
using DG.Polly.Contracts;

namespace DG.Polly.Business.Documents.Queries.GetMetadata
{
    public interface IGetDocumentMetadateQuery
    {
        Task<DocumentMetadataContract> ExecuteAsync(string id);
    }
}
