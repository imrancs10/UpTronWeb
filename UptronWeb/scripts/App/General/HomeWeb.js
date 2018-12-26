$(document).ready(function () {
    fillNewsAndUpdate();
});

function fillNewsAndUpdate() {
    let marquee = $('#newsUpdateMarquee');
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Home/GetNewsAndUpdateList',
        async: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            int = 1;
            var jsonData = JSON.parse(data);
            var htmlDOM = '';
            $.each(jsonData, function (i, item) {
                htmlDOM += '<p>';
                if (item.IsNew === true) {
                    htmlDOM += '<img src="../img/NewIcon.gif" style="padding-right:5px;" />';
                }
                htmlDOM += '<a href="../Home/ViewNewsFile/' + item.Id + '" target="_blank">' + item.Title;
                htmlDOM += '<img src="../img/readmore.jpg" /></a></p><hr />';
            });
            marquee.html(htmlDOM);
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

