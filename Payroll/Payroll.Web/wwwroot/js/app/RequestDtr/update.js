
//function for updating employee's record
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var emp = JSON.stringify({
        'request_dtr_id': $('#request_dtr_id').val(),
        'employee_id': 0,
        'shift_date': $('#shift_date').val(),
        'ref_shift_id': $('#ref_shift_id').val(),
        'time_in': $('#time_in').val(),
        'time_out': $('#time_out').val(),
        'reason': $('#reason').val(),
        'ref_status_id': 1,
        'approver_id': 0,
        'approver_date': null,
        'approver_remark': null,
        'date_deleted': null
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

        success: function (data) {
            Grid();
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
        'request_dtr_id': $('#request_dtr_id').val(),
        'employee_id': 0,
        'shift_date': $('#shift_date').val(),
        'ref_shift_id': $('#ref_shift_id').val(),
        'time_in': $('#time_in').val(),
        'time_out': $('#time_out').val(),
        'reason': $('#reason').val(),
        'ref_status_id': 1,
        'approver_id': 0,
        'approver_date': null,
        'approver_remark': $('#approver_remark').val(),
        'date_deleted': null
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
                var urli = 'RequestOvertime/Delete';
                var id = $('#request_overtime_id').val();
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
                        swal_success();
                        Grid();
                    }
                });

            }
        });
    }
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
            var id = $('#request_dtr_id').val();
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