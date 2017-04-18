function setGluePrice() {
    var id = $('#Format').val();
    console.log(id);    // -
    if (id == "") {
        $('#gluePrice').text("Выберите формат");
    } else {
        $.ajax({
            type: 'POST',
            url: "/Home/SetGluePrice/" + id,
            success: function (data) {
                console.log(data);
                $('#gluePrice').text(new Decimal(data));// заменяем содержимое присланным частичным представлением
            }
        });
    }
}

function countPageInit() {
    $('#countOfPage').on('keyup input', function () {
        setPageCountPrice();
        setPrintingBlockPrice();
    });
};

function setPageCountPrice() {
    var countOfPage = $('#countOfPage').val();
    console.log(countOfPage);
    $.ajax({
        type: 'POST',
        url: "/Home/SetPageCountPrice/" + countOfPage,
        success: function (data) {
            console.log("data: ", data);
            $('#countOfPagePrice').text(new Decimal(data));
        }
    });
}

function blockFormingInit() {
    $('#tetr').on('keyup input', function () {
        setBlockFormingPrice();
        setFillisterPrice();
    });
};

function setBlockFormingPrice() {
    var tetrCountStr = $('#tetr').val();

    var formingType = $('#FormingType').val();
    console.log("tetr count = ", tetrCountStr, "\nforming type = ", formingType);
    $.ajax({
        type: 'POST',
        url: "/Home/SetSewingPrice/" + tetrCountStr + formingType,
        success: function (data) {
            console.log("data: ", data);
            $('#blockPrice').text(new Decimal(data));
        }
    });
};

function setFillisterPrice() {
    $('#fillisterLi').css("display", "none");
    if ($('#FormingType').val() == 2) {
        $('#fillisterLi').css("display", "block");

        var tetrCount = $('#tetr').val();
        if ($('#fillister').is(':checked') == true) {
            $.ajax({
                type: 'POST',
                url: "/Home/SetFillisterPrice/" + tetrCount,
                success: function (data) {
                    console.log(data);
                    $('#fillisterPrice').text(new Decimal(data));
                }
            });
        } else {
            $('#fillisterPrice').text(new Decimal(0));
        }
    } else {
        $('#fillisterPrice').text(new Decimal(0));
    }
}

function setDecorativeStitchingPrice() {
    var text = $('#decorativeStitchingCheckbox').is(':checked');
    $('#decorativeStitchingPrice').text(text);
}

function setTrimmingBlockPrice() {
    $.ajax({                                    // Подрезка блока
        type: 'POST',
        url: "/Home/SetTrimmingBlockPrice/",
        success: function (data) {
            console.log(data);
            $('#trimmingBlockPrice').text(new Decimal(data));
        }
    });
}

function setCardboardPrice() {
    var id = $('#Cardboard').val();
    var formatValue = $('#Format').val();

    if (id != "" && formatValue != "") {
        var id = parseInt(id, 10);
        var formatValue = parseInt(formatValue, 10);

        id = id * 100 + formatValue;
        console.log(id);    // -
        $.ajax({
            type: 'POST',
            url: "/Home/SetMaterialPrice/" + id,
            success: function (data) {
                console.log(data);
                $('#cardboardPrice').text(new Decimal(data));
                console.log($('#cardboardPrice').val());
            }
        });
    } else {
        $('#cardboardPrice').text("Введены не все данные");
    }
}

function setBindingMaterialPrice() {
    var id = $('#BindingMaterials').val();
    var formatValue = $('#Format').val();

    if (id != "" && formatValue != "") {
        var id = parseInt(id, 10);
        var formatValue = parseInt(formatValue, 10);

        id = id * 100 + formatValue;
        console.log(id); // -
        $.ajax({
            type: 'POST',
            url: "/Home/SetMaterialPrice/" + id,
            success: function(data) {
                console.log(data);
                $('#bindingMaterialPrice').text(new Decimal(data));
                console.log($('#bindingMaterialPrice').val());
            }
        });
    } else {
        $('#bindingMaterialPrice').text("Введены не все данные");
    }
}

function setPrintingBlockPrice() {
    var idFormat = $('#Format').val();
    if (idFormat == "") {
        $('#printBlockPrice').text(new Decimal(0));
        $('#printingAlert').css("display", "none");
    } else {
        if (idFormat == 1 || idFormat == 2) {
            $('#printBlockPrice').text(new Decimal(0));
            $('#printingAlert').css("display", "block");
            $('#printingAlert').text("Для данного формата печать не производится");
        } else {
            $('#printingAlert').css("display", "none");
            var idColorfulness = $('#Colorfulness').val();
            var idFormingType = $('#FormingType').val();
            var countOfPage = $('#countOfPage').val();
            var paperId = $('#Paper').val();
            $.ajax({
                type: 'POST',
                url: "/Home/SetPrintingBlockPrice?idColorfulness=" + idColorfulness + "&idFormat=" + idFormat
                + "&idFormingType=" + idFormingType + "&countOfPage=" + countOfPage + "&paperId=" + paperId,
                success: function (data) {
                    $('#printBlockPrice').text(new Decimal(data));
                }
            });
        }
    }
}

