// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function swal_success() {
    Swal.fire(
        'Process!',
        'Data Saved',
        'success'
    );
}

function swal_loadcomplete() {
    Swal.fire(
        'Process!',
        'Loading complete',
        'success'
    );
}

function swal_updaterole() {
    Swal.fire(
        'Process!',
        'Data Saved. Please logout the system to take effect',
        'success'
    );
}

function swal_error(errormessage) {
    Swal.fire(
        'Process Failed!',
        'Data saving failed: ' + errormessage,
        'error'
    );
}

function swal_errorloging(errormessage) {
    Swal.fire(
        'Process Failed!',
        'Login failed: ' + errormessage,
        'error'
    );
}

function swal_getdata() {
    Swal.fire(
        'Process Failed!',
        'Get Data failed',
        'error'
    );
}

function swal_reset() {
    Swal.fire(
        'Session reset',
        'Session has been reset.',
        'success'
    );
}

function swal_wait() {
    Swal.fire({
        title: 'Please Wait..!',
        text: 'Is working..',
        allowOutsideClick: false,
        allowEscapeKey: false,
        allowEnterKey: false,
        onOpen: () => {
            swal.showLoading();
        }
    })
}