
$(function () {
    $('#description').ckeditor();

});

function SelectCatagory() {
    $.ajax({
        type: "POST",
        data: { NameCatagory: $('#SelectCatagory').val() },
        url: "/Admin/Home/ChangeCatagory",
        success: function (response) {
            $("#overlay").fadeOut();
            var c = JSON.parse(response);
            if (c != "Err022") {

                $('#SelectZirCatagory')
                    .find('option')
                    .remove();
                for (var i = 0; i < c.length; i++) {
                    $('#SelectZirCatagory').append('<option value="' + c[i].Name + '">' + c[i].Name + '</option>');
                }
            }
        },
        error: function (response) {
            $("#overlay").fadeOut();
            alert("خطا در ثبت اطلاعات لطفا بعدا امتحان کنید"); //
        }
    });
};
SelectCatagory();


function AddProductToData() {
    var ZirCatagoryName = $("#SelectZirCatagory").val();
    var NameCatagoryAdd = $("#SelectCatagory").val();
    if (ZirCatagoryName == "" || NameCatagoryAdd == "") {
        alert('دسته یا زیر دسته کالا خالیست');
        return false;
    }
    if ($("#nameKala").val() == "") {
        alert('نام کالا خالیست');
        return false;
    }
    if ($("#description").val() == "") {
        alert('توضیحات کالا خالیست');
        return false;

    }
    if ($("#price").val() == "") {
        alert('قیمت کالا خالیست');
        return false;

    }
    if ($("#keywords").val() == "") {
        alert('کلمات  کالا خالیست');
        return false;

    }

    let IsSuggested;
    if ($('#IsSuggested').is(":checked")) {
        IsSuggested = true;

    }
    else if ($('#IsSuggested').is(":not(:checked)")) {
        IsSuggested = false;
    }
    $.ajax({
        type: "POST",
        data: {
            Name: $("#nameKala").val(),
            DescriptionHtml: $("#description").val(),
            Price: $("#price").val(),
            keywords: $("#keywords").val(),
            SelectCatagory: $("#SelectCatagory").val(),
            SelectZirCatagory: $("#SelectZirCatagory").val(),
            IsSuggested: IsSuggested,
            Discount: $("#discount").val(),
        },
        url: "/Admin/Home/AddProduct",
        success: function (response) {
            if (response.success) {
                $("#overlay").fadeOut();
                alert(response.responseText);
                location.reload();
            } else {
                $("#overlay").fadeOut();
                alert(response.responseText);
            }
        },
        error: function (response) {
            $("#overlay").fadeOut();
            alert("خطا در ثبت اطلاعات لطفا بعدا امتحان کنید"); //
        }
    });


}