$(document).ready(function () {
    $("#add").on('click', function (ev) {
        ev.preventDefault();
        ev.stopPropagation();
        var path = $(this).attr('href');
        var basePath = window.location.protocol + "//" + window.location.host;
        var jxhr = $.get(basePath+path);
        jxhr.done(function () {
            toastr.success("Friend Request Sent.");
            location.reload();
        }).fail(function() {
            toastr.error("And error happened while sending the request. Try again later.");
        });

    });
});
$(document).ready(function () {
    $("#remove").on('click', function (ev) {
        ev.preventDefault();
        ev.stopPropagation();
        var path = $(this).attr('href');
        var basePath = window.location.protocol + "//" + window.location.host;
        var jxhr = $.get(basePath + path);
        jxhr.done(function () {
            toastr.success("Freind Removed");
            location.reload();
        }).fail(function () {
            toastr.error("And error happened while removing friend. Try again later.");
        });

    });
});
$(document).ready(function () {
    $(".accept").on('click', function (ev) {
        ev.preventDefault();
        ev.stopPropagation();
        var path = $(this).attr('href');
        var basePath = window.location.protocol + "//" + window.location.host;
        var jxhr = $.get(basePath + path);
        jxhr.done(function () {
            toastr.success("Freind Added");
            location.reload();
        }).fail(function () {
            toastr.error("And error happened while sending request. Try again later.");
        });

    });
});
$(document).ready(function () {
    $(".reject").on('click', function (ev) {
        ev.preventDefault();
        ev.stopPropagation();
        var path = $(this).attr('href');
        var basePath = window.location.protocol + "//" + window.location.host;
        var jxhr = $.get(basePath + path);
        jxhr.done(function () {
            toastr.success("Request Rejected");
            location.reload();
        }).fail(function () {
            toastr.error("And error happened while sending request. Try again later.");
        });

    });
});
$(document).ready(function () {
    $("#cancel").on('click', function (ev) {
        ev.preventDefault();
        ev.stopPropagation();
        var path = $(this).attr('href');
        var basePath = window.location.protocol + "//" + window.location.host;
        var jxhr = $.get(basePath + path);
        jxhr.done(function () {
            toastr.success("Friend Request Canceled");
            location.reload();
        }).fail(function () {
            toastr.error("And error happened while sending request. Try again later.");
        });

    });
});