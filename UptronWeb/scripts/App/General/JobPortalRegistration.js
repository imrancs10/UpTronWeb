$(document).ready(function () {
    $('#DOB').datepicker({
        changeMonth: true,
        changeYear: true,
        stepMonths: true,
        yearRange: "-100:+0",
        dateFormat: 'dd/mm/yy',
        maxDate: 0
    }); 
});

$('#DOB').siblings('span').click(function () {
    $('#DOB').datepicker("show");
    //$('#ui-datepicker-div').css('width', $('#DOB').width() + parseFloat($('#DOB').siblings('span').css('width')) + 20 + 'px');
});

fillState();
//fillReligion();
function fillState() {
    let dropdown = $('#state');
    dropdown.empty();
    dropdown.append('<option value="">Select</option>');
    dropdown.prop('selectedIndex', 0);
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Common/GetSates',
        async: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $.each(data, function (key, entry) {
                dropdown.append($('<option></option>').attr('value', entry.StateId).text(entry.StateName));
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

$('#state').on('change', function (e) {
    var valueSelected = this.value;
    fillCity(valueSelected);
});

function fillCity(stateId) {
    let dropdown = $('#city');
    dropdown.empty();
    dropdown.append('<option value="">Select</option>');
    dropdown.prop('selectedIndex', 0);
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Common/GetCities',
        data: '{stateId: "' + stateId + '" }',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $.each(data, function (key, entry) {
                dropdown.append($('<option></option>').attr('value', entry.CityId).text(entry.CityName));
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

//function fillReligion() {
//    let dropdown = $('#religion');
//    dropdown.empty();
//    dropdown.append('<option value="">Select</option>');
//    dropdown.prop('selectedIndex', 0);
//    $.ajax({
//        dataType: 'json',
//        type: 'POST',
//        url: '/Common/GetReligion',
//        async: true,
//        contentType: "application/json; charset=utf-8",
//        success: function (data) {
//            $.each(data, function (key, entry) {
//                dropdown.append($('<option></option>').attr('value', entry.ReligionId).text(entry.ReligionName));
//            });
//        },
//        failure: function (response) {
//            alert(response);
//        },
//        error: function (response) {
//            alert(response.responseText);
//        }
//    });
//}
$('#btnSave').click(function (e) {
    if (!$("#form")[0].checkValidity()) {
        $("#form").find("#submit-hidden").click();
        return false;
    }
    e.preventDefault();

    var registrationJson = {
        Name: $('#txtName').val(),
        fatherName: $('#txtfatherName').val(),
        Mothername: $('#txtMotherName').val(),
        DOB: $('#DOB').val(),
        Gender: $('#Gender').val(),
        MaritalStatus: $('#MaritalStatus').val(),
        Religion: $('#Religion').val(),
        IdentityType: $('#IdentityDetails').val(),
        IdentityNo: $('#txtIdentityNo').val(),
        MobileNo: $('#txtMobile').val(),
        AlternateNo: $('#txtAlternate').val(),
        EmailId: $('#txtEmail').val(),
        Pincode: $('#txtPinCode').val(),
        CityId: $('#city').val(),
        StateId: $('#state').val(),
        Experience: $('#Experience').val(),
        Disability: $('#disabilities').val(),
        ExServiceMan: $('#ExServiceman').val(),
        PersonHeight: $('#txtHeight').val(),
        JobRegistrationLanguage: [],
        JobRegistrationSkills: [],
        JobRegistrationEmployement: [],
        JobRegistrationQualification: []
    };

    if ($('#rdbEyeSightNormal').prop('checked'))
        registrationJson.EyeSight = $('#rdbEyeSightNormal').val();
    else
        registrationJson.EyeSight = $('#rdbEyeSightNormal').val();
    var languages = $('#LanguageDropdown').val();
    if (languages != null) {
        $.each(languages, function (i, item) {
            registrationJson.JobRegistrationLanguage.push({ Language: item });
        });
    }

    var skills = $('#SkillDropdown').val();
    if (skills != null) {
        $.each(skills, function (i, item) {
            registrationJson.JobRegistrationSkills.push({ Skill: item });
        });
    }

    var organization = $('#txtOrganization').val();

    if (organization !== '') {
        registrationJson.JobRegistrationEmployement.push(
            {
                OrganizationName: organization,
                Post: $('#txtPostHeld').val(),
                FromMonth: $('#FromMonth').val(),
                FromYear: $('#FromYear').val(),
                ToMonth: $('#ToMonth').val(),
                ToYear: $('#ToYear').val(),
                IndustryType: $('#Industry').val(),
                Salary: $('#txtSalary').val()
            });
    }


    var qualificationRow = $('#tableQualification tbody tr');

    var filledQualification = $.grep(qualificationRow, function (row) {
        return $(row).find('#Qualification').val() !== "";
    });

    if (filledQualification != null) {
        $.each(filledQualification, function (i, row) {
            registrationJson.JobRegistrationQualification.push(
                {
                    Qualification: $(row).find('#Qualification').val(),
                    Board: $(row).find('#txtUniversity').val(),
                    YearOfPassing: $(row).find('#Year').val(),
                    Marks: $(row).find('#Percentage').val(),
                    Specialization: $(row).find('#Specialization').val(),
                    CourseType: $(row).find('#Course').val()
                });
        });
    }

    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Home/JobPortalSave',
        data: JSON.stringify(registrationJson),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            saveFiles();
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

});

function saveFiles() {
    //save files
    if (window.FormData !== undefined) {
        //resume
        var fileUpload = $("#fileResume").get(0);
        var files = fileUpload.files;
        // Create FormData object  
        var fileData = new FormData();
        // Looping over all files and add it to FormData object  
        fileData.append(files[0].name, files[0]);

        //image
        fileUpload = $("#fileImage").get(0);
        files = fileUpload.files;
        fileData.append(files[0].name, files[0]);
        $.ajax({
            dataType: 'json',
            type: 'POST',
            url: '/Home/JobPortalFileSave',
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            data: fileData,
            success: function (data) {
                alert(data);
                utility.alert.setAlert(utility.alert.alertType.info, data);
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }
}

function convertDateTimeFormat(dateInput) {
    var todayTime = new Date();
    var month = format(todayTime.getMonth() + 1);
    var day = format(todayTime.getDate());
    var year = format(todayTime.getFullYear());
    return month + "/" + day + "/" + year;
}

$('#txtEmail').blur(function () {
    $.ajax({
        dataType: 'JSON',
        type: 'Post',
        data: '{emailId: "' + $(this).val() + '"}',
        url: '/Home/CheckJobSeekerEmailId',
        async: true,
        contentType: "application/json; charset = utf-8",
        success: function (data) {
            if (data == true) {
                utility.alert.setAlert("Email Exists", "Your Email Id is already taken, choose another one.");
                $('#txtEmail').val('');
            }
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
});

$('#SkillDropdown').change(function () {
    var skills = $(this).val();
    if (skills != null) {
        $.each(skills, function (i, item) {
            if (item === "Data Operator") {
                $('#SkillDropdown').attr("required", "required");
            }
        });
    }
});