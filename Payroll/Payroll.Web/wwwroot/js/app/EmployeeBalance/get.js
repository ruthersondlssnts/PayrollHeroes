
function getbyID(id) {
    GetLeaveType();
    GetEmployee();
    $('#btnSave').hide();
    $('#btnUpdate').hide();

    $('#employee_balance_id').val(0);
    $('#employee_id option:first').prop('selected', true);
    $('#ref_leave_type_id option:first').prop('selected', true);

    $('#acquire_date').val('');
    $('#expire_date').val('');
    $('#quantity').val('');
    $('#remarks').val('');


    if (id != null) {
        $.ajax({

            url: 'GetbyID' + '/' + id,
            typr: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                $('#employee_balance_id').val(result.employee_balance_id);
                $('#employee_id').val(result.employee_id);
                $('#ref_leave_type_id').val(result.ref_leave_type_id);

                if (result.acquire_date.length > 0) {
                    var d = result.acquire_date.slice(0, 10).split('-');
                    var val = d[1] + '/' + d[2] + '/' + d[0];
                    $('#acquire_date').val(val);
                }
                if (result.expire_date.length > 0) {
                    d = result.expire_date.slice(0, 10).split('-');
                    val = d[1] + '/' + d[2] + '/' + d[0];
                    $('#expire_date').val(val);
                }

                //ONCE SET. This cannot be changed
                $('#expire_date').prop('disabled', true);
                $('#acquire_date').prop('disabled', true);

                $('#quantity').val(result.quantity);
                $('#remarks').val(result.remarks);

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

function GetLeaveType() {
    $.ajax({
        type: "GET",
        url: "GetLeaveType",
        data: "{}",
        success: function (data) {
            var s = '';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ref_leave_type_id + '">' + data[i].ref_leave_type_name + '</option>';
            }
            $("#ref_leave_type_id").html(s);
        }
    });
}

function GetEmployee() {
    $.ajax({
        type: "GET",
        url: "GetEmployee",
        data: "{}",
        success: function (data) {
            var s = '';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].employee_id + '">' + data[i].name + '</option>';
            }
            $("#employee_id").html(s);
        }
    });
}