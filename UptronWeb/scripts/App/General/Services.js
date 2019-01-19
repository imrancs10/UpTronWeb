$(document).ready(function () {
    fillServiceMenu();
    fillServiceSlider();
    fillLeftServices();
});

function fillServiceMenu() {
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Home/GetServiceMenus',
        async: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $.each(data, function (i, Item) {
                $("#serviceList").append('<li><a href="../Home/Services?Id='+Item.Id+'">' + Item.Name + '</a></li>');
            });
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function fillServiceSlider() {
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Home/GetAllServiceSliderList',
        async: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            int = 1;
            var htmlDOM = '';
            var lengthPartner = data.length;
            var loopCount = 1;
            if (lengthPartner > 4) {
                loopCount = Math.ceil(lengthPartner / 4);
            }
            var index = 0;
            for (var i = 0; i < loopCount; i++) {
                if (i === 0) {
                    htmlDOM += '<div class="item active"><div class="row">';
                }
                else {
                    htmlDOM += '<div class="item"><div class="row">';
                }

                for (var j = 0; j < 4; j++) {
                    if (index < lengthPartner) {
                        //htmlDOM += '<div class="col-sm-2 col-xs-12">' +
                        //    '<div class="partnersLogo clearfix">' +
                        //    '<img src="' + data[index].PartnerImage + '" alt="Image Partner" style="height:110px;width:225px;">' +
                        //    '</div>' +
                        //    '</div>';
                        htmlDOM = + '<div class="col-sm-3 col-xs-12">'+
                                    '<div class="productImage isotopeSelector">'+
                            '<figure>' +
                            '<img src="' + data[index].SliderImage + '" alt="' + data[index].Name + '" class="img-responsive">' +
                                            '<div class="overlay-background">'+
                                                '<div class="inner"></div>'+
                                            '</div>'+
                                            '<div class="overlay">'+
                                                '<a class="fancybox-pop" rel="portfolio-1" href="~/img/gallery/event01.jpg">'+
                                                    '<div class="overlayInfo">'+
                                                        '<h4><i class="fa fa-plus" aria-hidden="true"></i> <br></h4>'+
                                                    '</div>'+
                                                '</a>'+
                                            '</div>'+
                                        '</figure>'+
                                        '<div class="event-details bg-white p-20">'+
                                            '<a href=""><h4 class="text-thm pb-5">"' + data[index].Name + '"</h4></a>'+
                                        '</div>'+
                                    '</div>'+
                                '</div>';
                        index++;
                    }
                }
                htmlDOM += '</div></div>';
            }

            $('#ServiceDiv').html(htmlDOM);
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function fillLeftServices() {
    debugger
    $.ajax({
        dataType: 'json',
        type: 'POSt',
        url: '/Home/GetAllServiceSliderList',
        async: true,
        contentType : "application/json; charset = utf-8"
        success: function (data) {
            int = 1;
            var htmlDOM = '';
            $.each(data, function (i, item) {
                htmlDOM += '<div class="media">'+
                    '<a class="media-left" href="blog-single-right-sidebar.html">' +
                    '<img class="media-object" src="' + item.SliderImage + '" alt="' + item.Name + '" clas="img-responsive">' +
                '</a>'+
                    '<div class="media-body">' +
                    '<h4 class="media-heading"><a href="~/Home/Service_Hospital">' + item.Name + '</a></h4>' +
                    '<h6>Read More...</h6>'+
                '</div>'+
            '</div>';
            });
            $('#ServicesLeftDiv').html(htmlDOM);
        },
        success: function (response) {
            alert(response);
            Error: function(response) {
                alert(response, responseText);
            }
        }
    });
}

