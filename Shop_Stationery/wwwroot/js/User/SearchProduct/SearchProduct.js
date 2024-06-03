function OpenSelectCategory() {
    $(".body-select-category").toggle(1000);
}
function OpenCategory(classForOpen) {
    $("." + classForOpen).toggle(1000);
}
$(".form-search-filter  input:not(.input-search-filter)").click(function () {
    $(".form-search-filter").submit();
});

$(".form-search-filter option").click(function () {
    $(".form-search-filter").submit();
});

function pagination(id, thispage) {
    var url = window.location.href.toString();
    if (url.includes('&Page=')) {
        window.location = url.replace('Page=' + thispage, 'Page=' + id);
    }
    else {
        window.location += '&Page='+id;
    }
}

function paginationNext(thispage) {
    var url = window.location.href.toString();
    var nextPage = thispage + 1;
    if (url.includes('&Page=')) {
        window.location = url.replace('Page=' + thispage, 'Page=' + nextPage);    }
    else {
        window.location += '&Page=' + nextPage;
    }
    
}

function AddBasket(id) {
    $.ajax({
        url: "/product/AddBasket",
        method: "Get",
        data: { productid: id, color: 0, size: 0 },
        success: function (data) {
            if (data == "login") {
                $('.alert-login').css('display', 'block');
                //window.location = "/PhoneNumber";
            } else
                if (data == true) {
                    $.ajax({
                        url: "/Basket/CountBasket",
                        method: "Get",
                        success: function (data) {
                            $(".countbasket").text("" + data + "");
                            ReadBasket();
                        },
                        error: function () {
                            alert("error");
                        }
                    });
                } else {
                    ReadBasket();
                }
        },
        error: function () {
            alert("error");
        }
    });
}