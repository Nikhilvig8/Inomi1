$(document).ready(function () {

    $('#StudentDetails').DataTable({
        "bSort": false
    });


    $("#btnAdmSubmit").click(function () {

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
        var Id = $('#AcademicsId').val();
        var Student = $('#ddlStudent').val();
        var Class = $('#txtClass').val();
        var Year = $('#txtYear').val();
        var Board = $('#txtBoard').val();
        var School = $('#txtSchool').val();
        var OverallGPA = $('#txtOverallGPA').val();
        var Transcript = $('#txtTranscript').val();

        if (Student == '' || Student == null) {
            $('#lblStudent').text('Please choose student.');
            $('#lblStudent').show();
            $('#ddlStudent').focus();
            return;
        }
        else {
            $('#lblStudent').text('');
            $('#lblStudent').hide();
        }

        if (Class == '' || Class == null) {
            $('#lblClass').text('Please enter class.');
            $('#lblClass').show();
            $('#txtClass').focus();
            return;
        }
        else {
            $('#lblClass').text('');
            $('#lblClass').hide();
        }


        if (Year == '' || Year == null) {
            $('#lblYear').text('Please enter year.');
            $('#lblYear').show();
            $('#txtYear').focus();
            return;
        }
        else {
            $('#lblYear').text('');
            $('#lblYear').hide();
        }

        if (Board == '' || Board == null) {
            $('#lblBoard').text('Please enter board.');
            $('#lblBoard').show();
            $('#txtBoard').focus();
            return;
        } else {
            $('#lblBoard').text('');
            $('#lblBoard').hide();
        }

        if (School == '' || School == null) {
            $('#lblSchool').text('Please enter school.');
            $('#lblSchool').show();
            $('#txtSchool').focus();
            return;
        } else {
            $('#lblSchool').text('');
            $('#lblSchool').hide();
        }

        if (OverallGPA == '' || OverallGPA == null) {
            $('#lblOverallGPA').text('Please enter over all GPA.');
            $('#lblOverallGPA').show();
            $('#txtOverallGPA').focus();
            return;
        } else {
            $('#lblOverallGPA').text('');
            $('#lblOverallGPA').hide();
        }
        if (Transcript == '' || Transcript == null) {
            $('#lblTranscript').text('Please choose transcript.');
            $('#lblTranscript').show();
            $('#txtTranscript').focus();
            return;
        } else {
            $('#lblTranscript').text('');
            $('#lblTranscript').hide();
        }

        Id = escapeHtml(Id);
        Student = escapeHtml(Student);
        Class = escapeHtml(Class);
        Year = escapeHtml(Year);
        Board = escapeHtml(Board);
        School = escapeHtml(School);
        OverallGPA = escapeHtml(OverallGPA);
        Transcript = escapeHtml(Transcript);


        data.push({
            Id: Id,
            Student: Student,
            Class: Class,
            Year: Year,
            Board: Board,
            School: School,
            OverallGPA: OverallGPA,
            Transcript: Transcript,

        });

        var fileUpload = $("input[name='FichierTranscript']").get(0);
        var files = fileUpload.files;

        var fileData = new FormData();

        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        $("#btnAdmSubmit").text('Processing...');
        $("#btnAdmSubmit").attr("disabled", true);
        $("#btnAdmBack").attr("disabled", true);
        var json = JSON.stringify(data);
        $.ajax
            ({
                url: "/StudentDetails/InsertAcedemiscDetails?json=" + json + "&Id=" + Id + "",
                contentType: false,
                processData: false,
                type: "POST",
                data: fileData,
                success: function (drr) {
                    $("#btnAdmSubmit").text('Save');
                    $("#btnAdmSubmit").attr("disabled", false);
                    $("#btnAdmBack").attr("disabled", false);

                    var tab = "AcedemicsGrid";
                    var StrMain = "Record has been save successfully.";
                    var url = "/StudentDetails/StudentDetails/?StrMain=" + StrMain + "&tab=" + tab;
                    window.location.href = url;
                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    });

    $('#StudentDetails').on('click', '#delete', function () {
        var id = $(this).closest("tr").find('td:eq(6)').text();
        var result = confirm("Do you Want to delete?");
        if (result) {
            $.ajax
                ({
                    url: "/StudentDetails/DeleteAcedemiscDetails?id=" + id + "",
                    type: "Get",
                    success: function (drr) {
                        var tab = "AcedemicsGrid";
                        var StrMain = "Record has been delete successfully.";
                        var url = "/StudentDetails/StudentDetails/?StrMain=" + StrMain + "&tab=" + tab;
                        window.location.href = url;
                    },
                    Error: function (err) {
                        Console.log(err);
                    }
                });

        }
    });

    $('#StudentDetails').on('click', '#edit', function () {
        $('#AcedemicsGrid').hide();
        $('#academics').show();
        $('#btnAdmSubmit').text('Update');
        var id = $(this).closest("tr").find('td:eq(6)').text();
        $.ajax
            ({
                url: "/StudentDetails/EditAcedemiscDetails?id=" + id + "",
                type: "Get",
                success: function (data) {

                    var item = JSON.parse(data);

                    for (var i = 0; i < item.length; i++) {
                        $('#AcademicsId').val(item[0].Id)
                        $('#ddlStudent').val(item[0].StudentID)
                        $('#txtClass').val(item[0].Class)
                        $('#txtYear').val(item[0].Year)
                        $('#txtBoard').val(item[0].Board)
                        $('#txtSchool').val(item[0].School)
                        $('#txtOverallGPA').val(item[0].OverAllGPAMark)
                        $('#txtTranscript').val(item[0].Transcript)

                    }
                },
                Error: function (err) {
                    Console.log(err);
                }
            });

    });

    $('#academics-tab').click(function () {
        $('#AcedemicsGrid').show();
        $('#academics').hide();

        $('#TestScoreGrid').hide();
        $('#Test-Reports').hide();

        $('#ActivitiesGrid').hide();
        $('#ECA').hide();

        $('#LeaderShipGrid').hide();
        $('#leadership').hide();
        $('#PathfinderGrid').hide();
        $('#Pathfinder').hide();
    });

    $('#AddNew').click(function () {
        $('#academics').show();
        $('#AcedemicsGrid').hide();

        $('#TestScoreGrid').hide();
        $('#Test-Reports').hide();


        $('#ActivitiesGrid').hide();
        $('#ECA').hide();

        $('#LeaderShipGrid').hide();
        $('#leadership').hide();
        $('#PathfinderGrid').hide();
        $('#Pathfinder').hide();

        $('#AcademicsId').val('')
        $('#ddlStudent').val('')
        $('#txtClass').val('')
        $('#txtYear').val('')
        $('#txtBoard').val('')
        $('#txtSchool').val('')
        $('#txtOverallGPA').val('')
        $('#txtTranscript').val('')

    });

    $('#btnAdmBack').click(function () {
        $('#AcedemicsGrid').show();
        $('#academics').hide();

        $('#TestScoreGrid').hide();
        $('#Test-Reports').hide();


        $('#ActivitiesGrid').hide();
        $('#ECA').hide();

        $('#LeaderShipGrid').hide();
        $('#leadership').hide();
        $('#PathfinderGrid').hide();
        $('#Pathfinder').hide();
      
    });

});


$(window).on('load', function () {
    setTimeout(function () { $("#msgErroStudent").hide(); }, 3000);
});