$(document).ready(function () {
    $('#CounsellorDetails').DataTable({
        "bSort": false
    });

    $("#CounsellorSubmit").click(function () {

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
        var Id = $('#CounsellorId').val();
        var Name = $('#CslName').val();
        var Education = $('#CslEducation').val();
        var Lnk = $('#CslLink').val();
        var Country = $('#CslCountry').val();
        var City = $('#CslCity').val();
        var Phone = $('#CslPhone').val();
        var Email = $('#CslEmail').val();
        var Experience = $('#CslExperience').val();
        var Picture = $('#CslPicture').val();
        var Achievements = $('#CslAchievements').val();
        var filter = /^[7-9][0-9]{9}$/;
        var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;

        if (Name == '' || Name == null) {
            $('#msgCslName').text('Please enter Name.');
            $('#msgCslName').show();
            $('#CslName').focus();
            return;
        } else {
            $('#msgCslName').text('');
            $('#msgCslName').hide();
        }


        if (Education == '' || Education == null) {
            $('#msgCslEducation').text('Please enter Education.');
            $('#msgCslEducation').show();
            $('#CslEducation').focus();
            return;
        } else {
            $('#msgCslEducation').text('');
            $('#msgCslEducation').hide();
        }


        if (Lnk == '' || Lnk == null) {
            $('#msgCslLink').text('Please enter Link.');
            $('#msgCslLink').show();
            $('#CslLink').focus();
            return;
        }
        else {
            $('#msgCslLink').text('');
            $('#msgCslLink').hide();
        }


        if (Country == '' || Country == null) {
            $('#msgCslCountry').text('Please select Country.');
            $('#msgCslCountry').show();
            $('#CslCountry').focus();
            return;
        } else {
            $('#msgCslCountry').text('');
            $('#msgCslCountry').hide();
        }


        if (City == '' || City == null) {
            $('#msgCslCity').text('Please select City.');
            $('#msgCslCity').show();
            $('#CslCity').focus();
            return;
        } else {
            $('#msgCslCity').text('');
            $('#msgCslCity').hide();
        }


        if (Phone == '' || Phone == null) {
            $('#msgCslPhone').text('Please enter Phone.');
            $('#msgCslPhone').show();
            $('#CslPhone').focus();
            return;
        } else {
            $('#msgCslPhone').text('');
            $('#msgCslPhone').hide();
        }

        if (filter.test(Phone)) {
            $('#msgCslPhone').text('');
            $('#msgCslPhone').hide();
        }
        else {
            $('#msgCslPhone').text("Please enter valid mobile no.");
            $('#msgCslPhone').show();
            return;
        }


        if (Email == '' || Email == null) {
            $('#msgCslEmail').text('Please enter Email.');
            $('#msgCslEmail').show();
            $('#CslEmail').focus();
            return;
        } else {
            $('#msgCslEmail').text('');
            $('#msgCslEmail').hide();
        }

        if (testEmail.test(Email)) {
            $('#msgCslEmail').text('');
            $('#msgCslEmail').hide();
        }
        else {
            $('#msgCslEmail').text('Please enter valid email.');
            $('#msgCslEmail').show();
            $('#CslEmail').focus();
            return;
        }


        if (Experience == '' || Experience == null) {
            $('#msgCslExperience').text('Please enter Experience.');
            $('#msgCslExperience').show();
            $('#CslExperience').focus();
            return;
        } else {
            $('#msgCslExperience').text('');
            $('#msgCslExperience').hide();
        }


        if (Picture == '' || Picture == null) {
            $('#msgCslPicture').text('Please choose Picture.');
            $('#msgCslPicture').show();
            $('#CslPicture').focus();
            return;
        } else {
            $('#msgCslPicture').text('');
            $('#msgCslPicture').hide();
        }

        if (Achievements == '' || Achievements == null) {
            $('#msgCslAchievements').text('Please enter Achievements.');
            $('#msgCslAchievements').show();
            $('#CslAchievements').focus();
            return;
        } else {
            $('#msgCslAchievements').text('');
            $('#msgCslAchievements').hide();
        }

        Name = escapeHtml(Name);
        Education = escapeHtml(Education);
        Lnk = escapeHtml(Lnk);
        Country = escapeHtml(Country);
        City = escapeHtml(City);
        Phone = escapeHtml(Phone);
        Email = escapeHtml(Email);
        Experience = escapeHtml(Experience);
        Picture = escapeHtml(Picture);
        Achievements = escapeHtml(Achievements);


        data.push({
            Id: Id,
            Name: Name,
            Education: Education,
            Lnk: Lnk,
            Country: Country,
            City: City,
            Phone: Phone,
            Email: Email,
            Experience: Experience,
            Picture: Picture,
            Achievements: Achievements,

        });

        var fileUpload = $("input[name='CounsellorFichier']").get(0);
        var files = fileUpload.files;

        var fileData = new FormData();

        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        $("#CounsellorSubmit").text('Processing...');
        $("#CounsellorSubmit").attr("disabled", true);
        $("#CounsellorBack").attr("disabled", true);

        var json = JSON.stringify(data);
        $.ajax
            ({
                url: "/AddNew/InsertCounsoellorStatus?json=" + json + "&Id=" + Id + "",
                type: "Get",
                contentType: false,
                processData: false,
                type: "POST",
                data: fileData,
                success: function (drr) {
                    $("#CounsellorSubmit").text('Save');
                    $("#CounsellorSubmit").attr("disabled", false);
                    $("#CounsellorBack").attr("disabled", false);
                    var tab = "CounsellorGrid";
                    var StrMain = "Record has been update successfully.";
                    var url = "/AddNew/Student/?StrMain=" + StrMain + "&tab=" + tab;
                    window.location.href = url;
                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    });

    $('#CounsellorDetails').on('click', '#delete', function () {
        var id = $(this).closest("tr").find('td:eq(7)').text();
        var result = confirm("Do you Want to delete?");
        if (result) {
            $.ajax
                ({
                    url: "/AddNew/DeleteCounsellorDetails?id=" + id + "",
                    type: "Get",
                    success: function (drr) {
                        var tab = "CounsellorGrid";
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

    $('#CounsellorDetails').on('click', '#edit', function () {
        $('#CounsellorGrid').hide();
        $('#counsellor').show();
        $('#CounsellorSubmit').text('Update');
        var id = $(this).closest("tr").find('td:eq(7)').text();
        $.ajax
            ({
                url: "/AddNew/EditCounsellorDetails?id=" + id + "",
                type: "Get",
                success: function (data) {

                    var item = JSON.parse(data);

                    for (var i = 0; i < item.length; i++) {
                        $('#CounsellorId').val(item[0].Id)
                        $('#CslName').val(item[0].Name)
                        $('#CslEducation').val(item[0].Education)
                        $('#CslLink').val(item[0].Link)
                        $('#CslCountry').val(item[0].Country)

                        var selectedCountry = $("#CslCountry option:selected").val();
                        $.ajax
                            ({
                                url: "/AddNew/GetCityList?Cid=" + selectedCountry + "",
                                type: "Get",
                                success: function (data) {
                                    var itemdd = JSON.parse(data);
                                    var CityList = '<option value="">--Select--</option>';
                                    for (var i = 0; i < itemdd.length; i++) {
                                        debugger;
                                        if (itemdd[i].CTID == item[0].City) {
                                            CityList += '<option value="' + itemdd[i].CTID + '" selected>' + itemdd[i].Name + '</option>';
                                        }
                                        else {
                                            CityList += '<option value="' + itemdd[i].CTID + '">' + itemdd[i].Name + '</option>';
                                        }
                                    }
                                    $("#CslCity").html(CityList);
                                },
                                Error: function (err) {
                                    Console.log(err);
                                }
                            });

                        //$('#CslCity').val(item[0].City)
                        $('#CslPhone').val(item[0].Phone)
                        $('#CslEmail').val(item[0].Email)
                        $('#CslExperience').val(item[0].Experience)

                        $('#CslPicture').val(item[0].Picture)
                        $('#CslAchievements').val(item[0].Achievements)
                    }

                },
                Error: function (err) {
                    Console.log(err);
                }
            });

    });

    $('#counsellortab').click(function () {
        $('#CounsellorGrid').show();
        $('#counsellor').hide();
        $('#student').hide();
        $('#studentGrid').hide();
        $('#CollegeGrid').hide();
        $('#college').hide();
        $('#CountryGrid').hide();
        $('#country').hide();
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
    });
    $('#CounsellorBack').click(function () {
        $('#CounsellorGrid').show();
        $('#counsellor').hide();
        $('#student').hide();
        $('#studentGrid').hide();
        $('#CollegeGrid').hide();
        $('#college').hide();
        $('#CountryGrid').hide();
        $('#country').hide();
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
    });
    $('#CounsellorAddNew').click(function () {
        $('#CounsellorGrid').hide();
        $('#counsellor').show();
        $('#student').hide();
        $('#studentGrid').hide();
        $('#CollegeGrid').hide();
        $('#college').hide();
        $('#CountryGrid').hide();
        $('#country').hide();
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

        $('#CounsellorSubmit').text('Save');
        $('#CounsellorId').val('')
        $('#CslName').val('')
        $('#CslEducation').val('')
        $('#CslLink').val('')
        $('#CslCountry').val('')
        $('#CslCity').val('')
        $('#CslPhone').val('')
        $('#CslEmail').val('')
        $('#CslExperience').val('')

        $('#CslPicture').val('')
        $('#CslAchievements').val('')
    });
});