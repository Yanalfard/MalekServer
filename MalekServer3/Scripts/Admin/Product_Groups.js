

function Create(CatagoryId) {
    $.get("/Admin/Product_Groups/Create/" + CatagoryId,
        function (result) {
            $("#myModal").modal();
            $("#myModalLabel").html("افزودن گروه جدید");
            $("#myModalBody").html(result);
        });
}


function CreateZirDate(CatagoryId) {
    $.get("/Admin/Product_Groups/CreateZirDate/" + CatagoryId,
        function (result) {
            $("#myModal").modal();
            $("#myModalLabel").html("افزودن زیر  گروه جدید");
            $("#myModalBody").html(result);
        });
}


function Edit(id) {
    $.get("/Admin/Product_Groups/Edit/" + id,
        function (result) {
            $("#myModal").modal('show');
            $("#myModalLabel").html("ویرایش گروه");
            $("#myModalBody").html(result);
        });
}
function EditZirCatagory(id) {
    $.get("/Admin/Product_Groups/EditZirCatagory/" + id,
        function (result) {
            $("#myModal").modal('show');
            $("#myModalLabel").html("ویرایش زیر گروه");
            $("#myModalBody").html(result);
        });
}
function Delete(id) {

    if (confirm('آیا از حذف دسته  مورد نظر مطمئن هستید؟')) {
        // Save it!
        $.ajax({
            /**/
            url: "/Admin/Product_Groups/Delete/" + id,
            /**/
            type: "Post",
        }).done(function (response) {
            if (response.success) {
                alert(response.responseText);
                location.reload();
            } else {
                alert(response.responseText);
            }
            location.reload();
        });

    } else {
        // Do nothing!
    }

}

function Delete(id) {
    if (confirm('آیا از حذف   مورد نظر مطمئن هستید؟')) {
        // Save it!
        $.ajax({
            /**/
            // url: "/Admin/Product_Groups/Delete/" + id,
            url: "/Admin/Home/DeleteCatagory/" + id,
            /**/
            type: "Post",
        }).done(function (response) {
            if (response.success) {
                alert(response.responseText);
                location.reload();
            } else {
                alert(response.responseText);
            }

        });
    } else {
        alert("دسته مورد نظر قابل حذف نیست");
    }
}

function success() {
    $("#myModal").modal('hide');
}

function addCatagory() {

    var CatagoryName = $("#CatagoryName").val();
    if (CatagoryName == "") {
        alert('دسته کالا خالیست');
        return false;
    }
    else {

        $.ajax({
            type: "POST",
            data: { CatagoryName: $("#CatagoryName").val() },
            url: "/Admin/Home/AddCatagory",
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
    };
};

function UpdateCatagory() {

    var CatagoryName = $("#CatagoryName").val();
    var IdCatagory = $("#IdCatagory").val();
    if (CatagoryName == "") {
        alert('دسته کالا خالیست');
        return false;
    }
    else {

        $.ajax({
            type: "POST",
            data: { Name: $("#CatagoryName").val(), id: $("#IdCatagory").val() },
            url: "/Admin/Home/UpdateCatagory",
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
    };
};

function addZirCatagory() {
    var ZirCatagoryName = $("#ZirCatagoryName").val();
    var NameCatagoryAdd = $("#NameCatagoryAdd").val();

    if (ZirCatagoryName == "" || NameCatagoryAdd == "") {
        alert('زیر دسته کالا خالیست');
        return false;
    }
    else {
        let IsOnFirstPage;
        if ($('#IsOnFirstPage').is(":checked")) {
            IsOnFirstPage = true;

        }
        else if ($('#IsOnFirstPage').is(":not(:checked)")) {
            IsOnFirstPage = false;
        }
        $.ajax({
            type: "POST",
            data: { ZirCatagoryName: $("#ZirCatagoryName").val(), NameCatagoryAdd: $("#NameCatagoryAdd").val(), IsOnFirstPage: IsOnFirstPage },
            url: "/Admin/Home/AddZirCatagory",
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
}


function UpdateZirCatagory() {
    var ZirCatagoryName = $("#ZirCatagoryName").val();
    var NameCatagoryAdd = $("#NameCatagoryAdd").val();

    if (ZirCatagoryName == "" || NameCatagoryAdd == "") {
        alert('زیر دسته کالا خالیست');
        return false;
    }
    else {
        let IsOnFirstPage;
        if ($('#IsOnFirstPage').is(":checked")) {
            IsOnFirstPage = true;

        }
        else if ($('#IsOnFirstPage').is(":not(:checked)")) {
            IsOnFirstPage = false;
        }
        $.ajax({
            type: "POST",
            data: { Name: $("#ZirCatagoryName").val(), id: $("#IdCatagory").val(), IsOnFirstPage: IsOnFirstPage },
            url: "/Admin/Home/UpdateZirCatagory",
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
}