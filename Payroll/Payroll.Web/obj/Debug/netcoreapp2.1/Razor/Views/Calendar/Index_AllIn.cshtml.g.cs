#pragma checksum "C:\Users\biboy\Source\repos\Payroll\Payroll.Web\Views\Calendar\Index_AllIn.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4a866f441ac5fc2ca1ff7314dd723299d088f5fc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Calendar_Index_AllIn), @"mvc.1.0.view", @"/Views/Calendar/Index_AllIn.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Calendar/Index_AllIn.cshtml", typeof(AspNetCore.Views_Calendar_Index_AllIn))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4a866f441ac5fc2ca1ff7314dd723299d088f5fc", @"/Views/Calendar/Index_AllIn.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c22d830b85aae7d6f2014fc1a4ef656594314354", @"/Views/_ViewImports.cshtml")]
    public class Views_Calendar_Index_AllIn : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
#line 1 "C:\Users\biboy\Source\repos\Payroll\Payroll.Web\Views\Calendar\Index_AllIn.cshtml"
  
    /**/

    ViewBag.Title = "Calendar";


#line default
#line hidden
            BeginContext(55, 23, false);
#line 7 "C:\Users\biboy\Source\repos\Payroll\Payroll.Web\Views\Calendar\Index_AllIn.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
            EndContext();
            BeginContext(78, 1840, true);
            WriteLiteral(@"
<div class=""container-fluid"">
    <div class=""row"">
        <div class=""col-12"">
                <div class=""container-fluid"">
                <div class=""row"">
                        <div class=""col-md-12"">
                            <div class=""card card-primary"">
                                <div class=""card-body p-0"">
                                    <!-- THE CALENDAR -->
                                    <div id=""calendar""></div>
                                </div>
                                <!-- /.card-body -->
                            </div>
                        </div>
                    </div>
                <div class=""row"">
                    <div class=""col-md-2"">
                        <div class=""sticky-top mb-3"">
                            <div class=""card"">
                                <div class=""card-header"">
                                    <h4 class=""card-title"">Legend</h4>
                                </div>
                    ");
            WriteLiteral(@"            <div class=""card-body"">
                                    <!-- the events -->
                                    <div id=""external-events"">
                                        <div class=""external-event bg-success"">Approved</div>
                                        <div class=""external-event bg-warning"">For Approval</div>
                                    </div>
                                </div>
                                <!-- /.card-body -->
                            </div>
                            <!-- /.card -->
                        </div>
                    </div>
                    <div class=""col-md-10"">

                        </div>
                    </div>
                </div>
                </div>
        </div>
    </div>

");
            EndContext();
            BeginContext(1948, 425, true);
            WriteLiteral(@"
<div class=""modal fade"" id=""myModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""myModalLabel"" aria-hidden=""true"" data-keyboard=""false"" data-backdrop=""static"">
    <div class=""modal-dialog"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h4 class=""modal-title"" id=""myModalLabel"">Leave Details</h4>
            </div>
            <div class=""modal-body"">
                ");
            EndContext();
            BeginContext(2373, 1285, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "98d19ef0b8074e42b1651fc856159627", async() => {
                BeginContext(2389, 1262, true);
                WriteLiteral(@"

                    <input type=""hidden"" id=""request_leave_id"" />
                    <div class=""form-group"">
                        <label for=""Name"">Name</label>
                        <input type=""text"" class=""form-control"" id=""name"" />
                    </div>
                    <div class=""form-group"">
                        <label for=""Name"">Date</label>
                        <input type=""text"" class=""form-control"" id=""leave_date"" placeholder=""Date"" />
                    </div>
                    <div class=""form-group"">
                        <label>Type</label>
                        <select class=""custom-select"" id=""ref_leave_type_id"">
                        </select>

                    </div>

                    <div class=""form-group"">
                        <label for=""Name"">Reason</label>
                        <input type=""text"" class=""form-control"" id=""reason"" placeholder=""Reason"" />
                    </div>

                    <div class=""form-gro");
                WriteLiteral("up\">\r\n                        <label for=\"Name\">Approver Remarks</label>\r\n                        <input type=\"text\" class=\"form-control\" id=\"approver_remark\" placeholder=\"Approver Remarks\" />\r\n                    </div>\r\n                ");
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
            BeginContext(3658, 183, true);
            WriteLiteral("\r\n            </div>\r\n            <div class=\"modal-footer\">\r\n                <button type=\"button\" class=\"btn btn-primary\" id=\"btnApprove\" onclick=\"Process(true);\">Approve</button>\r\n");
            EndContext();
            BeginContext(3973, 157, true);
            WriteLiteral("                <button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Close</button>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            EndContext();
            DefineSection("scripts", async() => {
                BeginContext(4148, 13335, true);
                WriteLiteral(@"
    <script>
        $(document).ready(function () {
            Grid();
            $('#leave_date').datepicker({
                forceParse: false
            });
        });
        function getbyIDApproval(id) {
            $('#request_leave_id').val('');
            $('#leave_date').val('');

            $('#leave_day').val('');
            $('#reason').val('');
            $('#leave_date').css('border-color', 'lightgrey');
            $('#leave_day').css('border-color', 'lightgrey');
            $('#reason').css('border-color', 'lightgrey');
            $('#approver_remark').val('');
            $('#ref_leave_type_id option:first').prop('selected', true);

            $('#leave_date').prop('disabled', true);
            $('#name').prop('disabled', true);
            $('#reason').prop('disabled', true);
            $('#ref_leave_type_id').prop('disabled', true);


            if (id != null) {
                $.ajax({

                    url: 'RequestLeave/GetByID' + '/' + ");
                WriteLiteral(@"id,
                    typr: ""GET"",
                    contentType: ""application/json;charset=UTF-8"",
                    dataType: ""json"",
                    success: function (result) {
                        var d = result.leave_date.slice(0, 10).split('-');
                        var val = d[1] + '/' + d[2] + '/' + d[0];
                        $('#request_leave_id').val(result.request_leave_id);
                        $('#leave_date').val(val);
                        $('#ref_leave_type_id').val(result.ref_leave_type_id);
                        $('#leave_day').val(result.leave_day);
                        $('#reason').val(result.reason);
                        $('#name').val(result.employee_.last_name + ', ' + result.employee_.first_name);
                        if (result.ref_status_id == 1) {
                            $('#btnApprove').show();
                        }
                        $('#myModal').modal('show');
                    },
                    error: fun");
                WriteLiteral(@"ction (errormessage) {
                        swal_error(errormessage);
                    }
                });
            } else {
                $('#myModal').modal('show');
            }
        }
        function Process(bool) {
            var res = validateremarks();
            if (res == false) {
                return false;
            }

            var routes = 'RequestLeave/Approve';

            if (bool == false) {
                routes = 'RequestLeave/Disapprove';
            }
            var dataObject = JSON.stringify({
                'request_leave_id': $('#request_leave_id').val(),
                'leave_date': $('#leave_date').val(),
                'ref_leave_type_id': $('#ref_leave_type_id').val(),
                'leave_day': 1,
                'ref_department_id': $('#ref_department_id').val(),
                'reason': $('#reason').val(),
                'ref_status_id': 1,
                'approver_id': '',
                'approver_date': '',
  ");
                WriteLiteral(@"              'approver_remark': $('#approver_remark').val(),
                'date_deleted': ''
            });
            swal_wait();
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'POST',
                url: routes,
                data: dataObject,

                success: function (data) {
                    $('#myModal').modal('hide');
                    Swal.hideLoading();
                    swal_success();
                    $('#calendar').fullCalendar('removeEvents');
                    $('#calendar').fullCalendar('refetchEvents');
                },
                error: function (errormessage) {
                    Swal.hideLoading();
                    swal_error();
                }
            });
        }
        function Grid() {
            $('#calendar').fullCalendar({
                plugins: ['bootstrap', 'interaction', 'dayGrid', 'timeGrid'],
               ");
                WriteLiteral(@" header:
                {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
                buttonText: {
                    today: 'today',
                    month: 'month',
                    week: 'week',
                    day: 'day'
                },

                events: function (start, end, timezone, callback) {
                    $.ajax({
                        url: 'Calendar/GetData',
                        type: ""GET"",
                        dataType: ""JSON"",

                        success: function (result) {
                            var events = [];

                            $.each(result, function (i, data) {
                                var bgcol = '';
                                var fontcol = '';
                                if (data.ref_status_id == 1) {
                                    bgcol = 'orange';
                      ");
                WriteLiteral(@"              fontcol = 'black'
                                }
                                if (data.ref_status_id == 2) {
                                    bgcol = 'green';
                                    fontcol = 'white'
                                }
                                events.push(
                                    {
                                        title: '(' + data.ref_leave_type_.ref_leave_type_code + ')' + data.employee_.last_name,
                                        start: moment(data.leave_date).format('YYYY-MM-DD'),
                                        end: moment(data.leave_date).format('YYYY-MM-DD'),
                                        backgroundColor: bgcol, //Blue
                                        borderColor: 'white', //Blue
                                        textColor: fontcol,
                                        description: data.reason,
                                        id: data.request_leave_id
           ");
                WriteLiteral(@"                         });
                            });

                            callback(events);
                        }
                    });
                },

                eventRender: function (event, element) {
                    element.qtip(
                        {
                            content: event.description,
                            position: {
                                my: 'center left',  // Position my top left...
                                at: 'center right', // at the bottom right of...
                            }
                        });
                },
                eventClick: function (event, jsEvent, view) {
                    getbyIDApproval(event.id);
                    $('#fullCalModal').modal();
                },

                selectable: true,
                select: function (start, end) {
                    selectedEvent = {
                        eventID: 0,
                        title: '',
");
                WriteLiteral(@"                        description: '',
                        start: start,
                        end: end,
                        allDay: false,
                        color: ''
                    };
                    getbyID();
                    $('#leave_date').val(moment(start).format('MM/DD/YYYY'));
                    $('#calendar').fullCalendar('unselect');
                },
                editable: true,
            });
        }

        function getbyID(id) {
            GetLeaveType();
            $('#btnSave').hide();
            $('#btnUpdate').hide();
            $('#btnDelete').hide();
            $('#request_leave_id').val(0);

            $('#leave_date').val('');
            $('#approver_remark').val('');

            $('#reason').val('');
            $('#leave_date').css('border-color', 'lightgrey');
            $('#leave_day').css('border-color', 'lightgrey');
            $('#reason').css('border-color', 'lightgrey');
            $('#ref_leave_type");
                WriteLiteral(@"_id option:first').prop('selected', true);
            $('#leave_day option:first').prop('selected', true);

            if (id != null) {
                $.ajax({

                    url: 'RequestLeave/GetbyID' + '/' + id,
                    typr: ""GET"",
                    contentType: ""application/json;charset=UTF-8"",
                    dataType: ""json"",
                    success: function (result) {
                        var d = result.leave_date.slice(0, 10).split('-');
                        var val = d[1] + '/' + d[2] + '/' + d[0];
                        $('#request_leave_id').val(result.request_leave_id);
                        $('#leave_date').val(val);
                        $('#ref_leave_type_id').val(result.ref_leave_type_id);
                        $('#leave_day').val(result.leave_day);
                        $('#reason').val(result.reason);
                        $('#myCreateModal').modal('show');
                        if (result.ref_status_id == 1) {
        ");
                WriteLiteral(@"                    $('#btnUpdate').show();
                            $('#btnDelete').show();
                        }
                        if (result.ref_status_id == 3) {
                            $('#btnDelete').show();

                        }

                    },
                    error: function (errormessage) {
                        swal_error();
                    }
                });
            } else {
                $('#myCreateModal').modal('show');
                $('#btnSave').show();
            }
        }

        function GetLeaveType() {
            $.ajax({
                type: ""GET"",
                url: ""RequestLeave/GetLeaveType"",
                data: ""{}"",
                success: function (data) {
                    var s = '';
                    for (var i = 0; i < data.length; i++) {
                        s += '<option value=""' + data[i].ref_leave_type_id + '"">' + data[i].ref_leave_type_name + '</option>';
                    }
                WriteLiteral(@"
                    $(""#ref_leave_type_id"").html(s);
                }
            });
        }

        function Update() {
            var res = validate();
            if (res == false) {
                return false;
            }
            var dataObject = JSON.stringify({
                'request_leave_id': $('#request_leave_id').val(),
                'leave_date': $('#leave_date').val(),
                'ref_leave_type_id': $('#ref_leave_type_id').val(),
                'leave_day': $('#leave_day').val(),
                'ref_department_id': $('#ref_department_id').val(),
                'reason': $('#reason').val(),
                'ref_status_id': 1,
                'approver_id': '',
                'approver_date': '',
                'approver_remark': '',
                'date_deleted': ''
            });
            Swal.fire({
                title: 'Please Wait..!',
                text: 'Is working..',
                allowOutsideClick: false,
                a");
                WriteLiteral(@"llowEscapeKey: false,
                allowEnterKey: false,
                onOpen: () => {
                    swal.showLoading();
                }
            })
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'POST',
                url: 'RequestLeave/Update',
                data: dataObject,

                success: function (data) {
                    Grid();
                    $('#myCreateModal').modal('hide');
                    Swal.hideLoading();
                    swal_success();
                    $('#calendar').fullCalendar('removeEvents');
                    $('#calendar').fullCalendar('refetchEvents');
                },
                error: function (response) {
                    Swal.hideLoading();
                    var err = JSON.parse(response.responseText);
                    swal_error(err.errorMessage);
                }
            });
        } 

    ");
                WriteLiteral(@"    function validate() {
            var isValid = true;
            if ($('#leave_date').val().trim() == """") {
                $('#leave_date').css('border-color', 'Red');
                isValid = false;
            }
            else {
                $('#leave_date').css('border-color', 'lightgrey');
            }

            if ($('#reason').val().trim() == """") {
                $('#reason').css('border-color', 'Red');
                isValid = false;
            }
            else {
                $('#reason').css('border-color', 'lightgrey');
            }

            return isValid;
        }

        function validateremarks() {
            var isValid = true;
            if ($('#approver_remark').val().trim() == """") {
                $('#approver_remark').css('border-color', 'Red');
                isValid = false;
            }
            else {
                $('#approver_remark').css('border-color', 'lightgrey');
            }

            return isValid;
   ");
                WriteLiteral("     }\r\n    </script>\r\n");
                EndContext();
            }
            );
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