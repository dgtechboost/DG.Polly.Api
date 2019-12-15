using System;
using System.Threading.Tasks;
using DG.Polly.Business.Documents.Queries.Get;
using DG.Polly.Business.Documents.Queries.GetMetadata;
using DG.Polly.Business.Documents.Queries.GetStatus;
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
        private readonly IGetDocumentStatusQuery _getDocumentStatusQuery;

        public DocumentController(
            IGetDocumentQuery getDocumentQuery,
            IGetDocumentMetadateQuery getDocumenMetadataQuery,
            IGetDocumentStatusQuery getDocumentStatusQuery)
        {
            _getDocumentQuery = getDocumentQuery;
            _getDocumenMetadataQuery = getDocumenMetadataQuery;
            _getDocumentStatusQuery = getDocumentStatusQuery;
        }

        /// Endpoint resource to get document based on Id
        /// </summary>
        /// <returns>Document</returns>
        [HttpGet("{id}")]
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
        /// <returns>Document metadata object</returns>
        [HttpGet("{id}/metadata")]
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

        /// Endpoint resource to get document status based on Id
        /// </summary>
        /// <returns>Document status object</returns>
        [HttpGet("{id}/status")]
        public async Task<IActionResult> GetDocumentStatus(string id)
        {
            try
            {
                var documentStatusById = await _getDocumentStatusQuery.ExecuteAsync(id, default);

                return Ok(documentStatusById);
            }
            catch (Exception ex)
            {
                return BadRequest();

            }
        }
    }
}
