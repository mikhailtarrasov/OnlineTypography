function setGluePrice() {
    var id = $('#Format').val();
    console.log(id);    
    if (id == "") {
        $('#gluePrice').text("Выберите формат");
    } else {
        $.ajax({
            type: 'POST',
            url: "/Home/SetGluePrice/" + id,
            success: function (data) {
                console.log(data);
                $('#gluePrice').text(new Decimal(data));
            }
        });
    }
}

function countPageInit() {
    $('#countOfPage').on('keyup input', function () {
        var pageCount = $('#countOfPage').val();
        //setPageCountPrice();
        setPrintingBlockPrice();
        setJobsPricesPerPage(pageCount);
    });
};

//function setPageCountPrice() {
//    var countOfPage = $('#countOfPage').val();
//    console.log(countOfPage);
//    $.ajax({
//        type: 'POST',
//        url: "/Home/SetPageCountPrice/" + countOfPage,
//        success: function (data) {
//            console.log("data: ", data);
//            $('#countOfPagePrice').text(new Decimal(data));
//        }
//    });
//}

function blockFormingInit() {
    $('#tetr').on('keyup input', function () {
        var tetrCount = $('#tetr').val();
        setBlockFormingPrice();
        //setFillisterPrice();
        setJobsPricesPerTetr(tetrCount);
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

//function setFillisterPrice() {
//    $('#fillisterLi').css("display", "none");
//    if ($('#FormingType').val() == 2) {
//        $('#fillisterLi').css("display", "block");

//        var tetrCount = $('#tetr').val();
//        if ($('#fillister').is(':checked') == true) {
//            $.ajax({
//                type: 'POST',
//                url: "/Home/SetFillisterPrice/" + tetrCount,
//                success: function (data) {
//                    console.log(data);
//                    $('#fillisterPrice').text(new Decimal(data));
//                }
//            });
//        } else {
//            $('#fillisterPrice').text(new Decimal(0));
//        }
//    } else {
//        $('#fillisterPrice').text(new Decimal(0));
//    }
//}

//function setDecorativeStitchingPrice() {
//    var text = $('#decorativeStitchingCheckbox').is(':checked');
//    $('#decorativeStitchingPrice').text(text);
//}

//function setTrimmingBlockPrice() {
//    var idFormingType = $('#FormingType').val();
//    if (idFormingType == "") {
//        $('#trimmingBlockPrice').text(new Decimal(0));
//    } else {
//        $.ajax({                                    // Подрезка блока
//            type: 'POST',
//            url: "/Home/SetTrimmingBlockPrice/",
//            success: function (data) {
//                console.log(data);
//                $('#trimmingBlockPrice').text(new Decimal(data));
//            }
//        });
//    }   
//}

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
        //$('#cardboardPrice').text("Введены не все данные");
        $('#cardboardPrice').text(new Decimal(0));
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
        //$('#bindingMaterialPrice').text("Введены не все данные");
        $('#bindingMaterialPrice').text(new Decimal(0));
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

//function setAdditionalCost() {
//    $.ajax({
//        type: 'POST',
//        url: "/Home/GetAdditionalCost",
//        success: function (data) {
//            $('#additionalCost').text(new Decimal(data));
//        }
//    });
//}

function calculatePrice() {
    
    //var decorativeStitchingPrice = $('#decorativeStitchingPrice').text();       /* true/false */

    try {
        var oneProductPrice = new Decimal(0);
        $('.partOfThePrice').each(function () {
            var a = $(this).text();
            oneProductPrice = oneProductPrice.plus(a);
        });
        //var gluePrice               = new Decimal($('#gluePrice').text());
        ////var countOfPagePrice        = new Decimal($('#countOfPagePrice').text());
        ////var fillisterPrice          = new Decimal($('#fillisterPrice').text());
        //var blockPrice              = new Decimal($('#blockPrice').text());
        ////var trimmingBlockPrice      = new Decimal($('#trimmingBlockPrice').text());
        //var cardboardPrice          = new Decimal($('#cardboardPrice').text());
        //var bindingMaterialPrice    = new Decimal($('#bindingMaterialPrice').text());
        //var printBlockPrice         = new Decimal($('#printBlockPrice').text());
        ////var additionalCost          = new Decimal($('#additionalCost').text());

        //var oneProductPrice = (gluePrice.plus(blockPrice)
        //    .plus(cardboardPrice)
        //    .plus(bindingMaterialPrice)
        //    .plus(printBlockPrice)).toFixed(2);

        var editionCirculation = new Decimal($('#editionCirculation').val());               // тираж (кол-во изделий)
        var coefficientOfСomplexity = new Decimal($('#coefficientOfСomplexity').val());     // коэффициент сложности

        var totalPrice = coefficientOfСomplexity * oneProductPrice * editionCirculation;
        oneProductPrice = oneProductPrice * coefficientOfСomplexity;

        $('#oneProductPrice').text(oneProductPrice.toFixed(2));
        $('#editionCirculationTable').text(editionCirculation);
        $('#totalPrice').text(totalPrice.toFixed(2));
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

function printPriceBlocksForJobs() {
    $('.jobCheckbox').change(function () {
        var idCheckbox = this.id;
        var priceElement = '#' + idCheckbox + '.partOfThePrice';
        if ($('#' + idCheckbox).is(':checked') == true) {
            var cost = this.value.replace(/,/, '.');
            switch (this.name) {
                case 'Лист':
                    cost = getCostForJobPerPage(cost);
                    break;
                case 'Тетрадь':
                    cost = getCostForJobPerTetr(cost);
                    break;
                case 'Изделие':
                    break;
                default:
                    cost = 0;
                    break;
            }
            $(priceElement).text(cost);
            //.log($('#' + idCheckbox).is(':checked'));
        } else {
            $(priceElement).text(new Decimal(0));
        }
    });
}

function getCostForJobPerTetr(cost) {
    var tetrCount = $('#tetr').val();
    if (tetrCount !== undefined)
        cost *= tetrCount;
    else cost = 0;
    return cost;
}

function getCostForJobPerPage(cost) {
    var countOfPage = $('#countOfPage').val();
    if (countOfPage !== undefined)
        cost *= countOfPage;
    else cost = 0;
    return cost;
}

function setJobsPricesPerPage(pageCount) {
    $('.jobCheckbox').each(function() {
        if (this.name === 'Лист') {
            var idCheckbox = this.id;
            var priceElement = '#' + idCheckbox + '.partOfThePrice';
            var cost = this.value;
            if ($('#' + idCheckbox).is(':checked') === true) {
                cost = this.value.replace(/,/, '.');
                cost = getCostForJobPerPage(cost);
            } else cost = 0;
            $(priceElement).text(cost);
        }
    });
};

function setJobsPricesPerTetr(tetrCount) {
    $('.jobCheckbox').each(function () {
        var idCheckbox = this.id;
        var priceElement = '#' + idCheckbox + '.partOfThePrice';
        var cost = this.value;

        if (this.name === 'Тетрадь') {
            if ($('#' + idCheckbox).is(':checked') === true) {
                cost = this.value.replace(/,/, '.');
                cost = getCostForJobPerTetr(cost);
            } else cost = 0;
            $(priceElement).text(cost);
        } 
    });
};

function jobPricesInit() {
    var idFormingType = $('#FormingType').val();
    $('.jobCheckbox').each(function () {
        var idCheckbox = this.id;
        var priceElement = '#' + idCheckbox + '.partOfThePrice';
        if ($('#' + idCheckbox).is(':checked') == true) {
            var cost = this.value.replace(/,/, '.');
            switch (this.name) {
                case 'Лист':
                    cost = getCostForJobPerPage(cost);
                    break;
                case 'Тетрадь':
                    cost = getCostForJobPerTetr(cost);
                    break;
                case 'Изделие':
                    break;
                default:
                    cost = 0;
                    break;
            }
            $(priceElement).text(cost);
            //.log($('#' + idCheckbox).is(':checked'));
        } else {
            $(priceElement).text(new Decimal(0));
        }


        //var idCheckbox = this.id;
        //var priceElement = '#' + idCheckbox + '.partOfThePrice';
        //var cost = this.value;

        //if (this.name === 'Тетрадь' && idFormingType === 2) {
        //    $(priceElement).css("display", "block");
        //} else if (this.name === 'Тетрадь' && idFormingType === 1) {
        //    cost = 0;
        //    $(priceElement).css("display", "none");
        //}
        //$(priceElement).text(cost);
    });
}

$(function() {
    //countPageInit();
    //blockFormingInit();

    //var elems = $(".en");
    //var elemsTotal = elems.length;
    //for (var i = 0; i < elemsTotal; ++i) { $(elems[i]).attr('id', i) }

    $('.partOfThePrice').each(function() {      // Обнуляем все частичные стоимости
        $(this).text(new Decimal(0));
    });

    printPriceBlocksForJobs();

    //setFillisterPrice();                        // Фальцовка
    //setDecorativeStitchingPrice();              // Декоративная строчка true/false
    //setTrimmingBlockPrice();                    // Подрезка блока
    //setAdditionalCost();                        // Дополнительная стоимость

    $('#Format').change(function () {
        setCardboardPrice();
        setBindingMaterialPrice();
        setPrintingBlockPrice();

        setGluePrice();
    });

    $('#FormingType').change(function () {
        var id = $(this).val();
        $("#printBlockPrice").text(new Decimal(0));
        //setTrimmingBlockPrice();

        if (id == "") {
            $('#formingTypeBlock').text(id);                // Убираем блок, если не выбран тип формировки

            //$('#countOfPagePrice').text(new Decimal(0));    //ПРИ СМЕНЕ ТИПА ФОРМИРОВКИ МОЖНО ОБНУЛИТЬ ВСЁ, ЧТО ЗАВИСИТ ОТ ЛИСТОВ
            $('#blockPrice').text(new Decimal(0));          // Обнуляем все данные, которые могли остаться с расчёта с выбранным блоком
            //$('#fillisterPrice').text(new Decimal(0));      // Убираем Фальцовку из списка цен
            //$('#fillisterLi').css("display", "none");       // 
        } else {
            $.ajax({
                type: 'POST',
                url: "/Home/_FormingTypePartial/" + id,
                success: function (data) {
                    $('#formingTypeBlock').replaceWith(data),   // заменяем содержимое присланным частичным представлением

                    //setPageCountPrice();
                    setBlockFormingPrice();
                    //setFillisterPrice();

                    countPageInit();
                    blockFormingInit();

                    setPrintingBlockPrice();

                    //$('#fillister').change(function () {
                    //    setFillisterPrice();
                    //});

                    $('#Colorfulness').change(function () {
                        setPrintingBlockPrice();
                    });

                    $('#Paper').change(function() {
                        setPrintingBlockPrice();
                    });
                }
            });
        }
        jobPricesInit();
    });

    $('#Cardboard').change(function () {
        setCardboardPrice();
    });

    $('#BindingMaterials').change(function () {
        setBindingMaterialPrice();
    });

    //$('#decorativeStitchingCheckbox').change(function () {
    //    console.log($('#decorativeStitchingCheckbox').is(':checked'));
    //    setDecorativeStitchingPrice();
    //});

    $('#calculateButton').click(function () {
        calculatePrice();
    });

    //$('#logoCheckbox').change(function() {
    //    viewMoreForLogo();
    //});
});