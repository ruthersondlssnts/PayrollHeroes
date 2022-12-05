
//function for updating employee's record
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
       

    var emp = JSON.stringify({
        'group_id': $('#group_id').val(),
        'name': $('#name').val(),
        'currentval': $('#currentval').val(),
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
            Grid($('#currentval').val());
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
function Delete(id) {
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
                    Grid($('#currentval').val());
                }
            });

        }
    });
}

function addEmp() {

    var emp = JSON.stringify({
        'employee_id': $('#employee_id').val(),
        'group_id': $('#currentval').val(),
        'approver_all': false,
        'ot_approver': false,
        'ob_approver': false,
        'leave_approver': false,
        'dtr_approver': false,

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

        url: 'ValidateEmpExist' + '/' + $('#employee_id').val(),
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            if (result != '') {

                Swal.hideLoading();
                Swal.fire({
                    title: 'The employee already assigned to other department. Do you sure you want to move this employee here?',
                    //text: "You won't be able to revert this!",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes',
                    allowOutsideClick: false,
                }).then((result) => {
                    if (result.value) {
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
                            url: 'UpdateEmpGroup',
                            data: emp,
                            headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
                            success: function (data) {
                                Grid($('#currentval').val());
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
                });
            } else {
                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    type: 'POST',
                    traditional: true,
                    url: 'UpdateEmpGroup',
                    data: emp,
                    headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
                    success: function (data) {
                        Grid($('#currentval').val());
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
            
        },
        error: function (errormessage) {
            swal_error();
        }
    });

    
}

