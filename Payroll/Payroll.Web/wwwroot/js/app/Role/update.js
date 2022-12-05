
//function for updating employee's record
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var data = [];
    $('[id^=chk_]').each(function () {
        var sThisVal = (this.checked ? "true" : "false");
        data.push({
            'is_enable': sThisVal,
            'permission_id': $(this).val(),
            'role_id': $('#role_id').val(),
        });
    });
    var emp = JSON.stringify({
        'role_id': $('#role_id').val(),
        'role_name': $('#role_name').val(),
        'role_permission_': data
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
            swal_updaterole();
        },
        error: function (response) {
            Swal.hideLoading();
            var err = JSON.parse(response);
            swal_error(err.errorMessage);
        }
    });
} 
