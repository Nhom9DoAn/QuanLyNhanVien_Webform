using System.Web.Http;
using WebActivatorEx;
using QLNS;
using Swashbuckle.Application;
using System;
using System.IO;
using System.Collections.Generic;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]
namespace QLNS
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    // Thông tin cơ bản
                    c.SingleApiVersion("v1", "QLNS API Documentation")
                        .Description("API Quản Lý Nhân Sự");
                        

                    // Nhóm API theo HTTP Method
                    c.GroupActionsBy(apiDesc => apiDesc.HttpMethod.ToString());

                    // Tùy chỉnh operation sorting
                    c.OrderActionGroupsBy(new DescendingAlphabeticComparer());

                    c.DescribeAllEnumsAsStrings();

                })
                .EnableSwaggerUi(c =>
                {
                    // Tùy chỉnh UI
                    c.DocumentTitle("QLNS API Documentation");
                    c.DocExpansion(DocExpansion.List);

                    // Thêm CSS tùy chỉnh
                    c.InjectStylesheet(thisAssembly, "QLNS.Content.swagger-custom.css");
                });
        }
    }

    public class DescendingAlphabeticComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return string.Compare(y, x);
        }
    }
}