
function getbyID(id) {
    $('#btnSave').hide();
    $('#btnUpdate').hide();
    $('#btnDelete').hide();

    $('#ref_shift_id').val(0);
    $('#shift_name').val('');
    $('#shift_in').val('');
    $('#shift_in').css('border-color', 'lightgrey');
    $('#shift_in').mask("##:##", { reverse: false });


    $('#shift_out').val('');
    $('#shift_out').css('border-color', 'lightgrey');
    $('#shift_out').mask("##:##", { reverse: false });

    $('#break_in').val('');
    $('#break_in').css('border-color', 'lightgrey');
    $('#break_in').mask("##:##", { reverse: false });


    $('#break_out').val('');
    $('#break_out').css('border-color', 'lightgrey');
    $('#break_out').mask("##:##", { reverse: false });


    $('#break_hour').val('');
    $('#required_hour').val('');

    if (id != null) {
        $.ajax({

            url: 'RefShift/GetbyID' + '/' + id,
            typr: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                $('#ref_shift_id').val(result.ref_shift_id);
                $('#shift_name').val(result.shift_name);
                $('#shift_in').val(result.shift_in);
                $('#shift_out').val(result.shift_out);
                $('#break_in').val(result.break_in);
                $('#break_out').val(result.break_out);

                $('#break_hour').val(result.break_hour);
                $('#required_hour').val(result.required_hour);

                $('#btnUpdate').show();
                $('#btnDelete').show();
                $('#myModal').modal('show');

            },
            error: function (errormessage) {
                swal_error();
            }
        });
    } else {
        $('#myModal').modal('show');
        GetDetailsEmpty();
        $('#btnSave').show();
    }
}
