function validate() {
    var isValid = true;
    if ($('#ref_leave_type_name').val().trim() == "") {
        $('#ref_leave_type_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ref_leave_type_name').css('border-color', 'lightgrey');
    }

    if ($('#ref_leave_type_code').val().trim() == "") {
        $('#ref_leave_type_code').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ref_leave_type_code').css('border-color', 'lightgrey');
    }
    return isValid;
}
