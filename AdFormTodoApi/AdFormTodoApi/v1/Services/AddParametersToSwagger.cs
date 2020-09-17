using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace AdFormTodoApi.Services
{
    public class AddParametersToSwagger : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();


            if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor descriptor && descriptor.ControllerName.StartsWith("Todo"))
            {
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "x-correlation-id",
                    In = ParameterLocation.Header,
                    Description = "Corrrelation ID",
                    Required = true
                });
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "accept",
                    In = ParameterLocation.Header,
                    Description = "",
                    Required = true
                });
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "content-location",
                    In = ParameterLocation.Header,
                    Description = "",
                    Required = true
                });

            }
        }
    }
}
