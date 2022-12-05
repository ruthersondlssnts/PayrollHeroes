function Grid(id) {
    if (id == null) {
        id = 1;
    }
    //var ancestor1 = $('#ancestor1').val();
    //var nextval = $('#nextval').val();
    //$('#currentval').val(id);
    $("#divEmployeeList").hide();
    $("#empList").hide();

    $.ajax({
        type: "get",
        url: "GetInfo/" + id,
        dataType: "JSON",
        success: function (data) {

            if (data.ancestor1 != null) {
 
                $("#previous").show();
                $("#divGroupList").show();
                $("#divEmployeeList").hide();
                
                $('#ancestor1').val(data.ancestor1);
                $('#nextval').val(data.nextval);
                $('#currentval').val(data.group_id);
                $('#current_level').val(data.current_level);
                if (data.current_level == 1) {
                    document.getElementById("myHeader").innerHTML = "Branch List";
                }
                if (data.current_level == 2) {
                    document.getElementById("myHeader").innerHTML = "Department List";
                }
                if (data.current_level == 3) {
                    $("#divGroupList").hide();
                    $("#divEmployeeList").show();
                    $("#process").hide();
                    document.getElementById("myEmployeeList").innerHTML = "Employee List for " + data.name;
                    
                    $("#empList").show();
                    GetEmployee();
                    GridEmployee(data.group_id);
                }
            }
            else {
                $("#divGroupList").show();
                $("#previous").hide();
                $('#nextval').val(data.nextval);
                $('#currentval').val(data.group_id);
                document.getElementById("myHeader").innerHTML = "Company List";
            }
        }
    });

    $("#jsGrid1").jsGrid({

        width: "100%",
        pageSize: 15,
        sorting: true,
        paging: true,
        autoload: true,
        controller: {
            loadData: function (filter) {
                return $.ajax({
                    type: "GET",
                    url: 'GetData' + '/' + id,
                    data: filter,
                    dataType: "JSON",
                    headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
                });
            }
        },

        fields: [
            
            {
                name: "name", title: "Name", type: "text", width: 80,
                itemTemplate: function (value, item) {
                    return '<a href="#" onclick="return getbyID(' + item.group_id + ')">' + item.name + '</a>';
                }
            },

            {
                title: "Action", type: "text", width: 10,
                itemTemplate: function (value, item) {
                    return '<a href="#" onclick="return Grid(' + item.group_id + ')">Go Inside</a> &nbsp;| &nbsp;<a href="#" onclick="return Delete(' + item.group_id + ')">Delete</a>';
                }
            },
        ]
    });
}

function GridEmployee(id) {
    if (id == null) {
        id = 1;
    }


    $("#jsGrid2").jsGrid({

        width: "100%",
        pageSize: 15,
        sorting: true,
        paging: true,
        autoload: true,
        controller: {
            loadData: function (filter) {
                return $.ajax({
                    type: "GET",
                    url: 'GetEmployeeList' + '/' + id,
                    data: filter,
                    dataType: "JSON",
                    headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
                });
            }
        },

        fields: [

            {
                name: "name", title: "Name", type: "text", width: 80,
                itemTemplate: function (value, item) {
                    return item.employee_.last_name + ', ' + item.employee_.first_name;
                }
            },
            //{ name: "approver_all", title: "Approver All", type: "checkbox", width: 20 },
            {
                name: "approver_all", title: "Approver All", type: "checkbox", width: 20,
                itemTemplate: function (value, item) {
                    
                    return $("<input>").attr("type", "checkbox")
                        .attr("checkbox", function () {
                            $(this).prop("checked", item.approver_all);
                        })
                        .on("change", function () {

                        item.Checked = $(this).is(":checked");
                        
                        
                        if (!item.Active && item.Checked) {
                            updateApproval(item.employee_.employee_id, 1, true);
                            $(this).prop("checked", true);
                        } else {
                            updateApproval(item.employee_.employee_id, 1, false);
                        }
                    });
                }
            },
            {
                name: "ot_approver", title: "OT Approver", type: "checkbox", width: 20,
                itemTemplate: function (value, item) {

                    return $("<input>").attr("type", "checkbox")
                        .attr("checkbox", function () {
                            $(this).prop("checked", item.ot_approver);
                        })
                        .on("change", function () {
                        item.Checked = $(this).is(":checked");
                        if (!item.Active && item.Checked) {
                            updateApproval(item.employee_.employee_id, 2, true);
                            $(this).prop("checked", true);
                        } else {
                            updateApproval(item.employee_.employee_id, 2, false);
                        }
                    });
                } },
            {
                name: "ob_approver", title: "OB Approver", type: "checkbox", width: 20,
                itemTemplate: function (value, item) {

                    return $("<input>").attr("type", "checkbox")
                        .attr("checkbox", function () {
                            $(this).prop("checked", item.ob_approver);
                        })
                        .on("change", function () {
                        item.Checked = $(this).is(":checked");
                        if (!item.Active && item.Checked) {
                            updateApproval(item.employee_.employee_id, 3, true);
                            $(this).prop("checked", true);
                        } else {
                            updateApproval(item.employee_.employee_id, 3, false);
                        }
                    });
                } },
            {
                name: "leave_approver", title: "Leave Approver", type: "checkbox", width: 20,
                itemTemplate: function (value, item) {

                    return $("<input>").attr("type", "checkbox")
                        .attr("checkbox", function () {
                            $(this).prop("checked", item.leave_approver);
                        })
                        .on("change", function () {
                        item.Checked = $(this).is(":checked");
                        if (!item.Active && item.Checked) {
                            updateApproval(item.employee_.employee_id, 4, true);
                            $(this).prop("checked", true);
                        } else {
                            updateApproval(item.employee_.employee_id, 4, false);
                        }
                    });
                } },
            {
                name: "dtr_approver", title: "DTR Approver", type: "checkbox", width: 20,
                itemTemplate: function (value, item) {

                    return $("<input>")
                        .attr("type", "checkbox")
                        .attr("checkbox", function () {
                            $(this).prop("checked", item.dtr_approver);
                        })
                        .on("change", function () {
                        item.Checked = $(this).is(":checked");
                        if (!item.Active && item.Checked) {
                            updateApproval(item.employee_.employee_id, 5, true);
                            $(this).prop("checked", true);
                        } else {
                            updateApproval(item.employee_.employee_id, 5, false);
                        }
                    });
                } },
            {
                title: "Action", type: "text", width: 10,
                itemTemplate: function (value, item) {
                    return '<a href="#" onclick="return Delete(' + item.employee_id + ')">Remove</a>';
                }
            },
        ]
    });
}


function goPrevious() {
    Grid($('#ancestor1').val());
}

function updateApproval(employee_id, type, value) {

    var emp = {
        'employee_id': employee_id,
        'type': type,
        'value': value,
    };

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'GET',
        traditional: true,
        url: 'UpdateApproval',
        data: emp,
        headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
        success: function (data) {

        },
        error: function (response) {

        }
    });
}