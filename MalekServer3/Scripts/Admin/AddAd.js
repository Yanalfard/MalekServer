$(function () {
    $('#editor').ckeditor();

});

function AddAdToData() {

    if ($("#titreBlog").val() == "") {
        alert(' تیتر خالیست');
        return false;
    }
    if ($("#inputImage").val() == "") {
        alert('عکس خالیست');
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