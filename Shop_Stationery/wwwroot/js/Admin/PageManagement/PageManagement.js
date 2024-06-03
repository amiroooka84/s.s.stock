
$(".button-submit-file").click(function () {
    if ($(".input-name-folder").empty) {

        var txtbtn = $(".button-submit-file").text();
            $.ajax({
            method: "post",
                url: "/PagesManagement/AddBanner",
                data: new FormData(document.forms[0]),
            contentType: false,
            processData: false,
            success: function (data) {
                if (data == true) {
                    $('.Results-create-file').text('بنر اضافه شد');
                    $('.Results-create-file').css('color', 'green');
                    $('.value-progressBar').css('background-color', 'green');
                    document.forms[0].reset();
                }
                else {
                    $('.Results-create-file').text('بنر اضافه نشد');
                    $('.Results-create-file').css('color', 'red');
                    $('.value-progressBar').css('background-color', 'red'); 
                }
            },
            error: function (data) {
                //alert("error");
                //$('.Results-create-file').text('بنر اضافه نشد');
                //$('.Results-create-file').css('color', 'red');
                //$('.value-progressBar').css('background-color' , 'red');
            },
            xhr: function () {
                var xhr = $.ajaxSettings.xhr();
                xhr.upload.onprogress = function (data) {
                    var perc = Math.round((data.loaded / data.total) * 100);
                    $('.value-progressBar').css('visibility', 'visible');
                    $('.value-progressBar').text(perc + '%');
                    $('.value-progressBar').css('width', perc + '%');
                }
                return xhr;
            }
        });
    }
    else {
        $(".Results-create-file").text("لطفا فرم را کامل کنید");
        $(".Results-create-file").css("color", "red");
    }
});

$('.form-add-banner > input').click(function () {
    $('.Results-create-file').text('');
    $('.Results-create-file').css('color', 'green');
    $('.value-progressBar').css('visibility', 'hidden');
    $('.value-progressBar').text(0);
    $('.value-progressBar').css('width', '1%');
});

function DeleteBanner(NameFile) {
    $.ajax({
        method: "post",
        url: "/PagesManagement/DeleteBanner",
        data: { NameFile: NameFile },
        success: function (data) {
            if (data == true) {
                alert("بنر حذف شد");
            }
            else {
                alert("بنر حذف نشد");
            }
        },
        error: function (data) {
            alert("بنر حذف نشد");
        },
    });
}