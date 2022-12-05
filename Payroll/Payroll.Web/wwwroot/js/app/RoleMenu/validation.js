function validate() {
    var isValid = true;
    if ($('#department_name').val().trim() == "") {
        $('#department_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#department_name').css('border-color', 'lightgrey');
    }

 
    return isValid;
}
