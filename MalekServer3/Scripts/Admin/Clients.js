function deleteGroup(id) {
    if (confirm('آیا از حذف کابر  مورد نظر مطمئن هستید؟')) {
        // Save it!
        $.ajax({
            /**/
            url: "/Admin/Home/DeleteClient/" + id,
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