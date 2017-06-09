function Paper() {
    
}

$(function () {
    $('#paperPartial').css("display", "none");

    $('#materialTypeId').change(function() {
        if ($('#materialTypeId').val() == 3) {
            $('#paperPartial').css("display", "block");
            $('#formatId').val("5");
            $('#sheetsPerPackage').val("500");
        } else {
            $('#paperPartial').css("display", "none");
            $('#formatId').val("");
            $('#sheetsPerPackage').val("");
        }
    });
});