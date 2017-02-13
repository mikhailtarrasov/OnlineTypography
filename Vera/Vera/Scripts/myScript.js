
//$(function () {
//    $('#Format').change(function () {
//        // получаем выбранный id
//        var id = $(this).val();
//        console.log(id);    // -

//        console.log(b);
//        $.ajax({
//            type: 'POST',
//            url: 'SetGluePrice/' + id,
//            success: function (data) {
//                $('#price').text(data);// заменяем содержимое присланным частичным представлением
//            }
//        });
//    });
//});

//$(function() {
//    $('#FormingType').change(function() {
//        var id = $(this).val();
        
//        $.ajax({
//            type: 'POST',
//            url: '@Url.Action("FormingType")/' + id,
//            success: function(data) {
//                $('#formingTypeBlock').replaceWith(data); // заменяем содержимое присланным частичным представлением
//            }
//        });
//    });
//});

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