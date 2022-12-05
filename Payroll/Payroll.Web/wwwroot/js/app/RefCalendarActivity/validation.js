function validate() {
    var isValid = true;
    if ($('#work_date').val().trim() == "") {
        $('#work_date').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#work_date').css('border-color', 'lightgrey');
    }

    if ($('#description').val().trim() == "") {
        $('#description').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#description').css('border-color', 'lightgrey');
    }



    return isValid;
}