using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Filters
{
    public class ApiVersionOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation,
            OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            var exists = operation.Parameters.Any(p =>
                p.Name == "version");

            if (!exists)
            {
                operation.Parameters.Insert(0,
                    new OpenApiParameter
                    {
                        Name = "version",
                        In = ParameterLocation.Path,
                        Required = true,
                        Description = "API Version",
                        Schema = new OpenApiSchema
                        {
                            Type = "string",
                            Default = new Microsoft.OpenApi.Any.OpenApiString("1")
                        }
                    });
            }
        }
    }
}