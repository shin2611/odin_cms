#pragma checksum "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "99885d922902217fd04e8862babe112dfa79a1e3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_GetUserInfo), @"mvc.1.0.view", @"/Views/Home/GetUserInfo.cshtml")]
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
#line 1 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
using DataAccess.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
using ODIN_CMS.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"99885d922902217fd04e8862babe112dfa79a1e3", @"/Views/Home/GetUserInfo.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e4022e19e6a211cba1262f2c61ee9349b44f7f5a", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_GetUserInfo : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UserInfoViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("selected", new global::Microsoft.AspNetCore.Html.HtmlString("selected"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("defaultForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
  
    var UserInfo = Model.Users;
    var Groups = Model.ListGroups;
    var MerchantList = Model.ListMerchant;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
 if (IdentityHelper.GroupID <= 3)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg\" role=\"document\">\r\n        <div class=\"modal-content\">\r\n            <div class=\"modal-header\">\r\n");
#nullable restore
#line 15 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                 if (UserInfo != null && UserInfo.UserID > 0)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h4 class=\"modal-title\" id=\"myModalLabel33\">C???p nh???t user </h4>\r\n");
#nullable restore
#line 18 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h4 class=\"modal-title\" id=\"myModalLabel33\">Th??m m???i user </h4>\r\n");
#nullable restore
#line 22 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">??</button>\r\n            </div>\r\n            <div class=\"modal-body\">\r\n                <div class=\"box\">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "99885d922902217fd04e8862babe112dfa79a1e36579", async() => {
                WriteLiteral(@"
                        <div class=""box-body"">
                            <div class=""row"">
                                <div class=""col-12"">
                                    <div class=""form-group row"">
                                        <label for=""txtUserName"" class=""col-sm-2 col-form-label"">Username</label>
                                        <div class=""col-sm-10"">
                                            <input type=""text"" id=""txtUserName"" maxlength=""30"" name=""username""");
                BeginWriteAttribute("value", " value=\"", 1537, "\"", 1563, 1);
#nullable restore
#line 34 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
WriteAttributeValue("", 1545, UserInfo.Username, 1545, 18, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" class=""form-control"" placeholder=""Nh???p t??n T??i Kho???n"" />
                                        </div>
                                    </div>
                                    <div class=""form-group row"">
                                        <label for=""txtEmail"" class=""col-sm-2 col-form-label"">Email</label>
                                        <div class=""col-sm-10"">
                                            <input type=""text"" id=""txtEmail"" name=""Email"" maxlength=""150""");
                BeginWriteAttribute("value", " value=\"", 2060, "\"", 2083, 1);
#nullable restore
#line 40 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
WriteAttributeValue("", 2068, UserInfo.Email, 2068, 15, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" class=""form-control"" placeholder=""Nh???p ?????a ch??? Email"" data-rule-required=""true"" />
                                        </div>
                                    </div>
                                    <div class=""form-group row"">
                                        <label for=""txtFullName"" class=""col-sm-2 col-form-label"">Fullname</label>
                                        <div class=""col-sm-10"">
                                            <input type=""text"" id=""txtFullName"" name=""fullname"" maxlength=""50""");
                BeginWriteAttribute("value", " value=\"", 2617, "\"", 2643, 1);
#nullable restore
#line 46 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
WriteAttributeValue("", 2625, UserInfo.FullName, 2625, 18, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" class=""form-control"" placeholder=""Nh???p h??? t??n"" />
                                        </div>
                                    </div>
                                    <div class=""form-group row"">
                                        <label for=""txtpassword"" class=""col-sm-2 col-form-label"">Password</label>
                                        <div class=""col-sm-10"">
                                            <input type=""password"" id=""txtpassword"" name=""Password"" maxlength=""30"" class=""form-control"" placeholder=""Nh???p m???t kh???u"" />
                                        </div>
                                    </div>
                                    <div class=""form-group row"">
                                        <label for=""txtConfirmPass"" class=""col-sm-2 col-form-label"">Rewrite Password</label>
                                        <div class=""col-sm-10"">
                                            <input type=""password"" id=""txtConfirmPass"" name=""ConfirmPassword"" maxleng");
                WriteLiteral(@"th=""30"" class=""form-control"" placeholder=""nh???p l???i m???t kh???u"" />
                                        </div>
                                    </div>
                                    <div class=""form-group row"">
                                        <label for=""txtConfirmPass"" class=""col-sm-2 col-form-label"">Tr???ng th??i</label>
                                        <div class=""col-sm-10"">
                                            <div class=""checkbox"">
");
#nullable restore
#line 65 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                                                 if (UserInfo.Status == true)
                                                {

#line default
#line hidden
#nullable disable
                WriteLiteral("<input type=\"checkbox\" id=\"cbxIsActive\" checked=\"checked\" style=\"opacity:1;left:5px;\" />");
#nullable restore
#line 66 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                                                                                                                                         }
                                                else
                                                {

#line default
#line hidden
#nullable disable
                WriteLiteral("<input type=\"checkbox\" id=\"cbxIsActive\" style=\"opacity:1;left:5px;\" />");
#nullable restore
#line 68 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                                                                                                                       }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                                            </div>
                                        </div>
                                    </div>
                                    <div class=""form-group row"">
                                        <label for=""txtConfirmPass"" class=""col-sm-2 col-form-label"">Nh??m t??i kho???n :</label>
                                        <div class=""col-sm-10"">
                                            <select class=""form-select form-control select2"" id=""ddlGroupInfo"">
");
#nullable restore
#line 76 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                                                 if (Groups != null && Groups.Count > 0)
                                                {
                                                    if (UserInfo.UserID > 0)
                                                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "99885d922902217fd04e8862babe112dfa79a1e313336", async() => {
#nullable restore
#line 80 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                                                                                                        Write(ViewBag.GroupName);

#line default
#line hidden
#nullable disable
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 80 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                                                           WriteLiteral(ViewBag.GroupID);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 81 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                                                    }
                                                    foreach (var item in Groups)
                                                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "99885d922902217fd04e8862babe112dfa79a1e315776", async() => {
#nullable restore
#line 84 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                                                                                 Write(item.Name);

#line default
#line hidden
#nullable disable
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 84 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                                                           WriteLiteral(item.GroupID);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 85 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                                                    }
                                                }

#line default
#line hidden
#nullable disable
                WriteLiteral("                                            </select>\r\n                                        </div>\r\n                                    </div>\r\n");
#nullable restore
#line 90 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                                     if (IdentityHelper.MerchantID == 1)
                                    {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                                        <div class=""form-group row"">
                                            <label for=""txtConfirmPass"" class=""col-sm-2 col-form-label"">Ch???n c?? s??? :</label>
                                            <div class=""col-sm-10"">
                                                <select class=""form-select form-control select2"" id=""ddlMerchantInfo"">
");
#nullable restore
#line 96 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                                                     if (MerchantList != null && MerchantList.Count > 0)
                                                    {
                                                        if (UserInfo.UserID > 0)
                                                        {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "99885d922902217fd04e8862babe112dfa79a1e319329", async() => {
#nullable restore
#line 100 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                                                                                                               Write(ViewBag.MerchantName);

#line default
#line hidden
#nullable disable
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 100 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                                                               WriteLiteral(ViewBag.MerchantID);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 101 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                                                        }
                                                        foreach (var item in MerchantList)
                                                        {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "99885d922902217fd04e8862babe112dfa79a1e321811", async() => {
#nullable restore
#line 104 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                                                                                        Write(item.MerchantName);

#line default
#line hidden
#nullable disable
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 104 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                                                               WriteLiteral(item.MerchantID);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 105 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                                                        }
                                                    }

#line default
#line hidden
#nullable disable
                WriteLiteral("                                                </select>\r\n                                            </div>\r\n                                        </div>\r\n");
#nullable restore
#line 110 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
                                    }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                                </div>
                                <!-- /.col -->
                            </div>
                        </div>
                        <div class=""modal-footer"">
                            <button type=""button"" class=""btn btn-danger text-left"" data-dismiss=""modal"">Close</button>
                            <button type=""submit"" class=""btn btn-primary"" onclick=""SaveData();"">Save changes</button>
                        </div>
                        <input type=""hidden""");
                BeginWriteAttribute("value", " value=\"", 8132, "\"", 8156, 1);
#nullable restore
#line 119 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"
WriteAttributeValue("", 8140, UserInfo.UserID, 8140, 16, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" id=\"txtUserID\" />\r\n\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </div>\r\n\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <!-- /.modal -->\r\n");
#nullable restore
#line 128 "C:\Project\odin_cms\ODIN_CMS\Views\Home\GetUserInfo.cshtml"

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UserInfoViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
