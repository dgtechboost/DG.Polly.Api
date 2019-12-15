using System;
using System.Threading.Tasks;
using DG.Polly.Contracts;

namespace DG.Polly.Business.Documents.Queries.Get
{
    public class GetDocumentQuery
        : IGetDocumentQuery
    {
        public GetDocumentQuery()
        {

        }

        public Task<DocumentContract> ExecuteAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
