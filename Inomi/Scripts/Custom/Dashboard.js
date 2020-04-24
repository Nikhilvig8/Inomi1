$("#Class").focusout(function (event) {



    if ($(this).val() == '') {
        $('#Class').addClass('nik');
        return false;
    }
    else {
        $('#Class').removeClass('nik');
        return true;
    }

});
$("#Year").focusout(function (event) {



    if ($(this).val() == '') {
        $('#Year').addClass('nik');
        return false;
    }
    else {
        $('#Year').removeClass('nik');
        return true;
    }

});
$("#Board").focusout(function (event) {



    if ($(this).val() == '') {
        $('#Board').addClass('nik');
        return false;
    }
    else {
        $('#Board').removeClass('nik');
        return true;
    }

});
$("#School").focusout(function (event) {



    if ($(this).val() == '') {
        $('#School').addClass('nik');
        return false;
    }
    else {
        $('#School').removeClass('nik');
        return true;
    }

});

$("#OverAllGPAMark").focusout(function (event) {



    if ($(this).val() == '') {
        $('#OverAllGPAMark').addClass('nik');
        return false;
    }
    else {
        $('#OverAllGPAMark').removeClass('nik');
        return true;
    }

});
$("#Imagefile").focusout(function (event) {



    if ($(this).val() == '') {
        $('#Imagefile').addClass('nik');
        return false;
    }
    else {
        $('#Imagefile').removeClass('nik');
        return true;
    }

});
$("#Subject").focusout(function (event) {



    if ($(this).val() == '') {
        $('#Subject').addClass('nik');
        return false;
    }
    else {
        $('#Subject').removeClass('nik');
        return true;
    }

});

