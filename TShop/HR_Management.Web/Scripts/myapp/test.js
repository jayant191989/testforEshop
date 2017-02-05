$(document).ready(function () {
    $.ajax({
        url: "/Employees/GetEmployee",
        type: "POST",
        dataType: 'json',
        success: function (data) {
            var dataTableInstance = $('#dataTable').DataTable({
                data: data,
                columns: [
                    { 'data': 'FullName' },
                    { 'data': 'Email' },
                    { 'data': 'DepartmentID' },
                    {
                        'data': 'Mobile',
                        'render': function (mobile) {
                            return "91- " + mobile;
                        },
                        'searchable': false,
                        'sortable': false
                    },
                    {
                        'data': 'JoinDate',
                        'render': function (jsonDate) {
                            if (!jsonDate) {
                                return jsonDate;
                            }
                            var date = new Date(parseInt(jsonDate.substr(6)));
                            var month = date.getMonth() + 1;
                            return date.getDate() + '/' + month + '/' + date.getFullYear();
                        }
                    },
                    {
                        'data': 'Options',
                        'render': function (refData) {
                            return '<a href=/Employees/Details/' + refData + '  class="glyphicon glyphicon-zoom-in" >' + '</a>' + ' |' + '<a href=/Employees/Edit/' + refData + '  class="glyphicon glyphicon-pencil" >' + '</a>' + ' |' + '<a href=/Employees/Delete/' + refData + '  class="glyphicon glyphicon-remove" >' + '</a>';
                        }
                    },

                ]
            });

            $('#dataTable tfoot th').each(function () {
                var title = $('#dataTable thead th').eq($(this).index()).text();
                $(this).html('<input type="text" placeholder="Search ' + title + '"/>')
            })

            dataTableInstance.columns().every(function () {
                var dataTableColumn = this;


            })

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //  alert('hi' + XMLHttpRequest.responseText);
            if (XMLHttpRequest.status == 400) {
                $('#MessageToClient').text(XMLHttpRequest.responseText);
            }
            else {
                $('#MessageToClient').text(XMLHttpRequest.responseText);
            }
        }
    });
})

$(document).ready(function () {

    $('.save-record').on('click', function () {
        var UserId = $('#UserId').val();
        var ModuleMode = [];

        $('.tBody tr').each(function () {
            var chkChild = $(this).find('.chkChild');
            //alert(chkChild.is(":checked") + "   " + $(this).find('.chkChild').is(":checked"))
            if (chkChild.is(":checked")) {
                var hidmoduleid = $(this).find('.hidmoduleid').val();

                ModuleMode.push({ UserId: UserId, ModuleId: hidmoduleid });
            }
        });


        $.ajax({
            url: "/UserAccess/SaveUserAccess",
            //data: {
            //    __RequestVerificationToken: token,
            //    UserModuleLst: JSON.stringify(ModuleMode)
            //},
            data: {
                UserModuleLst: JSON.stringify(ModuleMode)
            },
            type: 'POST',
            success: function (data) {
                if (data != null) {
                    if (data.objResponse.IsSuccess == "false") {
                        $("#popupError").html(data.objResponse.StrResponse);
                    }
                    else {
                        $("#popupError").html("");
                        $("#popupError").html(data.objResponse.StrResponse);

                    }
                }
                $(".ajax-loading-block-window").hide();
            },
            error: function (xhr, data) {
                $(".ajax-loading-block-window").hide();
                alert(xhr.responseText)
            }
        });

    });


});



    $(document).ready(function () {
        var bid = $('#dwnBranches').val();
        var did = $('#dwnDepartments').val();
        $.ajax({
            url: "/Attendences/GetEmployees",
            type: "GET",
            data: { branchId: bid, departmentId: did },
            // dataType: 'json',
            success: function (data) {
                $('#renderEmployees').html(data);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //alert('hi' + XMLHttpRequest.responseText);
                if (XMLHttpRequest.status == 400) {
                    $('#MessageToClient').text(XMLHttpRequest.responseText);
                }
                else {
                    $('#MessageToClient').text('The web server had an error.');
                }
            }
        })

        $('#dwnBranches').on('change', function () {
            var bid = $('#dwnBranches').val();
            string branchId = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(bid);
            if (!bid == 0)
                $.ajax({
        url: "/Attendences/GetDepartments",
        type: "POST",
        data: { Id: bid },
        dataType: 'json',
        success: function (data) {
            var items = "<option>---------------------</option>";
            $.each(data.DepartmentList, function (i, item) {
                items += "<option value='" + item.Value + "'>" + item.Text + "</option>";
            });
            //alert(items);
            $('#dwnDepartments').html(items);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert('hi' + XMLHttpRequest.responseText);
            if (XMLHttpRequest.status == 400) {
                $('#MessageToClient').text(XMLHttpRequest.responseText);
            }
            else {
                $('#MessageToClient').text('The web server had an error.');
            }
        }
    })

});

$('#Filter').on('click', function () {
    var bid = $('#dwnBranches').val();
    var did = $('#dwnDepartments').val();
    @*@{ string branchId = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(bid);}*@
    if (!bid == 0)
        $.ajax({
url: "/Attendences/GetEmployees",
type: "GET",
data: { branchId: bid, departmentId: did },
// dataType: 'json',
success: function (data) {
    $('#renderEmployees').html(data);
    // location.reload();
},
error: function (XMLHttpRequest, textStatus, errorThrown) {
    //alert('hi' + XMLHttpRequest.responseText);
    if (XMLHttpRequest.status == 400) {
        $('#MessageToClient').text(XMLHttpRequest.responseText);
    }
    else {
        $('#MessageToClient').text('The web server had an error.');
    }
}
})
});

$("#attendenceForm").submit(function () {
    //if ($("#attendenceForm").valid()) {
    $.ajax({
        url: this.action,
        type: this.method,
        data: $(this).serialize(),
        success: function (result) {
            if (result.success) {
                $("#laborsModal").modal("hide");
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
    //}
});
})
