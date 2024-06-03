var ID = 0;

function ReadCategory(id) {
    ID = id;
    $.ajax({
        url: "/CategoryManagement/ReadCategoryById",
        method: "Get",
        data: { id: id },
        success: function (data) {
            $("#input-category-id").val(id);
            $("#Input-Category-Name").val(data.name);
            $("#Input-Category-Category").val(data.categoryid);
            $("#Input-Category-img").val(data.image);
            $("#btn-Category").text("ویرایش دسته");
            $(".label-file1").text("عکسه دسته");
            $(".label-file2").css("display", "inline");
            $(".close-editing").css("display", "block");
            $(".form-add-category").removeAttr("action");
            $(".form-add-category").attr("action", "/CategoryManagement/EditCategory");
        },
        error: function () {
            alert("error");
        }
    });
}

function CloseEditing() {
    $("#Input-Category-Name").val("");
    $("#Input-Category-Category").val(0);
    $("#Input-Category-img").empty();
    $("#btn-Category").text("ثبت دسته");
    $(".label-file1").text("عکسه دسته*");
    $(".label-file2").css("display", "none");
    $(".close-editing").css("display", "none");
    $(".form-add-category").removeAttr("action");
    $(".form-add-category").attr("action", "/CategoryManagement");
}