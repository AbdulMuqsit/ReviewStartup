$(document).ready(function () {
    $("#search").on("input", function () {
        if (window.event && event.type === "propertychange" && event.propertyName !== "value")
            return;

        window.clearTimeout($(this).data("searchTimeOut"));
        var title = $("#search").val();

        if (title === "") {
            $("#searchResults").empty();
            $("#searchResults").removeClass("searchResults");
        } else {
            $(this).data("searchTimeOut", setTimeout(function () {
                var basePath = window.location.protocol + "//" + window.location.host + "/";
                $.getJSON(basePath + "api/Search", { title: title }, populateSearch);

            }, 500));
        }
    });

});
$(document).mouseup(function (e) {
    var searResultsDiv = $("#searchResults");

    if (!searResultsDiv.is(e.target)
        && searResultsDiv.has(e.target).length === 0 && !$("#search").is(e.target)) {
        $("#searchResults").empty();
        $("#searchResults").removeClass("searchResults");
    }
});
function populateSearch(data) {
    var html = "";
    var basePath = window.location.protocol + "//" + window.location.host + "/";
    if (data.MediaPosts.length === 0 && data.Users.length === 0)
        return;
    for (var i = 0; i < data.MediaPosts.length; i++) {
        if (data.MediaPosts[i].Thumbnail) {
            html += "<a href='" + basePath + "MediaPosts/Details/" + data.MediaPosts[i].Id + "'>" + "<div class='row'>" +
              "<img src='data:image/png;base64," + data.MediaPosts[i].Thumbnail + "' />" +
              "<span>" + data.MediaPosts[i].Title + "</span>" +
              "</div>" +
              "</a>";
        } else {
            html += "<a href='" + basePath + "MediaPosts/Details/" + data.MediaPosts[i].Id + "'>" + "<div class='row'> " +
                "<img src='../../Resources/Placeholder.png'/>" +
                "<span>" + data.MediaPosts[i].Title + "</span>" +
                "</div>" +
                "</a>";
        }
    }
    for (var i = 0; i < data.Users.length; i++) {
        if (data.Users[i].Picture) {
            html += "<a href='" + basePath + "Users/Details/" + data.Users[i].UserName + "'>" +
                "<div class='row'>" +
                "<img src='data:image/png;base64," + data.Users[i].Picture + "' />" +
                "<span>" + data.Users[i].UserName + "</span>" +

                "</div>" + "</a>";
        } else {
            html += "<a href='" + basePath + "Users/Details/" + data.Users[i].UserName + "'>" +
                "<div class='row'>" +
                "<img src='../../Resources/Placeholder.jpg'/>" +
                "<span>" + data.Users[i].UserName + "</span>" +
                "</div>" +
                "</a>";

        }
    }

    $("#searchResults").html(html);
    $("#searchResults").addClass("searchResults");
}