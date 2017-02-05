$(function () {
    $("a[data-modal=createparticular]").on("click", function () {
        $("#createParticularModalContent").load(this.href, function () {
            $("#createParticularModal").modal({ keyboard: true }, "show");

            $("#createparticular").submit(function () {
                if ($("#createparticular").valid()) {
                    $.ajax({
                        url: this.action,
                        type: this.method,
                        data: $(this).serialize(),
                        success: function (result) {
                            if (result.success) {
                                $("#createParticularModal").modal("hide");
                                showNotification("Value update successfully", "success");
                            } else {
                                $("#MessageToClient").text("The data was not updated.");
                                showNotification("Value can not update successfully", "error");
                            }
                        },
                        error: function () {
                            $("#MessageToClient").text("The web server had an error.");
                            showNotification("Something happan wrong", "error");
                        }
                    });
                    return false;
                }
            });

        })
        return false;
    })
})