function DeleteProduct(id) {
    if (confirm('آیا از حذف کالا  مورد نظر مطمئن هستید؟')) {
        // Save it!
        $.ajax({
            /**/
            url: "/Admin/Home/DeleteProduct/" + id,
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
