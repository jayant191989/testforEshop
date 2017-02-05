$(function () {
    $("a[data-modal=editbatch]").on("click", function () {
        $("#editbatchModalContent").load(this.href, function () {
            $("#editbatchModal").modal({ keyboard: true }, "show");

            $("#editbatchform").submit(function () {
                if ($("#editbatchform").valid()) {
                    $.ajax({
                        url: this.action,
                        type: this.method,
                        data: $(this).serialize(),
                        success: function (result) {
                            if (result.success) {
                                $("#editbatchModal").modal("hide");
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