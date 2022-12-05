
function getbyID(id) {
    $('#btnSave').hide();
    $('#btnUpdate').hide();
    $('#btnDelete').hide();

    $('#ref_shift_id').val(0);
    $('#grace_period').val(0);
    $('#shift_name').val('');
    $('#shift_in').val('');
    $('#shift_in').css('border-color', 'lightgrey');
    $('#shift_in').mask("##:##", { reverse: false });


    $('#shift_out').val('');
    $('#shift_out').css('border-color', 'lightgrey');
    $('#shift_out').mask("##:##", { reverse: false });

    $('#break_in').val('');
    $('#break_in').css('border-color', 'lightgrey');
    $('#break_in').mask("##:##", { reverse: false });


    $('#break_out').val('');
    $('#break_out').css('border-color', 'lightgrey');
    $('#break_out').mask("##:##", { reverse: false });

    $('#nd_start').val('');
    $('#nd_start').css('border-color', 'lightgrey');
    $('#nd_start').mask("##:##", { reverse: false });

    $('#nd_end').val('');
    $('#nd_end').css('border-color', 'lightgrey');
    $('#nd_end').mask("##:##", { reverse: false });

    $('#include_grace_period').prop('checked', true);
    $('#is_auto_ot').prop('checked', true);
    $('#is_nd').prop('checked', true);

    $('#break_hour').val('');
    $('#required_hour').val('');

    if (id != null) {
        $.ajax({

            url: 'GetbyID' + '/' + id,
            typr: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                $('#ref_shift_id').val(result.ref_shift_id);
                $('#shift_name').val(result.shift_name);
                $('#shift_in').val(result.shift_in);
                $('#shift_out').val(result.shift_out);
                $('#break_in').val(result.break_in);
                $('#break_out').val(result.break_out);

                $('#nd_start').val(result.nd_start);
                $('#nd_end').val(result.nd_start);

                $('#grace_period').val(result.grace_period);
                $('#break_hour').val(result.break_hour);
                $('#required_hour').val(result.required_hour);

                if (result.include_grace_period == true) {
                    $('#include_grace_period').prop('checked', true);
                } else {
                    $('#include_grace_period').prop('checked', false);
                }

                if (result.is_auto_ot == true) {
                    $('#is_auto_ot').prop('checked', true);
                } else {
                    $('#is_auto_ot').prop('checked', false);
                }

                if (result.is_auto_ot == true) {
                    $('#is_nd').prop('checked', true);
                } else {
                    $('#is_nd').prop('checked', false);
                }

                $('#btnUpdate').show();
                $('#btnDelete').show();
                $('#myModal').modal('show');
                GetDetails();
            },
            error: function (errormessage) {
                swal_error();
            }
        });
    } else {
        $('#myModal').modal('show');
        GetDetailsEmpty();
        $('#btnSave').show();
    }
}

function GetDetails() {
    var id = $('#ref_shift_id').val();
    $.ajax({
        type: "GET",
        url: 'GetDays/' + id,
        dataType: "json",
        success: function (data) {
            var s = '';
            s += '<table class = "table table-hover">';
            s += '<tr>';
            s += '<th style="width:50%">Work Day</th>';
            s += '<th style="width:50%">Required Hr.</th>';
            s += '</tr>';
            for (var i = 0; i < data.length; i++) {
                var checked = '';
                if (data[i].rest_day == false) {
                    checked = 'checked';
                }

                s += '<tr>';
                s += '<td>';
                s += '<div class="form-check">';
                s += '<input class="form-check-input" type="checkbox" id="chk_' + i + '" ' + checked + ' value="' + data[i].ref_shift_detail_id + '" />' + data[i].day;
                s += '</div>';
                s += '</td>';
                s += '<td>';
                s += '<input class="form-input" type="text" id="txt_' + i + '" width="5px" value="' + data[i].required_hour + '" />';
                s += '</td>';
                s += '</tr>';
            }
            s += '</table>';
            $("#chkbx").html(s);
        }
    });  
}

function GetDetailsEmpty() {
    $.ajax({
        type: "GET",
        url: 'GetDaysEmpty/',
        dataType: "json",
        success: function (data) {
            var s = '';
            s += '<table class = "table table-hover">';
            s += '<tr>';
            s += '<th style="width:50%">Work Day</th>';
            s += '<th style="width:50%">Required Hr.</th>';
            s += '</tr>';
            for (var i = 0; i < data.length; i++) {
                var checked = '';
                if (data[i].rest_day == false) {
                    checked = 'checked';
                }

                s += '<tr>';
                s += '<td>';
                s += '<div class="form-check">';
                s += '<input class="form-check-input" type="checkbox" id="chk_' + i + '" ' + checked + ' value="0">' + data[i].day;
                s += '</div>';
                s += '</td>';
                s += '<td>';
                s += '<input class="form-input" type="text" id="txt_' + i + '" width="5px" value="' + data[i].required_hour + '">';
                s += '</td>';
                s += '</tr>';
            }
            s += '</table>';
            $("#chkbx").html(s);
        }
    });
}