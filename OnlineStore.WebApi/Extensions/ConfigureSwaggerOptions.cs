using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace OnlineStore.WebApi.Extensions;
public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
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
            var info = new OpenApiInfo()
            {
                Title = Assembly.GetCallingAssembly().GetName().Name,
                Version = description.ApiVersion.ToString()
            };
            if (description.IsDeprecated) info.Description += "This API version has been deprecated.";

            options.SwaggerDoc(description.GroupName, info);
        }
    }

    public void Configure(string name, SwaggerGenOptions options)
    {
        Configure(options);
    }
}