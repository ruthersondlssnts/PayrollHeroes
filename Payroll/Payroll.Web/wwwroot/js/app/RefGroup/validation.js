function validate() {
    var isValid = true;
    if ($('#name').val().trim() == "") {
        $('#name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#name').css('border-color', 'lightgrey');
    }

   
    return isValid;
}

function ValidateEmpExist(id) {
    $.ajax({

        url: 'ValidateEmpExist' + '/' + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            alert(result);

        },
        error: function (errormessage) {
            swal_error();
        }
    });
}
