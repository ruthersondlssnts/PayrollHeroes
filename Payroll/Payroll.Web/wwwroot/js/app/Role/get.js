
function getbyID(id) {
    $('#btnSave').hide();
    $('#btnUpdate').hide();

    $('#role_id').val(0);
    $('#role_name').val('');


    if (id != null) {
        $.ajax({

            url: 'GetbyID' + '/' + id,
            typr: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                $('#role_id').val(result.role_id);
                $('#role_name').val(result.role_name);

                $('#btnUpdate').show();
                $('#myModal').modal('show');
                GetDetails();
            },
            error: function (errormessage) {
                swal_error();
            }
        });
    } else {
        $('#myModal').modal('show');
        GetDetails2();
        $('#btnSave').show();
    }
}

function GetDetails2() {
    var id = $('#role_id').val();
    $.ajax({
        type: "GET",
        url: 'GetDetailsEmpty/',
        dataType: "json",
        success: function (data) {
            var s = '';
            s += '<div class="container">';
            s += '<div class="card-body">';
            s += '<div class="row">';
            for (var i = 0; i < data.length; i++) {
                var checked = '';
                if (data[i].is_enable == true) {
                    checked = 'checked';
                }
                s += '<div class="col-6">';
                s += '<input class="form-check-input" type="checkbox" id="chk_' + i + '" ' + checked + ' value="' + data[i].permission_id + '">';
                s += '<label class="form-check-label">' + data[i].permission_name + '</label>';
                s += '</div>';
            }
            s += '</div>';
            s += '</div>';
            s += '</div>';
            $("#chkbx").html(s);
        }
    });
}

function GetDetails() {
    var id = $('#role_id').val();
    $.ajax({
        type: "GET",
        url: 'GetDetails/' + id,
        dataType: "json",
        success: function (data) {
            var s = '';
            s += '<div class="container">';
            s += '<div class="card-body">';
            s += '<div class="row">';
            for (var i = 0; i < data.length; i++) {
                var checked = '';
                if (data[i].is_enable == true) {
                    checked = 'checked';
                }
                s += '<div class="col-6">';
                s += '<input class="form-check-input" type="checkbox" id="chk_' + i + '" ' + checked + ' value="' + data[i].permission_id + '">';
                s += '<label class="form-check-label">' + data[i].permission_name + '</label>';
                s += '</div>';
            }
            s += '</div>';
            s += '</div>';
            s += '</div>';
            $("#chkbx").html(s);
        }
    });
}