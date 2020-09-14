$('#inputImage').bind('change', function () {
    var fileSize = this.files[0].size / 1024;
    if (fileSize > 2048) {
        alert('سایز عکس باید کمتر از 2 مگابایت باشد');
        $('#inputImage').val("");
        return false;
    }
});