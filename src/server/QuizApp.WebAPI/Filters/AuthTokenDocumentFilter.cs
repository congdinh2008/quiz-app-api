using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace QuizApp.WebAPI;


/// <summary>
/// AuthTokenDocumentFilter
/// </summary>
public class AuthTokenDocumentFilter : IDocumentFilter
{
    /// <summary>
    /// AuthTokenDocumentFilter
    /// </summary>
    /// <param name="swaggerDoc"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Tags = [
                    new OpenApiTag
                    {
                        Name = "Token",
                        Description = "to get new access token."
                    }
                ];
    }
}