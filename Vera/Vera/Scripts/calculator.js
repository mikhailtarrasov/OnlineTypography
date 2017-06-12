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
        setPrintingBlockPrice();
        setJobsPricesPerPage(pageCount);
    });
};

function blockFormingInit() {
    $('#tetr').on('keyup input', function () {
        var tetrCount = $('#tetr').val();
        setBlockFormingPrice();
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

//--------------------------------------------------------------------------------------
function calculatePrice() {
     try {
        var oneProductPrice = new Decimal(0);
        $('.partOfThePrice').each(function () {
            var a = $(this).text();
            oneProductPrice = oneProductPrice.plus(a);
        });

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
}//-------------------------------------------------------------------------------------

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
        } else {
            $(priceElement).text(new Decimal(0));
        }
    });
}

//--------------------------------------------------------------------------------------
$(function() {
    $('.partOfThePrice').each(function() {      // Обнуляем все частичные стоимости
        $(this).text(new Decimal(0));
    });

    printPriceBlocksForJobs();

    $('#Format').change(function () {
        setCardboardPrice();
        setBindingMaterialPrice();
        setPrintingBlockPrice();

        setGluePrice();
    });

    $('#FormingType').change(function () {
        var id = $(this).val();
        $("#printBlockPrice").text(new Decimal(0));
        
        if (id == "") {
            $('#formingTypeBlock').text(id);                // Убираем блок, если не выбран тип формировки

            //$('#countOfPagePrice').text(new Decimal(0));    //ПРИ СМЕНЕ ТИПА ФОРМИРОВКИ МОЖНО ОБНУЛИТЬ ВСЁ, ЧТО ЗАВИСИТ ОТ ЛИСТОВ
            $('#blockPrice').text(new Decimal(0));          // Обнуляем все данные, которые могли остаться с расчёта с выбранным блоком
        } else {
            $.ajax({
                type: 'POST',
                url: "/Home/_FormingTypePartial/" + id,
                success: function (data) {
                    $('#formingTypeBlock').replaceWith(data),   // заменяем содержимое присланным частичным представлением

                    setBlockFormingPrice();

                    countPageInit();
                    blockFormingInit();

                    setPrintingBlockPrice();

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

    $('#calculateButton').click(function () {
        calculatePrice();
    });
});