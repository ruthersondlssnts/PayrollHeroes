
//function for updating employee's record
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    
    var emp = JSON.stringify({
        'ref_calendar_activity_id': $('#ref_calendar_activity_id').val(),
        'work_date': $('#work_date').val(),
        'ref_day_type_id': $('#ref_day_type_id').val(),
        'description': $('#description').val(),
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
            var err = JSON.parse(response.responseText);
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
            var id = $('#ref_calendar_activity_id').val();
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