
function getbyID(id) {
    $('#btnSave').hide();
    $('#btnUpdate').hide();

    $('#ref_leave_type_id').val(0);
    $('#ref_leave_type_name').val('');
    $('#ref_leave_type_code').val('');
    $('#with_pay').prop('checked', true);

    if (id != null) {
        $.ajax({

            url: 'GetbyID' + '/' + id,
            typr: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                $('#ref_leave_type_id').val(result.ref_leave_type_id);
                $('#ref_leave_type_name').val(result.ref_leave_type_name);
                $('#ref_leave_type_code').val(result.ref_leave_type_code);

                if (result.with_pay == true) {
                    $('#with_pay').prop('checked', true);
                } else {
                   $('#with_pay').prop('checked', false);
                }


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
