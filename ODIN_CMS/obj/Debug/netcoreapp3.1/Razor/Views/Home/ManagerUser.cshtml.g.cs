#pragma checksum "C:\Project\odin_cms\ODIN_CMS\Views\Home\ManagerUser.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ea8a82ac5fcaf5dd8379e80f1a9e1580e002305c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_ManagerUser), @"mvc.1.0.view", @"/Views/Home/ManagerUser.cshtml")]
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
#line 2 "C:\Project\odin_cms\ODIN_CMS\Views\_ViewImports.cshtml"
using ODIN_CMS.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Project\odin_cms\ODIN_CMS\Views\Home\ManagerUser.cshtml"
using DataAccess.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ea8a82ac5fcaf5dd8379e80f1a9e1580e002305c", @"/Views/Home/ManagerUser.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e4022e19e6a211cba1262f2c61ee9349b44f7f5a", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_ManagerUser : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "-1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Project\odin_cms\ODIN_CMS\Views\Home\ManagerUser.cshtml"
  
    ViewData["Title"] = "ManagerUser";
    Layout = "~/Views/Shared/_Layout.cshtml";

    var listGroup = ((List<Groups>)ViewBag.ListGroup);
    var listMerchant = ((List<Merchant>)ViewBag.ListMerchant);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<section class=""content"">
    <div class=""col-12"">
        <div class=""box"">
            <div class=""box-header with-border"">
                <h3 class=""box-title"">Tìm Kiếm Danh Sách Người Dùng</h3>
            </div>
            <div class=""box-body"">
                <div class=""row"">
                    <div class=""col-lg-4 mb-1"">
                        <div class=""form-group"">
                            <label class=""control-label""><i class=""fa fa-flag""></i>Trạng thái</label>
                            <div class=""col-lg-8 col-md-8 col-sm-8"">
                                <select class=""form-control select2"" id=""ddlActive"">
                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ea8a82ac5fcaf5dd8379e80f1a9e1580e002305c5124", async() => {
                WriteLiteral("Lấy tất cả");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ea8a82ac5fcaf5dd8379e80f1a9e1580e002305c6323", async() => {
                WriteLiteral("Hoạt Động");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ea8a82ac5fcaf5dd8379e80f1a9e1580e002305c7523", async() => {
                WriteLiteral("Không Hoạt Động");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class=""col-lg-4 mb-1"">
                        <div class=""form-group"">
                            <label class=""control-label""><i class=""fa fa-flag""></i>Nhóm tài khoản</label>
                            <div class=""col-lg-8 col-md-8 col-sm-8"">
                                <select class=""form-control select2"" id=""ddlGroup"">
                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ea8a82ac5fcaf5dd8379e80f1a9e1580e002305c9242", async() => {
                WriteLiteral("Lấy tất cả");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 37 "C:\Project\odin_cms\ODIN_CMS\Views\Home\ManagerUser.cshtml"
                                     if (listGroup != null && listGroup.Count > 0)
                                    {
                                        foreach (var item in listGroup)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ea8a82ac5fcaf5dd8379e80f1a9e1580e002305c10859", async() => {
#nullable restore
#line 41 "C:\Project\odin_cms\ODIN_CMS\Views\Home\ManagerUser.cshtml"
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
#line 41 "C:\Project\odin_cms\ODIN_CMS\Views\Home\ManagerUser.cshtml"
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
#line 42 "C:\Project\odin_cms\ODIN_CMS\Views\Home\ManagerUser.cshtml"
                                        }
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </select>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n");
#nullable restore
#line 48 "C:\Project\odin_cms\ODIN_CMS\Views\Home\ManagerUser.cshtml"
                     if (IdentityHelper.MerchantID == 1)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        <div class=""col-lg-4 mb-1"">
                            <div class=""form-group"">
                                <label class=""control-label""><i class=""fa fa-flag""></i>Cơ sở</label>
                                <div class=""col-lg-8 col-md-8 col-sm-8"">
                                    <select class=""form-control select2"" id=""ddlMerchant"">
                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ea8a82ac5fcaf5dd8379e80f1a9e1580e002305c13745", async() => {
                WriteLiteral("Lấy tất cả");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 56 "C:\Project\odin_cms\ODIN_CMS\Views\Home\ManagerUser.cshtml"
                                         if (listMerchant != null && listMerchant.Count > 0)
                                        {
                                            foreach (var item in listMerchant)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ea8a82ac5fcaf5dd8379e80f1a9e1580e002305c15392", async() => {
#nullable restore
#line 60 "C:\Project\odin_cms\ODIN_CMS\Views\Home\ManagerUser.cshtml"
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
#line 60 "C:\Project\odin_cms\ODIN_CMS\Views\Home\ManagerUser.cshtml"
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
#line 61 "C:\Project\odin_cms\ODIN_CMS\Views\Home\ManagerUser.cshtml"
                                            }
                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    </select>\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n");
