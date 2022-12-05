
//function for updating employee's record
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
   

    var emp = JSON.stringify({
        'employee_id': $('#employee_id').val(),
        'employee_serial': $('#employee_serial').val(),
        'username': $('#username').val(),
        'password': $('#password').val(),


        'last_name': $('#last_name').val(),
        'first_name': $('#first_name').val(),
        'middle_name': $('#middle_name').val(),
        'email_address': $('#email_address').val(),

        'gender': $('#gender').val(),
        'contact_number': $('#contact_number').val(),
        'sss': $('#sss').val(),
        'pagibig': $('#pagibig').val(),
        'philhealth': $('#philhealth').val(),

        'marital_status': $('#marital_status').val(),
        'date_hire': $('#date_hire').val(),
        'date_resign': $('#date_resign').val(),
        'ref_shift_id': $('#ref_shift_id').val(),
        'ref_department_id': $('#ref_department_id').val(),

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
        url: 'Employee/Update',
        traditional: true,
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


function Delete() {
    Swal.fire({
        title: 'Are you sure you want to delete this?',
        //text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',
        allowOutsideClick: false,
    }).then((result) => {
        if (result.value) {
            var urli = 'Delete';
            var id = $('#employee_id').val();
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
                url: urli + '/' + id,
                type: "GET",
                error: function (response) {
                    Swal.hideLoading();
                    swal_error("error");
                },
                success: function (response) {
                    Swal.hideLoading();
                    $('#myModal').modal('hide');
                    swal_success();
                    Grid();
                }
            });

        }
    });
}