'use strict';
/*
 Theme Name: CarePro - Bootstrap HTML template
 Description: Custom JS are defined in this class
 Author: Jyostna
 Author URI: http://themeforest.net/user/jyostna
 Version: 1.0

 -------------------------------------------- */
/*
 TABLE OF CONTENT
 -------------------------------------------------

 1- Preloader and twentytwenty
 2- Footer
 3- Stylespage
 4- Header
 5- AboutUs
 6- DatePicker
 7- Terms
 8- ContactUs
 9- Timetable
 10- Index1
 11- Index2
 12- Index4
 13- Index5
 14- Departments
 15- Departments Services
 16- Mailchimp
 17- Header2
 18- Dribbble
 19- Gallery
 20. Index5 Header
 21- Backttotop
 22- Fixed Header
 23. Swiper
 24. Revolution Slider
 -------------------------------------*/
/*------------Preloader and twentytwenty----------------------------*/
$(window).on('load', function () {
    $('.preloader img, .preloader').fadeOut();
    $('.active_gallery').click();
    if ($.fn.twentytwenty !== undefined) {
        $("#twenty").twentytwenty({
            default_offset_pct: 0.5,
            orientation: 'horizontal'
        });
    }
    return false;
});


/*--------------End of Preloader and twentytwenty--------------------*/

// Analytics code

