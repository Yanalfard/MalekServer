jQuery(function ($) {
    $(document).ajaxSend(function () {
        $("#overlay").fadeIn(300);
    });
});

function AddToCart(id) {
    if ($("#HideuserIdNodAddAdmin").val() == "3") {
        alert("شما مدیر هسنید نمی توانید اضافه کنید");
        return false;
    }
    $.ajax({
        url: "/ShopCart/AddToCart/" + id,
        type: "Get"
    }).done(function (result) {
        $("#shopCart").html(result);
        $("#overlay").fadeOut();
        alert('به سبد خرید اضافه شد');
    });
}
$(function () {
    $("#shopCart").load("/ShopCart/ShopCartCount");
});