using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ECommerce.API.Configurations.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                var info = new OpenApiInfo
                {
                    Title = "Ecommerce API",
                    Version = description.ApiVersion.ToString(),
                    Description = description.IsDeprecated ? "⚠ This API version is deprecated. Please use a newer version." : "ECommerce API Documentation"
                };

                options.SwaggerDoc(description.GroupName, info);
            }
             options.DocInclusionPredicate((docname, apiDesc) => apiDesc.GroupName == docname);
        }
    }
}
