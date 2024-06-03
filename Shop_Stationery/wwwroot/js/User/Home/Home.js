
    setInterval(function () {
                var valueScroll;
                if ($(window).width() >= 991) {
        valueScroll = 256;
                } else if ($(window).width() >= 765) {
        valueScroll = 206;
        }
                else {
                    valueScroll = 148;
                }
        var a = $(".slider-set-best-selling").scrollLeft();
        $(".slider-set-best-selling").scrollLeft(a - valueScroll);
        }, 6000);

$(".best-selling-btn-prev").click(function () {
            var valueScroll;
        if ($(window).width() >= 991) {
            valueScroll = 256;
        } else if ($(window).width() >= 765) {
            valueScroll = 206;
        }
        else {
            valueScroll = 148;
        }

        var a = $(".slider-set-best-selling").scrollLeft();
        $(".slider-set-best-selling").scrollLeft(a - valueScroll);
    });

$(".best-selling-btn-next").click(function () {
            var valueScroll;
            if ($(window).width() >= 991) {
        valueScroll = 256;
            } else if ($(window).width() >= 765) {
        valueScroll = 206;
        }
            else {
                valueScroll = 148;
            }

        var a = $(".slider-set-best-selling").scrollLeft();
        $(".slider-set-best-selling").scrollLeft(a + valueScroll);
    });

$(".slider-Offer-btn-next").click(function () {
    var valueScroll = 200;

    var a = $(".slider-Offer-Box-ul").scrollLeft();
    $(".slider-Offer-Box-ul").scrollLeft(a + valueScroll);
});

$(".slider-Offer-btn-prev").click(function () {
    var valueScroll = 200;


    var a = $(".slider-Offer-Box-ul").scrollLeft();
    $(".slider-Offer-Box-ul").scrollLeft(a - valueScroll);
});

$(".menu-trigger").click(function () {
    $(".mySidenav").toggle();
});

function AddBasket(id) {
    $.ajax({
        url: "/product/AddBasket",
        method: "Get",
        data: { productid: id, color: 0, size: 0 },
        success: function (data) {
            if (data == "login") {
                $('.alert-login').css('display' , 'block');
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