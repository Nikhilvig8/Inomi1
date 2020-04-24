$(document).ready(function () {

    $('#CountryDetails').DataTable({
        "bSort": false
    });

    $("#CountrySubmit").click(function () {
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
        var Id = $('#CountryId').val();
        var Name = $('#CountryName').val();
        var JobProspect = $('#CountryJobProspect').val();
        var VisaProcess = $('#CountryVisaProcess').val();
        var Link = $('#CountryLink').val();

        if (Name == '' || Name == null) {
            $('#msgCountryName').text('Please select country name.');
            $('#msgCountryName').show();
            $('#CountryName').focus();
            return;
        }
        else {
            $('#msgCountryName').text('');
            $('#msgCountryName').hide();
        }


        if (JobProspect == '' || JobProspect == null) {
            $('#msgCountryJobProspect').text('Please enter JobProspect.');
            $('#msgCountryJobProspect').show();
            $('#CountryJobProspect').focus();
            return;
        } else {
            $('#msgCountryJobProspect').text('');
            $('#msgCountryJobProspect').hide();
        }


        if (VisaProcess == '' || VisaProcess == null) {
            $('#msgCountryVisaProcess').text('Please enter VisaProcess.');
            $('#msgCountryVisaProcess').show();
            $('#CountryVisaProcess').focus();
            return;
        } else {
            $('#msgCountryVisaProcess').text('');
            $('#msgCountryVisaProcess').hide();
        }


        if (Link == '' || Link == null) {
            $('#msgCountryLink').text('Please enter Link.');
            $('#msgCountryLink').show();
            $('#CountryLink').focus();
            return;
        } else {
            $('#msgCountryLink').text('');
            $('#msgCountryLink').hide();
        }

        Name = escapeHtml(Name);
        JobProspect = escapeHtml(JobProspect);
        VisaProcess = escapeHtml(VisaProcess);
        Link = escapeHtml(Link);

        data.push({
            Id:Id,
            Name: Name,
            JobProspect: JobProspect,
            VisaProcess: VisaProcess,
            Link: Link
        });

        $("#CountrySubmit").text('Processing...');
        $("#CountrySubmit").attr("disabled", true);
        $("#CountryBack").attr("disabled", true);

        var json = JSON.stringify(data);
        $.ajax
            ({
                url: "/AddNew/InsertCountryStatus?json=" + json + "&Id=" + Id + "",
                type: "Get",
                success: function (drr) {

                    $("#CountrySubmit").text('Save');
                    $("#CountrySubmit").attr("disabled", false);
                    $("#CountryBack").attr("disabled", false);

                    var tab = "CountryGrid";
                    var StrMain = "Record has been save successfully.";
                    var url = "/CounsellorAddNew/CounsellorAddNew/?StrMain=" + StrMain + "&tab=" + tab;
                    window.location.href = url;
                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    });

    $('#CountryDetails').on('click', '#delete', function () {
        var id = $(this).closest("tr").find('td:eq(4)').text();
        var result = confirm("Do you Want to delete?");
        if (result) {
            $.ajax
                ({
                    url: "/AddNew/DeleteCountryDetails?id=" + id + "",
                    type: "Get",
                    success: function (drr) {
                        var tab = "CountryGrid";
                        var StrMain = "Record has been deleted successfully.";
                        var url = "/CounsellorAddNew/CounsellorAddNew/?StrMain=" + StrMain + "&tab=" + tab;
                        window.location.href = url;
                    },
                    Error: function (err) {
                        Console.log(err);
                    }
                });

        }
    });

    $('#CountryDetails').on('click', '#edit', function () {
        $('#CountryGrid').hide();
        $('#country').show();
        $('#CountrySubmit').text('Update');
        var id = $(this).closest("tr").find('td:eq(4)').text();
        $.ajax
            ({
                url: "/AddNew/EditCountryDetails?id=" + id + "",
                type: "Get",
                success: function (data) {

                    var item = JSON.parse(data);

                    for (var i = 0; i < item.length; i++) {
                        $('#CountryId').val(item[0].Id)
                        $('#CountryName').val(item[0].NameOfCountry)
                        $('#CountryJobProspect').val(item[0].JobProspect)
                        $('#CountryVisaProcess').val(item[0].VisaProcess)
                        $('#CountryLink').val(item[0].Link)
                    }
                },
                Error: function (err) {
                    Console.log(err);
                }
            });

    });

    $('#countrytab').click(function () {
        $('#CountryGrid').show();
        $('#country').hide();

        $('#CollegeGrid').hide();
        $('#counsellor').hide();
        $('#student').hide();
        $('#studentGrid').hide();
        $('#CounsellorGrid').hide()
        $('#college').hide();
        $('#ProductGrid').hide();
        $('#product').hide();
        $('#CourseGrid').hide();
        $('#course').hide();
        $('#TaskGrid').hide();
        $('#task').hide();
        $('#TestGrid').hide();
        $('#test').hide();
        $('#FormatGrid').hide();
        $('#format').hide();

        $('#ProjectGrid').hide();
        $('#project').hide();
    });
    $('#CountryBack').click(function () {
        $('#CountryGrid').show();
        $('#country').hide();
        $('#CollegeGrid').hide();
        $('#counsellor').hide();
        $('#student').hide();
        $('#studentGrid').hide();
        $('#CounsellorGrid').hide()
        $('#college').hide();
        $('#ProductGrid').hide();
        $('#product').hide();
        $('#CourseGrid').hide();
        $('#course').hide();
        $('#TaskGrid').hide();
        $('#task').hide();
        $('#TestGrid').hide();
        $('#test').hide();
        $('#FormatGrid').hide();
        $('#format').hide();

        $('#ProjectGrid').hide();
        $('#project').hide();
    });
    $('#CountryAddNew').click(function () {
        $('#CountryGrid').hide();
        $('#country').show();
        $('#college').hide();
        $('#student').hide();
        $('#studentGrid').hide();
        $('#CounsellorGrid').hide();
        $('#CollegeGrid').hide();
        $('#counsellor').hide();
        $('#ProductGrid').hide();
        $('#product').hide();
        $('#CourseGrid').hide();
        $('#course').hide();
        $('#TaskGrid').hide();
        $('#task').hide();
        $('#TestGrid').hide();
        $('#test').hide();
        $('#FormatGrid').hide();
        $('#format').hide();

        $('#ProjectGrid').hide();
        $('#project').hide();

        $('#CountrySubmit').text('Save');
        $('#CollegeId').val('')
        $('#CountryName').val('')
        $('#CountryJobProspect').val('')
        $('#CountryVisaProcess').val('')
        $('#CountryLink').val('')

    });
});