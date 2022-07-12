#pragma checksum "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\OrderList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ec90b09a33ee5d53aa1333b400777105c29a1df6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_OrderList), @"mvc.1.0.view", @"/Views/Admin/OrderList.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\_ViewImports.cshtml"
using Xure.App.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\_ViewImports.cshtml"
using Xure.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\_ViewImports.cshtml"
using Xure.App.Infrastructure;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\_ViewImports.cshtml"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ec90b09a33ee5d53aa1333b400777105c29a1df6", @"/Views/Admin/OrderList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a1db5bdf7ffa3e64d42da7d13d4ee674430ed9a3", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Admin_OrderList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<OrderProduct>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\OrderList.cshtml"
  
    Layout = "_AdminLayout";
    ViewBag.Title = "Заказы";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container-fluid\">\r\n    <div class=\"row\">\r\n        <div class=\"col mt-4\">\r\n            <h1>Информация о заказах</h1>\r\n");
#nullable restore
#line 12 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\OrderList.cshtml"
             if(Model != null) {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <table class=""table table-bordered"">
                <thead class=""thead thead light"">
                    <tr>
                        <th>Заказ</th>
                        <th>Продукт</th>
                        <th>Дата заказа</th>
                        <th>Код отслеживания</th>
                        <th>Магазин</th>
                        <th>Продавец</th>
                        <th>Клиент</th>

                    </tr>
                </thead>

                <tbody>
");
#nullable restore
#line 28 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\OrderList.cshtml"
                     foreach(var i in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>");
#nullable restore
#line 31 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\OrderList.cshtml"
                           Write(i.OrderId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 32 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\OrderList.cshtml"
                           Write(i.Product.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 33 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\OrderList.cshtml"
                           Write(i.OrderDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 34 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\OrderList.cshtml"
                           Write(i.TrackNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 35 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\OrderList.cshtml"
                           Write(i.Product.Seller.Company.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 36 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\OrderList.cshtml"
                           Write(i.Product.Seller.UserInfo.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 36 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\OrderList.cshtml"
                                                               Write(i.Product.Seller.UserInfo.Surname);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 36 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\OrderList.cshtml"
                                                                                                  Write(i.Product.Seller.UserInfo.MiddleName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 37 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\OrderList.cshtml"
                           Write(i.Order.Client.UserInfo.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 37 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\OrderList.cshtml"
                                                             Write(i.Order.Client.UserInfo.Surname);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                        </tr>\r\n");
#nullable restore
#line 39 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\OrderList.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\r\n            </table>\r\n");
#nullable restore
#line 42 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\OrderList.cshtml"
            } else 
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <h2>Закказов нет</h2>\r\n");
#nullable restore
#line 45 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\OrderList.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<OrderProduct>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
