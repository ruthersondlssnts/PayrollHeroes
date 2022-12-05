
//function for updating employee's record
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var detail = 
        [
            {
                'ref_shift_detail_id': GetValue(0),
                'day': 'MON',
                'rest_day': IsRestDay(0),
                'required_hour': $('#txt_0').val()
            },
            {
                'ref_shift_detail_id': GetValue(1),
                'day': 'TUE',
                'rest_day': IsRestDay(1),
                'required_hour': $('#txt_1').val()
            },
            {
                'ref_shift_detail_id': GetValue(2),
                'day': 'WED',
                'rest_day': IsRestDay(2),
                'required_hour': $('#txt_2').val()
            },
            {
                'ref_shift_detail_id': GetValue(3),
                'day': 'THU',
                'rest_day': IsRestDay(3),
                'required_hour': $('#txt_3').val()
            },
            {
                'ref_shift_detail_id': GetValue(4),
                'day': 'FRI',
                'rest_day': IsRestDay(4),
                'required_hour': $('#txt_4').val()
            },
            {
                'ref_shift_detail_id': GetValue(5),
                'day': 'SAT',
                'rest_day': IsRestDay(5),
                'required_hour': $('#txt_5').val()
            },
            {
                'ref_shift_detail_id': GetValue(6),
                'day': 'SUN',
                'rest_day': IsRestDay(6),
                'required_hour': $('#txt_6').val()
            }
        ];
    

    var emp = JSON.stringify({
        'ref_shift_id': $('#ref_shift_id').val(),
        'shift_name': $('#shift_name').val(),
        'shift_in': $('#shift_in').val(),
        'shift_out': $('#shift_out').val(),
        'break_in': $('#break_in').val(),
        'break_out': $('#break_out').val(),
        'required_hour': $('#required_hour').val(),
        'ref_shift_detail_': JSON.stringify(detail)
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
        url: 'RefShift/Update',
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

function IsRestDay(id) {
    if ($('#chk_' + id).is(':checked')) {
        return 'false';
    } else {
        return 'true';
    }
}
function GetValue(id) {
    if ($('#ref_shift_id').val() == '')
        return 0;
    return $('#chk_' + id).val();
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
            var urli = 'RefShift/Delete';
            var id = $('#ref_shift_id').val();
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