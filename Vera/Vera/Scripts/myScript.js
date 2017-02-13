
$(function () {
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
});

$(function () {
    $('#FormingType').change(function () {
        var id = $(this).val();

        $.ajax({
            type: 'POST',
            url: "/Home/FormingType/" + id,
            success: function (data) {
                $('#formingTypeBlock').replaceWith(data), // заменяем содержимое присланным частичным представлением

                $(function () {
                    var tetrCountStr = 1;
                    var formingType = $('#FormingType').val();
                    console.log("tetr count = ", tetrCountStr, "\nforming type = ", formingType);
                    $.ajax({
                        type: 'POST',
                        url: "/Home/SetSewingPrice/" + tetrCountStr + formingType,
                        success: function (data) {
                            console.log("data: ", data);
                            //$('#blockPrice').empty();
                            $('#blockPrice').text(data);
                        }
                    });
                });
            }
        });

        
    });
});

$(function setPageCountPrice() {
    var countOfPage = $('#countOfPage').val();
    console.log(countOfPage);
    $.ajax({
        type: 'POST',
        url: "/Home/SetPageCountPrice/" + countOfPage,
        success: function (data) {
            console.log("data: ");
            console.log(data);
            $('#countOfPagePrice').empty();
            $('#countOfPagePrice').text(data);
        }
    });
});

$(function () {
    $('#tetr').on('keyup input', function () {
        var tetrCountStr = $(this).val();
        var formingType = $('#FormingType').val();
        //var tetrCountInt = parseInt(tetrCountStr);
        //console.log(tetrCountInt);
        $.ajax({
            type: 'POST',
            url: "/Home/SetSewingPrice/" + tetrCountStr + formingType,
            success: function (data) {
                console.log("data: ");
                console.log(data);
                $('#blockPrice').empty();
                $('#blockPrice').text(data);
            }
        });
    });
});

$(function () {
    $('#countOfPage').on('keyup input', function () {
        var countOfPage = $(this).val();
        console.log(countOfPage);
        $.ajax({
            type: 'POST',
            url: "/Home/SetPageCountPrice/" + countOfPage,
            success: function (data) {
                console.log("data: ");
                console.log(data);
                $('#countOfPagePrice').empty();
                $('#countOfPagePrice').text(data);
            }
        });
    });
});

//$(function () {
//    $('#tetr').keyup(function () {
//        var tetrCount = $('#tetr').val();
//        var id = $('#Format').val();
//        console.log(tetrCount);
//        console.log(id);
//        $.ajax({
//            type: 'POST',
//            url: '@Url.Action("SetSewingPrice")/' + id + '/' + tetrCount,
//            success: function (data) {
//                console.log(data);
//                $('#blockPrice').empty();
//                $('#blockPrice').text(data);
//            }
//        });
//    });
//});