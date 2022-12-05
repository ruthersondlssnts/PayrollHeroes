
function getbyID(id) {
    
    
    $('#btnSave').hide();
    $('#btnUpdate').hide();

    $('#role_menu_id').val(0);
    $('#role_menu_parent_id').val(0);

    $('#display_name').val('');
    $('#display_icon').val('');
    $('#controller_name').val('');
    $('#action_name').val('');
    $('#is_enable').prop('checked', true);
    $('#role_menu_parent_id option:first').prop('selected', true);

    $('#role_id option:first').prop('selected', true);

    if (id != null) {
        $.ajax({

            url: 'GetbyID' + '/' + id,
            typr: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                
                $('#role_id').val(result.role_id);
                $('#role_menu_parent_id').val(result.role_menu_parent_id);
                $('#role_menu_id').val(result.role_menu_id);
                $('#display_name').val(result.display_name);
                $('#display_icon').val(result.display_icon);
                $('#controller_name').val(result.controller_name);
                $('#action_name').val(result.action_name);

                if (result.is_enable == true) {
                    $('#is_enable').prop('checked', true);
                } else {
                    $('#is_enable').prop('checked', false);
                }

                $('#btnUpdate').show();
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

function GetRoot() {
    $.ajax({
        type: "GET",
        url: "GetRoot",
        data: "{}",
        success: function (data) {
            var s = '<option value="0">ROOT</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].role_menu_id + '">' + data[i].display_name + '</option>';
            }
            $("#role_menu_parent_id").html(s);

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
