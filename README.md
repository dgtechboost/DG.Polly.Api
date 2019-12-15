# DG.Polly.Api
Web Api to get Document, Document metadata with added resilience using Polly

Call "api/document/12345/status" to get document status

This API returns the status of a document, either created , notReady or notFound.

Status should be 'created' for it to return status response object with messageId

For other status it would keep trying. Timespan can be adjusted. If required cancellation token can stop polling.

Resource for {id}/metadata is part of contrived scenario

Samee as resource for {id}

The idea is to call resource with {id} - messageId

The query would call get status query

Once it succesfull it would call metadata query to get the required data.
