#pragma checksum "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetGroupInfo.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0e4a71e6b485c4233817f36380a9c28678896c11"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_GetGroupInfo), @"mvc.1.0.view", @"/Views/Home/GetGroupInfo.cshtml")]
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
#line 1 "C:\Project\odin_cms\ODIN_CMS\Views\_ViewImports.cshtml"
using ODIN_CMS;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetGroupInfo.cshtml"
using DataAccess.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetGroupInfo.cshtml"
using ODIN_CMS.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0e4a71e6b485c4233817f36380a9c28678896c11", @"/Views/Home/GetGroupInfo.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e4022e19e6a211cba1262f2c61ee9349b44f7f5a", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_GetGroupInfo : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<GroupEdit>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetGroupInfo.cshtml"
 if ((int)ViewBag.AdGroupID <= 3)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg\" role=\"document\">\r\n        <div class=\"modal-content\">\r\n            <div class=\"modal-header\">\r\n");
#nullable restore
#line 10 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetGroupInfo.cshtml"
                 if (Model != null && Model.GroupID > 0)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h4 class=\"modal-title\" id=\"myModalLabel33\">Cập nhật thông tin nhóm tài khoản</h4>\r\n");
#nullable restore
#line 13 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetGroupInfo.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h4 class=\"modal-title\" id=\"myModalLabel33\">Thêm mới nhóm tài khoản </h4>\r\n");
#nullable restore
#line 17 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetGroupInfo.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">×</button>\r\n            </div>\r\n            <div class=\"modal-body\">\r\n                <div class=\"box\">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0e4a71e6b485c4233817f36380a9c28678896c115514", async() => {
                WriteLiteral(@"
                        <div class=""box-body"">
                            <div class=""row"">
                                <div class=""col-12"">
                                    <div class=""form-group row"">
                                        <label for=""txtGroupName"" class=""col-sm-2 col-form-label"">Nhóm tài khoản</label>
                                        <div class=""col-sm-10"">
                                            <input type=""text"" id=""txtGroupName"" maxlength=""200"" name=""GroupName""");
                BeginWriteAttribute("value", " value=\"", 1422, "\"", 1441, 1);
#nullable restore
#line 29 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetGroupInfo.cshtml"
WriteAttributeValue("", 1430, Model.Name, 1430, 11, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\" placeholder=\"Nhập tên nhóm tài khoản\" />\r\n                                            <input type=\"hidden\" id=\"txtGroupID\"");
                BeginWriteAttribute("value", " value=\"", 1586, "\"", 1608, 1);
#nullable restore
#line 30 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetGroupInfo.cshtml"
WriteAttributeValue("", 1594, Model.GroupID, 1594, 14, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
                                        </div>
                                    </div>
                                </div>
                               
                                <div class=""modal-footer"">
                                    <button type=""button"" class=""btn btn-danger text-left"" data-dismiss=""modal"">Close</button>
                                    <button type=""button"" class=""btn btn-primary"" onclick=""SaveGroup();"">Save changes</button>
                                </div>
                            </div>
                        </div>
                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </div>\r\n                \r\n            </div>\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 47 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetGroupInfo.cshtml"

}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<GroupEdit> Html { get; private set; }
    }
}
#pragma warning restore 1591
