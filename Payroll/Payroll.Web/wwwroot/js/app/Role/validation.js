function validate() {
    var isValid = true;
    if ($('#role_name').val().trim() == "") {
        $('#role_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#role_name').css('border-color', 'lightgrey');
    }

 
    return isValid;
}
