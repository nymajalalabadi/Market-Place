#pragma checksum "D:\C#\MarketPlace\MarketPlace.Web\Areas\User\Views\Ticket\TicketDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "67968823cb81a3f3609027d79913129eb06062bd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_User_Views_Ticket_TicketDetail), @"mvc.1.0.view", @"/Areas/User/Views/Ticket/TicketDetail.cshtml")]
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
#line 1 "D:\C#\MarketPlace\MarketPlace.Web\Areas\User\Views\_ViewImports.cshtml"
using MarketPlace.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\C#\MarketPlace\MarketPlace.Web\Areas\User\Views\_ViewImports.cshtml"
using MarketPlace.Web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\C#\MarketPlace\MarketPlace.Web\Areas\User\Views\Ticket\TicketDetail.cshtml"
using MarketPlace.Application.Utils;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\C#\MarketPlace\MarketPlace.Web\Areas\User\Views\Ticket\TicketDetail.cshtml"
using MarketPlace.DataLayer.DTOs.Contacts;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67968823cb81a3f3609027d79913129eb06062bd", @"/Areas/User/Views/Ticket/TicketDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e8610fbe0cb10ada5d00defb645f03933b741248", @"/Areas/User/Views/_ViewImports.cshtml")]
    public class Areas_User_Views_Ticket_TicketDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MarketPlace.DataLayer.DTOs.Contacts.TicketDetailDTO>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_AnswerTicketPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "D:\C#\MarketPlace\MarketPlace.Web\Areas\User\Views\Ticket\TicketDetail.cshtml"
  
    ViewData["Title"] = "جزیات تیکت";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("styles", async() => {
                WriteLiteral("\r\n    <link rel=\"stylesheet\" href=\"/css/ChatRoom.css\" />\r\n");
            }
            );
            WriteLiteral(@"

<div class=""breadcrumbs_area"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-12"">
                <div class=""breadcrumb_content"">
                    <ul>
                        <li><a href=""/"">خانه</a></li>
                        <li>");
#nullable restore
#line 21 "D:\C#\MarketPlace\MarketPlace.Web\Areas\User\Views\Ticket\TicketDetail.cshtml"
                       Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</div>


<section class=""main_content_area"">
    <div class=""container"">
        <div class=""account_dashboard"">
            <div class=""row"">
                <div class=""col-sm-12 col-md-3 col-lg-3"">
                    ");
#nullable restore
#line 35 "D:\C#\MarketPlace\MarketPlace.Web\Areas\User\Views\Ticket\TicketDetail.cshtml"
               Write(await Component.InvokeAsync("UserSidebar"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                </div>
                <div class=""col-sm-12 col-md-9 col-lg-9"">
                    <!-- Tab panes -->
                    <div class=""tab-content dashboard_content"">
                        <div class=""tab-pane fade active show"" id=""account-details"">
                            <h3>");
#nullable restore
#line 41 "D:\C#\MarketPlace\MarketPlace.Web\Areas\User\Views\Ticket\TicketDetail.cshtml"
                           Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h3>\r\n\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "67968823cb81a3f3609027d79913129eb06062bd6055", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 43 "D:\C#\MarketPlace\MarketPlace.Web\Areas\User\Views\Ticket\TicketDetail.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = (new AnswerTicketDTO{Id = Model.Ticket.Id});

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n                            <hr />\r\n                            <ul class=\"messages\" id=\"messages\">\r\n");
#nullable restore
#line 47 "D:\C#\MarketPlace\MarketPlace.Web\Areas\User\Views\Ticket\TicketDetail.cshtml"
                                 if (Model.TicketMessages != null && Model.TicketMessages.Any())
                                {
                                    foreach (var message in Model.TicketMessages)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <li");
            BeginWriteAttribute("class", " class=\"", 1809, "\"", 1896, 3);
            WriteAttributeValue("", 1817, "message", 1817, 7, true);
#nullable restore
#line 51 "D:\C#\MarketPlace\MarketPlace.Web\Areas\User\Views\Ticket\TicketDetail.cshtml"
WriteAttributeValue(" ", 1824, message.SenderId == Model.Ticket.OwnerId ? "right" : "left", 1825, 62, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 1887, "appeared", 1888, 9, true);
            EndWriteAttribute();
            WriteLiteral(@">
                                            <div class=""avatar"">
                                                <img src=""/img/defaults/avatar.jpg"" alt=""Alternate Text"">
                                            </div>
                                            <div class=""text_wrapper"">
                                                <div class=""time"">
                                                    ");
#nullable restore
#line 57 "D:\C#\MarketPlace\MarketPlace.Web\Areas\User\Views\Ticket\TicketDetail.cshtml"
                                               Write(message.CreateDate.ToStringShamsiDate());

#line default
#line hidden
#nullable disable
            WriteLiteral(" ساعت ");
#nullable restore
#line 57 "D:\C#\MarketPlace\MarketPlace.Web\Areas\User\Views\Ticket\TicketDetail.cshtml"
                                                                                             Write(message.CreateDate.ToString("HH:mm"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                </div>\r\n                                                <div class=\"text\" style=\"font-size: 16px\">\r\n                                                    ");
#nullable restore
#line 60 "D:\C#\MarketPlace\MarketPlace.Web\Areas\User\Views\Ticket\TicketDetail.cshtml"
                                               Write(Html.Raw(message.Text));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                </div>\r\n                                            </div>\r\n                                        </li>\r\n");
#nullable restore
#line 64 "D:\C#\MarketPlace\MarketPlace.Web\Areas\User\Views\Ticket\TicketDetail.cshtml"
                                    }
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            </ul>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</section>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MarketPlace.DataLayer.DTOs.Contacts.TicketDetailDTO> Html { get; private set; }
    }
}
#pragma warning restore 1591
