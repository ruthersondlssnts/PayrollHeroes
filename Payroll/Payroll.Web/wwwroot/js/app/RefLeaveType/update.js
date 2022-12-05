
//function for updating employee's record

function WithPay() {
    if ($('#with_pay').is(':checked')) {
        return 'true';
    } else {
        return 'false';
    }
}

function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var emp = JSON.stringify({
        'ref_leave_type_id': $('#ref_leave_type_id').val(),
        'ref_leave_type_name': $('#ref_leave_type_name').val(),
        'ref_leave_type_code': $('#ref_leave_type_code').val(),
        'with_pay': WithPay()
    });

    Swal.fire({
        title: 'Please Wait..!',
        text: 'Is working..',
        allowOutsideClick: false,
        allowEscapeKey: false,
        allowEnterKey: false,
        onOpen: () => {
            swal.showLoading();
        }
    });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        traditional: true,
        url: 'Update',
        data: emp,
        headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
        success: function (data) {
            Grid();
            $('#myModal').modal('hide');
            Swal.hideLoading();
            swal_success();
        },
        error: function (response) {
            Swal.hideLoading();
            var err = JSON.parse(response);
            swal_error(err.errorMessage);
        }
    });
} 
