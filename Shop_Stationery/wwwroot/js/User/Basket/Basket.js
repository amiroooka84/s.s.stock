function ChengeNumberProduct(attr, basket1, typeChenge) {
    if (typeChenge == "lot") {
        var chenge = Number($(".number-product-basket-" + attr).text()) + 1;
    } else {
        var chenge = Number($(".number-product-basket-" + attr).text()) - 1;
    }
    if (chenge < 1) {
        chenge = 1;
    }
    $.ajax({
        url: "/Basket/AddNumberProduct",
        method: "Get",
        data: { number: chenge, attrid: attr, basket: basket1 },
        success: function (data) {
            window.location.reload();
        },
        //xhr: function () {
        //    var xhr = $.ajaxSettings.xhr();
        //    xhr.upload.onprogress = function (data) {
        //        $(".btn-add-basket > span").css("display", "none");
        //        $(".loading").css("display", "block");

        //    }
        //    return xhr;
        //},
        error: function () {
            alert("error");
        }
    });
}

function DeleteBasket(id) {
    $.ajax({
        url: "/Basket/DeleteBasket",
        method: "Get",
        data: { id: id },
        success: function (data) {
          
            if (data) {
                window.location.reload();
            }
            else {

            }
        },
        error: function () {
            alert("error");
        }
    });
}