$(document).ready(function () {

    $('#CourseDetails').DataTable({
        "bSort": false
    });

    $("#CourseSubmit").click(function () {

        function escapeHtml(emailBody) {
            return emailBody
                .replace(/&/g, "~amp;")
                .replace(/</g, "~lt;")
                .replace(/>/g, "~gt;")
                .replace(/"/g, "~quot;")
                .replace(/'/g, "~039;")
                .replace(/#/g, "~040;");
        }

        let data = [];

        var Id = $('#CourseId').val();
        var Name = $('#CourseName').val();
        var College = $('#CourseCollege').val();
        var Career = $('#CourseCareer').val();
        var Link = $('#CourseLink').val();

        if (Name == '' || Name == null) {
            $('#msgCourseName').text('Please enter course name.');
            $('#msgCourseName').show();
            $('#CourseName').focus();
            return;
        } else {
            $('#msgCourseName').text('');
            $('#msgCourseName').hide();
        }


        if (College == '' || College == null) {
            $('#msgCourseCollege').text('Please select College.');
            $('#msgCourseCollege').show();
            $('#CourseCollege').focus();
            return;
        } else {
            $('#msgCourseCollege').text('');
            $('#msgCourseCollege').hide();
        }


        if (Career == '' || Career == null) {
            $('#msgCourseCareer').text('Please enter Career.');
            $('#msgCourseCareer').show();
            $('#CourseCareer').focus();
            return;
        } else {
            $('#msgCourseCareer').text('');
            $('#msgCourseCareer').hide();
        }


        if (Link == '' || Link == null) {
            $('#msgCourseLink').text('Please enter Link.');
            $('#msgCourseLink').show();
            $('#CourseLink').focus();
            return;
        } else {
            $('#msgCourseLink').text('');
            $('#msgCourseLink').hide();
        }

        Id = escapeHtml(Id);
        Name = escapeHtml(Name);
        College = escapeHtml(College);
        Career = escapeHtml(Career);
        Link = escapeHtml(Link);

        data.push({
            Id: Id,
            Name: Name,
            College: College,
            Career: Career,
            Link: Link
        });

        $("#CourseSubmit").text('Processing...');
        $("#CourseSubmit").attr("disabled", true);
        $("#CourseBack").attr("disabled", true);

        var json = JSON.stringify(data);
        $.ajax
            ({
                url: "/AddNew/InsertCourseStatus?json=" + json + "&Id=" + Id + "",
                type: "Get",
                success: function (drr) {
                    $("#CourseSubmit").text('Save');
                    $("#CourseSubmit").attr("disabled", false);
                    $("#CourseBack").attr("disabled", false);

                    var tab = "CourseGrid";
                    var StrMain = "Record has been save successfully.";
                    var url = "/CounsellorAddNew/CounsellorAddNew/?StrMain=" + StrMain + "&tab=" + tab;
                    window.location.href = url;
                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    });

    $('#CourseDetails').on('click', '#delete', function () {
        var id = $(this).closest("tr").find('td:eq(4)').text();
        var result = confirm("Do you Want to delete?");
        if (result) {
            $.ajax
                ({
                    url: "/AddNew/DeleteCourseDetails?id=" + id + "",
                    type: "Get",
                    success: function (drr) {
                        var tab = "CourseGrid";
                        var StrMain = "Record has been delete successfully.";
                        var url = "/CounsellorAddNew/CounsellorAddNew/?StrMain=" + StrMain + "&tab=" + tab;
                        window.location.href = url;
                    },
                    Error: function (err) {
                        Console.log(err);
                    }
                });

        }
    });

    $('#CourseDetails').on('click', '#edit', function () {
        $('#CourseGrid').hide();
        $('#course').show();
        $('#CourseSubmit').text('Update');
        var id = $(this).closest("tr").find('td:eq(4)').text();
        $.ajax
            ({
                url: "/AddNew/EditCourseDetails?id=" + id + "",
                type: "Get",
                success: function (data) {

                    var item = JSON.parse(data);

                    for (var i = 0; i < item.length; i++) {
                        $('#CourseId').val(item[0].Id)
                        $('#CourseName').val(item[0].NameOfCourse)
                        $('#CourseCollege').val(item[0].College)
                        $('#CourseCareer').val(item[0].Career)
                        $('#CourseLink').val(item[0].Link)
                    }
                },
                Error: function (err) {
                    Console.log(err);
                }
            });

    });

    $('#coursetab').click(function () {
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
        $('#TaskGrid').hide();
        $('#task').hide();
        $('#CourseGrid').show();
        $('#course').hide();
        $('#TestGrid').hide();
        $('#test').hide();
        $('#FormatGrid').hide();
        $('#format').hide();

        $('#ProjectGrid').hide();
        $('#project').hide();
    });
    $('#CourseBack').click(function () {
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
        $('#TaskGrid').hide();
        $('#task').hide();
        $('#CourseGrid').show();
        $('#course').hide();
        $('#TestGrid').hide();
        $('#test').hide();
        $('#FormatGrid').hide();
        $('#format').hide();
        $('#ProjectGrid').hide();
        $('#project').hide();
    });
    $('#CourseAddNew').click(function () {
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
        $('#TaskGrid').hide();
        $('#task').hide();
        $('#CourseGrid').hide();
        $('#course').show();
        $('#TestGrid').hide();
        $('#test').hide();
        $('#FormatGrid').hide();
        $('#format').hide();
        $('#ProjectGrid').hide();
        $('#project').hide();

        $('#CourseSubmit').val('Save')
        $('#CourseId').val('')
        $('#CourseName').val('')
        $('#CourseCollege').val('')
        $('#CourseCareer').val('')
        $('#CourseLink').val('')
    });


});