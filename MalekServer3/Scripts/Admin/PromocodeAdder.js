function DeleteDiscount(id) {
    if (confirm('آیا از حذف درصد  مورد نظر مطمئن هستید؟')) {
        // Save it!
        $.ajax({
            /**/
            url: "/Admin/Home/DeleteDiscount/" + id,
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
function AddDiscount(Name, Discount, Count) {
    if (confirm('آیا از اضافه کردن درصد  مورد نظر مطمئن هستید؟')) {
        // Save it!
        $.ajax({
            /**/
            data: {
                Name: Name,
                Discount: Discount,
                Count, Count
            },
            url: "/Admin/Home/AddDiscount",
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
