function GetPrintingPrice() {
    var formatId = $('#FormatId').val();
    var colorfulnessId = $('#ColorfulnessId').val();
    if (formatId != "" && colorfulnessId != "") {
        $.ajax({
            type: 'POST',
            url: "/Printing/GetPrintingPrice?formatId=" +
                formatId +
                "&colorfulnessId=" +
                colorfulnessId,
            success: function (data) {
                var printingPrice = new Decimal(data).toFixed(2);
                if (printingPrice != 0) {
                    printingPrice = printingPrice.replace('.', ',');
                    $('#printingPrice').val(printingPrice);
                }
            }
        });
    }
    
}

$(function () {
    $('#FormatId').change(function () {
        GetPrintingPrice();
    });
    $('#ColorfulnessId').change(function () {
        GetPrintingPrice();
    });
})