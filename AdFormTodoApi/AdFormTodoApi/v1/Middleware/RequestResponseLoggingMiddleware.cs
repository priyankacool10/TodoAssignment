using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AdFormTodoApi.v1.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }


        public async Task Invoke(HttpContext context) 
        {
            await LogRequest(context);
            //await _next(context);
            await LogResponse(context);

        }

        private async Task LogRequest(HttpContext context) 
        {
            context.Request.EnableBuffering();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            _logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
                           $"Schema:{context.Request.Scheme} {Environment.NewLine}" +
                           $"Host: {context.Request.Host} {Environment.NewLine}" +
                           $"Path: {context.Request.Path} {Environment.NewLine}" +
                           $"Header: {DisplayHeaders(context.Request.Headers)} {Environment.NewLine}" +
                           $"QueryString: {context.Request.QueryString} {Environment.NewLine}" +
                           $"Request Body: {ReadStreamInChunks(requestStream)}");
            context.Request.Body.Position = 0;


        }

        private static string DisplayHeaders(IHeaderDictionary headers) 
        {
            var headerDictionary = new Dictionary<string, StringValues>(headers);
            foreach (var pair in headerDictionary)
            {
                return  pair.Key+ ":"+ pair.Value+",";
            }
            return string.Empty;
        }
        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            //We need to read the response stream from the beginning...
            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                                                   0,
                                                   readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }
        private async Task LogResponse(HttpContext context) {

            var originalBodyStream = context.Response.Body;

            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            await _next(context); //Calling subsequent middlewares and then returning back with response


            //We need to read the response stream from the beginning...
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            _logger.LogInformation($"Http Response Information:{Environment.NewLine}" +
                                   $"Response Status: {context.Response.StatusCode} {Environment.NewLine}" +
                                   $"Response Headers: {DisplayHeaders(context.Response.Headers)} " +
                                   $"Response Body: {responseBodyText}");

            await responseBody.CopyToAsync(originalBodyStream);
        }

    }
       
    
}
/*
 
1. First, read the request and format it into a string.
2. Next, create a dummy MemoryStream to load the new response into.
3. Then, wait for the server to return a response.
4. Finally, copy the dummy MemoryStream (containing the actual response) into the original stream, which gets returned to the client.
**
The reason for the creation of a dummy MemoryStream is that the response stream can only be read once.
During that read, we log what the response was as well as create a new stream (which hasn't been read yet)
and return that new stream to the client.
     
     
     */