$(function () {
    $("a[data-modal=deletebatch]").on("click", function () {
        $("#deletebatchModalContent").load(this.href, function () {
            $("#deletebatchModal").modal({ keyboard: true }, "show");

            $("#deletebatchform").submit(function () {
                if ($("#deletebatchform").valid()) {
                    $.ajax({
                        url: this.action,
                        type: this.method,
                        data: $(this).serialize(),
                        success: function (result) {
                            if (result.success) {
                                $("#deletebatchModal").modal("hide");
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