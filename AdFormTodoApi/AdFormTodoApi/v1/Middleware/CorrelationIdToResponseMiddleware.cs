﻿using AdFormTodoApi.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AdFormTodoApi.Middleware
{
    public class CorrelationIdToResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CorrelationIdToResponseMiddleware> _logger;
        public CorrelationIdToResponseMiddleware(RequestDelegate next, ILogger<CorrelationIdToResponseMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }
        public Task InvokeAsync(HttpContext context, CorrelationID correlationIDs)
        {
            context.Request.Headers.TryGetValue("x-correlation-id", out var traceValue);
            _logger.LogInformation("Corelation Id Tagged with Request: {0}", traceValue);
            if (string.IsNullOrWhiteSpace(traceValue))
            {
                traceValue = new Guid().ToString();
                
            }
            context.Response.OnStarting((state) =>
            {
                context.Response.Headers.Add("x-correlation-id", traceValue);
                return Task.FromResult(0);
            }, context);
           
            // ensures all entries are tagged with some values
            using (_logger.BeginScope(correlationIDs.GetCurrentID()))
            {
                // Call the next delegate/middleware in the pipeline
                return _next(context);
            }
        }
    }
}
