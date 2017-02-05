$(function () {
    $("a[data-modal=storeProduct]").on("click", function () {
        $("#storeProductModalContent").load(this.href, function () {
            $("#storeProductModal").modal({ keyboard: true }, "show");

            $("#ProductId").autocomplete({
                minLength: 1,
                source: function (request, response) {
                    var url = $(this.element).data("url");
                    $.getJSON(url, { term: request.term }, function (data) {
                        response(data)
                    })
                },
                appendTo: $(".modal-body"),
                select: function (event, ui) {
                    $("#hiddenproductid").val(ui.item.Id);
                    $("#ProductId").val(ui.item.Name);
                    $("#productCode").val(ui.item.Code);
                    $("#autoGeneterateName").val(ui.item.AutoGenerateName);
                    $("#modelNumber").val(ui.item.ModelNumber);
                    $("#CostPricePerUnit").select();
                },
                change: function (event, ui) {
                    if (!ui.item) {
                        $(event.target).val("");
                    }
                }

            })

            $("#enterstoreproduct").submit(function () {
                if ($("#enterstoreproduct").valid()) {
                    $.ajax({
                        url: this.action,
                        type: this.method,
                        data: $(this).serialize(),
                        success: function (result) {
                            if (result.success) {
                                $("#storeProductModal").modal("hide");
                                location.reload();
                            } else {
                                $("#MessageToClient").text("The data was not updated.");
                            }
                        },
                        error: function () {
                            $("#MessageToClient").text("The web server had an error.");
                        }
                    });
                    return false;
                }
            });

        });
        return false;
    });
});