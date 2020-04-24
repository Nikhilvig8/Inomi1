$(document).ready(function () {

    $('#TestScoreDetails').DataTable({
        "bSort": false
    });


    $("#btnTestSubmit").click(function () {

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
        var Id = $('#TestNameId').val();
        var Student = $('#ddlTestStudent').val();
        var TestName = $('#txtTestName').val();
        var Score = $('#txtScore').val();

        if (Student == '' || Student == null) {
            $('#lblTestStudent').text('Please choose student.');
            $('#lblTestStudent').show();
            $('#ddlTestStudent').focus();
            return;
        }
        else {
            $('#lblTestStudent').text('');
            $('#lblTestStudent').hide();
        }

        if (TestName == '' || TestName == null) {
            $('#lblTestName').text('Please enter test name.');
            $('#lblTestName').show();
            $('#txtTestName').focus();
            return;
        }
        else {
            $('#lblTestName').text('');
            $('#lblTestName').hide();
        }

        if (Score == '' || Score == null) {
            $('#lblScore').text('Please enter score.');
            $('#lblScore').show();
            $('#txtScore').focus();
            return;
        }
        else {
            $('#lblScore').text('');
            $('#lblScore').hide();
        }

        Id = escapeHtml(Id);
        Student = escapeHtml(Student);
        TestName = escapeHtml(TestName);
        Score = escapeHtml(Score);

        data.push({
            Id: Id,
            Student: Student,
            TestName: TestName,
            Score: Score
        });

        $("#btnTestSubmit").text('Processing...');
        $("#btnTestSubmit").attr("disabled", true);
        $("#btnTestBack").attr("disabled", true);
        var json = JSON.stringify(data);
        $.ajax
            ({
                url: "/StudentDetails/InsertTestScoreDetails?json=" + json + "&Id=" + Id + "",
                type: "Get",
                success: function (drr) {
                    $("#btnTestSubmit").text('Save');
                    $("#btnTestSubmit").attr("disabled", false);
                    $("#btnTestBack").attr("disabled", false);

                    var tab = "TestScoreGrid";
                    var StrMain = "Record has been save successfully.";
                    var url = "/StudentDetails/StudentDetails/?StrMain=" + StrMain + "&tab=" + tab;
                    window.location.href = url;
                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    });

    $('#TestScoreDetails').on('click', '#delete', function () {
        var id = $(this).closest("tr").find('td:eq(3)').text();
        var result = confirm("Do you Want to delete?");
        if (result) {
            $.ajax
                ({
                    url: "/StudentDetails/DeleteTestScoreDetails?id=" + id + "",
                    type: "Get",
                    success: function (drr) {
                        var tab = "TestScoreGrid";
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

    $('#TestScoreDetails').on('click', '#edit', function () {
        $('#TestScoreGrid').hide();
        $('#Test-Reports').show();
        $('#btnTestSubmit').text('Update');
        var id = $(this).closest("tr").find('td:eq(3)').text();
        $.ajax
            ({
                url: "/StudentDetails/EditTestScoreDetails?id=" + id + "",
                type: "Get",
                success: function (data) {

                    var item = JSON.parse(data);

                    for (var i = 0; i < item.length; i++) {
                        $('#TestNameId').val(item[0].Id)
                        $('#ddlTestStudent').val(item[0].StudentID)
                        $('#txtTestName').val(item[0].TestName)
                        $('#txtScore').val(item[0].Score)

                    }
                },
                Error: function (err) {
                    Console.log(err);
                }
            });

    });

    $('#Test-Reports-tab').click(function () {
        $('#AcedemicsGrid').hide();
        $('#academics').hide();

        $('#TestScoreGrid').show();
        $('#Test-Reports').hide();

        $('#ActivitiesGrid').hide();
        $('#ECA').hide();

        $('#LeaderShipGrid').hide();
        $('#leadership').hide();
        $('#PathfinderGrid').hide();
        $('#Pathfinder').hide();
    });

    $('#btnTestAddNew').click(function () {
        $('#AcedemicsGrid').hide();
        $('#academics').hide();

        $('#TestScoreGrid').hide();
        $('#Test-Reports').show();

        $('#ActivitiesGrid').hide();
        $('#ECA').hide();

        $('#LeaderShipGrid').hide();
        $('#leadership').hide();

        $('#TestNameId').val('')
        $('#ddlTestStudent').val('')
        $('#txtTestName').val('')
        $('#txtScore').val('')
        $('#PathfinderGrid').hide();
        $('#Pathfinder').hide();

    });

    $('#btnTestBack').click(function () {
        $('#AcedemicsGrid').hide();
        $('#academics').hide();

        $('#TestScoreGrid').show();
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
    setTimeout(function () { $("#msgErroStudent").hide(); }, 2000);
});