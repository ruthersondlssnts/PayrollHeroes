
function getbyID(id) {
    GetShift();
    GetDepartment();
    GetRole();
    $('#btnSave').hide();
    $('#btnUpdate').hide();
    $('#btnDelete').hide();

    $('#employee_id').val(0);
    $('#employee_serial').val('');
    $('#username').val('');
    $('#password').val('');

    $('#last_name').val('');
    $('#first_name').val('');
    $('#middle_name').val('');
    $('#email_address').val('');
    $('#gender').val('');

    $('#contact_number').val('');
    $('#sss').val('');
    $('#pagibig').val('');
    $('#philhealth').val('');

    $('#marital_status').val('');
    $('#date_hire').val('');
    $('#date_resign').val('');
    $('#ref_shift_id option:first').prop('selected', true);
    $('#ref_department_id option:first').prop('selected', true);
    $('#role_id option:first').prop('selected', true);

    $('#fingerprint').val('');
    $('#basic_pay').val('');

    if (id != null) {
        $.ajax({

            url: 'GetbyID' + '/' + id,
            typr: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                $('#employee_id').val(result.employee_id);
                $('#employee_serial').val(result.employee_serial);
                $('#username').val(result.username);
                $('#password').val(result.password);

                $('#last_name').val(result.last_name);
                $('#first_name').val(result.first_name);
                $('#middle_name').val(result.middle_name);
                $('#email_address').val(result.email_address);
                $('#gender').val(result.gender);

                $('#contact_number').val(result.contact_number);
                $('#sss').val(result.sss);
                $('#pagibig').val(result.pagibig);
                $('#philhealth').val(result.philhealth);

                $('#marital_status').val(result.marital_status);
                $('#date_hire').val(ConvertDate(result.date_hire));
                $('#date_resign').val(ConvertDate(result.date_resign));
                $('#ref_shift_id').val(result.ref_shift_id);
                $('#ref_department_id').val(result.ref_department_id);
                $('#basic_pay').val(result.basic_pay);
                $('#role_id').val(result.role_id);
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

function GetShift() {
    $.ajax({
        type: "GET",
        url: "GetShift",
        data: "{}",
        success: function (data) {
            var s = '';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ref_shift_id + '">' + data[i].shift_name + '</option>';
            }
            $("#ref_shift_id").html(s);
        }
    });
}

function GetDepartment() {
    $.ajax({
        type: "GET",
        url: "GetDepartment",
        data: "{}",
        success: function (data) {
            var s = '';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ref_department_id + '">' + data[i].department_name + '</option>';
            }
            $("#ref_department_id").html(s);
        }
    });
}

function GetRole() {
    $.ajax({
        type: "GET",
        url: "GetRole",
        data: "{}",
        success: function (data) {
            var s = '';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].role_id + '">' + data[i].role_name + '</option>';
            }
            $("#role_id").html(s);
        }
    });
}

function ConvertDate(data) {
    alert(data);
    if (data == null)
        return '';
    var d = data.slice(0, 10).split('-');
    var val = d[1] + '/' + d[2] + '/' + d[0];
    return val;
}