#pragma checksum "C:\Users\biboy\Source\repos\Payroll\Payroll.Web\Views\RoleMenu\_form.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "36a2c1746834e335691af531a2212aaba49f8fd8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_RoleMenu__form), @"mvc.1.0.view", @"/Views/RoleMenu/_form.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/RoleMenu/_form.cshtml", typeof(AspNetCore.Views_RoleMenu__form))]
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
#line 1 "C:\Users\biboy\Source\repos\Payroll\Payroll.Web\Views\_ViewImports.cshtml"
using Payroll.Web;

#line default
#line hidden
#line 2 "C:\Users\biboy\Source\repos\Payroll\Payroll.Web\Views\_ViewImports.cshtml"
using Payroll.Web.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"36a2c1746834e335691af531a2212aaba49f8fd8", @"/Views/RoleMenu/_form.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c22d830b85aae7d6f2014fc1a4ef656594314354", @"/Views/_ViewImports.cshtml")]
    public class Views_RoleMenu__form : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("main"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
            BeginContext(0, 427, true);
            WriteLiteral(@"<div class=""modal fade"" id=""myModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""myModalLabel"" aria-hidden=""true"" data-keyboard=""false"" data-backdrop=""static"">
    <div class=""modal-dialog"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h4 class=""modal-title"" id=""myModalLabel"">Role Menu Details</h4>
            </div>
            <div class=""modal-body"">
                ");
            EndContext();
            BeginContext(427, 1831, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e39d08385e3c47eb9129c09814aec2fd", async() => {
                BeginContext(443, 1808, true);
                WriteLiteral(@"
                    <input type=""hidden"" id=""role_menu_id"" />

                    <div class=""form-group"">
                        <label>Parent</label>
                        <select class=""custom-select"" id=""role_menu_parent_id""></select>
                    </div>
                    <div class=""form-group"">
                        <label>Role</label>
                        <select class=""custom-select"" id=""role_id""></select>
                    </div>
                    <div class=""form-group"">
                        <label for=""Name"">Display Name</label>
                        <input type=""text"" class=""form-control"" id=""display_name"" placeholder=""Display name"" />
                    </div>
                    <div class=""form-group"">
                        <label for=""Name"">Controller</label>
                        <input type=""text"" class=""form-control"" id=""controller_name"" placeholder=""Controller"" />
                    </div>
                    <div class=""form-group"">
  ");
                WriteLiteral(@"                      <label for=""Name"">Action</label>
                        <input type=""text"" class=""form-control"" id=""action_name"" placeholder=""Action"" />
                    </div>
                    <div class=""form-group"">
                        <label for=""Name"">Icon</label>
                        <input type=""text"" class=""form-control"" id=""display_icon"" placeholder=""Icon"" />
                    </div>
                    <div class=""form-group"">
                        <div class=""form-check"">

                            <input type=""checkbox"" class=""form-check-input"" id=""is_enable"" />
                            <label class=""form-check-label"" for=""Name"">Enabled</label>
                        </div>
                    </div>

                ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2258, 620, true);
            WriteLiteral(@"
            </div>
            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-primary"" id=""btnSave"" style=""display:none;"" onclick=""Update();"">Save</button>
                <button type=""button"" class=""btn btn-primary"" id=""btnUpdate"" style=""display:none;"" onclick=""Update();"">Update</button>
                <button type=""button"" class=""btn btn-danger"" id=""btnDelete"" style=""display:none;"" onclick=""Delete();"">Delete</button>
                <button type=""button"" class=""btn btn-default"" data-dismiss=""modal"">Close</button>
            </div>
        </div>
    </div>
</div>
");
            EndContext();
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
