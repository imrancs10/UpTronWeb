$(document).ready(function () {
    fillNewsAndUpdate();
    fillDirectorMessage();
    fillUpcomingEvents();
    fillPartner();
    fillKeyFunctionaries();
    //fillFunctionaries();
    fillNewsUpdateList();
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
                '<a href="#" style="color:#337ab7">Read More <i class="fa fa-arrow-circle-right" aria-hidden="true"></i></a>' +
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

//function fillKeyFunctionaries() {
//    $.ajax({
//        dataType: 'json',
//        type: 'POST',
//        url: '/Home/GetKeyFunctionariesList',
//        async: true,
//        contentType: "application/json; charset=utf-8",
//        success: function (data) {
//            int = 1;
//            var htmlDOM = '';
//                htmlDOM += '<div class="row">' +
//                    '<div class="col-md-12 col-sm-12 col-xs-12  br-bottom" >' +
//                    '<div class="col-md-4 col-sm-4">' +
//                    '<span class="photo" style="margin-left: -15px;">' +
//                    '<img src="' + Image + '" alt="' + data.Name + '" class="img-responsive" style="width:130px;height:130px;" />' +
//                    '</span>' +
//                    '</div>' +
//                    '<div class="col-md-8 col-sm-8">' +
//                    '<div class="key_title">' +
//                    '<h3>' + Name + '</h3>' +
//                    '<p>' + Designation + '</p>' +
//                    '<p>' + Location + '</p>' +
//                ' </div>' +
//                    '</div>' +
//                            '</div > '+
//                '</div >';
//                $('#keyfunctionariesDiv').html(htmlDOM);
//        },
//        failure: function (response) {
//            alert(response);
//        },
//        error: function (response) {
//            alert(response.responseText);
//        }
//    }
//    });
//}


//function fillKeyFunctionaries() {
//    $.ajax({
//        dataType: 'json',
//        type: 'POST',
//        url: '/Home/GetAllFunctionarieDetail',
//        async: true,
//        contentType: "application/json; charset=utf-8",
//        success: function (data) {
//            int = 1;
//            var htmlDOM = '';
//            $.each(jsonData, function (i, item) {
//                htmlDOM += '<div class="row">' +
//                    '<div class="col-md-12 col-sm-12 col-xs-12  br-bottom">' +
//                    '<div class="col-md-4 col-sm-4">' +
//                    '<span class="photo" style="margin-left: -15px;">' +
//                    '<img src="' + imgsrc + '" alt="' + item.Name + '" class="img-responsive" />' +
//                    '</span>' +
//                    '</div>' +
//                    '<div class="col-md-8 col-sm-8">' +
//                    '<div class="key_title">' +
//                    '<h3>' + item.Name + '</h3>' +
//                    '<p>' + item.Designation + '</p>' +
//                    '<p>' + + '</p>' +
//                    '</div>' +
//                    '</div>' +
//                    '</div>' +
//                    '</div>';
//            });
//            $('#keyfunctionariesDiv').html(htmlDOM);
//        },
//        failure: function (response) {
//            alert(response);
//        },
//        error: function (response) {
//            alert(response.responseText);
//        }
//    });
//}

function fillFunctionaries() {
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Home/GetFunctionariesList',
        async: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            int = 1;
            var jsonData = JSON.parse(data);
            var htmlDOM = '';
            $.each(jsonData, function (i, item) {
                htmlDOM += '<div class="row">'+
                    '<div class="col-md-12 col-sm-12 col-xs-12  br-bottom" >'+
                        '<div class="col-md-4 col-sm-4">'+
                    '<span class="photo" style="margin-left: -15px;">' +
                    '<img src="~/img/yogiji.jpg" alt="' + item.Name + '" class="img-responsive" />' +
                            '</span>'+
                        '</div>'+
                        '<div class="col-md-8 col-sm-8">'+
                    '<div class="key_title">' +
                    '<h3> ' + item.Name + '</h3>' +
                    '<p>' + item.Designation + ',<br /> ' + + '</p>' +
                            '</div>'+
                        '</div>'+
                            '</div>'+
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

function fillNewsUpdateList() {
    let li = $('#NewUpdateDiv');
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
                htmlDOM += '<li style="width:270px;"><p>';
                if (item.IsNew === true) {
                    htmlDOM += '<img src="../img/NewIcon.gif" style="padding-right:5px;height:15px;width:40px;"/>';
                }
                htmlDOM += '<a href="../Home/ViewNewsFile/' + item.Id + '" target="_blank">' + item.Title;
                htmlDOM += '<br/><img src="../img/readmore.jpg" style="height:15px;width:40px;"/></a></p></li>';
            });
            li.html(htmlDOM);
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

//function fillNewsUpdateList() {
//    $.ajax({
//        dataType: 'json',
//        type: 'POST',
//        url: '/Home/GetNewsAndUpdateList',
        
//        async: true,
//        contentType: "application/json; charset=utf-8",
//        success: function (data) {
//            int = 1;
//            var jsonData = JSON.parse(data);
//            var htmlDOM = '';
//            var loopCount = 1;
//            for (var i = 0; i < loopCount; i++) {
//                if (i === 0) {
//                    htmlDOM += '<li>';
//                }
//                else {
//                    htmlDOM += '<li class="odd">';
//                }

//                $.each(jsonData, function (i, item) {
//                htmlDOM += '<p>';
//                if (item.IsNew === true) {
//                    htmlDOM += '<img src="../img/NewIcon.gif" style="padding-right:5px;height:15px;width:40px;"/>';
//                }
//                htmlDOM += '<a href="../Home/ViewNewsFile/' + item.Id + '" target="_blank">' + item.Title;
//                htmlDOM += '<img src="../img/readmore.jpg" style="height:15px;width:40px;"/></a></p><hr />';
//            });
//            li.html(htmlDOM);
//            }

//            $('#PartnerDiv').html(htmlDOM);
//        },
//        failure: function (response) {
//            alert(response);
//        },
//        error: function (response) {
//            alert(response.responseText);
//        }
//    });
//}

