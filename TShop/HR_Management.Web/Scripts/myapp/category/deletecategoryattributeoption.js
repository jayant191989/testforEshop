$(function () {
    $("a[data-modal=deleteattributeoption]").on("click", function () {
        $("#deleteAttributeOptionModalContent").load(this.href, function () {
            $("#deleteAttributeOptionModal").modal({ keyboard: true }, "show");

            $("#deleteProductAttributeOptionForm").submit(function () {
                if ($("#deleteProductAttributeOptionForm").valid()) {
                    $.ajax({
                        url: this.action,
                        type: this.method,
                        data: $(this).serialize(),
                        success: function (result) {
                            if (result.success) {
                                $("#deleteAttributeOptionModal").modal("hide");
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