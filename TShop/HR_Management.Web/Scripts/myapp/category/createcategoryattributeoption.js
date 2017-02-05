$(function () {
    $("a[data-modal=createattributeoption]").on("click", function () {
        $("#createAttributeOptionModalContent").load(this.href, function () {
            $("#createAttributeOptionModal").modal({ keyboard: true }, "show");

            $("#addProductAttributeOptions").submit(function () {
                if ($("#addProductAttributeOptions").valid()) {
                    $.ajax({
                        url: this.action,
                        type: this.method,
                        data: $(this).serialize(),
                        success: function (result) {
                            if (result.success) {
                                $("#createAttributeOptionModal").modal("hide");
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