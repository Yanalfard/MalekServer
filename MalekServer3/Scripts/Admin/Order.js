function DeleteOrder(id) {
    if (confirm('آیا از حذف آیتم  مورد نظر مطمئن هستید؟')) {
        // Save it!
        $.ajax({
            /**/
            url: "/Admin/Order/DeleteOrder/" + id,
            /**/
            type: "Post",
        }).done(function (response) {
            if (response.success) {
                $("#overlay").fadeOut();
                alert(response.responseText);
                location.reload();
            } else {
                $("#overlay").fadeOut();
                alert(response.responseText);
            }
            location.reload();
        });

    } else {
        // Do nothing!
    }
}
