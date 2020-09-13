function DeleteComment(id) {
    if (confirm('آیا از حذف پیام  مورد نظر مطمئن هستید؟')) {
        // Save it!
        $.ajax({
            /**/
            url: "/Admin/Home/DeleteComment/" + id,
            /**/
            type: "Get",
        }).done(function (result) {
            $("#overlay").fadeOut();
            location.reload();
        });

    } else {
        // Do nothing!
    }
}