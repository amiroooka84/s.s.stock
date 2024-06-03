var ID = 0;


$(".open-box-select-attribute").click(function () {
    $(".box-select-attribute").toggle(2000);
});


function ReadProduct(id) {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0; ID = id;
    $(".input-shops").attr("checked" , false);
    $.ajax({
        url: "/ProductManagement/ReadProductById",
        method: "Get",
        data: { id: id },
        success: function (data) {
            data.pas.forEach(function (data) {
                $(".input-shops-" + data.shopid).attr("checked", true);
            });
            $("#input-Product-id").val(id);
            $("#input-Product-name").val(data.product.name); 
            $("#input-Product-slack").val(data.product.slack);
            $("#input-Product-code").val(data.product.code);
            $("#input-Product-price").val(data.product.price);
            $("#input-Product-discount").val(data.product.discount);
            $("#input-Product-number").val(data.product.number);
            $("#input-Product-categoryid").val(data.product.categoryid);
            $("#input-Product-discription").val(data.product.discription);
            $("#ck-content").html(data.product.discription);
            $("#input-Product-Specifications").val(data.product.specifications);
            $("#btn-Category").text("ویرایش کالا");
            $(".label-file1").text("عکسه کالا");
            $(".label-file2").css("display", "inline");
            $(".close-editing").css("display", "block");
            $(".form-add-Product").removeAttr("action");
            $(".form-add-Product").attr("action", "/ProductManagement/EditProduct");
            data.attributes.forEach(function (data, index) {
                $(".attribute-product-id-" + index).val(data.id);
                $(".attribute-product-size-" + index).val(data.size);
                $(".attribute-product-color-" + index).val(data.color);
                $(".attribute-product-number-" + index).val(data.number);
                $(".attribute-product-colorcode-" + index).val(data.colorcode);
            });
        },
        error: function () {
            alert("error");
        }
    });
}

function CloseEditing() {
    $("#input-Product-id").val(0);
    $("#input-Product-name").val("");
    $("#input-Product-code").val(0);
    $("#input-Product-price").val(0);
    $("#input-Product-discount").val(0);
    $("#input-Product-number").val(0);
    $("#input-Product-categoryid").val(0);
    $("#input-Product-discription").val("");
    $("#input-Product-Specifications").val("");
    $("#btn-Category").text("ثبت کالا");
    $(".label-file1").text("عکسه کالا*");
    $(".label-file2").css("display", "none");
    $(".close-editing").css("display", "none");
    $(".form-add-Product").removeAttr("action");
    $(".form-add-Product").attr("action", "/ProductManagement");
}