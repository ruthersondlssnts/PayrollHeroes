function validate() {
    var isValid = true;
    if ($('#employee_serial').val().trim() == "") {
        $('#employee_serial').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#employee_serial').css('border-color', 'lightgrey');
    }

    if ($('#username').val().trim() == "") {
        $('#username').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#username').css('border-color', 'lightgrey');
    }


    if ($('#password').val().trim() == "") {
        $('#password').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#password').css('border-color', 'lightgrey');
    }

    if ($('#last_name').val().trim() == "") {
        $('#last_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#last_name').css('border-color', 'lightgrey');
    }

    if ($('#first_name').val().trim() == "") {
        $('#first_name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#first_name').css('border-color', 'lightgrey');
    }

    if ($('#email_address').val().trim() == "") {
        $('#email_address').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#email_address').css('border-color', 'lightgrey');
    }

    if ($('#contact_number').val().trim() == "") {
        $('#contact_number').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#contact_number').css('border-color', 'lightgrey');
    }

    if ($('#sss').val().trim() == "") {
        $('#sss').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#sss').css('border-color', 'lightgrey');
    }
    if ($('#pagibig').val().trim() == "") {
        $('#pagibig').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#pagibig').css('border-color', 'lightgrey');
    }
    if ($('#philhealth').val().trim() == "") {
        $('#philhealth').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#philhealth').css('border-color', 'lightgrey');
    }
    if ($('#date_hire').val().trim() == "") {
        $('#date_hire').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#date_hire').css('border-color', 'lightgrey');
    }
    if ($('#basic_pay').val().trim() == "") {
        $('#basic_pay').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#basic_pay').css('border-color', 'lightgrey');
    }
    return isValid;
}
