$(function () {
    $("a[data-modal=deletecategoryattribute]").on("click", function () {
        $("#deleteCategoryModalContent").load(this.href, function () {
            $("#deleteCategoryAttributeModal").modal({ keyboard: true }, "show");

            $("#deletecategoryattribute").submit(function () {
                if ($("#deletecategoryattribute").valid()) {
                    $.ajax({
                        url: this.action,
                        type: this.method,
                        data: $(this).serialize(),
                        success: function (result) {
                            if (result.success) {
                                $("#deleteCategoryAttributeModal").modal("hide");
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