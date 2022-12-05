function validate() {
    var isValid = true;
    if ($('#shift_name').val().trim() == "") {
        $('#shift_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#shift_name').css('border-color', 'lightgrey');
    }

    if ($('#shift_in').val().trim() == "") {
        $('#shift_in').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#shift_in').css('border-color', 'lightgrey');
    }


    if ($('#shift_out').val().trim() == "") {
        $('#shift_out').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#shift_out').css('border-color', 'lightgrey');
    }

    if ($('#break_in').val().trim() == "") {
        $('#break_in').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#break_in').css('border-color', 'lightgrey');
    }

    if ($('#break_out').val().trim() == "") {
        $('#break_out').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#break_out').css('border-color', 'lightgrey');
    }

    if ($('#required_hour').val().trim() == "") {
        $('#required_hour').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#required_hour').css('border-color', 'lightgrey');
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