(function (i, s, o, g, r, a, m) {
    i['GoogleAnalyticsObject'] = r;
    i[r] = i[r] || function () {
            (i[r].q = i[r].q || []).push(arguments)
        }, i[r].l = 1 * new Date();
    a = s.createElement(o), m = s.getElementsByTagName(o)[0];
    a.async = 1;
    a.src = g;
    m.parentNode.insertBefore(a, m)
})(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');
ga('create', 'UA-59850948-1', 'auto');
ga('send', 'pageview');


$(document).ready(function () {


//================Start of footer================//
    if ($.fn.newsTicker !== undefined) {
        var newsticker1 = $('.newsticker').newsTicker({
            row_height: 80,
            max_rows: 3,
            duration: 1500
        });
        var newsticker2 = $('.newsticker1').newsTicker({
            row_height: 30,
            max_rows: 5,
            duration: 1500
        });
        var newsticker3 = $('.newsticker_blog').newsTicker({
            row_height: 135,
            max_rows: 3,
            duration: 1800
        });
    }
//================End of footer================//

//================Start of Styles page================//
    if ($.fn.iCheck !== undefined) {
        $('.red').iCheck({
            checkboxClass: 'icheckbox_minimal-blue',
            radioClass: 'iradio_square-blue'
        });
        $('.blue_check').iCheck({
            checkboxClass: 'icheckbox_minimal-red',
            radioClass: 'iradio_flat-blue',
            increaseArea: '20%' // optional
        });
        $(".timings, .gender, .anonymous, .amount").iCheck({
            radioClass: 'iradio_flat-blue'
        });
        $('.appointment_check').iCheck({
            checkboxClass: 'icheckbox_minimal-blue',
            radioClass: 'iradio_square-red'
        });
    }

    if ($.fn.select2 !== undefined) {
        $('.select2').select2({
            theme: 'bootstrap'
        });
        $('.appointment_select2, .dept_select2').select2();
    }
//================End of Styles page================//
//================Start of header================//

var search_large = $("#search_large");
    var search_wrap = $(".search_wrap");
    $(".search_icon").on('click', function () {
        search_large.css('transform', 'scale(1,1)');
        search_wrap.css({
            "transform": "scale(1,1)"
        });
        return false;
    });

    $("#search-close").on('click', function () {
        $("#search_large").css('transform', 'scale(1,0)');
        search_wrap.css({
            "transform": "scale(1,0)"
        });
        return false;
    });

//================End of header================//


// ================Start of about_us page================//

    $(".doctors_team").on('mouseleave', function () {
        $(this).find(".doctor_content").removeClass("animated fadeIn");
        $(this).find(".doctor_name").removeClass("animated fadeInUp").addClass("animated fadeInDown");
    }).on('mouseenter', function () {
        $(this).find(".doctor_content").addClass("animated fadeIn");
        $(this).find(".doctor_name").addClass("animated fadeInUp");
    });

    if ($.fn.lightbox !== undefined) {
        lightbox.option({
            'resizeDuration': 200,
            'wrapAround': true,
            'showImageNumberLabel': 0
        });
    }
    //doctors page
    if ($.fn.slider !== undefined) {
        $("#distance").slider({});
    }

    //================End of about_us page================//

//================Date picker================//
    if ($.fn.datetimepicker !== undefined) {
        var x = new Date();
        $('.datetimepicker_single, .datetimepicker1').datetimepicker({
            showClose: true,
            useCurrent: false,
            minDate: x,
            format: 'DD/MM/YYYY'
        });

        $('.datetimepicker_single2, .datetimepicker2').datetimepicker({
            format: 'LT',
            useCurrent: false
        });
        $('#datetimepicker_dob').datetimepicker({
            showClose: true,
            useCurrent: false,
            format: 'DD/MM/YYYY'
        });
    }
//================End of Date picker================//

//================Start of terms page================//

    $.each($('.make-switch'), function () {
        $(this).bootstrapSwitch({
            onText: $(this).data('onText'),
            offText: $(this).data('offText'),
            onColor: $(this).data('onColor'),
            offColor: $(this).data('offColor'),
            size: $(this).data('size'),
            labelText: $(this).data('labelText')
        });
    });
// ================End of terms page================//

// ================Start of contact_us page================//
    if ($.fn.bootstrapValidator !== undefined) {
        $('#contact_form_validation').bootstrapValidator({
            fields: {
                fullname: {
                    validators: {
                        notEmpty: {
                            message: 'Enter your name'
                        }
                    }
                },
                email_address: {
                    validators: {
                        regexp: {
                            regexp: /^\S+@\S{1,}\.\S{1,}$/,
                            message: 'The input is not a valid email address.'
                        },
                        notEmpty: {
                            message: 'The email address is required'
                        }
                    }
                },
                contact: {
                    validators: {
                        notEmpty: {
                            message: 'This field is required.'
                        },
                        regexp: {
                            regexp: /^\d+$/,
                            message: 'Contains digits only.'
                        }
                    }
                }
            }
        });
    }
    //contact3
    if ($.fn.gmap3 !== undefined) {
        $("#contact_map").gmap3({
            map: {
                options: {
                    center: [40.3140, -74.5089],
                    zoom: 6,
                    scrollwheel: false,
                    styles: [{
                        "featureType": "administrative",
                        "elementType": "labels.text.fill",
                        "stylers": [{"color": "#444444"}]
                    }, {
                        "featureType": "landscape",
                        "elementType": "all",
                        "stylers": [{"color": "#f2f2f2"}]
                    }, {
                        "featureType": "poi",
                        "elementType": "all",
                        "stylers": [{"visibility": "off"}]
                    }, {
                        "featureType": "road",
                        "elementType": "all",
                        "stylers": [{"saturation": -100}, {"lightness": 45}]
                    }, {
                        "featureType": "road.highway",
                        "elementType": "all",
                        "stylers": [{"visibility": "simplified"}]
                    }, {
                        "featureType": "road.arterial",
                        "elementType": "labels.icon",
                        "stylers": [{"visibility": "off"}]
                    }, {
                        "featureType": "transit",
                        "elementType": "all",
                        "stylers": [{"visibility": "off"}]
                    }, {
                        "featureType": "water",
                        "elementType": "all",
                        "stylers": [{"color": "#46bcec"}, {"visibility": "on"}]
                    }]
                }
            },
            marker: {
                values: [{
                    address: "Spring Haven Trail,New Jersey",
                    options: {
                        icon: "images/marker1.png"
                    }

                }]
            }

        });
    }
    // ================End of contact_us page================//

    //================Start of timetable page================//

    $('[data-toggle="tooltip"]').tooltip();
//================End of timetable page================//
//================Start of index1 page================//

    if ($.fn.circliful !== undefined) {
        $("#circle1").circliful({
            animationStep: 5,
            foregroundBorderWidth: 5,
            backgroundBorderWidth: 5,
            percent: 79,
            animateInView: true
        });
        $("#circle2").circliful({
            animationStep: 5,
            foregroundBorderWidth: 5,
            backgroundBorderWidth: 5,
            percent: 55,
            animateInView: true
        });
        $("#circle3").circliful({
            animationStep: 5,
            foregroundBorderWidth: 5,
            backgroundBorderWidth: 5,
            percent: 27,
            animateInView: true
        });
    }
    $('.index1_health_service').on('mouseenter', function () {
        $(this).find('i').css({"font-size": "50px", "transition": "0.5s"});
    }).on('mouseleave', function () {
        $(this).find('i').css("font-size", "40px");
    });


    //================End of index1 page================//

    //================Start of index2 page================//
    $(".health_content, .index5_section_effect, .index5_posts, .index6_services").on("mouseenter mouseleave", function () {
        $(this).find("h5").toggleClass("text-white");
        $(this).find("p").toggleClass("text-white");
        $(this).find("a").toggleClass("btn-default").toggleClass("btn-primary");
        return false;
    });

    $(".index2_services .right_content").on("mouseenter mouseleave", function(){
        $(this).find("i").toggleClass("font50").toggleClass("font60").css("transition", "0.5s");
        return false;
    });

    //================End of index2 page================//

    //================Start of index4 page================//

    $(".index4 .health_services .left_icon, .rank_align .right_align, .section4 .icon").on("mouseenter mouseleave", function () {
        $(this).find("i").toggleClass("text-white").toggleClass("text-primary");
        return false;
    });
    $(".index6_services").on('mouseenter', function () {
        $(this).find("i").addClass("text-white").removeClass("text-primary");
        $(this).find("h5").addClass("text-white");
    }).on('mouseleave', function () {
            $(this).find("i").removeClass("text-white").addClass("text-primary");
        $(this).find("h5").removeClass("text-white");
        });
    setTimeout(function () {
        $('.progress .progress-bar').each(function () {
            var x = $(this);
            var percent = x.attr("data-percentage");
            var current_percent = 0;
            var progress = setInterval(function () {
                if (current_percent >= percent) {
                    clearInterval(progress);
                } else {
                    current_percent += 1;
                    x.css('width', (current_percent) + '%');
                }
                var percentage = x.find(".progress-bar_percentage");
                percentage.text((current_percent) + '%');

            }, 50);

        });

    }, 50);


    //================End of index4 page================//

    //================Start of index5 page================//


    $(".index5_team").on("mouseenter mouseleave", function () {
        $(this).find("h5").toggleClass("text-primary");
        return false;
    });

        //================End of index5 page================//

        /*=============Start of departments page==============*/
        $(".dept_imgmod").on("click", function () {
            $("#heart .modal-title").text($(this).attr("data-dept"));
        });

//================End of departments page================//
//================Start of departments_services page================//
        $(".panel-collapse").on('shown.bs.collapse', function () {
            $(this).parent(".panel").find(".accordion-section-title>div > .fa").removeClass("fa-plus").addClass("fa-minus");
            $(this).parent(".panel").find(".panel-heading").addClass("heading_hover");
        }).on('hide.bs.collapse', function () {
            $(this).parent(".panel").find(".accordion-section-title>div > .fa").removeClass("fa-minus").addClass("fa-plus");
            $(this).parent(".panel").find(".panel-heading").removeClass("heading_hover");
        }).on('shown.bs.collapse', function () {
            $(this).parent(".panel").find("#title-one").addClass("bg-primary").addClass("text-white");
        });

//================End of departments_services page================//

        /*========mailchimp================*/
        $('#subscribe,#subscribe1').on('submit', function () {
            if (!valid_email_address($("#email,#email1").val())) {
                swal({
                    type: 'error',
                    html: 'Please Enter Valid Email Address'
                })
            }
            else {
                $.ajax({
                    url: 'subscribe.php',
                    data: $('#email,#email1').serialize(),
                    type: 'POST',
                    success: function (msg) {
                        swal({
                            type: 'success',
                            html: 'You have Successfully Subscribed'
                        });
                    }
                });
            }

            return false;
        });

        function valid_email_address(email, email1) {
            var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);
            return pattern.test(email, email1);
        }

        /*---------_End of Mailchimp---------------*/

        //================Start of header2================//
        var windowWidth = $(window).width();
        $(window).on("resize", function () {
            windowWidth = $(window).width();
            responsive();
        });
        responsive();
        function responsive() {
            var header2 = $(".header2 .mega_dropdown .fa-angle-double-right");
            var branch = $(".branch_section_align");
            var header4 = $(".header4_logo, .facilities_content");
            var header_btn = $(".header_appointment_btn");
            var special_img = $(".specialised_img");
            var special_content = $(".specialised_content");
            var history = $(".history_content_align");
            var history1 = $(".history_content_align1");
            var testimonial = $(".testimonial_swiper_prev, .testimonial_swiper_next");
            var header_top = $(".header4 .header_top_icon");
            var header_info = $(".header_top_info");
            var envelope = $(".envelope_icon_align");
            var header_search = $(".header_search_icon");
            var contact = $(".contact_reset_btn");
            var experts = $(".experts_info");
            var images_right = $(".images_right_content");
            var images_left = $(".images_left_content .info");
            var right_content = $(".images_right_content .info");
            var team_btn = $(".index6_team_btn");
            var header1_logo = $(".header1 .navbar-brand img");
            var index4_content = $(".index4_news_content p");
            var button_404 = $(".btn2_404");
            var headers5_nav = $(".header5 .navbar-default .navbar-nav > li > a");

            if (windowWidth < 768) {
                header2.addClass("text-white").removeClass("text-primary");
                branch.addClass("p-d-40");
                header4.addClass("text-center");
                header_btn.css("display", "block").addClass("m-l-15");
                special_img.addClass("text-center").removeClass("right_align");
                special_content.addClass("text-center").removeClass("m-l-100");
                history.removeClass("content_border_right").addClass("content_border_bottom");
                history1.removeClass("content_border_right_clr").addClass("content_border_bottom_clr");
                header1_logo.removeClass("m-t-n5").addClass("m-t-3");
                index4_content.addClass("p-d-10");
                headers5_nav.css("color", "#646464");
            }
            if (windowWidth >= 768) {
                header2.addClass("text-primary").removeClass("text-white");
                branch.removeClass("p-d-40").addClass("p-b-10");
                header4.removeClass("text-center");
                header_btn.css("display", "none");
                special_img.removeClass("text-center").addClass("right_align");
                special_content.removeClass("text-center").addClass("m-l-100");
                history.addClass("content_border_right").removeClass("content_border_bottom");
                history1.addClass("content_border_right_clr").removeClass("content_border_bottom_clr");
                header1_logo.addClass("m-t-n5").removeClass("m-t-3");
                index4_content.removeClass("p-d-10");
            }
            if (windowWidth < 400) {
                testimonial.addClass("m-t-n80");
            }
            if (windowWidth >= 400) {
                testimonial.removeClass("m-t-n80");
            }
            if (windowWidth < 1200) {
                header_top.addClass("center-block").css("float", "none");
                header_info.addClass("text-center");
                envelope.addClass("m-t-20").css("padding-left", "25px");
                header_search.addClass("m-t-20");
                images_right.addClass("m-t-40");
                images_left.removeClass("m-t-70");
            }
            if (windowWidth >= 1200) {
                header_top.removeClass("center-block").css("float", "left");
                header_info.removeClass("text-center");
                envelope.removeClass("m-t-20").css("padding-left", "30px");
                header_search.removeClass("m-t-20");
                images_right.removeClass("m-t-40");
                images_left.addClass("m-t-70");
            }
            if (windowWidth < 353) {
                contact.addClass("m-t-10").removeClass("m-l-10");
            }
            if (windowWidth > 352) {
                contact.addClass("m-l-10").removeClass("m-t-10");
            }
            if (windowWidth < 341) {
                button_404.addClass("m-t-10");
            }
            if (windowWidth >= 341) {
                button_404.removeClass("m-t-10");
            }
            if (windowWidth > 992) {
                experts.addClass("right_align");
            }
            if (windowWidth < 992) {
                experts.removeClass("right_align");
            }
            if (windowWidth < 450) {
                right_content.addClass("p-d-20").removeClass("p-d-40");
            }
            if (windowWidth >= 450) {
                right_content.removeClass("p-d-20").addClass("p-d-40");
            }
            if (windowWidth < 386) {
                team_btn.addClass("m-t-10");
            }
            if (windowWidth >= 386) {
                team_btn.removeClass("m-t-10");
            }
        }

//================End of header2================//


        /*=====================Dribbble======================*/
        if ($(".shots")[0]) {
            $.jribbble.setToken('f688ac519289f19ce5cebc1383c15ad5c02bd58205cd83c86cbb0ce09170c1b4');

            $.jribbble.shots('debuts', {
                'per_page': 6,
                'timeframe': 'month',
                'sort': 'views'
            }).then(function (res) {
                var html = [];
                res.forEach(function (shot) {
                    html.push('<li class="shots--shot">');
                    html.push('<a href="' + shot.html_url + '" target="_blank">');
                    html.push('<img src="' + shot.images.normal + '">');
                    html.push('</a></li>');
                });

                $('.shots').html(html.join(''));
            });
        }

        /*---------------------End of Dribble-----------------*/

        /*----------------------Start of Gallery-----------------------------*/
        if ($.fn.isotope !== undefined) {
            var $container = $('#posts').isotope({
                itemSelector: '.item',
                isFitWidth: true
            });

            $container.isotope({
                filter: '*'
            });
            $('#filters').on('click', '.filter', function () {
                var filterValue = $(this).attr('data-filter');
                $container.isotope({
                    filter: filterValue
                });
            });
        }

        /*-------------------------End of gallery---------------*/

        /*------------------------------Index5 Header-------------------------*/

        var header5 = $(".header5 .navbar-default");
        var header5_nav = $(".header5 .navbar-default .navbar-nav > li > a");
        $(window).on('scroll load', function () {
            if ($(window).width() >= 768) {
                if ($(document).scrollTop() > 70) {
                    header5.css({
                        "background-color": "#fff",
                        "border-bottom": "1px solid #ddd",
                        "transition": "400ms",
                        "border-radius": "0"
                    });
                    header5_nav.css("color", "#646464").on("mouseenter", function () {
                        $(this).css("color", "#107fc9");
                    }).on("mouseleave", function () {
                        $(this).css("color", "#646464");
                    });
                } else {
                    header5.css({
                        "background-color": "transparent",
                        "border-bottom": "none"
                    });
                    header5_nav.css("color", "#fff").on("mouseenter", function () {
                        $(this).css("color", "#107fc9");
                    }).on("mouseleave", function () {
                        $(this).css("color", "#fff");
                    });
                }
            }
        });

        /*-------------------End of Index5 Header------------------*/


        /*-----------------------Back to top -------------------------*/

        var return_top = $('#return-to-top');

        return_top.fadeOut().on('click', function () {
            $('body,html').animate({
                scrollTop: 0
            }, 500);
            return false;
        });
        $(window).on('scroll', function () {
            if ($(this).scrollTop() >= 50) {
                return_top.fadeIn(200);
            } else {
                return_top.fadeOut(200);
            }
        });

        /*-----------------------Back to top End-------------------------*/

        /*-------------------------fixed header--------------------------------*/
        var header1_align = $(".header1_align");
        $(window).on('scroll load resize', function () {
            if ($(window).width >= 768) {
                if ($(document).scrollTop() > 50) {
                    header1_align.css({
                        "z-index": "1020",
                        "width": "100%",
                        "position": "fixed",
                        "top": "0"
                    });
                    $(".header1_align .navbar").css({
                        "border-radius": "0",
                        "border-bottom": "1px solid #ddd"
                    });
                }
                else {
                    header1_align.css({
                        "z-index": "1020",
                        "width": "100%",
                        "position": "static"
                    });
                }
            }
        });
        /*----------------------------End of fixed header-------------------*/

        /*-------------------Start of Swiper------------------*/
        if ($(".swiper_area")[0]) {
            var mySwiper = new Swiper('.swiper-container1', {
                autoplay: 1500,
                nextButton: '.swiper_next',
                prevButton: '.swiper_prev',
                loop: true,
                slidesPerView: 5,
                breakpoints: {
                    450: {
                        slidesPerView: 1

                    },
                    750: {
                        slidesPerView: 2
                    },
                    992: {
                        slidesPerView: 3
                    },
                    1200: {
                        slidesPerView: 4
                    }


                }
            });

            var swiper = new Swiper('.swiper-container2', {
                pagination: '.swiper-pagination',
                paginationClickable: true,
                slidesPerView: 1,
                loop: true,
                autoplay: 2000
            });
            var swiper_3 = new Swiper('.career_swiper', {
                autoPlay: 2000,
                loop: true,
                slidesPerView: 3,
                nextButton: '.career_swiper_next',
                prevButton: '.career_swiper_prev',
                spaceBetween: 30,
                breakpoints: {
                    630: {
                        slidesPerView: 1
                    },
                    992: {
                        slidesPerView: 2
                    },
                    1100: {
                        slidesPerView: 3
                    }
                }
            });
            var swiper_1 = new Swiper('.swiper-container_blog_single', {
                pagination: '.swiper-pagination',
                slidesPerView: 3,
                paginationClickable: true,
                spaceBetween: 30,
                breakpoints: {
                    510: {
                        slidesPerView: 1

                    },
                    992: {
                        slidesPerView: 2
                    },
                    1100: {
                        slidesPerView: 3
                    }

                },
                autoplay: 1500,
                loop: true
            });
// no side bar
            var swiper_2 = new Swiper('.swiper-container_blog_nobar', {
                pagination: '.swiper-pagination',
                slidesPerView: 4,
                paginationClickable: true,
                spaceBetween: 30,
                breakpoints: {
                    510: {
                        slidesPerView: 1

                    },
                    992: {
                        slidesPerView: 2
                    },
                    1100: {
                        slidesPerView: 3
                    }

                },
                autoplay: 1500,
                loop: true
            });
            var swiper2 = new Swiper('.testimonial_swiper', {
                pagination: '.swiper-pagination',
                paginationClickable: true,
                autoplay: 3000,
                loop: true,
                nextButton: '.testimonial_swiper_next',
                prevButton: '.testimonial_swiper_prev'
            });
            var index_swiper = new Swiper('.index1_swiper', {
                pagination: '.swiper-pagination',
                paginationClickable: true,
                autoplay: 2000,
                loop: true,
                slidesPerView: 3,
                spaceBetween: 30,
                breakpoints: {
                    600: {
                        slidesPerView: 1
                    },
                    992: {
                        slidesPerView: 2
                    },
                    1100: {
                        slidesPerView: 3
                    }

                }
            });
            var swiper_index3 = new Swiper('.swiper_index3_testimonial', {
                pagination: '.swiper-pagination',
                paginationClickable: true,
                autoplay: 2000,
                slidesPerView: 2,
                spaceBetween: 30,
                loop: true,
                nextButton: '.index3_test_next',
                prevButton: '.index3_test_prev',
                breakpoints: {
                    992: {
                        slidesPerView: 1
                    },
                    1100: {
                        slidesPerView: 2
                    }

                }
            });
            var swiper1_index3 = new Swiper('.swiper_index3_team', {
                pagination: '.swiper-pagination',
                paginationClickable: true,
                autoplay: 2000,
                slidesPerView: 4,
                loop: true,
                nextButton: '.index3_team_next',
                prevButton: '.index3_team_prev',
                breakpoints: {
                    590: {
                        slidesPerView: 1
                    },
                    800: {
                        slidesPerView: 2
                    },
                    1130: {
                        slidesPerView: 3
                    },
                    1200: {
                        slidesPerView: 4
                    }

                }
            });
            var swiper_index4 = new Swiper('.swiper_index4_testimonial', {
                pagination: '.swiper-pagination',
                paginationClickable: true,
                autoplay: 2000,
                loop: true
            });
            var index4_swiper = new Swiper('.swiper_index4_team', {
                pagination: '.swiper-pagination',
                paginationClickable: true,
                autoplay: 2000,
                loop: true,
                slidesPerView: 4,
                spaceBetween: 30,
                nextButton: '.index4_team_next',
                prevButton: '.index4_team_prev',
                breakpoints: {
                    550: {
                        slidesPerView: 1
                    },
                    780: {
                        slidesPerView: 2
                    },
                    992: {
                        slidesPerView: 3
                    },
                    1100: {
                        slidesPerView: 4
                    }
                }
            });
            var swiper_index5 = new Swiper('.swiper_index5_testimonial', {
                pagination: '.swiper-pagination',
                paginationClickable: true,
                loop: true,
                autoplay: 3000,
                slidesPerView: 2,
                breakpoints: {
                    991: {
                        slidesPerView: 1

                    },
                    1200: {
                        slidesPerView: 2
                    }
                }
            });
            var swiper_index6 = new Swiper('.swiper_index6_testimonial', {
                pagination: '.swiper-pagination',
                paginationClickable: true,
                loop: true,
                autoplay: 3000,
                nextButton: '.index6_next',
                prevButton: '.index6_prev'
            });
            var swiper_index2 = new Swiper('.awards_swiper', {
                pagination: '.swiper-pagination',
                slidesPerView: 5,
                spaceBetween: 30,
                autoplay: 2000,
                loop: true,
                nextButton: '.award_next',
                prevButton: '.award_prev',
                paginationClickable: true,
                breakpoints: {
                    500: {
                        slidesPerView: 1
                    },
                    600: {
                        slidesPerView: 2
                    },
                    850: {
                        slidesPerView: 3
                    },
                    992: {
                        slidesPerView: 4
                    },
                    1100: {
                        slidesPerView: 5
                    }

                }
            });
        }
        /*-------------------End of Swiper----------------------------*/
        /*--------------------Start of Revolution Slider-----------------*/

        if (typeof revslider_showDoubleJqueryError == 'function') {
            $(".tp-banner-slider").revolution({
                sliderType: "standard",
                dottedOverlay: "none",
                delay: 9000,
                startwidth: 1130,
                startheight: 400,
                hideThumbs: 200,
                thumbWidth: 100,
                thumbHeight: 50,
                thumbAmount: 5,
                simplifyAll: "off",


                navigationType: "bullet",
                navigationArrows: "nexttobullets",
                navigationStyle: "preview4",
                nextSlideOnWindowFocus: "off",

                touchenabled: "on",
                onHoverStop: "on",

                swipe_threshold: 75,
                swipe_velocity: 0.7,
                swipe_min_touches: 1,
                swipe_max_touches: 1,
                drag_block_vertical: false,


                parallax: {
                    type: "mouse",
                    origo: "slidercenter",
                    speed: 2000,
                    levels: [2, 3, 4, 5, 6, 7, 12, 16, 10, 25, 47, 48, 49, 50, 51, 55]

                },

                keyboardNavigation: "off",

                navigationHAlign: "center",
                navigationVAlign: "bottom",
                navigationHOffset: 0,
                navigationVOffset: 20,


                shadow: 0,
                fullWidth: "on",
                fullScreen: "off",

                spinner: "spinner0",

                stopLoop: "off",
                stopAfterLoops: -1,
                stopAtSlide: -1,

                shuffle: "off",

                autoHeight: "on",
                forceFullWidth: "on",


                hideThumbsOnMobile: "off",
                hideNavDelayOnMobile: 1500,
                hideBulletsOnMobile: "off",
                hideArrowsOnMobile: "off",
                hideThumbsUnderResolution: 0,

                hideSliderAtLimit: 0,
                hideCaptionAtLimit: 0,
                hideAllCaptionAtLilmit: 0,
                startWithSlide: 0,
                videoJsPath: "rs-plugin/videojs/",
                fullScreenOffsetContainer: ".header"
            });

        }

        /*-------------------End of Revolution Slider------------------*/

    }
    );



