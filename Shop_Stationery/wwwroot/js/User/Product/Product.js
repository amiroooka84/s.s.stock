
function Calculate(id) {
    $.ajax({
        url: "/product/CalculatePrice",
        method: "Get",
        data: { productid: $("#productid").val(), color: $(".input-select-color:checked").val(), size: "در اندازگیری دقت کنید مرجوعی سایز نداریم" },
        success: function (data) {
            if (data == "موجود در سبد خرید") {
                $(".btn-add-basket > span").text("موجود در سبد خرید");
                    $(".btn-add-basket").attr("onclick", "window.location = '/Account/Basket'");
            } else
                if (data.number < 1) {
                    $(".btn-add-basket > span").text("ناموجود");
                    $(".btn-add-basket").removeAttr("onclick");
                }
                else {
                    $(".btn-add-basket > span").text("افزودن به سبد خرید");
                    $(".btn-add-basket").attr("onclick", "AddBasket(" + id + ")");
                }
            $(".loading").css("display", "none");
            $(".btn-add-basket > span").css("display", "block");
        },
        xhr: function () {
            var xhr = $.ajaxSettings.xhr();
            xhr.upload.onprogress = function (data) {
                $(".btn-add-basket > span").css("display", "none");
                $(".loading").css("display", "block");
            }
            return xhr;
        },
        error: function () {
            alert("error");
        }
    });
}


function AddBasket(id) {
    $.ajax({
        url: "/product/AddBasket",
        method: "Get",
        data: { productid: id, color: 0, size: 0 },
        success: function (data) {
            if (data == "login") {
                $('.alert-login').css('display', 'block');
/*                window.location = "/PhoneNumber";*/
            } else
                if (data == true) {
                    $(".btn-add-basket > span").css("display", "block");
                    $(".btn-add-basket > span").text("اضافه شد");
                    $(".loading").css("display", "none");
                        $.ajax({
                            url: "/Basket/CountBasket",
                            method: "Get",
                            success: function (data) {
                                $(".countbasket").text("" + data + "");                            },
                            error: function () {
                                alert("error");
                            }
                        });

                } else {
                    $(".btn-add-basket > span").css("display", "block");
                    $(".btn-add-basket > span").text("اضافه نشد");
                    $(".loading").css("display", "none");
                }
            
        },
        xhr: function () {
            var xhr = $.ajaxSettings.xhr();
            xhr.upload.onprogress = function (data) {
                $(".btn-add-basket > span").css("display", "none");
                $(".loading").css("display", "block");

            }
            return xhr;
        },
        error: function () {
            alert("error");
        }
    });
}

function OpenOrder(evt, Order) {
    // Declare all variables
    var i, tabcontent, tablinks;

    // Get all elements with class="tabcontent" and hide them
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Get all elements with class="tablinks" and remove the class "active"
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // Show the current tab, and add an "active" class to the button that opened the tab
    document.getElementById(Order).style.display = "block";
    evt.currentTarget.className += " active";
}

/////////////////
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

function uninterest(id) {
    $.ajax({
        url: "/product/DeleteInterests",
        method: "Get",
        data: { productid: id },
        success: function (data) {
            if (data == true) {
                $("#interest").removeClass("isinterest fa-heart");
                $("#interest").addClass("uninterest  fa-heart-o");
                $("#interest").removeAttr("onclick");
                $("#interest").attr("onclick", "interest(" + id + ")");
            }
        },
        error: function () {
            alert("error");
        }
    });
}

function interest(id) {
    $.ajax({
        url: "/product/AddInterests",
        method: "get",
        data: { productid: id },
        success: function (data) {
            if (data == true) {
                $("#interest").removeClass("uninterest fa-heart-o");
                $("#interest").addClass("isinterest fa-heart");
                $("#interest").removeAttr("onclick");
                $("#interest").attr("onclick", "uninterest(" + id + ")");
            }
        },
        error: function () {
            alert("error");
        }
    });
}