$(function () {
    $("a[data-modal=editStoreProduct]").on("click", function () {
        $("#editStoreProductModalContent").load(this.href, function () {
            $("#editStoreProductModal").modal({ keyboard: true }, "show");
            alert("hello");

            $("#ProductName").autocomplete({
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

        });
        return false;
    });
});