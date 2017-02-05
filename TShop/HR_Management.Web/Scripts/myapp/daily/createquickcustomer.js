$(function () {
    $("a[data-modal=createcustomer]").on("click", function () {
        $("#createcustomerModalContent").load(this.href, function () {
            $("#createcustomerModal").modal({ keyboard: true }, "show");

            $("#createcustomer").submit(function () {
                if ($("#createcustomer").valid()) {
                    $.ajax({
                        url: this.action,
                        type: this.method,
                        data: $(this).serialize(),
                        success: function (result) {
                            if (result.success) {                                
                                $("#createcustomerModal").modal("hide");
                                //location.reload();
                                showNotification("Value update successfully", "success");
                            } else {
                                showNotification("Value can not update successfully", "error");
                                $("#MessageToClient").text("The data was not updated.");
                            }
                        },
                        error: function () {
                            showNotification("Something happan wrong", "error");
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