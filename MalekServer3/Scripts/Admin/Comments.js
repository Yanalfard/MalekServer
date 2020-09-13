function DeleteComment(id) {
    if (confirm('آیا از حذف پیام  مورد نظر مطمئن هستید؟')) {
        // Save it!
        $.ajax({
            /**/
            url: "/Admin/Home/DeleteComment/" + id,
            /**/
            type: "Get",
        }).done(function (result) {
            location.reload();
        });

    } else {
        // Do nothing!
    }
}
function ShowComment(id) {
    $.ajax({
        /**/
        url: "/Admin/Home/ShowHideComment/" + id,
        /**/
        type: "Post",
    }).done(function (result) {
        location.reload();
    });
}
function HideComment(id) {
    $.ajax({
        /**/
        url: "/Admin/Home/ShowHideComment/" + id,
        /**/
        type: "Post",
    }).done(function (result) {
        $("#overlay").fadeOut();
        location.reload();
    });
}