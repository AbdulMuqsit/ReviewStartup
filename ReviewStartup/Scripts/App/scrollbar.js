$(document).ready(function () {

    $(".friend-area").mCustomScrollbar({
        theme: "minimal-dark"
    });
}
);


$(document).ready(function () {
    $(".friend-area").height($(window).height() - 70);

    $(window).resize(function () {
        $(".friend-area").height($(window).height()-70);

    });
});