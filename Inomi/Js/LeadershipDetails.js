$(document).ready(function () {

    $('#LeadershipDetails').DataTable({
        "bSort": false
    });


    $("#btnLeadershipSubmit").click(function () {

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
        var Id = $('#LeadershipId').val();
        var Student = $('#ddlLeadershipStudent').val();
        var Leadership = $('#txtLeadership').val();


        if (Student == '' || Student == null) {
            $('#lblActivitiesStudent').text('Please choose student.');
            $('#lblActivitiesStudent').show();
            $('#ddlLeadershipStudent').focus();
            return;
        }
        else {
            $('#lblActivitiesStudent').text('');
            $('#lblActivitiesStudent').hide();
        }

        if (Leadership == '' || Leadership == null) {
            $('#lblLeadership').text('Please enter leadership.');
            $('#lblLeadership').show();
            $('#txtLeadership').focus();
            return;
        }
        else {
            $('#lblLeadership').text('');
            $('#lblLeadership').hide();
        }


        Id = escapeHtml(Id);
        Student = escapeHtml(Student);
        Leadership = escapeHtml(Leadership);

        data.push({
            Id: Id,
            Student: Student,
            Leadership: Leadership

        });

        $("#btnLeadershipSubmit").text('Processing...');
        $("#btnLeadershipSubmit").attr("disabled", true);
        $("#btnLeadershipBack").attr("disabled", true);
        var json = JSON.stringify(data);
        $.ajax
            ({
                url: "/StudentDetails/InsertLeadershipDetails?json=" + json + "&Id=" + Id + "",
                type: "Get",
                success: function (drr) {
                    $("#btnLeadershipSubmit").text('Save');
                    $("#btnLeadershipSubmit").attr("disabled", false);
                    $("#btnLeadershipBack").attr("disabled", false);

                    var tab = "LeadershipGrid";
                    var StrMain = "Record has been save successfully.";
                    var url = "/StudentDetails/StudentDetails/?StrMain=" + StrMain + "&tab=" + tab;
                    window.location.href = url;
                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    });

    $('#LeadershipDetails').on('click', '#delete', function () {
        var id = $(this).closest("tr").find('td:eq(2)').text();
        var result = confirm("Do you Want to delete?");
        if (result) {
            $.ajax
                ({
                    url: "/StudentDetails/DeleteLeadershipDetails?id=" + id + "",
                    type: "Get",
                    success: function (drr) {
                        var tab = "LeadershipGrid";
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

    $('#LeadershipDetails').on('click', '#edit', function () {
        $('#LeaderShipGrid').hide();
        $('#leadership').show();
        $('#btnLeadershipSubmit').text('Update');
        var id = $(this).closest("tr").find('td:eq(2)').text();
        $.ajax
            ({
                url: "/StudentDetails/EditLeadershipDetails?id=" + id + "",
                type: "Get",
                success: function (data) {

                    var item = JSON.parse(data);

                    for (var i = 0; i < item.length; i++) {
                        $('#LeadershipId').val(item[0].Id)
                        $('#ddlLeadershipStudent').val(item[0].StudentID)
                        $('#txtLeadership').val(item[0].Position)


                    }
                },
                Error: function (err) {
                    Console.log(err);
                }
            });

    });

    $('#leadership-tab').click(function () {
        $('#AcedemicsGrid').hide();
        $('#academics').hide();

        $('#TestScoreGrid').hide();
        $('#Test-Reports').hide();

        $('#ActivitiesGrid').hide();
        $('#ECA').hide();

        $('#LeaderShipGrid').show();
        $('#leadership').hide();
        $('#PathfinderGrid').hide();
        $('#Pathfinder').hide();
    });

    $('#btnLeadershipAddNew').click(function () {
        $('#AcedemicsGrid').hide();
        $('#academics').hide();

        $('#TestScoreGrid').hide();
        $('#Test-Reports').hide();

        $('#ActivitiesGrid').hide();
        $('#ECA').hide();

        $('#LeaderShipGrid').hide();
        $('#leadership').show();

        $('#LeadershipId').val('')
        $('#ddlLeadershipStudent').val('')
        $('#txtLeadership').val('')
        $('#PathfinderGrid').hide();
        $('#Pathfinder').hide();
    });

    $('#btnLeadershipBack').click(function () {
        $('#AcedemicsGrid').hide();
        $('#academics').hide();

        $('#TestScoreGrid').hide();
        $('#Test-Reports').hide();

        $('#ActivitiesGrid').hide();
        $('#ECA').hide();

        $('#LeaderShipGrid').show();
        $('#leadership').hide();
        $('#PathfinderGrid').hide();
        $('#Pathfinder').hide();
    });

});


$(window).on('load', function () {
    setTimeout(function () { $("#msgErroStudent").hide(); }, 3000);
});