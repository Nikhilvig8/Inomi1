$("#CountryList").change(function () {
    $("#CityList").html('<option value="">Select</option>');

    //$("#CityList").empty();
    //$("#CourseList").empty();
    //   debugger;
    //var k=  $("#CountryList").val();
    if ($("#CountryList").val() != 'undefined' && $("#CountryList").val() != null && $("#CountryList").val() != '') {


        $.ajax({
            type: 'POST',
            url: '/Councellor/GetStates', // we are calling json method  

            dataType: 'json',

            data: { Id: $("#CountryList").val() },


            success: function (states) {
                // states contains the JSON formatted list  
                // of states passed from the controller  
                //   $("#CityList").append('<option value="Select">Select</option>');
                $("#CityList").html('<option value="">Select</option>');
                $.each(states, function (i, state) {
                    $("#CityList").append('<option value="' + state.Value + '">' +
                        state.Text + '</option>');
                    // here we are adding option for States  

                });
            },
            error: function (ex) {
                alert('Failed to retrieve states.' + ex);
            }
        });


    }

    filter();
    return false;
});

$("#CouncellorList").change(function () {
    if ($("#CouncellorList").val() != 'undefined' && $("#CouncellorList").val() != null && $("#CouncellorList").val() != '') {


        $.ajax({
            type: 'POST',
            url: '/Councellor/GetStudent', // we are calling json method  

            dataType: 'json',

            data: { Id: $("#CouncellorList").val() },


            success: function (states) {
                // states contains the JSON formatted list  
                // of states passed from the controller  
                //   $("#CityList").append('<option value="Select">Select</option>');
                $("#StudentList").html('<option value="">Select</option>');
                $.each(states, function (i, state) {
                    $("#StudentList").append('<option value="' + state.Value + '">' +
                        state.Text + '</option>');
                    // here we are adding option for States  

                });
            },
            error: function (ex) {
                alert('Failed to retrieve states.' + ex);
            }
        });


    }
    filter();
    return false;
});

$("#StudentList").change(function () {

    filter();
    return false;

});
$("#CityList").change(function () {

    filter();
    return false;
});
$("#CourseList").change(function () {

    filter();
    return false;
});
$("#CouncellorList").change(function () {

    filter();
    return false;
});
$("#ApplicationThrough").change(function () {

    filter();
    return false;
});

$(document).on("click", ".select-list li", function () {
    var getValue = $(this).text();
    var htm = '';
    htm += '<span class="btn blue-btn"  style="color:white" >' + getValue + '</span>'
    var collegeid = $(this).parent($('div')).parent($('.selected-university')).find($('input[name="CollegeID"]')).val();

    if (collegeid != 'undefined' || collegeid != null) {
        $(this).parent($('div')).parent($('.selected-university')).html(htm);
        $.ajax({
            type: 'POST',
            url: '/Councellor/AdminStatusRecord', // we are calling json method

            dataType: 'json',

            data: { id: collegeid, Status: getValue },


            success: function (r) {
                //  alert(r + " record(s) inserted.");

            }

        });

    }


});


$('.nav-link').click(function (e) {


    $(this).click();
    filter();
    //e.preventDefault();

    //alert($(this).text());

});
$('#reset').click(function () {

    $('input[type="checkbox"]:checked').prop('checked', false);
    $('ul').children('li').find('a').removeClass('active');
    $('#tab').click();
    $('#StudentList').val('');
    $('#CountryList').val('');
    $('#CityList').val('');
    $('#CourseList').val('');
    $('#ApplicationThrough').val('');
    $('#CouncellorList').val('');
    filter();
    return false;

});
$('.checkbox-inner').find('input[type="checkbox"]').click(function () {



    if ($(this).is(":checked")) {
        $('input[type="checkbox"]:checked').prop('checked', false);
        $(this).prop('checked', true);



    }
    else if ($(this).is(":not(:checked)")) {
        $('input[type="checkbox"]:checked').prop('checked', false);
        $(this).prop('checked', false);

    }


    filter();

});

function filter() {
    var checklist = '';

    $('.checkbox-inner').find('input[type="checkbox"]').each(function () {

        if ($(this).is(":checked")) {
            checklist += $.trim($(this).parent('.checkbox').find('input[type="checkbox"]').val());
            checklist += ',';

        }

        //else if ($(this).is(":not(:checked)")) {
        //    $(this).parent('.checkbox').text();

        //}





    });

    var shortlist = $('.nav-pills').find('.active').text();
    checklist = checklist.slice(0, -1);
    var college = '';
    var country = $('#CountryList').val();
    var city = $('#CityList').val();
    var course = $('#CourseList').val();
    var application = $('#ApplicationThrough').val();
    var StudentList = $('#StudentList').val();
    
    var counsellorid = $('#CouncellorList').val();

    var TeamDetailPostBackURL = '/Councellor/applicationview3';



    $.ajax({
        type: "GET",
        url: TeamDetailPostBackURL,
        contentType: "application/json; charset=utf-8",
        data: { "studid": StudentList, "counid": counsellorid, "college": college, "country": country, "city": city, "course": course, "ranklist": checklist, "shortlist": shortlist, "application": application, },
        datatype: "json",
        success: function (data) {

            $('#selectedContent').html(data);
            $('#selectedContent').attr('style="height:100%;overflow: auto;"');
            checklist = '';

        },
        error: function () {
            alert("Dynamic content load failed.");
        }
    });

    //    height: 400px;
    //  overflow: auto;
}