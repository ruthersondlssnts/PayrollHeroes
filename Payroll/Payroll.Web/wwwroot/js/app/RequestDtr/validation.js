function validate() {
    var isValid = true;
    if ($('#shift_date').val().trim() == "") {
        $('#shift_date').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#shift_date').css('border-color', 'lightgrey');
    }
    if ($('#time_in').val().trim() == "") {
        $('#time_in').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#time_in').css('border-color', 'lightgrey');
    }
    if ($('#time_out').val().trim() == "") {
        $('#time_out').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#time_out').css('border-color', 'lightgrey');
    }
    if ($('#reason').val().trim() == "") {
        $('#reason').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#reason').css('border-color', 'lightgrey');
    }

    return isValid;
}



function validateremarks() {
    var isValid = true;
    if ($('#approver_remark').val().trim() == "") {
        $('#approver_remark').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#approver_remark').css('border-color', 'lightgrey');
    }

    return isValid;
}