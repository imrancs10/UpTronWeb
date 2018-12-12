//$(document).ready(function () {
//    $("#datepicker").daterangepicker({
//        locale: {
//            format: 'DD/MMM/YYYY'
//        }
//    });
//})

fillState();
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

$('#btnSave').click(function () {
    var registrationJson = {
        Name: $('#txtName').val(),
        fatherName: $('#txtfatherName').val(),
        Mothername: $('#txtMotherName').val(),
        DOB: $('#datepicker').val(),
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

    registrationJson.JobRegistrationEmployement.push(
        {
            OrganizationName: $('#txtOrganization').val(),
            Post: $('#txtPostHeld').val(),
            FromMonth: $('#FromMonth').val(),
            FromYear: $('#FromYear').val(),
            ToMonth: $('#ToMonth').val(),
            ToYear: $('#ToYear').val(),
            IndustryType: $('#Industry').val(),
            Salary: $('#txtSalary').val()
        });

    var qualificationRow = $('#tableQualification tbody tr');
    if (qualificationRow != null) {
        $.each(qualificationRow, function (i, row) {
            registrationJson.JobRegistrationQualification.push(
                {
                    Qualification: $('#Qualification').val(),
                    Board: $('#txtUniversity').val(),
                    YearOfPassing: $('#Year').val(),
                    Marks: $('#Percentage').val(),
                    Specialization: $('#Specialization').val(),
                    CourseType: $('#Course').val(),
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

});

function convertDateTimeFormat(dateInput) {
    var todayTime = new Date();
    var month = format(todayTime.getMonth() + 1);
    var day = format(todayTime.getDate());
    var year = format(todayTime.getFullYear());
    return month + "/" + day + "/" + year;
}