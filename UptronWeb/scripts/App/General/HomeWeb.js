$(document).ready(function () {
    fillNewsAndUpdate();
    fillDirectorMessage();
    fillUpcomingEvents();
    fillPartner();
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
                            '<img src="' + data[index].PartnerImage + '" alt="Image Partner">' +
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

