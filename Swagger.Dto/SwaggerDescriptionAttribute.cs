using System;

namespace Swagger.Dto
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class SwaggerDescriptionAttribute : Attribute
    {
        public SwaggerDescriptionAttribute(string description)
        {
            Description = description;
        }

        public SwaggerDescriptionAttribute(string description, object defaultValue)
        {
            Description = description;
            Default = defaultValue;
        }

        public string Description { get; set; }
        public object Default { get; set; }
    }
}
