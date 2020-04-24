$(document).ready(function () {

    $('#ActivitiesDetails').DataTable({
        "bSort": false
    });


    $("#btnActivitiesSubmit").click(function () {

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
        var Id = $('#ActivitiesId').val();
        var Student = $('#ddlActivitiesStudent').val();
        var Activities = $('#txtActivities').val();
        var Achievement = $('#txtAchievement').val();
        var Details = $('#txtDetails').val();

        if (Student == '' || Student == null) {
            $('#lblActivitiesStudent').text('Please choose student.');
            $('#lblActivitiesStudent').show();
            $('#ddlActivitiesStudent').focus();
            return;
        }
        else {
            $('#lblActivitiesStudent').text('');
            $('#lblActivitiesStudent').hide();
        }

        if (Activities == '' || Activities == null) {
            $('#lblActivities').text('Please enter activities.');
            $('#lblActivities').show();
            $('#txtActivities').focus();
            return;
        }
        else {
            $('#lblActivities').text('');
            $('#lblActivities').hide();
        }

        if (Achievement == '' || Achievement == null) {
            $('#lblAchievement').text('Please enter achievement.');
            $('#lblAchievement').show();
            $('#txtAchievement').focus();
            return;
        }
        else {
            $('#lblAchievement').text('');
            $('#lblAchievement').hide();
        }

        if (Details == '' || Details == null) {
            $('#lblDetails').text('Please enter details.');
            $('#lblDetails').show();
            $('#txtDetails').focus();
            return;
        }
        else {
            $('#lblDetails').text('');
            $('#lblDetails').hide();
        }

        Id = escapeHtml(Id);
        Student = escapeHtml(Student);
        Activities = escapeHtml(Activities);
        Achievement = escapeHtml(Achievement);
        Details = escapeHtml(Details);

        data.push({
            Id: Id,
            Student: Student,
            Activities: Activities,
            Achievement: Achievement,
            Details: Details
        });

        $("#btnActivitiesSubmit").text('Processing...');
        $("#btnActivitiesSubmit").attr("disabled", true);
        $("#btnActivitiesBack").attr("disabled", true);
        var json = JSON.stringify(data);
        $.ajax
            ({
                url: "/StudentDetails/InsertActivitiesDetails?json=" + json + "&Id=" + Id + "",
                type: "Get",
                success: function (drr) {
                    $("#btnActivitiesSubmit").text('Save');
                    $("#btnActivitiesSubmit").attr("disabled", false);
                    $("#btnActivitiesBack").attr("disabled", false);

                    var tab = "ActivitiesGrid";
                    var StrMain = "Record has been save successfully.";
                    var url = "/StudentDetails/StudentDetails/?StrMain=" + StrMain + "&tab=" + tab;
                    window.location.href = url;
                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    });

    $('#ActivitiesDetails').on('click', '#delete', function () {
        var id = $(this).closest("tr").find('td:eq(4)').text();
        var result = confirm("Do you Want to delete?");
        if (result) {
            $.ajax
                ({
                    url: "/StudentDetails/DeleteActivitiesDetails?id=" + id + "",
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

    $('#ActivitiesDetails').on('click', '#edit', function () {
        $('#ActivitiesGrid').hide();
        $('#ECA').show();
        $('#btnActivitiesSubmit').text('Update');
        var id = $(this).closest("tr").find('td:eq(4)').text();
        $.ajax
            ({
                url: "/StudentDetails/EditActivitiesDetails?id=" + id + "",
                type: "Get",
                success: function (data) {

                    var item = JSON.parse(data);

                    for (var i = 0; i < item.length; i++) {
                        $('#ActivitiesId').val(item[0].Id)
                        $('#ddlActivitiesStudent').val(item[0].StudentID)
                        $('#txtActivities').val(item[0].Activities)
                        $('#txtAchievement').val(item[0].Achievements)
                        $('#txtDetails').val(item[0].Details)


                    }
                },
                Error: function (err) {
                    Console.log(err);
                }
            });

    });

    $('#ECA-tab').click(function () {
        $('#AcedemicsGrid').hide();
        $('#academics').hide();

        $('#TestScoreGrid').hide();
        $('#Test-Reports').hide();

        $('#ActivitiesGrid').show();
        $('#ECA').hide();

        $('#LeaderShipGrid').hide();
        $('#leadership').hide();
        $('#PathfinderGrid').hide();
        $('#Pathfinder').hide();
    });

    $('#btnActivitiesAddNew').click(function () {
        $('#AcedemicsGrid').hide();
        $('#academics').hide();

        $('#TestScoreGrid').hide();
        $('#Test-Reports').hide();

        $('#ActivitiesGrid').hide();
        $('#ECA').show();

        $('#LeaderShipGrid').hide();
        $('#leadership').hide();

        $('#ActivitiesId').val('')
        $('#ddlActivitiesStudent').val('')
        $('#txtActivities').val('')
        $('#txtAchievement').val('')
        $('#txtDetails').val('')
        $('#PathfinderGrid').hide();
        $('#Pathfinder').hide();

    });

    $('#btnActivitiesBack').click(function () {
        $('#AcedemicsGrid').hide();
        $('#academics').hide();

        $('#TestScoreGrid').hide();
        $('#Test-Reports').hide();

        $('#ActivitiesGrid').show();
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