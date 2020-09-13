function deleteAd(id) {
    if (confirm('آیا از حذف تبلیغ  مورد نظر مطمئن هستید؟')) {
        // Save it!
        $.ajax({
            /**/
            url: "/Admin/Ad/DeleteAd/" + id,
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