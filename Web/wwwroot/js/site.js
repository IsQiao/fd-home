// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(window).scroll(function () {
    const nav = $('#navbarMain');
    const top = 30;
    if ($(window).scrollTop() >= top) {
        nav.addClass('navbar-fixed');
    } else {
        nav.removeClass('navbar-fixed');
    }
});