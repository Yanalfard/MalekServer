$(function () {
    $('#editor').ckeditor();

});
$('#inputImage').bind('change', function () {
    var fileSize = this.files[0].size / 1024;

    if (fileSize > 2048) {
        alert('سایز عکس باید کمتر از 2 مگابایت باشد');
        $('#inputImage').val("");
        return false;
    }
});
function EditAdToData() {

    if ($("#titreBlog").val() == "") {
        alert(' تیتر خالیست');
        return false;
    }
    if ($("#inputImage").val() == "") {
        $.ajax({
            type: "POST",
            data: {
                id: $("#hiddenId").val(),
                Link: $("#titreBlog").val(),
                PositionId: $("#selectPositionAds").val(),
                Image: $("#imgBlog").val(),
            },
            url: "/Admin/Ad/EditAd",
            success: function (response) {
                if (response.success) {
                    $("#overlay").fadeOut();
                    alert(response.responseText);
                    window.location.href = "/Admin/Ad/Ads";
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
    else {
        var formdata = new FormData();
        var file = $('#inputImage')[0];
        formdata.append('file', file.files[0]);
        $.ajax({
            url: '/api/upload/uploadfile',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: formdata,
            success: function (result) {
                $.ajax({
                    type: "POST",
                    data: {
                        id: $("#hiddenId").val(),
                        Link: $("#titreBlog").val(),
                        PositionId: $("#selectPositionAds").val(),
                        Image: result,
                    },
                    url: "/Admin/Ad/EditAd",
                    success: function (response) {
                        if (response.success) {
                            $("#overlay").fadeOut();
                            alert(response.responseText);
                            window.location.href = "/Admin/Ad/Ads";
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

            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    }

}