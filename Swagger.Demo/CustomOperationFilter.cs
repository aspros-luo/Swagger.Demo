using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace Swagger.Demo
{
    public class CustomOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var apiDesc = context.ApiDescription;
            var attributes = GetActionAttributes(apiDesc);

            if (!attributes.Any())
                return;

            if (operation.Responses == null)
            {
                operation.Responses = new Dictionary<string, Response>();
            }

            foreach (var attribute in attributes)
            {
                ApplyAttribute(operation, context, attribute);
            }
        }

        private static void ApplyAttribute(Operation operation, OperationFilterContext context, CustomSwaggerResponseAttribute attribute)
        {
            //var key = attribute.StatusCode.ToString();
            var key = "200";
            Response response;
            if (!operation.Responses.TryGetValue(key, out response))
            {
                response = new Response();
            }

            if (attribute.Description != null)
                response.Description = attribute.Description;

            if (response.Schema == null)
            {
                response.Schema = new Schema();
            }
            var dataSchema = new Schema();

            if (attribute.IsPage)
            {
                dataSchema.Properties = new Dictionary<string, Schema>
                {
                    { "page_no", new Schema{ Type = "integer",Description = "页面数"} },
                    { "page_size", new Schema{ Type = "integer",Description = "页面个数"}},
                    { "total_page", new Schema{ Type = "integer",Description = "总页数"}},
                    { "total_count", new Schema{ Type = "integer",Description = "总数量"}}
                };
            }
            else if (attribute.IsSubmit)
            {
                dataSchema.Properties = new Dictionary<string, Schema>
                {
                    { "code", new Schema{ Type = "string",Description = "http 请求码"}},
                    { "is_success", new Schema{ Type = "boolean",Description = "是否请求成功"}},
                    { "msg", new Schema{ Type = "string",Description = "返回信息"}}
                };
            }
            foreach (var data in attribute.Datas)
            {
                if (!string.IsNullOrWhiteSpace(data.Key))
                {
                    var schema = new Schema();
                    if (data.Value == typeof(int) || data.Value == typeof(long))
                    {
                        schema.Type = "integer";
                    }
                    else if (data.Value == typeof(string))
                    {
                        schema.Type = "string";
                    }
                    else if (data.Value == typeof(decimal))
                    {
                        schema.Type = "number";
                    }
                    else if (data.Value == typeof(bool))
                    {
                        schema.Type = "boolean";
                    }
                    else if (attribute.IsList || attribute.IsPage)
                    {
                        schema.Type = "array";
                        schema.Items = new Schema() { Ref = context.SchemaRegistry.GetOrRegister(data.Value).Ref.Replace("DTO", "Dto").ToUnderscoreCase().Replace("/_", "/") };
                    }
                    else
                    {
                        schema.Ref = context.SchemaRegistry.GetOrRegister(data.Value).Ref.Replace("DTO", "Dto").ToUnderscoreCase().Replace("/_", "/");
                    }                    
                    if (dataSchema.Properties != null)
                    {
                        dataSchema.Properties.Add(data.Key, schema);
                    }
                    else
                    {
                        var properties = new Dictionary<string, Schema>
                        {
                            { data.Key, schema}
                        };
                        dataSchema.Properties = properties;
                    }
                }
            }
            response.Schema.Properties = new Dictionary<string, Schema>
            {
                { "data", dataSchema }
            };
            response.Schema.Items = null;
            operation.Responses[key] = response;
        }

        private static IEnumerable<CustomSwaggerResponseAttribute> GetActionAttributes(ApiDescription apiDesc)
        {
            var controllerAttributes = apiDesc.ControllerAttributes().OfType<CustomSwaggerResponseAttribute>();
            var actionAttributes = apiDesc.ActionAttributes().OfType<CustomSwaggerResponseAttribute>();
            return controllerAttributes.Union(actionAttributes);
        }
    }
}
