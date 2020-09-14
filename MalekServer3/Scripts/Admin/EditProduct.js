$(function () {
    $('#description').ckeditor();
});


function SelectMotherCatagory() {
    $.ajax({
        type: "POST",
        data: { NamemotherCatagory: $('#SelectZirCatagory').val() },
        url: "/Admin/Home/ChangeMotherCatagory",
        success: function (response) {
            var c = JSON.parse(response);
            $('#SelectCatagory').val(c);

        },
        error: function (response) {
            $("#overlay").fadeOut();
            alert("خطا در ثبت اطلاعات لطفا بعدا امتحان کنید"); //
        }
    });
};
SelectMotherCatagory();
function SelectCatagory() {
    $.ajax({
        type: "POST",
        data: { NameCatagory: $('#SelectCatagory').val() },
        url: "/Admin/Home/ChangeCatagory",
        success: function (response) {
            $("#overlay").fadeOut();
            var c = JSON.parse(response);
            $('#SelectZirCatagory')
                .find('option')
                .remove();
            for (var i = 0; i < c.length; i++) {
                $('#SelectZirCatagory').append('<option value="' + c[i].Name + '">' + c[i].Name + '</option>');

            }
        },
        error: function (response) {
            $("#overlay").fadeOut();
            alert("خطا در ثبت اطلاعات لطفا بعدا امتحان کنید"); //
        }
    });
};
//SelectCatagory();
function EditProductToData() {
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
            id: $("#hiddenId").val(),
            Name: $("#nameKala").val(),
            DescriptionHtml: $("#description").val(),
            Price: $("#price").val(),
            keywords: $("#keywords").val(),
            SelectCatagory: $("#SelectCatagory").val(),
            SelectZirCatagory: $("#SelectZirCatagory").val(),
            IsSuggested: IsSuggested,
            Discount: $("#discount").val(),
        },
        url: "/Admin/Home/EditProduct",
        success: function (response) {
            if (response.success) {
                $("#overlay").fadeOut
                alert(response.responseText);
                window.location.href = "/Admin/Home/Products";
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

