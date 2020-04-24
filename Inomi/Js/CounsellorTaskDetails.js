$(document).ready(function () {
    $('#TaskDetails').DataTable({
        "bSort": false
    });

    $("#TaskSubmit").click(function () {

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
        var Id = $('#TaskId').val();
        var Counsellor = $('#MddlStudent').val();
        var Task = $('#Task').val();
        var Deadline = $('#TaskDeadline').val();

        if (Counsellor == '' || Counsellor == null) {
            $('#msgTaskCounsellor').text('Please select student.');
            $('#msgTaskCounsellor').show();
            $('#TaskCounsellor').focus();
            return;
        } else {
            $('#msgTaskCounsellor').text('');
            $('#msgTaskCounsellor').hide();
        }


        if (Task == '' || Task == null) {
            $('#msgTask').text('Please enter Task.');
            $('#msgTask').show();
            $('#Task').focus();
            return;
        } else {
            $('#msgTask').text('');
            $('#msgTask').hide();
        }


        if (Deadline == '' || Deadline == null) {
            $('#msgTaskDeadline').text('Please enter deadline date.');
            $('#msgTaskDeadline').show();
            $('#TaskDeadline').focus();
            return;
        } else {
            $('#msgTaskDeadline').text('');
            $('#msgTaskDeadline').hide();
        }

        Id = escapeHtml(Id);
        Counsellor = escapeHtml(Counsellor);
        Task = escapeHtml(Task);
        Deadline = escapeHtml(Deadline);

        data.push({
            Id: Id,
            Counsellor: Counsellor,
            Task: Task,
            Deadline: Deadline
        });

        var json = JSON.stringify(data);
        $("#TaskSubmit").text('Processing...');
        $("#TaskSubmit").attr("disabled", true);
        $("#TaskBack").attr("disabled", true);
        $.ajax
            ({
                url: "/AddNew/InsertTaskStatus?json=" + json + "&Id=" + Id + "",
                type: "Get",
                success: function (drr) {

                    $("#TaskSubmit").text('Save');
                    $("#TaskSubmit").attr("disabled", false);
                    $("#TaskBack").attr("disabled", false);

                    var tab = "TaskGrid";
                    var StrMain = "Record has been save successfully.";
                    var url = "/CounsellorAddNew/CounsellorAddNew/?StrMain=" + StrMain + "&tab=" + tab;
                    window.location.href = url;
                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    });

    $('#TaskDetails').on('click', '#delete', function () {
        var id = $(this).closest("tr").find('td:eq(3)').text();
        var result = confirm("Do you Want to delete?");
        if (result) {
            $.ajax
                ({
                    url: "/AddNew/DeleteTaskDetails?id=" + id + "",
                    type: "Get",
                    success: function (drr) {
                        var tab = "TaskGrid";
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

    $('#TaskDetails').on('click', '#edit', function () {
        $('#TaskGrid').hide();
        $('#task').show();
        $('#TaskSubmit').text('Update');
        var id = $(this).closest("tr").find('td:eq(3)').text();
        $.ajax
            ({
                url: "/AddNew/EditTaskDetails?id=" + id + "",
                type: "Get",
                success: function (data) {

                    var item = JSON.parse(data);

                    for (var i = 0; i < item.length; i++) {
                        $('#TaskId').val(item[0].Id)
                        $('#TaskCounsellor').val(item[0].Counsellor)
                        $('#Task').val(item[0].Task)
                        $('#TaskDeadline').val(item[0].Deadline)
                    }
                },
                Error: function (err) {
                    Console.log(err);
                }
            });

    });


    $('#tasktab').click(function () {
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

        $('#TaskGrid').show();
        $('#task').hide();
        $('#TestGrid').hide();
        $('#test').hide();
        $('#FormatGrid').hide();
        $('#format').hide();

        $('#ProjectGrid').hide();
        $('#project').hide();
    });
    $('#TaskBack').click(function () {
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
        $('#TaskGrid').show();
        $('#task').hide();
        $('#TestGrid').hide();
        $('#test').hide();
        $('#FormatGrid').hide();
        $('#format').hide();
        $('#ProjectGrid').hide();
        $('#project').hide();

    });
    $('#TaskAddNew').click(function () {
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
        $('#task').show();
        $('#TestGrid').hide();
        $('#test').hide();
        $('#FormatGrid').hide();
        $('#format').hide();

        $('#TaskSubmit').val('')
        $('#TaskId').val('')
        $('#TaskCounsellor').val('')
        $('#Task').val('')
        $('#TaskDeadline').val('')
        $('#ProjectGrid').hide();
        $('#project').hide();

    });



    var today = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
    $('#TaskDeadline').datepicker({
        uiLibrary: 'bootstrap4',
        iconsLibrary: 'fontawesome',
        minDate: today,
        maxDate: function () {
            return $('#TaskDeadline').val();
        }
    });
});