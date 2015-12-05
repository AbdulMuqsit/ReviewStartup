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

            }, 300));
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
    if (data.MediaPosts.length > 0) {
        html += "<div class='separator'>Media</div>";

    }
    for (var i = 0; i < data.MediaPosts.length; i++) {
        if (data.MediaPosts[i].Thumbnail) {
            html += "<a href='" + basePath + "MediaPosts/Details/" + data.MediaPosts[i].Id + "'>" + "<div class='row'>" +
              "<img src='data:image/png;base64," + data.MediaPosts[i].Thumbnail + "' />" +
              "<h4>" + data.MediaPosts[i].Title + "</h4>" +
               "<span>" + data.MediaPosts[i].Type + "</span>" +
              "</div>" +
              "</a>";
        } else {
            html += "<a href='" + basePath + "MediaPosts/Details/" + data.MediaPosts[i].Id + "'>" + "<div class='row'> " +
                "<img src='../../Resources/Placeholder.png'/>" +
                "<h4>" + data.MediaPosts[i].Title + "</h4>" +
               "<span>" + data.MediaPosts[i].Type + "</span>" +
               "</div>" +
                "</a>";
        }
    }
    if (data.Users.length > 0) {
        html += "<div class='separator'>Users</div>";

    }
    for (var i = 0; i < data.Users.length; i++) {
        if (data.Users[i].Picture) {
            html += "<a href='" + basePath + "Users/Details/" + data.Users[i].UserName + "'>" +
                "<div class='row'>" +
                "<img src='data:image/png;base64," + data.Users[i].Picture + "' />" +
                "<h4>" + data.Users[i].UserName + "</h4>" +

                "</div>" + "</a>";
        } else {
            html += "<a href='" + basePath + "Users/Details/" + data.Users[i].UserName + "'>" +
                "<div class='row'>" +
                "<img src='../../Resources/Placeholder.jpg'/>" +
                "<h4>" + data.Users[i].UserName + "</h4>" +
                "</div>" +
                "</a>";

        }
    }

    $("#searchResults").html(html);
    $("#searchResults").addClass("searchResults");
}