#nullable restore
#line 67 "C:\Project\odin_cms\ODIN_CMS\Views\Home\ManagerUser.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n                <div class=\"row\">\r\n                    <div class=\"col-lg-4\">\r\n");
#nullable restore
#line 71 "C:\Project\odin_cms\ODIN_CMS\Views\Home\ManagerUser.cshtml"
                         if ((int)ViewBag.GroupID <= 2)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("<button type=\"button\" id=\"btnCreate\" class=\"btn btn-success\"><i class=\"mdi mdi-account-plus\"></i> Thêm User</button>");
#nullable restore
#line 72 "C:\Project\odin_cms\ODIN_CMS\Views\Home\ManagerUser.cshtml"
                                                                                                                                             }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class=""col-12"">
        <div class=""box"">
            <div class=""box-header with-border"">
                <h3 class=""box-title"">Danh sách người dùng</h3>
            </div>
            <div class=""box-body"">
                <div class=""table-responsive"" id=""UsersPartial"">

                </div>
            </div>
        </div>
    </div>
</section>

<script type=""text/javascript"">
        $(document).ready(function () {
            GetList();
            $(""#ddlActive"").change(function () {
                GetList();
            });
            $(""#ddlGroup"").change(function () {
                GetList();
            });
            $(""#ddlMerchant"").change(function () {
                GetList();
            });
            $(""#btnCreate"").click(function () {
                ViewUserData(0);
            });
            //let table1 = document.querySelector('#table1');");
            WriteLiteral(@"
            //let dataTable = new simpleDatatables.DataTable(table1);

            $('#defaultForm').bootstrapValidator({
                message: 'This value is not valid',
                //live: 'submitted',
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    username: {
                        message: 'The username is not valid',
                        validators: {
                            notEmpty: {
                                message: 'Username không được bỏ trống'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9\.]+$/,
                                message: 'Username gồm ký tự alphabeta hoặc số'
                            },
                            stringLength: {
          ");
            WriteLiteral(@"                      min: 3,
                                max: 30,
                                message: 'Username nhỏ nhất 3 ký tự và nhiều nhất 30 ký tự'
                            },
                            different: {
                                field: 'Password',
                                message: 'Username và password phải khác nhau'
                            }
                        }
                    },
                    fullname: {
                        message: 'Bạn chưa nhập họ tên.',
                        validators: {
                            notEmpty: {
                                message: 'Bạn chưa nhập họ tên'
                            },
                            stringLength: {
                                min: 3,
                                max: 50,
                                message: 'họ tên dài từ 3 đến 50 ký tự'
                            },
                            regexp: {
                             ");
            WriteLiteral(@"   regexp: /^[a-zA-Z0-9áàảãạăắằẳẵặâấầẩẫậđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửựýỳỷỹỵÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỰÝỲỶỸỴ\.\-\_\s]+$/,
                                message: 'Họ tên không được nhập các ký tự đặc biệt'
                            },
                        }
                    },
                    Email: {
                        validators: {
                            notEmpty: {
                                message: 'Email không được bỏ trống'
                            },
                            emailAddress: {
                                message: 'Bạn đã nhập sai định dạng địa chỉ email'
                            }
                        }
                    },
                    Password: {
                        validators: {
                            stringLength: {
                                min: 6,
                                max: 30,
                                message: 'Password phải từ 6 đ");
            WriteLiteral(@"ến 30 ký tự'
                            },
                            identical: {
                                field: 'ConfirmPassword',
                                message: 'Password và confirmPassword không trùng khớp'
                            },
                            different: {
                                field: 'username',
                                message: 'Password không thể trùng với Username'
                            }
                        }
                    },
                    ConfirmPassword: {
                        validators: {
                            identical: {
                                field: 'Password',
                                message: 'Password và confirmPassword không trùng khớp'
                            },
                            different: {
                                field: 'username',
                                message: 'Password không thể trùng với Username'
                            ");
            WriteLiteral(@"}
                        }
                    }
                }
            })
                .on('success.form.bv', function (e) {
                    // Prevent submit form
                    e.preventDefault();

                    var $form = $(e.target),
                        validator = $form.data('bootstrapValidator');
                    SaveData();
                })
                .on('error.form.bv', function (e) {
                    // Active the panel element containing the first invalid element
                    e.preventDefault();
                    var $form = $(e.target),
                        validator = $form.data('bootstrapValidator');
                    swal(""Có một vài lỗi trong khung nhập liệu dưới đây ! kiểm tra lại"");
                });

        });
        function GetList() {
            parameters = {
                isActive: $('#ddlActive :selected').val(),
                groupId: $('#ddlGroup :selected').val(),
               ");
            WriteLiteral(@" merchantId: $('#ddlMerchant :selected').val()
            };
            utils.Loading();
            var urlRequestAns = UrlRoot + ""Home/ListUsers"";
            $.ajax({
                type: 'GET',
                url: urlRequestAns,
                data: parameters,
                success: function (data) {
                    $(""#UsersPartial"").html(data);
                    utils.unLoading();
                },
                error: function () {
                    utils.unLoading();
                }
            });
    };

    function ViewUserData(id) {
        utils.Loading();
        $.ajax({
            type: 'GET',
            url: UrlRoot + ""Home/GetUserInfo"",
            data: {
                'id': id
            },
            dataType: 'html',
            success: function (data) {
                utils.unLoading();
                $(""#PopupThongBao"").html(data).modal('show');
            }
        });
    }
    //Save Data
    function SaveData() {
  ");
            WriteLiteral(@"      var param = {
            'UserID': $(""#txtUserID"").val(),
            'Username': $(""#txtUserName"").val(),
            'FullName': $(""#txtFullName"").val(),
            'Email': $(""#txtEmail"").val(),
            'Password': $(""#txtpassword"").val(),
            'Status': $(""#cbxIsActive"").is("":checked"") ? true : false,
            'GroupID': $('#ddlGroupInfo').val(),
            'MerchantID': $('#ddlMerchantInfo').val(),
        };
        utils.Loading();
        $.ajax({
            type: 'POST',
            url: UrlRoot + ""Home/SaveDataUser"",
            data: param,
            async: true,
            success: function (data) {
                debugger;
                utils.unLoading();
                if (data.responseCode >= 0) {
                    $('#PopupThongBao').modal('hide');
                    bootbox.alert(data.responseDesc);
                    setTimeout(function () { GetList(); }, 2500);
                }
                else {
                    bootbox.al");
            WriteLiteral(@"ert(data.responseDesc);
                }
            }
        });
    };

    //Update Status User
    function UpdateStatusUser(id, status) {
        var type = ""Vô hiệu hóa"";
        if (status == 0) {
            type = ""Kích hoạt"";
        }
        bootbox.confirm(""Bạn muốn "" + type + "" Tài khoản này ?"", function (conf) {
            if (conf == 1) {
                var urlUpstatus = UrlRoot + ""Home/UpdateActiveUser"";
                utils.Loading();
                $.ajax({
                    type: 'POST',
                    url: urlUpstatus,
                    data: { id: id },
                    success: function (data) {
                        utils.unLoading();
                        if (data.ResponseCode >= 0) {
                            bootbox.alert(data.ResponseDesc);
                            setTimeout(function () {
                                GetList();
                            }, 1000);
                        }
                        else {
 ");
            WriteLiteral(@"                           bootbox.alert(data.ResponseDesc);
                            return;
                        }
                    },
                    error: function () {
                        utils.UnLoading();
                    }
                });
            }
            return;
        });
    };
      function DeleteData(id) {
            var IsDelete = '");
#nullable restore
#line 321 "C:\Project\odin_cms\ODIN_CMS\Views\Home\ManagerUser.cshtml"
                       Write(ViewBag.GroupID);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"';
            if (parseInt(IsDelete) > 2) {
            bootbox.alert(""Bạn không có quyền xóa chức năng"");
            return;
        }
        bootbox.confirm(""Bạn chắc chắn muốn xóa tài khoản này ??? "", function (result) {
            if (result == true) {
                utils.Loading();
                $.ajax({
                    type: 'POST',
                    url: UrlRoot + ""Home/DeleteUser"",
                    data: {
                        'id': id
                    },
                    success: function (data) {
                        debugger;
                        utils.unLoading();
                        if (data.responseCode >= 0) {
                            bootbox.alert(data.responseDesc);
                            GetList();
                        }
                        else {
                            bootbox.alert(data.responseDesc);
                            return;
                        }
                    }
                });");
            WriteLiteral("\r\n            }\r\n        });\r\n\r\n}\r\n</script>\r\n");
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
