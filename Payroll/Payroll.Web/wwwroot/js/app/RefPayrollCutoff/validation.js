function validate() {
    var isValid = true;
    if ($('#z').val().trim() == "") {
        $('#cutoff_date_start').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#cutoff_date_start').css('border-color', 'lightgrey');
    }

    if ($('#cutoff_date_end').val().trim() == "") {
        $('#cutoff_date_end').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#cutoff_date_end').css('border-color', 'lightgrey');
    }


    
    return isValid;
}