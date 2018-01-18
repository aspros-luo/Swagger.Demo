using Newtonsoft.Json.Serialization;
using Swagger.Dto;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Swagger.Demo
{
    public class CustomSchemaFilter:ISchemaFilter
    {
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            var jsonObjectContract = context.JsonContract as JsonObjectContract;
            if (jsonObjectContract == null) return;

            var typeInfo = context.SystemType.GetTypeInfo();
            schema.Description = typeInfo.GetCustomAttributes<SwaggerDescriptionAttribute>().FirstOrDefault()?.Description ?? "";
            var dicProperties = new List<Tuple<string, string, object>>();
            var properties = typeInfo.GetProperties();
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttributes<SwaggerDescriptionAttribute>().FirstOrDefault();
                if (attribute != null && dicProperties.All(s => s.Item1 != attribute.Description))
                {
                    dicProperties.Add(new Tuple<string, string, object>(property.Name, attribute.Description, attribute.Default));
                }
            }
            if (schema.Properties == null) return;
            foreach (var property in schema.Properties)
            {
//                property.Value.Description = dicProperties.Select(s=>s.Item1==property.Key);

                property.Value.Description = dicProperties.FirstOrDefault(s => s.Item1.ToUnderscoreCase() == property.Key.ToUnderscoreCase())?.Item2;
                property.Value.Default = dicProperties.FirstOrDefault(s => s.Item1.ToUnderscoreCase() == property.Key.ToUnderscoreCase())?.Item3;
            }
        }
    }
}
