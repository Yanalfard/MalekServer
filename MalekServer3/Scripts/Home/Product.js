$(document).ready(function () {
    $('.radioName').click(function () {
        var NameChose = $('input[name=rating]:checked').val();
        $('.HideStatusId').val($('input[name=rating]:checked').val());
    });
});
function addComment() {
    $.ajax({
        type: "POST",
        data: {
            id: $("#HideProductId").val(),
            raiting: $("#HideStatusId").val(),
            comment: $("#txtComment").val(),
            userId: $("#HideuserId").val()
        },
        url: "/Home/AddComment",
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
            alert(response.responseText);
        }
    });
}
