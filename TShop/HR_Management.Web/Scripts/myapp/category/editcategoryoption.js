$(function () {
    $("a[data-modal=editattributeoption]").on("click", function () {
        $("#editAttributeOptionModalContent").load(this.href, function () {
            $("#editAttributeOptionModal").modal({ keyboard: true }, "show");

            $("#editProductAttributeOption").submit(function () {
                if ($("#editProductAttributeOption").valid()) {
                    $.ajax({
                        url: this.action,
                        type: this.method,
                        data: $(this).serialize(),
                        success: function (result) {
                            if (result.success) {
                                $("#editAttributeOptionModal").modal("hide");
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