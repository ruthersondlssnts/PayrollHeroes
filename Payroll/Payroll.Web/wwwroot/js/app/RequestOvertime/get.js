
function getbyID(id) {
    $('#btnSave').hide();
    $('#btnUpdate').hide();
    $('#btnDelete').hide();

    $('#request_overtime_id').val(0);
    $('#overtime_date').val('');
    $('#time_in').val('');
    $('#time_out').val('');
    $('#reason').val('');

    $('#time_in').css('border-color', 'lightgrey');
    $('#time_out').css('border-color', 'lightgrey');
    $('#time_in').mask("##:##", { reverse: false });
    $('#time_out').mask("##:##", { reverse: false });

    $('#ref_overtime_type_id option:first').prop('selected', true);

    if (id != null) {
        $.ajax({
            
            url: 'GetbyID' + '/' + id,
            typr: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                var d = result.overtime_date.slice(0, 10).split('-');
                var val = d[1] + '/' + d[2] + '/' + d[0];
                $('#request_overtime_id').val(result.request_overtime_id);
                $('#overtime_date').val(val);
                $('#time_in').val(result.time_in);
                $('#time_out').val(result.time_out);
                $('#reason').val(result.reason);
                $('#ref_overtime_type_id').val(result.ref_overtime_type_id);
                $('#myModal').modal('show');
                if (result.ref_status_id == 1) {
                    $('#btnUpdate').show();
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
        $('#myModal').modal('show');
        $('#btnSave').show();
    }
}

function getbyIDApproval(id) {
    $('#request_overtime_id').val('');
    $('#overtime_date').val('');
    $('#time_in').val('');
    $('#time_out').val('');
    $('#reason').val('');
    $('#approver_remark').val('');

    $('#overtime_date').prop('disabled', true);
    $('#time_in').prop('disabled', true);
    $('#time_out').prop('disabled', true);
    $('#reason').prop('reason', true);
    $('#ref_overtime_type_id').prop('disabled', true);


    if (id != null) {
        $.ajax({

            url: 'GetByID' + '/' + id,
            typr: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                var d = result.overtime_date.slice(0, 10).split('-');
                var val = d[1] + '/' + d[2] + '/' + d[0];
                $('#request_overtime_id').val(result.request_overtime_id);
                $('#overtime_date').val(val);
                $('#time_in').val(result.time_in);
                $('#time_out').val(result.time_out);
                $('#reason').val(result.reason);
                $('#ref_overtime_type_id').val(result.ref_overtime_type_id);
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
