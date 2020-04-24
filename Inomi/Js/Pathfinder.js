$(document).ready(function () {

    $('#PathfinderDetails').DataTable({
        "bSort": false
    });


    $("#btnPathfinder").click(function () {

        function escapeHtml(emailBody) {
            return emailBody
                .replace(/&/g, "~amp;")
                .replace(/</g, "~lt;")
                .replace(/>/g, "~gt;")
                .replace(/"/g, "~quot;")
                .replace(/'/g, "~#039;")
                .replace(/#/g, "~040;");
        }

        let data = [];
        
        var Student = $('#ddlPFStudent').val();
        var Pathfinder = $('#txtPathfinder').val();

        if (Student == '' || Student == null) {
            $('#lblFPStudent').text('Please choose student.');
            $('#lblFPStudent').show();
            $('#ddlPFStudent').focus();
            return;
        }
        else {
            $('#lblFPStudent').text('');
            $('#lblFPStudent').hide();
        }
        if (Pathfinder == '' || Pathfinder == null) {
            $('#lblPathfinder').text('Please choose picture.');
            $('#lblPathfinder').show();
            $('#lblPathfinder').focus();
            return;
        } else {
            $('#lblPathfinder').text('');
            $('#lblPathfinder').hide();
        }

        Student = escapeHtml(Student);
        Pathfinder = escapeHtml(Pathfinder);

        var Id = Student;

        data.push({
            Student: Student,
            Pathfinder: Pathfinder

        });

        var fileUpload = $("input[name='FichierPathfinder']").get(0);
        var files = fileUpload.files;

        var fileData = new FormData();

        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        $("#btnPathfinder").text('Processing...');
        $("#btnPathfinder").attr("disabled", true);
        $("#btnPfBack").attr("disabled", true);
        var json = JSON.stringify(data);
        $.ajax
            ({
                url: "/StudentDetails/InsertPathfinderDetails?json=" + json + "&Id=" + Id + "",
                contentType: false,
                processData: false,
                type: "POST",
                data: fileData,
                success: function (drr) {
                    $("#btnPathfinder").text('Processing...');
                    $("#btnPathfinder").attr("disabled", true);
                    $("#btnPfBack").attr("disabled", true);

                    var tab = "PathfinderGrid";
                    var StrMain = "Record has been save successfully.";
                    var url = "/StudentDetails/StudentDetails/?StrMain=" + StrMain + "&tab=" + tab;
                    window.location.href = url;
                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    });

    //$('#StudentDetails').on('click', '#delete', function () {
    //    var id = $(this).closest("tr").find('td:eq(6)').text();
    //    var result = confirm("Do you Want to delete?");
    //    if (result) {
    //        $.ajax
    //            ({
    //                url: "/StudentDetails/DeleteAcedemiscDetails?id=" + id + "",
    //                type: "Get",
    //                success: function (drr) {
    //                    var tab = "AcedemicsGrid";
    //                    var StrMain = "Record has been delete successfully.";
    //                    var url = "/StudentDetails/StudentDetails/?StrMain=" + StrMain + "&tab=" + tab;
    //                    window.location.href = url;
    //                },
    //                Error: function (err) {
    //                    Console.log(err);
    //                }
    //            });

    //    }
    //});

    //$('#StudentDetails').on('click', '#edit', function () {
    //    $('#AcedemicsGrid').hide();
    //    $('#academics').show();
    //    $('#btnAdmSubmit').text('Update');
    //    var id = $(this).closest("tr").find('td:eq(6)').text();
    //    $.ajax
    //        ({
    //            url: "/StudentDetails/EditAcedemiscDetails?id=" + id + "",
    //            type: "Get",
    //            success: function (data) {

    //                var item = JSON.parse(data);

    //                for (var i = 0; i < item.length; i++) {
    //                    $('#AcademicsId').val(item[0].Id)
    //                    $('#ddlStudent').val(item[0].StudentID)
    //                    $('#txtClass').val(item[0].Class)
    //                    $('#txtYear').val(item[0].Year)
    //                    $('#txtBoard').val(item[0].Board)
    //                    $('#txtSchool').val(item[0].School)
    //                    $('#txtOverallGPA').val(item[0].OverAllGPAMark)
    //                    $('#txtTranscript').val(item[0].Transcript)

    //                }
    //            },
    //            Error: function (err) {
    //                Console.log(err);
    //            }
    //        });

    //});

    $('#Pathfinder-tab').click(function () {
        $('#AcedemicsGrid').hide();
        $('#academics').hide();

        $('#TestScoreGrid').hide();
        $('#Test-Reports').hide();

        $('#ActivitiesGrid').hide();
        $('#ECA').hide();

        $('#LeaderShipGrid').hide();
        $('#leadership').hide();
        $('#PathfinderGrid').show();
        $('#Pathfinder').hide();
    });

    $('#AddPFNew').click(function () {
        $('#AcedemicsGrid').hide();
        $('#academics').hide();

        $('#TestScoreGrid').hide();
        $('#Test-Reports').hide();


        $('#ActivitiesGrid').hide();
        $('#ECA').hide();

        $('#LeaderShipGrid').hide();
        $('#leadership').hide();

        $('#PathfinderGrid').hide();
        $('#Pathfinder').show();

        $('#ddlPFStudent').val('');
        $('#txtPathfinder').val('');

    });


    $('#btnAdmBack').click(function () {
        $('#AcedemicsGrid').hide();
        $('#academics').hide();

        $('#TestScoreGrid').hide();
        $('#Test-Reports').hide();


        $('#ActivitiesGrid').hide();
        $('#ECA').hide();

        $('#LeaderShipGrid').hide();
        $('#leadership').hide();

        $('#PathfinderGrid').show();
        $('#Pathfinder').hide();

    });

});


$(window).on('load', function () {
    setTimeout(function () { $("#msgErroStudent").hide(); }, 2000);
});