function calculatePrice() {
    
    var decorativeStitchingPrice = $('#decorativeStitchingPrice').text();       /* true/false */

    try {
        var gluePrice               = new Decimal($('#gluePrice').text());
        var countOfPagePrice        = new Decimal($('#countOfPagePrice').text());
        var fillisterPrice          = new Decimal($('#fillisterPrice').text());
        var blockPrice              = new Decimal($('#blockPrice').text());
        var trimmingBlockPrice      = new Decimal($('#trimmingBlockPrice').text());
        var cardboardPrice          = new Decimal($('#cardboardPrice').text());
        var bindingMaterialPrice    = new Decimal($('#bindingMaterialPrice').text());
        var printBlockPrice         = new Decimal($('#printBlockPrice').text());

        var result = gluePrice.plus(countOfPagePrice)
            .plus(fillisterPrice)
            .plus(blockPrice)
            .plus(trimmingBlockPrice)
            .plus(cardboardPrice)
            .plus(bindingMaterialPrice)
            .plus(printBlockPrice);

        $('#totalPrice').text(result);
    } catch (e) {
        alert('Введены не все данные! Проверьте формы ввода!');
    } 
    


}

//function viewMoreForLogo() {
//    switch ($('#logoCheckbox').is(':checked')) {
//        case true:
//            $.ajax({
//                type: 'POST',
//                url: "/Home/SetGluePrice/" + id,
//                success: function (data) {
//                    console.log(data);
//                    $('#gluePrice').text(data);// заменяем содержимое присланным частичным представлением
//                    console.log($('#price').val());
//                }
//            });
//            break;
//        case false:
//            break;
//        default:
//            break;
//    }
//}

$(function() {
    //countPageInit();
    //blockFormingInit();
    $('.partOfThePrice').each(function() {      // Обнуляем все частичные стоимости
        $(this).text(new Decimal(0));
    });

    setFillisterPrice();                        // Фальцовка
    setDecorativeStitchingPrice();              // Декоративная строчка true/false
    setTrimmingBlockPrice();                    // Подрезка блока

    $('#Format').change(function () {
        // получаем выбранный id - его значение
        setCardboardPrice();
        setBindingMaterialPrice();
        setPrintingBlockPrice();

        setGluePrice();
    });

    $('#FormingType').change(function () {
        var id = $(this).val();

        $("#printBlockPrice").text(new Decimal(0));

        if (id == "") {
            $('#formingTypeBlock').text(id); // Убираем блок, если не выбран тип формировки

            $('#countOfPagePrice').text(new Decimal(0));    // 
            $('#blockPrice').text(new Decimal(0));          // Обнуляем все данные, которые могли остаться с расчёта с выбранным блоком
            $('#fillisterPrice').text(new Decimal(0));      // Убираем Фальцовку из списка цен
            $('#fillisterLi').css("display", "none");       // 
        } else {
            $.ajax({
                type: 'POST',
                url: "/Home/FormingType/" + id,
                success: function (data) {
                    $('#formingTypeBlock').replaceWith(data),   // заменяем содержимое присланным частичным представлением

                    setPageCountPrice();
                    setBlockFormingPrice();
                    setFillisterPrice();

                    countPageInit();
                    blockFormingInit();

                    setPrintingBlockPrice();

                    $('#fillister').change(function () {
                        setFillisterPrice();
                    });

                    $('#Colorfulness').change(function () {
                        setPrintingBlockPrice();
                    });

                    $('#Paper').change(function() {
                        setPrintingBlockPrice();
                    });
                }
            });
        }
    });

    $('#Cardboard').change(function () {
        setCardboardPrice();
    });

    $('#BindingMaterials').change(function () {
        setBindingMaterialPrice();
    });

    $('#decorativeStitchingCheckbox').change(function () {
        console.log($('#decorativeStitchingCheckbox').is(':checked'));
        setDecorativeStitchingPrice();
    });

    $('#calculateButton').click(function () {
        calculatePrice();
    });

    //$('#logoCheckbox').change(function() {
    //    viewMoreForLogo();
    //});
});