$(function () {
    $("a[data-modal=enroll]").on("click", function () {
        $("#enrollModalContent").load(this.href, function () {
            $("#enrollModal").modal({ keyboard: true }, "show");

            $("#enrollcustomer").submit(function () {
                if ($("#enrollcustomer").valid()) {
                    $.ajax({
                        url: this.action,
                        type: this.method,
                        data: $(this).serialize(),
                        success: function (result) {
                            if (result.success) {
                                $("#partsModal").modal("hide");
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

            $("#FirstName").autocomplete({
                minLength: 1,
                source: function (request, response) {
                    var url = $(this.element).data("url");
                    $.getJSON(url, { term: request.term }, function (data) {
                        response(data)
                    })
                },
                appendTo: $(".modal-body"),
                select: function (event, ui) {
                    $("#LastName").val(ui.item.LastName);
                    $("#Age").val(ui.item.Age);
                    $("#DateOfBirth").val(ui.item.DateOfBirth);
                    $("#FathersName").val(ui.item.FathersName);
                    $("#Email").val(ui.item.Email);
                    $("#Mobile").val(ui.item.Mobile);
                    //$("#DateOfJoining").val(ui.item.DateOfJoining);
                    //calulateExtendedPricePart();
                    $("#Email").select();
                },
                change: function (event, ui) {
                    if (!ui.item) {
                        $(event.target).val("");
                    }
                }

            })
        });
        return false;
    });
});