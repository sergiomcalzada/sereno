using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Sereno.Admin.Host.Options.OAuth2;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Sereno.Admin.Host.Options.Swagger
{
    public class SwaggerGenOptionsConfigure : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly OAuth2Options oauth2Options;

        public SwaggerGenOptionsConfigure(IOptions<OAuth2Options> oauth2Options)
        {
            this.oauth2Options = oauth2Options.Value;
        }

        public void Configure(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Swashbuckle Sample API",
                    Description = "A sample API for testing Swashbuckle"
                }
            );

            if (this.oauth2Options.IsEnabled)
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Name = "oauth2",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "oauth2"
                    },
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{this.oauth2Options.Authority}/connect/authorize"),
                            Scopes = new Dictionary<string, string>
                            {
                                {this.oauth2Options.Scope, this.oauth2Options.Scope}
                            }
                        }
                    }
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            }



            options.CustomOperationIds(description =>
            {
                if (description.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                {
                    var controllerName = controllerActionDescriptor.ControllerName;
                    var actionName = controllerActionDescriptor.AttributeRouteInfo.Name ??
                                     controllerActionDescriptor.ActionName;
                    var actionTemplate = controllerActionDescriptor.AttributeRouteInfo.Template;
                    var actionVerb = description.HttpMethod;
                    return $"{controllerName}_{actionName}_{actionTemplate}_{actionVerb}";
                }

                return description.ActionDescriptor.Id;
            });
            options.OperationFilter<BadRequestOperationFilter>();

            options.IncludeXmlComments(this.GetXmlCommentsPath(PlatformServices.Default.Application));
        }

        private string GetXmlCommentsPath(ApplicationEnvironment appEnvironment)
        {
            return Path.Combine(appEnvironment.ApplicationBasePath, "Sereno.Admin.Host.xml");
        }
    }
}