
function getbyID(id) {
    $('#btnSave').hide();
    $('#btnUpdate').hide();
    $('#btnDelete').hide();

    $('#ref_payroll_cutoff_id').val(0);
    $('#cutoff_date_start').val('');
    $('#cutoff_date_end').val('');
    $('#government').prop('checked', true);

    if (id != null) {
        $.ajax({
            
            url: 'GetbyID' + '/' + id,
            typr: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                var d1 = result.cutoff_date_start.slice(0, 10).split('-');
                var val1 = d1[1] + '/' + d1[2] + '/' + d1[0];

                var d2 = result.cutoff_date_end.slice(0, 10).split('-');
                var val2 = d2[1] + '/' + d2[2] + '/' + d2[0];

                $('#ref_payroll_cutoff_id').val(result.ref_payroll_cutoff_id);
                $('#cutoff_date_start').val(val1);
                $('#cutoff_date_end').val(val2);
                if (result.government == true) {
                    $('#government').prop('checked', true);
                } else {
                    $('#government').prop('checked', false);
                }
                $('#btnUpdate').show();
                //$('#btnDelete').show();
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

