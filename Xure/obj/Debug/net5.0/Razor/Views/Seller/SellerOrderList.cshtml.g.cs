#pragma checksum "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Seller\SellerOrderList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f601502158c8d564df518fcacbb70eb0ec1873db"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Seller_SellerOrderList), @"mvc.1.0.view", @"/Views/Seller/SellerOrderList.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f601502158c8d564df518fcacbb70eb0ec1873db", @"/Views/Seller/SellerOrderList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d3f7859f03052d3269795b3029a26278f259eca4", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Seller_SellerOrderList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SellerOrderViewModel>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "OrderProductInfo", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Seller", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Seller\SellerOrderList.cshtml"
  
    Layout = "_ClientLayout";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container-fluid\">\r\n    <div class=\"row\">\r\n        <div class=\"col-8 offset-2 m-1 p-1\">\r\n             <div class=\"m-3 p-3\">\r\n                <h2>Мои заказы</h2>\r\n            </div>\r\n");
#nullable restore
#line 12 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Seller\SellerOrderList.cshtml"
             foreach (var i in Model.OrderProducts)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f601502158c8d564df518fcacbb70eb0ec1873db5247", async() => {
                WriteLiteral("                                        \r\n                    <input type=\"hidden\"");
                BeginWriteAttribute("value", " value=\"", 502, "\"", 522, 1);
#nullable restore
#line 15 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Seller\SellerOrderList.cshtml"
WriteAttributeValue("", 510, i.ProductId, 510, 12, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" name=\"productId\">    \r\n                    <input type=\"hidden\"");
                BeginWriteAttribute("value", " value=\"", 587, "\"", 605, 1);
#nullable restore
#line 16 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Seller\SellerOrderList.cshtml"
WriteAttributeValue("", 595, i.OrderId, 595, 10, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" name=\"orderId\">\r\n                    <div class=\"row border-dark m-3 p-3\">\r\n                            <div class=\"col-4\">\r\n                                <img class=\"goodInfoSettings\"");
                BeginWriteAttribute("src", " src=\"", 793, "\"", 865, 2);
                WriteAttributeValue("", 799, "data:image/jpeg;base64,", 799, 23, true);
#nullable restore
#line 19 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Seller\SellerOrderList.cshtml"
WriteAttributeValue("", 822, Convert.ToBase64String(@i.Product.Image), 822, 43, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                            </div>\r\n\r\n                            <div class=\"col-5\">\r\n                                <h2>");
#nullable restore
#line 23 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Seller\SellerOrderList.cshtml"
                               Write(i.Product.Name);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h2>\r\n                                <h5>Количество: ");
#nullable restore
#line 24 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Seller\SellerOrderList.cshtml"
                                           Write(i.Quantity);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h5>\r\n                                <h5>Цена: ");
#nullable restore
#line 25 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Seller\SellerOrderList.cshtml"
                                     Write(i.Product.Price.PriceHistory.Value);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h5>                                                     
                                <button type=""submit"" class=""btn btn-outline-danger"">Отправить заказ</button>
                            </div>
                        </div>                                            
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 30 "C:\Users\denis\OneDrive\Desktop\Xure\Xure\Views\Seller\SellerOrderList.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SellerOrderViewModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
