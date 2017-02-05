$(function () {
    $("a[data-modal=fees]").on("click", function () {
        $("#feesModalContent").load(this.href, function () {
            $("#feesModal").modal({ keyboard: true }, "show");

            $("#customerfees").submit(function () {
                if ($("#customerfees").valid()) {
                    $.ajax({
                        url: this.action,
                        type: this.method,
                        data: $(this).serialize(),
                        success: function (result) {
                            if (result.success) {
                                $("#feesModal").modal("hide");
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