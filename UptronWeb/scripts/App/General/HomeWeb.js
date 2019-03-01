$(document).ready(function () {
    //fillSliderList();
    fillNewsAndUpdate();
    fillDirectorMessage();
    fillUpcomingEvents();
    fillPartner();
    fillFunctionaries();
    //fillNewsUpdateList();
    //fillServiceSlider();
    //fillFooterImage();
    fillDirectorMessagePage();
    fillWhyUptron();
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

function fillUpcomingEvents() {
    let marquee = $('#UpcomingEventsMarquee');
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Home/GetUpcomingEventsList',
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
                htmlDOM += '<a href="../Home/ViewUpcomingEventsFile/' + item.Id + '" target="_blank">' + item.Title;
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

function fillDirectorMessage() {
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Home/GetDirectorMessage',
        async: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            int = 1;
            var htmlDOM = '';
            htmlDOM += '<div class="col-xs-3">' +
                '<div class="reviewImage">' +
                '<img src="' + data.Photo + '" alt="' + data.Name + '" class="img-responsive" style="width:130px;height:130px;" />' +
                '</div>' +
                '</div>' +
                '<div class="col-xs-9">' +
                '<div class="reviewInfo" style="height:230px;">' +
                '<i class="fa fa-quote-left" aria-hidden="true"></i>' +
                '<p>' + data.Message.substr(0, 100) + '</p>' +
                '<h3>' + data.Name + '</h3>' +
                '<h4>' + data.Designation + '</h4>' +
                '<a href="/Home/FullDirector_Message" style="color:#337ab7">Read More <i class="fa fa-arrow-circle-right" aria-hidden="true"></i></a>' +
                '</div>' +
                '</div>';
            $('#divDirectorMessage').html(htmlDOM);
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function fillPartner() {
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Home/GetPartnerList',
        async: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            int = 1;
            var htmlDOM = '';
            var lengthPartner = data.length;
            var loopCount = 1;
            if (lengthPartner > 6) {
                loopCount = Math.ceil(lengthPartner / 6);
            }
            var index = 0;
            for (var i = 0; i < loopCount; i++) {
                if (i === 0) {
                    htmlDOM += '<div class="item active"><div class="row">';
                }
                else {
                    htmlDOM += '<div class="item"><div class="row">';
                }

                for (var j = 0; j < 6; j++) {
                    if (index < lengthPartner) {
                        htmlDOM += '<div class="col-sm-2 col-xs-12">' +
                            '<div class="partnersLogo clearfix">' +
                            '<img src="' + data[index].PartnerImage + '" alt="Image Partner" style="height:110px;width:225px;">' +
                            '</div>' +
                            '</div>';
                        index++;
                    }
                }
                htmlDOM += '</div></div>';
            }

            $('#PartnerDiv').html(htmlDOM);
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function fillFunctionaries() {
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Home/GetKeyFunctionariesList',
        async: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            int = 1;
            var htmlDOM = '';
            $.each(data, function (i, item) {
                htmlDOM += '<div class="row">' +
                    '<div class="col-md-12 col-sm-12 col-xs-12  br-bottom" >' +
                    '<div class="col-md-5 col-sm-5">' +
                    '<span class="photo">' +
                    '<img src="' + item.Image + '" alt="' + item.Name + '" class="img-responsive" style="height:130px;"/>' +
                    '</span>' +
                    '</div>' +
                    '<div class="col-md-7 col-sm-7">' +
                    '<div class="key_title">' +
                    '<h3> ' + item.Name + '</h3>' +
                    '<p>' + item.Designation + ',<br /> ' + item.Location + '</p>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div >';

            });
            $('#keyfunctionariesDiv').html(htmlDOM);
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function fillSliderList() {
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Home/GetAllSliderDetailList',
        async: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            int = 1;
            var htmlDOM = '';
            $.each(data, function (i, item) {
                htmlDOM += '<li data-index="rs-82" data-transition="random-premium"' +
                    'data-slotamount="7" data-masterspeed="500"' +
                    'data-saveperformance="on">' +
                    '<img src="' + item.SliderImage + '">' +
                    '<div class="tp-caption tp-resizeme sfb"' +
                    'data-x="350"' +
                    'data-y="50"' +
                    'data-speed="800"' +
                    'data-start="1000"' +
                    'data-easing="Power4.easeOut"' +
                    'data-endspeed="300"' +
                    'data-endeasing="Power1.easeIn"' +
                    'data-captionhidden="off"' +
                    'style="z-index: 6; text-align:center;color: #fff; font-size: 40px; font-family: Montserrat;letter-spacing: 3px;font-weight: 700;">' +
                    '' + item.SliderName + '' +
                    '</div>' +
                    '<div class="tp-caption mediumlarge_light_white_center customin customout start hidden-xs"' +
                    'data-x="440"' +
                    'data-hoffset="0"' +
                    'data-y="95"' +
                    'data-customin="x:0;y:0;z:0;rotationX:90;rotationY:0;rotationZ:0;scaleX:1;scaleY:1;skewX:0;skewY:0;opacity:0;transformPerspective:200;transformOrigin:50% 0%;"' +
                    'data-customout="x:0;y:0;z:0;rotationX:0;rotationY:0;rotationZ:0;scaleX:0.75;scaleY:0.75;skewX:0;skewY:0;opacity:0;transformPerspective:600;transformOrigin:50% 50%;"' +
                    'data-speed="1000"' +
                    'data-start="2000"' +
                    'data-easing="Back.easeInOut"' +
                    'data-endspeed="300"' +
                    'style="z-index:7; color: #fff; font-size: 26px;font-family: Noto Sans; letter-spacing: 1px; ">' +
                    '' + item.Caption1 + '' +
                    '</div>' +
                    '<div class="tp-caption mediumlarge_light_white_center customin customout start hidden-xs"' +
                    'data-x="580"' +
                    'data-hoffset="0"' +
                    'data-y="125"' +
                    'data-customin="x:0;y:0;z:0;rotationX:90;rotationY:0;rotationZ:0;scaleX:1;scaleY:1;skewX:0;skewY:0;opacity:0;transformPerspective:200;transformOrigin:50% 0%;"' +
                    'data-customout="x:0;y:0;z:0;rotationX:0;rotationY:0;rotationZ:0;scaleX:0.75;scaleY:0.75;skewX:0;skewY:0;opacity:0;transformPerspective:600;transformOrigin:50% 50%;"' +
                    'data-speed="1000"' +
                    'data-start="2000"' +
                    'data-easing="Back.easeInOut"' +
                    'data-endspeed="300"' +
                    'style="z-index:7; color: #fff; font-size: 26px; font-family: Noto Sans; letter-spacing: 1px; ">' +
                    '' + item.Caption2 + '' +
                    '</div>' +
                    '<div class="tp-caption mediumlarge_light_white_center customin customout start hidden-xs"' +
                    'data-x="590"' +
                    'data-hoffset="0"' +
                    'data-y="165"' +
                    'data-customin="x:0;y:0;z:0;rotationX:90;rotationY:0;rotationZ:0;scaleX:1;scaleY:1;skewX:0;skewY:0;opacity:0;transformPerspective:200;transformOrigin:50% 0%;"' +
                    'data-customout="x:0;y:0;z:0;rotationX:0;rotationY:0;rotationZ:0;scaleX:0.75;scaleY:0.75;skewX:0;skewY:0;opacity:0;transformPerspective:600;transformOrigin:50% 50%;"' +
                    'data-speed="1000"' +
                    'data-start="2000"' +
                    'data-easing="Back.easeInOut"' +
                    'data-endspeed="300"' +
                    'style="z-index:7; color: #fff; font-size: 25px; font-weight:600; font-family: Noto Sans; letter-spacing: 1px;font-style: italic; ">' +
                    '- ' + item.CaptionAuthor + '' +
                    '</div>' +
                    '</li>';

            });
            $('#SliderDIV').append(htmlDOM);
            $.getScript("/css/slider/js/jquery.themepunch.tools.min.js", function (data, textStatus, jqxhr) {
                console.log("Load was performed.");
                $.getScript("/css/slider/js/jquery.themepunch.revolution.min.js", function (data, textStatus, jqxhr) {
                    console.log("Load was performed.");
                });
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
                        htmlDOM += '<div class="col-sm-3 col-xs-12">' +
                            '<div class="productImage isotopeSelector">' +
                            '<figure>' +
                            '<img src="' + data[index].SliderImage + '" alt="' + data[index].Name + '" class="img-responsive">' +
                            '<div class="overlay-background">' +
                            '<div class="inner"></div>' +
                            '</div>' +
                            '<div class="overlay">' +
                            '<a class="fancybox-pop" rel="portfolio-1" href="' + data[index].SliderImage + '">' +
                            '<div class="overlayInfo">' +
                            '<h4><i class="fa fa-plus" aria-hidden="true"></i> <br></h4>' +
                            '</div>' +
                            '</a>' +
                            '</div>' +
                            '</figure>' +
                            '<div class="event-details bg-white p-20">' +
                            '<a href=""><h4 class="text-thm pb-5">' + data[index].Name + '</h4></a>' +
                            '</div>' +
                            '</div>' +
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

function fillDirectorMessagePage() {
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Home/GetDirectorMessagePage',
        async: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            int = 1;
            var htmlDOM = '';
            htmlDOM += '<div class="col-md-3">'+
                '<img src="' + data.Photo + '" alt="' + data.Name + '" class="img-responsive" style="width:180px;height:180px;" />'+
                                '</div>'+
                                '<div class="col-md-9">'+
                                    '<p> ' + data.Message + '</p>'+
                                '</div>';
            //htmlDOM += '<img src="' + data.Photo + '" alt="' + data.Name + '" class="img-responsive" style="width:210px;height:210px;" />'+
            //    '<p> ' + data.Message + '</p>';
            $('#divDirectorMessagePage').html(htmlDOM);
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function fillWhyUptron() {
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Home/GetAllWhyUptron',
        async: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            int = 1; 
            var htmlDOM = ''; 
            htmlDOM += '<i class="fa fa-calendar-o"></i>'+
                '<span class="counter-number">'+
                    '<span class="counter">' + data.Coun + '</span>'+
                '</span>' +
                    '<span class="counter-text">' + data.co + '</span>';
            $('#divWhyUptron').html(htmlDOM);
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

