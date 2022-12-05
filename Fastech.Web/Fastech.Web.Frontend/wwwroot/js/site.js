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