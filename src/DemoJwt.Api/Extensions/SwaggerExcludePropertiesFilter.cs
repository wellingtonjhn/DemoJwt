using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DemoJwt.Api.Extensions
{
    public class SwaggerExcludePropertiesFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            var excludeProperties = new[] { "notifications", "valid", "invalid" };

            foreach (var prop in excludeProperties)
            {
                if (model.Properties == null) continue;
                if (model.Properties.ContainsKey(prop))
                {
                    model.Properties.Remove(prop);
                }
            }
        }
    }
}