#pragma checksum "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\ClientList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c423e653f9ffafb72e49c07631bd981d66a44b86"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_ClientList), @"mvc.1.0.view", @"/Views/Admin/ClientList.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c423e653f9ffafb72e49c07631bd981d66a44b86", @"/Views/Admin/ClientList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a1db5bdf7ffa3e64d42da7d13d4ee674430ed9a3", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Admin_ClientList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<AppUser>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\ClientList.cshtml"
  
    Layout = "_AdminLayout";
    ViewBag.Title = "??????????????";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""container-fluid"">
    <div class=""row"">
        <div class=""col-8 offset-2 mt-3"">
        
            <h3>???????????????????? ?? ????????????????</h3>

            <table class=""table table-bordered"">
                <thead class=""thead thead-light"">
                    <tr>
                        <th>Id</th>
                        <th>??????</th>
                        <th>??????????????</th>
                        <th>??????????????</th>
                        <th>??????????</th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 24 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\ClientList.cshtml"
                     foreach (var i in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <th>");
#nullable restore
#line 27 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\ClientList.cshtml"
                           Write(i.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th>");
#nullable restore
#line 28 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\ClientList.cshtml"
                           Write(i.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th>");
#nullable restore
#line 29 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\ClientList.cshtml"
                           Write(i.Surname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th>");
#nullable restore
#line 30 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\ClientList.cshtml"
                           Write(i.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th>");
#nullable restore
#line 31 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\ClientList.cshtml"
                           Write(i.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                        </tr>\r\n");
#nullable restore
#line 33 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Admin\ClientList.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<AppUser>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
