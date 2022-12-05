function validate() {
    var isValid = true;
    if ($('#leave_date').val().trim() == "") {
        $('#leave_date').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#leave_date').css('border-color', 'lightgrey');
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