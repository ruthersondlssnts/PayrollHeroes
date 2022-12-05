function validate() {
    var isValid = true;
    if ($('#acquire_date').val().trim() == "") {
        $('#acquire_date').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#acquire_date').css('border-color', 'lightgrey');
    }

    if ($('#expire_date').val().trim() == "") {
        $('#expire_date').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#expire_date').css('border-color', 'lightgrey');
    }

    if ($('#quantity').val().trim() == "") {
        $('#quantity').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#quantity').css('border-color', 'lightgrey');
    }

    if ($('#remarks').val().trim() == "") {
        $('#remarks').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#remarks').css('border-color', 'lightgrey');
    }

    return isValid;
}
