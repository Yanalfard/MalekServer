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
function AddAdToData() {

    if ($("#titreBlog").val() == "") {
        UIkit.notification(' تیتر خالیست');
        return false;
    }
    if ($("#inputImage").val() == "") {
        UIkit.notification('عکس خالیست');
        return false;
    }
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
                    Link: $("#titreBlog").val(),
                    PositionId: $("#selectPositionAds").val(),
                    Image: result,
                },
                url: "/Admin/Ad/AddAd",
                success: function (response) {
                    if (response.success) {
                        $("#overlay").fadeOut();
                        alert(response.responseText);
                        location.reload();
                        window.location.href = "/Admin/Ad/Ads";
                    } else {
                        $("#overlay").fadeOut();
                        UIkit.notification(response.responseText);
                    }
                },
                error: function (response) {
                    $("#overlay").fadeOut();
                    UIkit.notification("خطا در ثبت اطلاعات لطفا بعدا امتحان کنید"); //
                }
            });

        },
        error: function (err) {
            alert(err.statusText);
        }
    });

}