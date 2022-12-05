
function getbyID(id) {
    GetShift();
    $('#btnSave').hide();
    $('#btnUpdate').hide();
    $('#btnDelete').hide();

    $('#request_dtr_id').val(0);
    $('#ref_shift_id').val('');
    $('#shift_date').val('');
    $('#time_in').val('');
    $('#time_out').val('');
    $('#reason').val('');

    $('#time_in').css('border-color', 'lightgrey');
    $('#time_out').css('border-color', 'lightgrey');
    $('#time_in').mask("##:##", { reverse: false });
    $('#time_out').mask("##:##", { reverse: false });

    $('#ref_shift_id option:first').prop('selected', true);

    if (id != null) {
        $.ajax({

            url: 'GetbyID' + '/' + id,
            typr: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                var d = result.shift_date.slice(0, 10).split('-');
                var val = d[1] + '/' + d[2] + '/' + d[0];
                $('#request_dtr_id').val(result.request_dtr_id);

                $('#shift_date').val(val);
                $('#time_in').val(result.time_in);
                $('#time_out').val(result.time_out);
                $('#reason').val(result.reason);
                $('#ref_shift_id').val(result.ref_shift_id);
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
    GetShift();
    $('#request_dtr_id').val('');
    $('#shift_date').val('');
    $('#time_in').val('');
    $('#time_out').val('');
    $('#reason').val('');
    $('#approver_remark').val('');
    $('#shift_date').prop('disabled', true);
    $('#time_in').prop('disabled', true);
    $('#time_out').prop('disabled', true);
    $('#reason').prop('disabled', true);
    $('#ref_shift_id').prop('disabled', true);


    if (id != null) {
        $.ajax({

            url: 'GetByID' + '/' + id,
            typr: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                var d = result.shift_date.slice(0, 10).split('-');
                var val = d[1] + '/' + d[2] + '/' + d[0];
                $('#request_dtr_id').val(result.request_dtr_id);
                $('#shift_date').val(val);
                $('#time_in').val(result.time_in);
                $('#time_out').val(result.time_out);
                $('#reason').val(result.reason);
                $('#ref_shift_id').val(result.ref_shift_id);
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

function GetShift() {
    $.ajax({
        type: "GET",
        url: "GetShift",
        data: "{}",
        success: function (data) {
            var s = '';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ref_shift_id + '">' + data[i].shift_name + '</option>';
            }
            $("#ref_shift_id").html(s);
        }
    });  
}