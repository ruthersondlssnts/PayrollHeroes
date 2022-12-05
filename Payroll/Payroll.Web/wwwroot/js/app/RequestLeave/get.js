
function getbyID(id) {
    GetLeaveType();
    GetRole();
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
    $('#ref_leave_type_id option:first').prop('selected', true);
    $('#leave_day option:first').prop('selected', true);



    if (id != null) {
        $.ajax({

            url: 'GetbyID' + '/' + id,
            typr: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                var d = result.leave_date.slice(0, 10).split('-');
                var val = d[1] + '/' + d[2] + '/' + d[0];
                $('#request_leave_id').val(result.request_leave_id);
                $('#leave_date').val(val);
                $('#ref_leave_type_id').val(result.ref_leave_type_id);
                $('#leave_day').val(result.leave_day);
                $('#reason').val(result.reason);
                $('#myModal').modal('show');

                if (result.ref_status_id == 1) {
                    $('#btnUpdate').show();
                    $('#btnDelete').show();
                }
                if (result.ref_status_id == 2) {
                    $('#btnUpdate').hide();
                    $('#btnDelete').hide();
                    $('#leave_date').prop('disabled', true);
                    $('#reason').prop('disabled', true);
                    $('#ref_leave_type_id').prop('disabled', true);
                }
                if (result.ref_status_id == 3) {
                    $('#btnDelete').show();
                    $('#leave_date').prop('disabled', true);
                    $('#reason').prop('disabled', true);
                    $('#ref_leave_type_id').prop('disabled', true);
                }
                
            },
            error: function (errormessage) {
                swal_error();
            }
        });
    } else {
        $('#leave_date').prop('disabled', false);
        $('#reason').prop('disabled', false);
        $('#ref_leave_type_id').prop('disabled', false);
        $('#myModal').modal('show');
        $('#btnUpdate').hide();
        $('#btnDelete').hide();
        $('#btnSave').show();
    }
}

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
    $('#reason').prop('disabled', true);
    $('#ref_leave_type_id').prop('disabled', true);


    if (id != null) {
        $.ajax({

            url: 'GetByID' + '/' + id,
            typr: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                var d = result.leave_date.slice(0, 10).split('-');
                var val = d[1] + '/' + d[2] + '/' + d[0];
                $('#request_leave_id').val(result.request_leave_id);
                $('#leave_date').val(val);
                $('#ref_leave_type_id').val(result.ref_leave_type_id);
                $('#leave_day').val(result.leave_day);
                $('#reason').val(result.reason);
                $('#myModal').modal('show');
            },
            error: function (errormessage) {
                swal_error(errormessage);
            }
        });
    } else {
        $('#myModal').modal('show');
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

function GetRole() {
    $.ajax({
        type: "GET",
        url: "GetRole",
        data: "{}",
        success: function (data) {
            var s = '';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].role_id + '">' + data[i].role_name + '</option>';
            }
            $("#role_id").html(s);
        }
    });
}