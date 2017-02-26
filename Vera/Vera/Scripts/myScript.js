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

$(function () {
    //countPageInit();
    //blockFormingInit();

    $('#Format').change(function () {
        // получаем выбранный id
        var id = $(this).val();
        console.log(id);    // -
        $.ajax({
            type: 'POST',
            url: "/Home/SetGluePrice/" + id,
            success: function (data) {
                console.log(data);
                $('#price').text(data);// заменяем содержимое присланным частичным представлением
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

                countPageInit();
                blockFormingInit();
            }
        });
    });

});