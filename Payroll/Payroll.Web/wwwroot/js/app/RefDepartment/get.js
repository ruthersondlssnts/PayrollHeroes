
function getbyID(id) {
    $('#btnSave').hide();
    $('#btnUpdate').hide();

    $('#ref_department_id').val(0);
    $('#department_name').val('');

    Grid2(id);
    GetEmployeeList(id);
    if (id != null) {
        $.ajax({

            url: 'GetbyID' + '/' + id,
            typr: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                $('#ref_department_id').val(result.ref_department_id);
                $('#department_name').val(result.department_name);

                $('#btnUpdate').show();
                $('#myModal').modal('show');
                
            },
            error: function (errormessage) {
                swal_error();
            }
        });
    } else {
        $('#myModal').modal('show');
        $('#btnSave').show();
    }
}

function GetEmployeeList(id) {
    $.ajax({
        type: "GET",
        url: "GetApproverList/" + id,
        data: "{}",
        success: function (data) {
            var s = '';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].employee_id + '">' + data[i].name + '</option>';
            }
            $("#approver_list").html(s);
        }
    });
}