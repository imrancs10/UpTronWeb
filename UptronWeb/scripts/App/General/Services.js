$(document).ready(function () {
    fillServicesLeftSideBar();
});

function fillServicesLeftSideBar() {
    $.ajax({
        dataType: 'json',
        type: 'POSt',
        url: '/Home/GetAllServiceSliderList',
        async: true,
        contentType: "application/json; charset = utf-8",
        success: function (data) {
            int = 1;
            var htmlDOM = '';
            $.each(data, function (i, item) {
                htmlDOM += '<div class="media">' +
                    '<a class="media-left" href="blog-single-right-sidebar.html">' +
                    '<img class="media-object" src="' + item.SliderImage + '" alt="' + item.Name + '" clas="img-responsive">' +
                    '</a>' +
                    '<div class="media-body">' +
                    '<h4 class="media-heading"><a href="/Home/Services?Id=' + item.Id + '">' + item.Name + '</a></h4>' +
                    '<h6>Read More...</h6>' +
                    '</div>' +
                    '</div>';
            });
            $('#ServicesLeftDiv').html(htmlDOM);
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

