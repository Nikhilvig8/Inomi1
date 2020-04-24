$(document).ready(function () {
    $('#TestDetails').DataTable({
        "bSort": false
    });

    $("#TestSubmit").click(function () {

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
        var Id = $('#TestId').val();
        var Test = $('#TestName').val();

        if (Test == '' || Test == null) {
            $('#msgTestName').text('Please enter Test.');
            $('#msgTestName').show();
            $('#TestName').focus();
            return;
        } else {
            $('#msgTestName').text('');
            $('#msgTestName').hide();
        }

        Id = escapeHtml(Id);
        Test = escapeHtml(Test);

        data.push({
            Id: Id,
            Test: Test
        });

        $("#TestSubmit").text('Processing...');
        $("#TestSubmit").attr("disabled", true);
        $("#TestBack").attr("disabled", true);

        var json = JSON.stringify(data);
        $.ajax
            ({
                url: "/AddNew/InsertTestStatus?json=" + json + "&Id=" + Id + "",
                type: "Get",
                success: function (drr) {
                    $("#TestSubmit").text('Save');
                    $("#TestSubmit").attr("disabled", false);
                    $("#TestBack").attr("disabled", false);

                    var tab = "TestGrid";
                    var StrMain = "Record has been save successfully.";
                    var url = "/AddNew/Student/?StrMain=" + StrMain + "&tab=" + tab;
                    window.location.href = url;
                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    });

    $('#TestDetails').on('click', '#delete', function () {
        var id = $(this).closest("tr").find('td:eq(1)').text();
        var result = confirm("Do you Want to delete?");
        if (result) {
            $.ajax
                ({
                    url: "/AddNew/DeleteTestDetails?id=" + id + "",
                    type: "Get",
                    success: function (drr) {
                        var tab = "TestGrid";
                        var StrMain = "Record has been delete successfully.";
                        var url = "/AddNew/Student/?StrMain=" + StrMain + "&tab=" + tab;
                        window.location.href = url;
                    },
                    Error: function (err) {
                        Console.log(err);
                    }
                });

        }
    });

    $('#TestDetails').on('click', '#edit', function () {
        $('#TestGrid').hide();
        $('#test').show();
        $('#TestSubmit').text('Update');
        var id = $(this).closest("tr").find('td:eq(1)').text();

        $.ajax
            ({
                url: "/AddNew/EditTestDetails?id=" + id + "",
                type: "Get",
                success: function (data) {

                    var item = JSON.parse(data);

                    for (var i = 0; i < item.length; i++) {
                        $('#TestId').val(item[0].Id)
                        $('#TestName').val(item[0].NameOfTest)
                    }
                },
                Error: function (err) {
                    Console.log(err);
                }
            });

    });


    $('#testtab').click(function () {
        $('#ProductGrid').hide();
        $('#product').hide();
        $('#CountryGrid').hide();
        $('#country').hide();
        $('#CollegeGrid').hide();
        $('#counsellor').hide();
        $('#student').hide();
        $('#studentGrid').hide();
        $('#CounsellorGrid').hide()
        $('#college').hide();
        $('#CourseGrid').hide();
        $('#course').hide();

        $('#TaskGrid').hide();
        $('#task').hide();

        $('#TestGrid').show();
        $('#test').hide();
        $('#FormatGrid').hide();
        $('#format').hide();
    });
    $('#TestBack').click(function () {
        $('#ProductGrid').hide();
        $('#product').hide();
        $('#CountryGrid').hide();
        $('#country').hide();
        $('#CollegeGrid').hide();
        $('#counsellor').hide();
        $('#student').hide();
        $('#studentGrid').hide();
        $('#CounsellorGrid').hide()
        $('#college').hide();
        $('#CourseGrid').hide();
        $('#course').hide();
        $('#TaskGrid').hide();
        $('#task').hide();

        $('#TestGrid').show();
        $('#test').hide();
        $('#FormatGrid').hide();
        $('#format').hide();

    });
    $('#TestAddNew').click(function () {
        $('#ProductGrid').hide();
        $('#product').hide();
        $('#CountryGrid').hide();
        $('#country').hide();
        $('#college').hide();
        $('#student').hide();
        $('#studentGrid').hide();
        $('#CounsellorGrid').hide();
        $('#CollegeGrid').hide();
        $('#counsellor').hide();
        $('#CourseGrid').hide();
        $('#course').hide();
        $('#TaskGrid').hide();
        $('#task').hide();

        $('#TestGrid').hide();
        $('#test').show();

        $('#FormatGrid').hide();
        $('#format').hide();
        $('#TestSubmit').val('Save')
        $('#TestId').val('')
        $('#TestName').val('')
    });
});