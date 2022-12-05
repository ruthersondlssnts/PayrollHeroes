// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.



// Write your JavaScript code.
//leftmenu active class
$(document).ready(function () {
    $(".nav-link").removeClass("active");
    $("a[href='" + window.location.pathname + "']").addClass("active");
    var dropdown = $("a[href='" + window.location.pathname + "']").parent().parent().parent();
    dropdown.addClass("menu-open");
    dropdown.children("a").addClass("active");
});
//avatar-pic
$(document).ready(function () {
    $.ajax({
        url: "/Employee/GetEmployeePhotoPath",
        type: "get",
        success: function (data) {
            if (data!=null) {
                $("#avatar-pic").attr('src', "/employeephotos/" + data);
            }
        },
        error: function (err) {
        }
    });

});

var isSuccessPost = false;

var table_checkbox = function (data, type, full, meta) {
    var is_checked = data == true ? "checked" : "";
    return '<input type="checkbox" disabled class="checkbox" ' +
        is_checked + ' />';
}

function FormShow(result, error, title) {
    $("#form-modal .modal-body").html(result);
    $("#form-modal .modal-title").html(title);
    $("#form-modal").modal('show');
    $('#error-summary').hide();
    if (error!=null) {
        $('#error-summary').html(error);
        $('#error-summary').show();
    }
}
function FormShowUser(result, error, title) {
    $("#form-modal .modal-body").html(result);
    $("#form-modal .modal-title").html(title);
    $("#form-modal").modal('show');
    $('#error-summary').hide();
    if (error != null) {
        $('#errorlist').html(error);
        $('#error-summary').show();
    }
}
function FormHide() {
    $('#form-modal .modal-body').html('');
    $('#form-modal .modal-title').html('');
    $('#form-modal').modal('hide');
}

//alert swal
function AlertSuccess() {
    Swal.fire({
        icon: 'success',
        title: 'Your work has been saved',
        showConfirmButton: false,
        timer: 1500,
    })
}
function AlertError() {
    Swal.fire(
        'Process Failed!',
        'Server error: ' + 'Try again later',
        'error'
    );
}
function AlertDeleted() {
    Swal.fire(
        'Deleted!',
        'Your file has been deleted.',
        'success'
    )
}


