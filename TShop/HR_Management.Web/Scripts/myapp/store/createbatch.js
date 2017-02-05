$(function () {
    $("a[data-modal=batch]").on("click", function () {
        $("#batchModalContent").load(this.href, function () {
            $("#batchModal").modal({ keyboard: true }, "show");

            $("#createbatch").submit(function () {
                if ($("#createbatch").valid()) {
                    $.ajax({
                        url: this.action,
                        type: this.method,
                        data: $(this).serialize(),
                        success: function (result) {
                            if (result.success) {
                                $("#batchModal").modal("hide");
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

        })
        return false;
    })
})