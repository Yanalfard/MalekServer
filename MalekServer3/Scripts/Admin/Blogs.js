function deleteBlog(id) {
    if (confirm('آیا از حذف مقاله  مورد نظر مطمئن هستید؟')) {
        // Save it!
        $.ajax({
            /**/
            url: "/Admin/Home/DeleteBlog/" + id,
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