function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function AddDiscount() {
    if ($("#DiscountName").val() == "") {
        alert(' کد تخفیف خالیست');
        return false;
    }
    $.ajax({
        type: "POST",
        data: {
            name: $("#DiscountName").val(),
        },
        url: "/UserPanel/Home/CheckDiscount",
        success: function (response) {
            console.log(response);
            if (response.success) {
                var NumberPrice = $('#FinalPricehid').val();
                var price = parseInt(NumberPrice);
                var percent = parseInt(response.responseText);
                var final = price - Math.floor((price * percent) / 100);
                $('#FinalPrice').html(numberWithCommas(final));
                $('#SumFactor').val(final);
                $('#Discount').val(percent);
                $('#DiscountId').val(response.responseId);
                $('.AddDiscount').hide();
                
            } else {
                alert(response.responseText);
            }
        },
        error: function (response) {
            alert("کد تخفیف موجود نیست");
        }
    });
}