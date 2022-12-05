$(document).ready(function () {
    Grid();
    $('#acquire_date').datepicker({
        forceParse: false
    });

    //Prevent empty value on outside click
    $('#expire_date').datepicker({
        forceParse: false
    });
});
