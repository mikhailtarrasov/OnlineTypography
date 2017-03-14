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
                $('#gluePrice').text(data);// заменяем содержимое присланным частичным представлением
            }
        });
    }
}

function countPageInit() {
    $('#countOfPage').on('keyup input', function () {
        setPageCountPrice();
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
            $('#countOfPagePrice').text(data);
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
            $('#blockPrice').text(data);
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
                    $('#fillisterPrice').text(data);
                }
            });
        } else {
            $('#fillisterPrice').text(0);
        }
    } else {
        $('#fillisterPrice').text(0);
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
            $('#trimmingBlockPrice').text(data);
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
                $('#cardboardPrice').text(data);
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
                $('#bindingMaterialPrice').text(data);
                console.log($('#bindingMaterialPrice').val());
            }
        });
    } else {
        $('#bindingMaterialPrice').text("Введены не все данные");
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
    setFillisterPrice();                        // Фальцовка
    setDecorativeStitchingPrice();              // Декоративная строчка true/false
    setTrimmingBlockPrice();                    // Подрезка блока

    $('#Format').change(function () {
        // получаем выбранный id - его значение
        setCardboardPrice();
        setBindingMaterialPrice();

        setGluePrice();
    });

    $('#FormingType').change(function () {
        var id = $(this).val();

        $.ajax({
            type: 'POST',
            url: "/Home/FormingType/" + id,
            success: function (data) {
                $('#formingTypeBlock').replaceWith(data), // заменяем содержимое присланным частичным представлением

                setPageCountPrice();
                setBlockFormingPrice();
                setFillisterPrice();

                countPageInit();
                blockFormingInit();
                $('#fillister').change(function () {
                    setFillisterPrice();
                });
            }
        });
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

    //$('#logoCheckbox').change(function() {
    //    viewMoreForLogo();
    //});
});