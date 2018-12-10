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
        Gender: $('#Gender').val(),
        EmailId: $('#txtEmail').val(),
        JobRegistrationSkills: [
            { Skill: $("#SkillDropdown").val()[0] }
        ]
    };

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