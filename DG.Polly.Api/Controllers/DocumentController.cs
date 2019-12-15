using System;
using System.Threading.Tasks;
using DG.Polly.Business.Documents.Queries.Get;
using DG.Polly.Business.Documents.Queries.GetMetadata;
using Microsoft.AspNetCore.Mvc;

namespace DG.Polly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController
        : ControllerBase
    {
        private readonly IGetDocumentQuery _getDocumentQuery;
        private readonly IGetDocumentMetadateQuery _getDocumenMetadataQuery;

        public DocumentController(
            IGetDocumentQuery getDocumentQuery,
            IGetDocumentMetadateQuery getDocumenMetadataQuery)
        {
            _getDocumentQuery = getDocumentQuery;
            _getDocumenMetadataQuery = getDocumenMetadataQuery;
        }

        /// Endpoint resource to get document based on Id
        /// </summary>
        /// <returns>Document</returns>
        [HttpGet("document/{id}")]
        public async Task<IActionResult> GetDocument(string id)
        {
            try
            {
                var documentById = await _getDocumentQuery.ExecuteAsync(id);

                return Ok(documentById);
            }
            catch (Exception ex)
            {
                return BadRequest();

            }
        }

        /// Endpoint resource to get document metadata based on Id
        /// </summary>
        /// <returns>Document metadata</returns>
        [HttpGet("document/{id}")]
        public async Task<IActionResult> GetDocumentMetadata(string id)
        {
            try
            {
                var documentMetadataById = await _getDocumenMetadataQuery.ExecuteAsync(id);

                return Ok(documentMetadataById);
            }
            catch (Exception ex)
            {
                return BadRequest();

            }
        }
    }
}
