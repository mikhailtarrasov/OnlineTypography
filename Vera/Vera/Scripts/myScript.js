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
    var text = $('#decorativeStitching').is(':checked');
    $('#decorativeStitchingPrice').text(text);
}

$(function() {
    //countPageInit();
    //blockFormingInit();
    setFillisterPrice();
    setDecorativeStitchingPrice();

    $('#Format').change(function () {
        // получаем выбранный id - его значение
        var id = $(this).val();
        console.log(id);    // -
        $.ajax({
            type: 'POST',
            url: "/Home/SetGluePrice/" + id,
            success: function (data) {
                console.log(data);
                $('#gluePrice').text(data);// заменяем содержимое присланным частичным представлением
                console.log($('#price').val());
            }
        });
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

    $('#decorativeStitching').change(function () {
        console.log($('#decorativeStitching').is(':checked'));
        setDecorativeStitchingPrice();
    });

});