$('#acadmicbutton').click(function () {

    if ($('#Class').val() == '') {
        $('#Class').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else if ($('#Year').val() == '') {
        $('#Year').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else if ($('#Subject').val() == '') {
        $('#Subject').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else if ($('#Board').val() == '') {
        $('#Board').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else if ($('#School').val() == '') {
        $('#School').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else if ($('#OverAllGPAMark').val() == '') {
        $('#OverAllGPAMark').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else if ($('#Imagefile').val() == '') {
        $('#Imagefile').addClass('nik');
        //  $('#err').text('Please Enter Transcript file');
        return false;
    }
    else {

        if (confirm("Do you want to submit")) {

            $('#acadmicbutton').attr("Type", " Submit");
            $('#acadmicbutton').attr("Type", " button");
            $('#acadmicbutton').text("Submit");
        }
    }
});

$('#EditacademicsModal').on("click", '#Updateacadmicbutton', function (event) {
    
    

        if (confirm("Do you want to Update")) {

            $('#Updateacadmicbutton').attr("Type", " Submit");
            $('#Updateacadmicbutton').attr("Type", " button");
            $('#Updateacadmicbutton').text("Submit");
        }
    
});


$('#EditExtraactivitesModal').on("click", '#EditCurricularActivitesbutton', function (event) {



    if (confirm("Do you want to Update")) {

        $('#EditCurricularActivitesbutton').attr("Type", " Submit");
        $('#EditCurricularActivitesbutton').attr("Type", " button");
        $('#EditCurricularActivitesbutton').text("Submit");
    }

});

$('#EditLeadershipModal').on("click", '#EditLeadershipPositionsbutton', function (event) {



    if (confirm("Do you want to Update")) {

        $('#EditLeadershipPositionsbutton').attr("Type", " Submit");
        $('#EditLeadershipPositionsbutton').attr("Type", " button");
        $('#EditLeadershipPositionsbutton').text("Submit");
    }

});


$('#EdittestreportModal').on("click", '#UpdateTestreportbutton', function (event) {



    if (confirm("Do you want to Update")) {

        $('#UpdateTestreportbutton').attr("Type", " Submit");
        $('#UpdateTestreportbutton').attr("Type", " button");
        $('#UpdateTestreportbutton').text("Submit");
    }

});




$("#TestName").focusout(function (event) {



    if ($(this).val() == '') {
        $('#TestName').addClass('nik');
        return false;
    }
    else {
        $('#TestName').removeClass('nik');
        return true;
    }

});
$("#Score").focusout(function (event) {



    if ($(this).val() == '') {
        $('#Score').addClass('nik');
        return false;
    }
    else {
        $('#Score').removeClass('nik');
        return true;
    }

});
$('#Testreportbutton').click(function () {
    if ($('#TestName').val() == '') {
        $('#TestName').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else if ($('#Score').val() == '') {
        $('#Score').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else {


        if (confirm("Do you want to submit")) {
            $('#Testreportbutton').attr("Type", " Submit");
            $('#Testreportbutton').attr("Type", " button");
            $('#Testreportbutton').text("Submit");
        }
    }
});

$("#Details").focusout(function (event) {



    if ($(this).val() == '') {
        $('#Details').addClass('nik');
        return false;
    }
    else {
        $('#Details').removeClass('nik');
        return true;
    }

});
$("#Achievements").focusout(function (event) {



    if ($(this).val() == '') {
        $('#Achievements').addClass('nik');
        return false;
    }
    else {
        $('#Achievements').removeClass('nik');
        return true;
    }

});
$("#Activities").focusout(function (event) {



    if ($(this).val() == '') {
        $('#Activities').addClass('nik');
        return false;
    }
    else {
        $('#Activities').removeClass('nik');
        return true;
    }

});
$('#ExtraCurricularActivitesbutton').click(function () {
    if ($('#Activities').val() == '') {
        $('#Activities').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else if ($('#Achievements').val() == '') {
        $('#Achievements').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else if ($('#Details').val() == '') {
        $('#Details').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else {

        if (confirm("Do you want to submit")) {

            $('#ExtraCurricularActivitesbutton').attr("Type", " Submit");
            $('#ExtraCurricularActivitesbutton').attr("Type", " button");
            $('#ExtraCurricularActivitesbutton').text("Submit");
        }
    }
});
$("#Position").focusout(function (event) {



    if ($(this).val() == '') {
        $('#Position').addClass('nik');
        return false;
    }
    else {
        $('#Position').removeClass('nik');
        return true;
    }

});
$('#LeadershipPositionsbutton').click(function () {
    if ($('#Position').val() == '') {
        $('#Position').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }

    else {


        if (confirm("Do you want to submit")) {
            $('#LeadershipPositionsbutton').attr("Type", " Submit");
            $('#LeadershipPositionsbutton').attr("Type", " button");
            $('#LeadershipPositionsbutton').text("Submit");
        }
    }
});


$('.anchorDetail').click(function () {
    var id = $(this).attr('id');

    $('#UploadId').val(id);

    $('#uploadFileModal').modal('show');

});
$('#UploadEssaybutton').click(function () {
    if ($('#UploadFiles').val() == '') {
        $('#UploadFiles').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }

    else {

        if (confirm("Do you want to submit")) {

            $('#UploadEssaybutton').attr("Type", " Submit");
            $('#UploadEssaybutton').attr("Type", " button");
            $('#UploadEssaybutton').text("Submit");
            $('#uploadFileModal').modal('hide');
        }
    }
});


$("#Title").focusout(function (event) {



    if ($(this).val() == '') {
        $('#Title').addClass('nik');
        return false;
    }
    else {
        $('#Title').removeClass('nik');
        return true;
    }

});


$('#interestinglinksbutton').click(function () {
    if ($('#Title').val() == '') {
        $('#Title').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else if (CKEDITOR.instances[ckEditorID].getData() == '' || CKEDITOR.instances[ckEditorID].getData() == null) {
        $('#cke_ckeExample').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }

    else {

        $('#ckeExample').val(CKEDITOR.instances[ckEditorID].getData());


        if (confirm("Do you want to submit")) {

            $('#interestinglinksbutton').attr("Type", " Submit");
            $('#interestinglinksbutton').attr("Type", " button");
            $('#interestinglinksbutton').text("Submit");

        }
    }
});






$(function () {
    $(".Editacademic").click(function () {
        
        var TeamDetailPostBackURL = '/Student/EditAcademics';
        $('#EditacademicsModal').modal('show');
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('id');
        
        $.ajax({
            type: "GET",
            url: TeamDetailPostBackURL,
            contentType: "application/json; charset=utf-8",
            data: { "Id": id },
            datatype: "json",
            success: function (data) {

                $('#Editacademics').html(data);

            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });



    $(".EditExtraCurricularActivites").click(function () {

        var TeamDetailPostBackURL = '/Student/EditExtraCurricularActivites';
        $('#EditExtraactivitesModal').modal('show');
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('id');

        $.ajax({
            type: "GET",
            url: TeamDetailPostBackURL,
            contentType: "application/json; charset=utf-8",
            data: { "Id": id },
            datatype: "json",
            success: function (data) {

                $('#EditExtraactivites').html(data);

            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });

    $(".EditLeadershipPositions").click(function () {

        var TeamDetailPostBackURL = '/Student/EditLeadershipPositions';
        $('#EditLeadershipModal').modal('show');
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('id');

        $.ajax({
            type: "GET",
            url: TeamDetailPostBackURL,
            contentType: "application/json; charset=utf-8",
            data: { "Id": id },
            datatype: "json",
            success: function (data) {

                $('#EditEditLeadership').html(data);

            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });




    $(".Edittestreport").click(function () {

        var TeamDetailPostBackURL = '/Student/Edittestreport';
        $('#EdittestreportModal').modal('show');
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('id');

        $.ajax({
            type: "GET",
            url: TeamDetailPostBackURL,
            contentType: "application/json; charset=utf-8",
            data: { "Id": id },
            datatype: "json",
            success: function (data) {

                $('#EditTestreport').html(data);

            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });


    $(".anchorDetail1").click(function () {

        var TeamDetailPostBackURL = '/Student/ViewUploadEssay';
        $('#viewModal').modal('show');
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('id');

        $.ajax({
            type: "GET",
            url: TeamDetailPostBackURL,
            contentType: "application/json; charset=utf-8",
            data: { "Id": id },
            datatype: "json",
            success: function (data) {

                $('#myModalContent').html(data);

            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });

    $(".anchorDetail2").click(function () {
        var TeamDetailPostBackURL = '/Student/ViewMilestone';
        $('#defineModal').modal('show');
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('id');

        $.ajax({
            type: "GET",
            url: TeamDetailPostBackURL,
            contentType: "application/json; charset=utf-8",
            data: { "Id": id },
            datatype: "json",
            success: function (data) {

                $('#viewModal').html(data);

            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });

});




$('#Addcallnote').click(function () {
    var html = '';
    html += '';
    html += '<div class="clone">';
    html += '<input class="form-control Date text-box single-line" data-val="true" data-val-required="The Date field is required." id="Date" name="Date" type="text" value="">';
    html += '<input class="form-control Academics text-box single-line" data-val="true" data-val-required="The Academics field is required." id="Academics" name="Academics" type="text" value="">';
    html += '<input class="form-control Testing text-box single-line" data-val="true" data-val-required="The Testing field is required." id="Testing" name="Testing" type="text" value="">';
    html += '<input class="form-control College text-box single-line" data-val="true" data-val-required="The College field is required." id="College" name="College" type="text" value="">';
    html += '<input class="form-control Project1 text-box single-line" data-val="true" data-val-required="The Project1 field is required." id="Project1" name="Project1" type="text" value="">';
    html += '<input class="form-control Project2 text-box single-line" data-val="true" data-val-required="The Project2 field is required." id="Project2" name="Project2" type="text" value="">';
    html += '<input class="form-control ExtraCurricular text-box single-line" data-val="true" data-val-required="The ExtraCurricular field is required." id="ExtraCurricular" name="ExtraCurricular" type="text" value="">';
    html += '<input class="form-control Misc text-box single-line" data-val="true" data-val-required="The Misc field is required." id="Misc" name="Misc" type="text" value="">';
    html += '<input class="form-control Suggestedcourse text-box single-line" data-val="true" data-val-required="The Suggestedcourse field is required." id="Suggestedcourse" name="Suggestedcourse" type="text" value="">';
    html += '<button type="button" href="#" class="remove btn btn-danger">Remove</button>';
    html += '</div> ';



    $('.sameclone').append(html);
});
$(document).on("click", '.remove', function (event) {
    alert('');
    $(this).parents(".clone").remove();

});


$(document).on("click", "#btnSave", function () {


    var CallNoteDetail = new Array();

    if ($('#CallNoteDate').val() == '') {
        $('#CallNoteDate').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }

    else if ($('#Academics').val() == '') {
        $('#Academics').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else if ($('#Testing').val() == '') {
        $('#Testing').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else if ($('#College').val() == '') {
        $('#College').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else if (CKEDITOR.instances[ckEditorID].getData() == '' || CKEDITOR.instances[ckEditorID].getData() == null) {
        $('#cke_Project1').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else if ($('#Project2').val() == '') {
        $('#Project2').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else if ($('#ExtraCurricular').val() == '') {
        $('#ExtraCurricular').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else if ($('#Misc').val() == '') {
        $('#Misc').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else if ($('#Suggestedcourse').val() == '') {
        $('#Suggestedcourse').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }
    else {

        if (confirm('Do you want to record call note')) {

            $('#Project1').val(CKEDITOR.instances[ckEditorID].getData());
            $(".clone").each(function () {


                let CallNoteDate = $(this).find('.CallNoteDate').val();

                let Academics = $(this).find('.Academics').val();
                let Testing = $(this).find('.Testing').val();
                let College = $(this).find('.College').val();
                let Project1 = $(this).find('.Project1').val();
                let Project2 = $(this).find('.Project2').val();
                let ExtraCurricular = $(this).find('.ExtraCurricular').val();
                let Misc = $(this).find('.Misc').val();
                let Suggestedcourse = $(this).find('.Suggestedcourse').val();



                CallNoteDetail.push({

                    CallNoteDate: CallNoteDate,
                    Academics: Academics,
                    Testing: Testing,
                    College: College,
                    Project1: Project1,
                    Project2: Project2,
                    ExtraCurricular: ExtraCurricular,
                    Misc: Misc,
                    Suggestedcourse: Suggestedcourse

                });






            });


            $.ajax({
                type: "POST",
                url: "/Student/InsertCallNote",
                data: JSON.stringify(CallNoteDetail),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    //alert(r + " record(s) inserted.");
                    window.location.href = '/Student/callnote';
                }
            });
        }
    }
});
//$(document).on("change", '#Date', function (event)
//{

//    alert('nik');

//});
//$("#Date")..(function (event) {



//    if ($(this).val() == '') {
//        $('#Date').addClass('nik');
//        return false;
//    }
//    else {
//        $('#Date').removeClass('nik');
//        return true;
//    }

//});
$("#Academics").focusout(function (event) {



    if ($(this).val() == '') {
        $('#Academics').addClass('nik');
        return false;
    }
    else {
        $('#Academics').removeClass('nik');
        return true;
    }

});
$("#Testing").focusout(function (event) {



    if ($(this).val() == '') {
        $('#Testing').addClass('nik');
        return false;
    }
    else {
        $('#Testing').removeClass('nik');
        return true;
    }

});
$("#College").focusout(function (event) {



    if ($(this).val() == '') {
        $('#College').addClass('nik');
        return false;
    }
    else {
        $('#College').removeClass('nik');
        return true;
    }

});
$("#Project1").focusout(function (event) {



    if ($(this).val() == '') {
        $('#Project1').addClass('nik');
        return false;
    }
    else {
        $('#Project1').removeClass('nik');
        return true;
    }

});
$("#Project2").focusout(function (event) {



    if ($(this).val() == '') {
        $('#Project2').addClass('nik');
        return false;
    }
    else {
        $('#Project2').removeClass('nik');
        return true;
    }

});
$("#ExtraCurricular").focusout(function (event) {



    if ($(this).val() == '') {
        $('#ExtraCurricular').addClass('nik');
        return false;
    }
    else {
        $('#ExtraCurricular').removeClass('nik');
        return true;
    }

});
$("#Misc").focusout(function (event) {



    if ($(this).val() == '') {
        $('#Misc').addClass('nik');
        return false;
    }
    else {
        $('#Misc').removeClass('nik');
        return true;
    }

});
$("#Suggestedcourse").focusout(function (event) {



    if ($(this).val() == '') {
        $('#Suggestedcourse').addClass('nik');
        return false;
    }
    else {
        $('#Suggestedcourse').removeClass('nik');
        return true;
    }

});


function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
}


$("#CountryList").change(function () {
    $("#CityList").html('<option value="">Select</option>');

    //$("#CityList").empty();
    //$("#CourseList").empty();
    //   debugger;
    //var k=  $("#CountryList").val();
    if ($("#CountryList").val() != 'undefined' && $("#CountryList").val() != null && $("#CountryList").val() != '') {


        $.ajax({
            type: 'POST',
            url: '/Student/GetStates', // we are calling json method  

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
$("#CollegeList").change(function () {
    //$("#CourseList").html('<option value="">Select</option>');
    //if ($("#CollegeList").val() != 'undefined' && $("#CollegeList").val() != null && $("#CollegeList").val() != '') {
    //    $.ajax({
    //        type: 'POST',
    //        url: '/Student/GetCourse', // we are calling json method  

    //        dataType: 'json',

    //        data: { Id: $("#CollegeList").val() },


    //        success: function (states) {
    //            // states contains the JSON formatted list  
    //            // of states passed from the controller  
    //            //  $("#CourseList").append('<option value="Select">Select</option>');
    //            $("#CourseList").html('<option value="">Select</option>');
    //            $.each(states, function (i, state) {
    //                $("#CourseList").append('<option value="' + state.Value + '">' +
    //                    state.Text + '</option>');
    //                // here we are adding option for States  

    //            });
    //        },
    //        error: function (ex) {
    //            alert('Failed to retrieve states.' + ex);
    //        }
    //    });
    //}
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
$("#ApplicationThrough").change(function () {

    filter();
    return false;
});




$('#milestoneupload').click(function () {
    if ($('#salaryslip').val() == '') {
        $('#salaryslip').addClass('nik');
        // $('#err').text('Please Enter Your Class');
        return false;
    }

    else {

        if (confirm("Do you want to submit")) {

            $('#milestoneupload').attr("Type", " Submit");
            $('#milestoneupload').attr("Type", " button");
            $('#milestoneupload').text("Submit");

        }
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
    $('#CollegeList').val('');
    $('#CountryList').val('');
    $('#CityList').val('');
    $('#CourseList').val('');
    $('#ApplicationThrough').val('');
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
    var college = $('#CollegeList').val();
    var country = $('#CountryList').val();
    var city = $('#CityList').val();
    var course = $('#CourseList').val();
    var application = $('#ApplicationThrough').val();

    var TeamDetailPostBackURL = '/Student/applicationview2';



    $.ajax({
        type: "GET",
        url: TeamDetailPostBackURL,
        contentType: "application/json; charset=utf-8",
        data: { "college": college, "country": country, "city": city, "course": course, "ranklist": checklist, "shortlist": shortlist, "application": application, },
        datatype: "json",
        success: function (data) {

            $('#selectedContent').html(data);
            $('#selectedContent').attr('style="height:500px;"');
            checklist = '';

        },
        error: function () {
            alert("Dynamic content load failed.");
        }
    });


}


