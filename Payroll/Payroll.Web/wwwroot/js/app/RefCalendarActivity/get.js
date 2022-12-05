
function getbyID(id) {
    GetDayType();

    $('#btnSave').hide();
    $('#btnUpdate').hide();
    $('#btnDelete').hide();

    $('#ref_calendar_activity_id').val(0);
    $('#work_date').val('');
    $('#description').val('');
    $('#ref_day_type_id option:first').prop('selected', true);

   
    if (id != null) {
        $.ajax({

            url: 'GetbyID' + '/' + id,
            typr: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                $('#ref_calendar_activity_id').val(result.ref_calendar_activity_id);

                var d = result.work_date.slice(0, 10).split('-');
                var val = d[1] + '/' + d[2] + '/' + d[0];

                $('#work_date').val(val);
                $('#description').val(result.description);
                $('#shift_out').val(result.shift_out);
                $('#ref_day_type_id').val(result.ref_day_type_id);
                
                $('#btnUpdate').show();
                $('#btnDelete').show();
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

function GetDayType() {
    $.ajax({
        type: "GET",
        url: "GetDayType",
        data: "{}",
        success: function (data) {
            var s = '';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ref_day_type_id + '">' + data[i].date_type_name + '</option>';
            }
            $("#ref_day_type_id").html(s);
        }
    });
}
