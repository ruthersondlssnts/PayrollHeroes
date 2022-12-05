#pragma checksum "C:\Users\biboy\Source\repos\Payroll\Payroll.Web\Views\EmployeeTimesheet\TimesheetReport.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fea8c348b036bf6cf01c9287f25f8b42c433bf52"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_EmployeeTimesheet_TimesheetReport), @"mvc.1.0.view", @"/Views/EmployeeTimesheet/TimesheetReport.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/EmployeeTimesheet/TimesheetReport.cshtml", typeof(AspNetCore.Views_EmployeeTimesheet_TimesheetReport))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fea8c348b036bf6cf01c9287f25f8b42c433bf52", @"/Views/EmployeeTimesheet/TimesheetReport.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c22d830b85aae7d6f2014fc1a4ef656594314354", @"/Views/_ViewImports.cshtml")]
    public class Views_EmployeeTimesheet_TimesheetReport : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\biboy\Source\repos\Payroll\Payroll.Web\Views\EmployeeTimesheet\TimesheetReport.cshtml"
  
    /**/

    ViewBag.Title = "Employee Leave";


#line default
#line hidden
            BeginContext(60, 2759, true);
            WriteLiteral(@"<div class=""container-fluid"">
    <div class=""row"">
        <div class=""col-12"">
            <div class=""card"">
                <div class=""card-header"">
                    <h3 class=""card-title"">Search</h3>
                </div>
                <div class=""card-body"">
                    <div class=""row"">
                        <div class=""col-sm-3"">
                            <!-- select -->
                            <div class=""form-group"">
                                <label>Cutoff</label>
                                <select class=""custom-select"" id=""cutoff""></select>
                            </div>

                        </div>
                        <div class=""col-sm-3"">
                            <div class=""form-group"">
                                <label>Company</label>
                                <select class=""custom-select"" id=""company""></select>
                            </div>
                            <div class=""form-group"">
              ");
            WriteLiteral(@"                  <label>Branch</label>
                                <select class=""custom-select"" id=""branch""></select>
                            </div>
                            <div class=""form-group"">
                                <label>Department</label>
                                <select class=""custom-select"" id=""department""></select>
                            </div>
                        </div>
                        <div class=""col-sm-3"">
                            <div class=""form-group"">
                                <label>&nbsp;</label>
                                <button type=""button"" class=""btn btn-block btn-default"" id=""search"" onclick=""return search()"">Search</button>
                            </div>
                            <div class=""form-group"">
                                <label>&nbsp;</label>
                                <button type=""button"" class=""btn btn-block btn-default"" id=""export"" onclick=""return exportdtr()"">Export</button>
 ");
            WriteLiteral(@"                           </div>
                        </div>
                        <div class=""col-sm-3"">
                            
                        </div>

                    </div>
                </div>
            </div>


            <div class=""card"">
                <div class=""card-header"">
                    <h3 class=""card-title"">Employee DTR</h3>
                </div>
                <!-- /.card-header -->
                <div class=""card-body"">
                    <div id=""jsGrid1""></div>
                </div>
                <!-- /.card-body -->
            </div>
        </div>
    </div>
    <!-- /.row -->
</div><!-- /.container-fluid -->


");
            EndContext();
            DefineSection("scripts", async() => {
                BeginContext(2837, 1349, true);
                WriteLiteral(@"
    <script>

        $(document).ready(function () {
            GetCutoff();
            getCompany();
            //$('#cutoff').change(function () {
            //    Grid();
            //});
            $('#company').change(function () {
                getBranch();
                getDepartment();
            });
            $('#branch').change(function () {
                getDepartment();
            });
            $('#department').change(function () {

            });
        });

        function Grid(id) {
            Swal.fire({
                title: 'Please Wait..!',
                text: 'Is working..',
                allowOutsideClick: false,
                allowEscapeKey: false,
                allowEnterKey: false,
                onOpen: () => {
                    swal.showLoading()
                }
            })

               var data = {
                 'ref_payroll_cutoff_id':  $(""#cutoff"").val(),
                 'group_id':id
             ");
                WriteLiteral(@"  };
                $(""#jsGrid1"").jsGrid({

                width: ""100%"",
                pageSize: 15,
                sorting: true,
                paging: true,
                autoload: true,
                controller: {
                    loadData: function (filter) {
                        var urli = '");
                EndContext();
                BeginContext(4187, 61, false);
#line 119 "C:\Users\biboy\Source\repos\Payroll\Payroll.Web\Views\EmployeeTimesheet\TimesheetReport.cshtml"
                               Write(Url.Action("GetAllEmployeeTimeSheetDTR", "EmployeeTimesheet"));

#line default
#line hidden
                EndContext();
                BeginContext(4248, 6896, true);
                WriteLiteral(@"';

                        return $.ajax({
                            type: ""GET"",
                            url: urli,
                            data: data,
                            dataType: ""JSON"",
                            success: function (data) {
                                Swal.hideLoading();
                                swal_loadcomplete();
                            }
                        });
                    }
                },

                    fields: [
                    {
                        title: ""Name"", type: ""text"", width: 50,
                        itemTemplate: function (value, item) {
                            return item.last_name + ', ' + item.first_name;
                        },
                    },
                    {
                        name: ""shift_date"", title: ""Date"", type: ""text"", width: 30,
                        itemTemplate: function (value, item) {
                            var d = value.slice(0, 10).");
                WriteLiteral(@"split('-');
                            var val = d[1] + '/' + d[2] + '/' + d[0];
                            return '<a href=""#"" onclick=""return getbyID(' + item.employee_timesheet_id + ')"">' + val + '</a>';
                        },
                    },
                    {
                        name: ""shift_date"", title: ""Day"", type: ""text"", width: 20,
                        itemTemplate: function (value, item) {
                            var weekday = [""SUN"", ""MON"", ""TUE"", ""WED"", ""THU"", ""FRI"", ""SAT""];
                            var a = new Date(value);
                            return weekday[a.getDay()];
                        },
                    },
                    { name: ""shift_name"", title: ""Shift"", width: 40 },
                    { name: ""date_type_code"", title: ""Type"", width: 30 },
                    {
                        name: ""required_hour"", title: ""RqHr"", type: ""number"", width: 20,
                        itemTemplate: function (value) {
              ");
                WriteLiteral(@"              if (value != null && value != 0) {
                                return value.toFixed(2);
                            } else {
                                return '0.00';
                            }

                        },
                    },
                    {
                        name: ""rendered_hour"", title: ""RdHr"", type: ""number"", width: 20,
                        itemTemplate: function (value) {
                            if (value != null && value != 0) {
                                return value.toFixed(2);
                            } else {
                                return '0.00';
                            }

                        },
                    },
                    {
                        name: ""time_in1"", title: ""In"", type: ""text"", width: 20,
                        itemTemplate: function (value,item) {
                            if (value == null && value != 0) {
                                return item.time_i");
                WriteLiteral(@"n2;
                            } else {
                                return value;
                            }

                        }
                    },
                    {
                        name: ""time_out1"", title: ""Out"", type: ""text"", width: 20,
                        itemTemplate: function (value, item) {
                            if (value == null && value != 0) {
                                return item.time_out2;
                            } else {
                                return value;
                            }

                        }
                    },
                    { name: ""ot_in"", title: ""OtIn"", type: ""number"", width: 20 },
                    { name: ""ot_out"", title: ""OTOut"", type: ""number"", width: 20 },
                    {
                        name: ""late"", title: ""Late"", type: ""number"", width: 20,
                        itemTemplate: function (value) {

                            if (value != null && value != 0) ");
                WriteLiteral(@"{
                                return '<span style=""color: red;"">' + value.toFixed(2) + '</span>';
                            } else {
                                return '0.00';
                            }

                        },
                    },
                    {
                        name: ""undertime"", title: ""UT"", type: ""number"", width: 20,
                        itemTemplate: function (value) {
                            if (value != null && value != 0) {
                                return '<span style=""color: red;"">' + value.toFixed(2) + '</span>';
                            } else {
                                return '0.00';
                            }

                        },
                    },
                    {
                        name: ""ot"", title: ""OT"", type: ""number"", width: 20,

                        itemTemplate: function (value) {
                            if (value != null && value != 0) {
                        ");
                WriteLiteral(@"        return value.toFixed(2);
                            } else {
                                return '0.00';
                            }

                        },
                    },
                    {
                        name: ""ot8"", title: ""OT8"", type: ""number"", width: 20,
                        itemTemplate: function (value) {
                            if (value != null && value != 0) {
                                return value.toFixed(2);
                            } else {
                                return '0.00';
                            }

                        },
                    },
                    {
                        name: ""night_dif"", title: ""ND"", type: ""number"", width: 20,
                        itemTemplate: function (value) {
                            if (value != null && value != 0) {
                                return value.toFixed(2);
                            } else {
                                return '0");
                WriteLiteral(@".00';
                            }

                        },
                    },
                    {
                        name: ""absent"", title: ""AB"", type: ""number"", width: 20,
                        itemTemplate: function (value) {
                            if (value != null && value != 0) {
                                return '<span style=""color: red;"">' + value.toFixed(2) + '</span>';
                            } else {
                                return '0.00';
                            }

                        },
                    },

                ]
                });
        }

         function GetCutoff() {
            $.ajax({
                type: ""GET"",
                url: '");
                EndContext();
                BeginContext(11145, 44, false);
#line 281 "C:\Users\biboy\Source\repos\Payroll\Payroll.Web\Views\EmployeeTimesheet\TimesheetReport.cshtml"
                 Write(Url.Action("GetCutoff", "EmployeeTimesheet"));

#line default
#line hidden
                EndContext();
                BeginContext(11189, 635, true);
                WriteLiteral(@"',
                data: ""{}"",
                success: function (data) {
                    var s = '';
                    for (var i = 0; i < data.length; i++) {
                        s += '<option value=""' + data[i].ref_payroll_cutoff_id + '"">' + data[i].ref_payroll_cutoff_name + '</option>';
                    }
                    $(""#cutoff"").html(s);
                }

            });
        }

        function getCompany() {
            var data = {
                 'level':  1,
                 'group_id':0
            };
            $.ajax({
                type: ""GET"",
                url: '");
                EndContext();
                BeginContext(11825, 43, false);
#line 301 "C:\Users\biboy\Source\repos\Payroll\Payroll.Web\Views\EmployeeTimesheet\TimesheetReport.cshtml"
                 Write(Url.Action("GetGroup", "EmployeeTimesheet"));

#line default
#line hidden
                EndContext();
                BeginContext(11868, 798, true);
                WriteLiteral(@"',
                data: data,
                success: function (data) {
                    var s = '<option value=0>-- ALL --</option>';
                    for (var i = 0; i < data.length; i++) {
                        s += '<option value=""' + data[i].group_id + '"">' + data[i].name + '</option>';
                    }
                    $(""#company"").html(s);
                    $(""#company"").select2();
                    getBranch();
                }

            });


        }

        function getBranch() {
            var data = {
                 'level':  2,
                 'group_id':$(""#company"").val()
            };

            if ($(""#company"").val() != 0) {
                $.ajax({
                    type: ""GET"",
                    url: '");
                EndContext();
                BeginContext(12667, 43, false);
#line 327 "C:\Users\biboy\Source\repos\Payroll\Payroll.Web\Views\EmployeeTimesheet\TimesheetReport.cshtml"
                     Write(Url.Action("GetGroup", "EmployeeTimesheet"));

#line default
#line hidden
                EndContext();
                BeginContext(12710, 1042, true);
                WriteLiteral(@"',
                    data: data,
                    success: function (data) {
                        var s = '<option value=0>-- ALL --</option>';
                        for (var i = 0; i < data.length; i++) {
                            s += '<option value=""' + data[i].group_id + '"">' + data[i].name + '</option>';
                        }
                        $(""#branch"").html(s);
                        $(""#branch"").select2();
                        getDepartment();
                    }
                });
            }
            else {
                $(""#branch"").html('<option value=0>-- ALL --</option>');
                $(""#branch"").select2();
                getDepartment();
            }


        }

        function getDepartment() {

            var data = {
                 'level': 3,
                 'group_id':$(""#branch"").val()
            };
            if ($(""#branch"").val() != 0) {
                $.ajax({
                    type: ""GET"",
        ");
                WriteLiteral("            url: \'");
                EndContext();
                BeginContext(13753, 43, false);
#line 358 "C:\Users\biboy\Source\repos\Payroll\Payroll.Web\Views\EmployeeTimesheet\TimesheetReport.cshtml"
                     Write(Url.Action("GetGroup", "EmployeeTimesheet"));

#line default
#line hidden
                EndContext();
                BeginContext(13796, 1869, true);
                WriteLiteral(@"',
                    data: data,
                    success: function (data) {
                        var s = '<option value=0>-- ALL --</option>';
                        for (var i = 0; i < data.length; i++) {
                            s += '<option value=""' + data[i].group_id + '"">' + data[i].name + '</option>';
                        }
                        $(""#department"").html(s);
                        $(""#department"").select2();
                    }
                });
            }
            else {
                $(""#department"").html('<option value=0>-- ALL --</option>');
                $(""#department"").select2();
            }


        }

        function search() {
            if ($(""#department"").val() != 0) {
                Grid($(""#department"").val());
            }
            else
                if ($(""#branch"").val() != 0) {
                    Grid($(""#branch"").val());
                }
                else
                    if ($(""#company"")");
                WriteLiteral(@".val() != 0) {
                        Grid($(""#company"").val());
                    }
                    else { Grid(1); }
        }
        function exportdtr() {
            if ($(""#department"").val() != 0) {
                exportGrid($(""#department"").val());
            }
            else
                if ($(""#branch"").val() != 0) {
                    exportGrid($(""#branch"").val());
                }
                else
                    if ($(""#company"").val() != 0) {
                        exportGrid($(""#company"").val());
                    }
                    else { exportGrid(1); }
        }

        function exportGrid(id) {
            var data = {
                 'ref_payroll_cutoff_id':  $(""#cutoff"").val(),
                 'group_id':id
            };
            window.location = '");
                EndContext();
                BeginContext(15666, 44, false);
#line 412 "C:\Users\biboy\Source\repos\Payroll\Payroll.Web\Views\EmployeeTimesheet\TimesheetReport.cshtml"
                          Write(Url.Action("ExportDTR", "EmployeeTimesheet"));

#line default
#line hidden
                EndContext();
                BeginContext(15710, 101, true);
                WriteLiteral("\' + \'?ref_payroll_cutoff_id=\' + $(\"#cutoff\").val() + \"&group_id=\" + id;\r\n\r\n        }\r\n    </script>\r\n");
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
