
//function for updating employee's record
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var dataObject = JSON.stringify({
        'request_leave_id': $('#request_leave_id').val(),
        'leave_date': $('#leave_date').val(),
        'ref_leave_type_id': $('#ref_leave_type_id').val(),
        'leave_day': $('#leave_day').val(),
        'ref_department_id': $('#ref_department_id').val(),
        'reason': $('#reason').val(),
        'ref_status_id': 1,
        'approver_id': '',
        'approver_date': '',
        'approver_remark': '',
        'date_deleted': ''
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
    })
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: 'Update',
        data: dataObject,

        success: function (data) {
            Grid();
            Grid2();
            $('#myModal').modal('hide');
            Swal.hideLoading();
            swal_success();
        },
        error: function (response) {
            Swal.hideLoading();
            var err = JSON.parse(response.responseText);
            swal_error(err.errorMessage);
        }
    });
} 

function Process(bool) {
    var routes = 'Approve';
    var res = validateremarks();
    if (res == false) {
        return false;
    }

    if (bool == false) {
        routes = 'Disapprove';
    }
    var dataObject = JSON.stringify({
        'request_leave_id': $('#request_leave_id').val(),
        'leave_date': $('#leave_date').val(),
        'ref_leave_type_id': $('#ref_leave_type_id').val(),
        'leave_day': 1,
        'ref_department_id': $('#ref_department_id').val(),
        'reason': $('#reason').val(),
        'ref_status_id': 1,
        'approver_id': '',
        'approver_date': '',
        'approver_remark': $('#approver_remark').val(),
        'date_deleted': ''
    });
    swal_wait();
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: routes,
        data: dataObject,

        success: function (data) {
            Grid();
            $('#myModal').modal('hide');
            Swal.hideLoading();
            swal_success();
        },
        error: function (errormessage) {
            Swal.hideLoading();
            swal_error();
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
            var id = $('#request_Leave_id').val();
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
                    Grid2();
                }
            });

        }
    });
}