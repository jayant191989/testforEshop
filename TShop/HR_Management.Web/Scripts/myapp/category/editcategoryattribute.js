$(function () {
    $("a[data-modal=editcategoryattribute]").on("click", function () {
        $("#editCategoryModalContent").load(this.href, function () {
            $("#editCategoryAttributeModal").modal({ keyboard: true }, "show");

            $("#editProductAttributeOption").submit(function () {
                if ($("#editProductAttributeOption").valid()) {
                    $.ajax({
                        url: this.action,
                        type: this.method,
                        data: $(this).serialize(),
                        success: function (result) {
                            if (result.success) {
                                $("#editCategoryAttributeModal").modal("hide");
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