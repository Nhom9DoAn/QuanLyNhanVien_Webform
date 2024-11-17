using System.Web.Http;
using WebActivatorEx;
using Swashbuckle.Application;
using System.Reflection;

[assembly: PreApplicationStartMethod(typeof(QLNS.SwaggerConfig), "Register")]
namespace QLNS
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "QLNS API Documentation");

                    // Cấu hình mô tả API
                    c.DescribeAllEnumsAsStrings();
                    c.IncludeXmlComments(string.Format(@"{0}\bin\QLNS.XML",
                        System.AppDomain.CurrentDomain.BaseDirectory));
                })
                .EnableSwaggerUi(c =>
                {
                    // Sử dụng custom index.html
                    c.CustomAsset("index", thisAssembly, "QLNS.SwaggerUI.index.html");

                    // CSS và JS files
                    c.InjectStylesheet(thisAssembly, "QLNS.SwaggerUI.swagger-ui.css");
                    c.InjectJavaScript(thisAssembly, "QLNS.SwaggerUI.swagger-ui-bundle.js");
                    c.InjectJavaScript(thisAssembly, "QLNS.SwaggerUI.swagger-ui-standalone-preset.js");
                });
        }
    }
}