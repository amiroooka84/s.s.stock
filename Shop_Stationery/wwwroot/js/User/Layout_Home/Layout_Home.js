
function closeNav() {
	$(".menu-trigger").toggleClass('active');
	$('#mySidenav').slideToggle(200);
}

if ($('.menu-trigger').length) {
	$(".menu-trigger").on('click', function () {
		$(this).toggleClass('active');
		$('#mySidenav').toggle(200);
	});
}
$(".btn-login-signin-login").click(function () {
	$(".box-account").toggle("slow");
});
$(".logout").click(function () {
	$("#FormLogout").submit();
});




function ReadBasket() {
    var priceall = 0;
                            $(".box-sidBasket").empty();
    $.ajax({
        url: "/Basket/ReadBasket",
        method: "Get",
        data: {},
        success: function (data) {
            if (data == "login") {
                $('.alert-login').css('display', 'block');
/*                window.location = "/PhoneNumber";*/
            } else {
                data.forEach(function (basket) {
                    $.ajax({
                        url: "/Basket/ReadProduct",
                        method: "Get",
                        data: { productid: basket.productId },
                        success: function (product) {
                            $('.sidBasket').css('display', 'block');
                           var price = 0;
                            price = product.price - (product.price / 100 * product.discount);
                            priceall += price * basket.number;
                                $(".box-sidBasket").append(
                                    ' <div class="sidBasket-item">' +
                                    '<div class="img-sidBasket">' +
                                    '<img src="' + product.imagePath + '" alt="' + product.name + '" /><span class="discount-item">' + product.discount +'%</span>' +
                                    '</div>' +
                                    '<div class="body-item">' +
                                    '<a href="" class="Custom-Tag-A-1 name-product">' +
                                    product.name +
                                    '</a>' +
                                    '<span class="icon-del-basket" onclick="DeleteBasket(' + basket.id + ')">&times;</span>' +
                                    '<br />' +
                                    '<br />' +
                                    '<div class="price-item ">' + basket.number + ' &times; ' + product.price * basket.number + '  <span>قیمت:</span></div>' +
                                    '<div class="price-item text-success">' + basket.number + ' &times;  ' + price * basket.number + ' <span>قیمت با تخفیف:</span></div>' +
                                    '</div>' +
                                    '</div>'
                            );
                            $('.sidBasket-priceall > span').text(priceall.toString());
                        },
                        error: function () {
                            alert("error");
                        }
                    });
                });
            }
        },
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