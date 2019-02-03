$(document).ready(function () {
    fillServiceMenu();
});

function fillServiceMenu() {
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Home/GetServiceMenus',
        async: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //var liList = $("#serviceList").find('li');
            //if (liList.length > 0) {
            $.each(data, function (i, Item) {
                $("#serviceList").append('<li><a href="../Home/Services?Id=' + Item.Id + '">' + Item.Name + '</a></li>');
            });
            //}
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

