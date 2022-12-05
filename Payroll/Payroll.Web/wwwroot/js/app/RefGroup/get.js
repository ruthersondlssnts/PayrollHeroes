
function getbyID(id) {
    $('#btnSave').hide();
    $('#btnUpdate').hide();
    $('#btnDelete').hide();

    $('#group_id').val(0);
    $('#name').val('');

    if (id != null) {
        $.ajax({

            url: 'GetbyID' + '/' + id,
            typr: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
            
                if ($('#current_level').val() == '') {
                    document.getElementById("myModalLabel").innerHTML = "Company Detail";
                }
                if ($('#current_level').val() == 1) {
                    document.getElementById("myModalLabel").innerHTML = "Branch Detail";
                }
                if ($('#current_level').val() == 2) {
                    document.getElementById("myModalLabel").innerHTML = "Department Detail";
                }

                $('#group_id').val(result.group_id);
                $('#name').val(result.name);
                $('#btnUpdate').show();
                $('#btnDelete').show();
                $('#myModal').modal('show');

            },
            error: function (errormessage) {
                swal_error();
            }
        });
    } else {
        if ($('#current_level').val() == '') {
            document.getElementById("myModalLabel").innerHTML = "Company Detail";
        }
        if ($('#current_level').val() == 1) {
            document.getElementById("myModalLabel").innerHTML = "Branch Detail";
        }
        if ($('#current_level').val() == 2) {
            document.getElementById("myModalLabel").innerHTML = "Department Detail";
        }
        $('#myModal').modal('show');
        $('#btnSave').show();
    }
}

function GetEmployee() {
    $.ajax({
        type: "GET",
        url: "GetEmployee",
        data: "{}",
        success: function (data) {
            var s = '';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].employee_id + '">' + data[i].last_name + ', ' + data[i].first_name + '</option>';
            }
            $("#employee_id").html(s);
        }
    });

    $("#employee_id").select2();
}


