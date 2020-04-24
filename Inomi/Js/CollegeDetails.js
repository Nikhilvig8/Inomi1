$(document).ready(function () {
    $('#CollegeDetails').DataTable({
        "bSort": false
    });

    $("#CollegeSubmit").click(function () {
        
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
        let dataDetails = [];
        let dataScore = [];
        var Id = $('#CollegeId').val();
        var NameOfCollege = $('#CollegeName').val();
        //var CampusName = $('#CampusName').val();
        var CampusName = '';
        var Country = $('#CollegeCountry').val();
        var City = $('#CollegeCity').val();
        var Courses = $('#CollegeCourses').val();
        
        



        var TestingRequirement = $('#CollegeTestingRequirement').val();
        var Link = $('#CollegeLink').val();
        var EarlyDate = $('#EarlyDate').val();
        var LateDate = $('#LateDate').val();
        var ApplicationDeadline = $('#CollegeDate').val();

        var CollegeIRanking = $('#CollegeIRanking').val();
        var CollegeIndRanking = $('#CollegeIndRanking').val();
        var USNewsRanking = $('#USNewsRanking').val();
        var QSWorldRanking = $('#QSWorldRanking').val();
        var QSBySubject = $('#QSBySubject').val();
        var CollegeGrandeProfile = $('#CollegeGrandeProfile').val();
        var AboutUniversity = $('#txtAboutUniversity').val();
        
        var ApplicationMode = $('#CollegeApplicationMode').val();
        var EssayNameapp = $('#txtEssayNameapp').val();
        var Topicapp = $('#txtTopicapp').val();
        var WordCountapp = $('#txtWordCountapp').val();
        var EssayDeadlineapp = $('#txtEssayDeadlineapp').val();

        if (NameOfCollege == '' || NameOfCollege == null) {
            $('#msgCollegeName').text('Please enter Name Of College.');
            $('#msgCollegeName').show();
            $('#CollegeName').focus();
            return;
        } else {
            $('#msgCollegeName').text('');
            $('#msgCollegeName').hide();
        }
        if (ApplicationMode == '' || ApplicationMode == null) {
            $('#msgCollegeApplicationMode').text('Please select ApplicationMode.');
            $('#msgCollegeApplicationMode').show();
            $('#CollegeApplicationMode').focus();
            return;
        } else {
            $('#msgCollegeApplicationMode').text('');
            $('#msgCollegeApplicationMode').hide();
        }
        if ($('#txtEssayNameapp').val() == '' || $('#txtEssayNameapp').val() == null) {
            $('#msgEssayNameapp').text('Please enter essay name.');
            $('#msgEssayNameapp').show();
            $('#txtEssayNameapp').focus();
            return;
        } else {
            $('#msgEssayNameapp').text('');
            $('#msgEssayNameapp').hide();
        }

        if ($('#txtTopicapp').val() == '' || $('#txtTopicapp').val() == null) {
            $('#msgTopicapp').text('Please enter topic name.');
            $('#msgTopicapp').show();
            $('#txtTopicapp').focus();
            return;
        } else {
            $('#msgTopicapp').text('');
            $('#msgTopicapp').hide();
        }

        if ($('#txtWordCountapp').val() == '' || $('#txtWordCountapp').val() == null) {
            $('#msgWordCountapp').text('Please enter word count.');
            $('#msgWordCountapp').show();
            $('#txtWordCountapp').focus();
            return;
        } else {
            $('#msgWordCountapp').text('');
            $('#msgWordCountapp').hide();
        }

        if ($('#txtEssayDeadlineapp').val() == '' || $('#txtEssayDeadlineapp').val() == null) {
            $('#msgEssayDeadlineapp').text('Please enter essay deadline.');
            $('#msgEssayDeadlineapp').show();
            $('#txtEssayDeadlineapp').focus();
            return;
        } else {
            $('#msgEssayDeadlineapp').text('');
            $('#msgEssayDeadlineapp').hide();
        }

        //if (CampusName == '' || CampusName == null) {
        //    $('#msgCampusName').text('Please enter campus name.');
        //    $('#msgCampusName').show();
        //    $('#CampusName').focus();
        //    return;
        //} else {
        //    $('#msgCampusName').text('');
        //    $('#msgCampusName').hide();
        //}


        //if (Country == '' || Country == null) {
        //    $('#msgCollegeCountry').text('Please select Country.');
        //    $('#msgCollegeCountry').show();
        //    $('#CollegeCountry').focus();
        //    return;
        //} else {
        //    $('#msgCollegeCountry').text('');
        //    $('#msgCollegeCountry').hide();
        //}


        //if (City == '' || City == null) {
        //    $('#msgCollegeCity').text('Please select City.');
        //    $('#msgCollegeCity').show();
        //    $('#CollegeCity').focus();
        //    return;
        //} else {
        //    $('#msgCollegeCity').text('');
        //    $('#msgCollegeCity').hide();
        //}


        //if (Courses == '' || Courses == null) {
        //    $('#msgCollegeCourses').text('Please select Courses.');
        //    $('#msgCollegeCourses').show();
        //    $('#CollegeCourses').focus();
        //    return;
        //} else {
        //    $('#msgCollegeCourses').text('');
        //    $('#msgCollegeCourses').hide();
        //}


        


        //if (TestingRequirement == '' || TestingRequirement == null) {
        //    $('#msgCollegeTestingRequirement').text('Please select Testing Requirement.');
        //    $('#msgCollegeTestingRequirement').show();
        //    $('#CollegeTestingRequirement').focus();
        //    return;
        //} else {
        //    $('#msgCollegeTestingRequirement').text('');
        //    $('#msgCollegeTestingRequirement').hide();
        //}


        //if (Link == '' || Link == null) {
        //    $('#msgCollegeLink').text('Please enter Link.');
        //    $('#msgCollegeLink').show();
        //    $('#CollegeLink').focus();
        //    return;
        //} else {
        //    $('#msgCollegeLink').text('');
        //    $('#msgCollegeLink').hide();
        //}


        //if (EarlyDate == '' || EarlyDate == null) {
        //    $('#msgEarlyDate').text('Please enter ealry Date.');
        //    $('#msgEarlyDate').show();
        //    $('#EarlyDate').focus();
        //    return;
        //} else {
        //    $('#msgEarlyDate').text('');
        //    $('#msgEarlyDate').hide();
        //}

        //if (LateDate == '' || LateDate == null) {
        //    $('#msgLateDate').text('Please enter late Date.');
        //    $('#msgLateDate').show();
        //    $('#LateDate').focus();
        //    return;
        //} else {
        //    $('#msgLateDate').text('');
        //    $('#msgLateDate').hide();
        //}

        if (ApplicationDeadline == '' || ApplicationDeadline == null) {
            $('#msgCollegeDate').text('Please enter Application Deadline Date.');
            $('#msgCollegeDate').show();
            $('#CollegeDate').focus();
            return;
        } else {
            $('#msgCollegeDate').text('');
            $('#msgCollegeDate').hide();
        }

        //if (CollegeIRanking == '' || CollegeIRanking == null) {
        //    $('#msgCollegeIRanking').text('Please enter International rank.');
        //    $('#msgCollegeIRanking').show();
        //    $('#CollegeIRanking').focus();
        //    return;
        //} else {
        //    $('#msgCollegeIRanking').text('');
        //    $('#msgCollegeIRanking').hide();
        //}
        //if (CollegeIndRanking == '' || CollegeIndRanking == null) {
        //    $('#msgCollegeIndRanking').text('Please enter India rank.');
        //    $('#msgCollegeIndRanking').show();
        //    $('#CollegeIndRanking').focus();
        //    return;
        //} else {
        //    $('#msgCollegeIndRanking').text('');
        //    $('#msgCollegeIndRanking').hide();
        //}
        //if (USNewsRanking == '' || USNewsRanking == null) {
        //    $('#msgUSNewsRanking').text('Please enter US news ranking.');
        //    $('#msgUSNewsRanking').show();
        //    $('#USNewsRanking').focus();
        //    return;
        //} else {
        //    $('#msgUSNewsRanking').text('');
        //    $('#msgUSNewsRanking').hide();
        //}
        //if (QSWorldRanking == '' || QSWorldRanking == null) {
        //    $('#msgQSWorldRanking').text('Please enter QS world ranking.');
        //    $('#msgQSWorldRanking').show();
        //    $('#QSWorldRanking').focus();
        //    return;
        //} else {
        //    $('#msgQSWorldRanking').text('');
        //    $('#msgQSWorldRanking').hide();
        //}
        //if (QSBySubject == '' || QSBySubject == null) {
        //    $('#msgQSBySubject').text('Please enter QS by subject.');
        //    $('#msgQSBySubject').show();
        //    $('#QSBySubject').focus();
        //    return;
        //} else {
        //    $('#msgQSBySubject').text('');
        //    $('#msgQSBySubject').hide();
        //}
        //if (CollegeGrandeProfile == '' || CollegeGrandeProfile == null) {
        //    $('#msgCollegeGrandeProfile').text('Please enter Grande profile.');
        //    $('#msgCollegeGrandeProfile').show();
        //    $('#CollegeGrandeProfile').focus();
        //    return;
        //} else {
        //    $('#msgCollegeGrandeProfile').text('');
        //    $('#msgCollegeGrandeProfile').hide();
        //}
        //if (AboutUniversity == '' || AboutUniversity == null) {
        //    $('#msgAboutUniversity').text('Please describe about unversity/college.');
        //    $('#msgAboutUniversity').show();
        //    $('#txtAboutUniversity').focus();
        //    return;
        //} else {
        //    $('#msgAboutUniversity').text('');
        //    $('#msgAboutUniversity').hide();
        //}
        
        var VType = 0;
        Name = escapeHtml(NameOfCollege);
        Country = escapeHtml(Country);
        //City = escapeHtml(City);
        City = City;
        Courses = escapeHtml(Courses);
        ApplicationMode = escapeHtml(ApplicationMode);
        EssayNameapp=escapeHtml(EssayNameapp);
        Topicapp=escapeHtml(Topicapp);
        WordCountapp=escapeHtml(WordCountapp);
        EssayDeadlineapp=escapeHtml(EssayDeadlineapp);


        TestingRequirement = escapeHtml(TestingRequirement);
        Link = escapeHtml(Link);
        ApplicationDeadline = escapeHtml(ApplicationDeadline);
        CollegeIRanking = escapeHtml(CollegeIRanking);
        CollegeIndRanking = escapeHtml(CollegeIndRanking);
        USNewsRanking = escapeHtml(USNewsRanking);
        QSWorldRanking = escapeHtml(QSWorldRanking);
        QSBySubject = escapeHtml(QSBySubject);
        CollegeGrandeProfile = escapeHtml(CollegeGrandeProfile);
        AboutUniversity = escapeHtml(AboutUniversity);

        CampusName = escapeHtml(CampusName);
        EarlyDate = escapeHtml(EarlyDate);
        LateDate = escapeHtml(LateDate);


        data.push({
            Id: Id,
            Name: Name,
            Country: Country,
            City: City,
            Courses: Courses,
            ApplicationMode: ApplicationMode,
            
            EssayNameapp:EssayNameapp,
            Topicapp:Topicapp,
            WordCountapp:WordCountapp,
            EssayDeadlineapp:EssayDeadlineapp,


            TestingRequirement: TestingRequirement,
            Link: Link,
            ApplicationDeadline: ApplicationDeadline,
            CollegeIRanking: CollegeIRanking,
            CollegeIndRanking: CollegeIndRanking,
            USNewsRanking: USNewsRanking,
            QSWorldRanking: QSWorldRanking,
            QSBySubject: QSBySubject,
            CollegeGrandeProfile: CollegeGrandeProfile,
            CampusName: CampusName,
            EarlyDate: EarlyDate,
            LateDate: LateDate,
            AboutUniversity: AboutUniversity

        });

        var k = 0;
        var n = document.getElementById('ScoreDetails');

        $('#ScoreDetails tr').each(function () {
            let check = $(n.rows[0].cells[0]).text();
            let Type = $(n.rows[k].cells[0]).text();
            let Score = $(n.rows[k].cells[1]).text();

            //if (check == $(n.rows[1].cells[0]).text()) {
            //    $('#msgType').text('Score type are same.');
            //    $('#msgType').show();
            //    VType = '1';
            //    return;
            //} else {
            //    $('#msgType').text('');
            //    $('#msgType').hide();
            //    VType = '0';
            //}


            dataScore.push({

                Type: Type,
                Score: Score,
            });
            k++;
        });

        var i = 0;
        var t = document.getElementById('EssayDetails');

        $('#EssayDetails tr').each(function () {
            let EssayName = $(t.rows[i].cells[0]).text();
            let Topic = $(t.rows[i].cells[1]).text();
            let WordCount = $(t.rows[i].cells[2]).text();
            let Dealline = $(t.rows[i].cells[3]).text();


            dataDetails.push({

                EssayName: EssayName,
                Topic: Topic,
                WordCount: WordCount,
                Dealline: Dealline
            });
            i++;
        });

        var json = JSON.stringify(data);
        var jsonDetails = JSON.stringify(dataDetails);
        var jsonScore = JSON.stringify(dataScore);

        if (VType == '1') {
            return;
        }

        $("#CollegeSubmit").text('Processing...');
        $("#CollegeSubmit").attr("disabled", true);
        $("#CollegeBack").attr("disabled", true);
        
        $.ajax
            ({
                url: "/CounsellorAddNew/InsertCollegeStatus?json=" + json + "&Id=" + Id + "&jsonDetails=" + jsonDetails + "&jsonScore=" + jsonScore + "",
                type: "Get",
                success: function (drr) {

                    $("#CollegeSubmit").text('Save');
                    $("#CollegeSubmit").attr("disabled", false);
                    $("#CollegeBack").attr("disabled", false);

                    var tab = "CollegeGrid";
                    var StrMain = "Record has been save successfully.";
                    var url = "/AddNew/Student/?StrMain=" + StrMain + "&tab=" + tab;
                    window.location.href = url;

                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    });


    $('#CollegeDetails').on('click', '#delete', function () {
        var id = $(this).closest("tr").find('td:eq(8)').text();
        var result = confirm("Do you Want to delete?");
        if (result) {
            $.ajax
                ({
                    url: "/AddNew/DeleteCollegeDetails?id=" + id + "",
                    type: "Get",
                    success: function (drr) {
                        var tab = "CollegeGrid";
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

    $('#CollegeDetails').on('click', '#edit', function () {
        $('#CollegeGrid').hide();
        $('#college').show();
        $('#CollegeSubmit').text('Update');
        var id = $(this).closest("tr").find('td:eq(8)').text();
        $.ajax
            ({
                url: "/AddNew/EditCollegeDetails?id=" + id + "",
                type: "Get",
                success: function (data) {
                    var item = JSON.parse(data);
                    
                    for (var i = 0; i < item.length; i++) {
                        $('#CollegeId').val(item[0].Id)
                        $('#CollegeName').val(item[0].NameOfCollege)
                        $('#CollegeCountry').val(item[0].Country)

                        var selectedCountry = $("#CollegeCountry option:selected").val();
                        $.ajax
                            ({
                                url: "/AddNew/GetCityList?Cid=" + selectedCountry + "",
                                type: "Get",
                                success: function (data) {
                                    var itemdd = JSON.parse(data);
                                    var CityList = '<option value="">--Select--</option>';
                                    for (var i = 0; i < itemdd.length; i++) {

                                        if (itemdd[i].CTID == item[0].City) {
                                            CityList += '<option value="' + itemdd[i].CTID + '" selected>' + itemdd[i].Name + '</option>';
                                        }
                                        else {
                                            CityList += '<option value="' + itemdd[i].CTID + '">' + itemdd[i].Name + '</option>';
                                        }
                                    }
                                    $("#CollegeCity").html(CityList);
                                },
                                Error: function (err) {
                                    Console.log(err);
                                }
                            });

                        //$('#CollegeCity').val(item[0].City)

                        $('#CollegeCourses').val(item[0].Courses)
                        $('#CollegeApplicationMode').val(item[0].ApplicationMode)
                        $('#txtEssayNameapp').val(item[0].EssayName);
                        $('#txtTopicapp').val(item[0].Topic);
                        $('#txtWordCountapp').val(item[0].WordCount);
                        $('#txtEssayDeadlineapp').val(item[0].deadline);
                        


                        $('#CollegeTestingRequirement').val(item[0].TestingRequirement)
                        $('#CollegeLink').val(item[0].Link)
                        $('#CollegeDate').val(item[0].ApplicationDeadline)

                        $('#CollegeIRanking').val(item[0].InternationalRank)
                        $('#CollegeIndRanking').val(item[0].IndianRank)
                        $('#USNewsRanking').val(item[0].USNewsRanking)
                        $('#QSWorldRanking').val(item[0].QSWorldRanking)
                        $('#QSBySubject').val(item[0].QSBySubject)
                        $('#CollegeGrandeProfile').val(item[0].GradeProfile)

                        $('#CampusName').val(item[0].CampusName)
                        $('#EarlyDate').val(item[0].EarlyDate)
                        $('#LateDate').val(item[0].LateDate)
                        $('#txtAboutUniversity').val(item[0].AboutCollege)

                    }

                    $.ajax
                        ({
                            url: "/CounsellorAddNew/GetEssayDetails?CID=" + id + "",
                            type: "Get",
                            success: function (data) {
                                
                                var itemCD = JSON.parse(data);
                                var College = new Array();

                                for (var i = 0; i < itemCD.length; i++) {
                                    College.push([itemCD[i].EssayName, itemCD[i].Topic, itemCD[i].WordCount, itemCD[i].deadline]);
                                }

                                for (var i = 0; i < College.length; i++) {
                                    AddRow(College[i][0], College[i][1], College[i][2], College[i][3]);
                                }
                            },
                            Error: function (err) {
                                Console.log(err);
                            }
                        });

                    $.ajax
                        ({
                            url: "/CounsellorAddNew/GetScoreDetails?CID=" + id + "",
                            type: "Get",
                            success: function (data) {
                                
                                var itemS = JSON.parse(data);
                                var Score = new Array();

                                for (var i = 0; i < itemS.length; i++) {
                                    Score.push([itemS[i].Type, itemS[i].Score]);
                                }

                                for (var i = 0; i < Score.length; i++) {
                                    AddRowScore(Score[i][0], Score[i][1]);
                                }
                            },
                            Error: function (err) {
                                Console.log(err);
                            }
                        });

                },
                Error: function (err) {
                    Console.log(err);
                }
            });

    });

    $('#collegetab').click(function () {
        $('#CollegeGrid').show();
        $('#counsellor').hide();
        $('#student').hide();
        $('#studentGrid').hide();
        $('#CounsellorGrid').hide()
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
    $('#CollegeBack').click(function () {
        $('#CollegeGrid').show();
        $('#counsellor').hide();
        $('#student').hide();
        $('#studentGrid').hide();
        $('#CounsellorGrid').hide()
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


        $('#CollegeSubmit').text('Save');

        $('#CollegeId').val('')
        $('#CollegeName').val('')
        $('#CollegeCountry').val('')
        $('#CollegeCity').val('')
        $('#CollegeCourses').val('')
        $('#CollegeApplicationMode').val('')
        $('#CollegeTestingRequirement').val('')
        $('#CollegeLink').val('')
        $('#CollegeDate').val('')

        $('#CollegeIRanking').val('')
        $('#CollegeIndRanking').val('')
        $('#CollegeSatScores').val('')
        $('#CollegeActScores').val('')
        $('#CollegeGrandeProfile').val('')
        $('#CampusName').val('')
        $('#EarlyDate').val('')
        $('#LateDate').val('')
        $('#txtAboutUniversity').val('')
        $('#USNewsRanking').val('')
        $('#QSWorldRanking').val('')
        $('#QSBySubject').val('')

        $('#EssayDetails').text('')
        $('#ScoreDetails').text('')
    });
    $('#CollegeAddNew').click(function () {
        $('#college').show();
        $('#student').hide();
        $('#studentGrid').hide();
        $('#CounsellorGrid').hide();
        $('#CollegeGrid').hide();
        $('#counsellor').hide();
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

        $('#CollegeSubmit').text('Save');

        $('#CollegeId').val('')
        $('#CollegeName').val('')
        $('#CollegeCountry').val('')
        $('#CollegeCity').val('')
        $('#CollegeCourses').val('')
        $('#CollegeApplicationMode').val('')
        $('#CollegeTestingRequirement').val('')
        $('#CollegeLink').val('')
        $('#CollegeDate').val('')

        $('#CollegeIRanking').val('')
        $('#CollegeIndRanking').val('')
        $('#CollegeSatScores').val('')
        $('#CollegeActScores').val('')
        $('#CollegeGrandeProfile').val('')
        $('#CampusName').val('')
        $('#EarlyDate').val('')
        $('#LateDate').val('')
        $('#txtAboutUniversity').val('')
        $('#USNewsRanking').val('')
        $('#QSWorldRanking').val('')
        $('#QSBySubject').val('')

        $('#EssayDetails').text('')
        $('#ScoreDetails').text('')

    });
});



window.onload = function () {
    //Build an array containing Customer records.
    var College = new Array();
    //customers.push(["John Hammond", "United States"]);
    //customers.push(["Mudassar Khan", "India"]);
    //customers.push(["Suzanne Mathews", "France"]);
    //customers.push(["Robert Schidner", "Russia"]);

    //Add the data rows.
    for (var i = 0; i < College.length; i++) {
        AddRow(College[i][0], College[i][1], College[i][2], College[i][3]);
    }
};

function Add() {
    var EssayName = document.getElementById("txtEssayName");
    var Topic = document.getElementById("txtTopic");
    var WordCount = document.getElementById("txtWordCount");
    var EssayDeadline = document.getElementById("txtEssayDeadline");

    

    if ($('#txtEssayName').val() == '' || $('#txtEssayName').val() == null) {
        $('#msgEssayName').text('Please enter essay name.');
        $('#msgEssayName').show();
        $('#txtEssayName').focus();
        return;
    } else {
        $('#msgEssayName').text('');
        $('#msgEssayName').hide();
    }

    if ($('#txtTopic').val() == '' || $('#txtTopic').val() == null) {
        $('#msgTopic').text('Please enter topic name.');
        $('#msgTopic').show();
        $('#txtTopic').focus();
        return;
    } else {
        $('#msgTopic').text('');
        $('#msgTopic').hide();
    }

    if ($('#txtWordCount').val() == '' || $('#txtWordCount').val() == null) {
        $('#msgWordCount').text('Please enter word count.');
        $('#msgWordCount').show();
        $('#txtWordCount').focus();
        return;
    } else {
        $('#msgWordCount').text('');
        $('#msgWordCount').hide();
    }

    if ($('#txtEssayDeadline').val() == '' || $('#txtEssayDeadline').val() == null) {
        $('#msgEssayDeadline').text('Please enter essay deadline.');
        $('#msgEssayDeadline').show();
        $('#txtEssayDeadline').focus();
        return;
    } else {
        $('#msgEssayDeadline').text('');
        $('#msgEssayDeadline').hide();
    }

    AddRow(EssayName.value, Topic.value, WordCount.value, EssayDeadline.value);
    EssayName.value = "";
    Topic.value = "";
    WordCount.value = "";
    EssayDeadline.value = "";
};

function Remove(button) {
    //Determine the reference of the Row using the Button.
    var row = button.parentNode.parentNode;
    var name = row.getElementsByTagName("TD")[0].innerHTML;
    if (confirm("Do you want to delete: " + name)) {

        //Get the reference of the Table.
        var table = document.getElementById("tblCollege");

        //Delete the Table row using it's Index.
        table.deleteRow(row.rowIndex);
    }
};

function AddRow(EssayName, Topic, WordCount, EssayDeadline) {
    //Get the reference of the Table's TBODY element.
    var tBody = document.getElementById("tblCollege").getElementsByTagName("TBODY")[0];

    //Add Row.
    row = tBody.insertRow(-1);

    //Add EssayName cell.
    var cell = row.insertCell(-1);
    cell.innerHTML = EssayName;

    //Add Topic cell.
    cell = row.insertCell(-1);
    cell.innerHTML = Topic;

    //Add WordCount cell.
    var cell = row.insertCell(-1);
    cell.innerHTML = WordCount;

    //Add EssayDeadline cell.
    cell = row.insertCell(-1);
    cell.innerHTML = EssayDeadline;

    //Add Button cell.
    cell = row.insertCell(-1);
    var btnRemove = document.createElement("INPUT");
    btnRemove.type = "button";
    btnRemove.value = "Remove";
    btnRemove.setAttribute("onclick", "Remove(this);");
    cell.appendChild(btnRemove);
}

function AddScore() {

    var Type = document.getElementById("ScoreType");
    var Score = document.getElementById("txtScore");

    if ($('#ScoreType').val() == '' || $('#ScoreType').val() == null) {
        $('#msgType').text('Please choose score type.');
        $('#msgType').show();
        $('#ScoreType').focus();
        return;
    } else {
        $('#msgType').text('');
        $('#msgType').hide();
    }

    if ($('#txtScore').val() == '' || $('#txtScore').val() == null) {
        $('#msgScore').text('Please enter score.');
        $('#msgScore').show();
        $('#txtScore').focus();
        return;
    } else {
        $('#msgScore').text('');
        $('#msgScore').hide();
    }
    
    
    
    
    AddRowScore(Type.value, Score.value);
    Type.value = "";
    Score.value = "";
};

function RemoveScore(button) {
    //Determine the reference of the Row using the Button.
    var row = button.parentNode.parentNode;
    var name = row.getElementsByTagName("TD")[0].innerHTML;
    if (confirm("Do you want to delete: " + name)) {

        //Get the reference of the Table.
        var table = document.getElementById("tblScore");

        //Delete the Table row using it's Index.
        table.deleteRow(row.rowIndex);
    }
};

function AddRowScore(Type, Score) {
    //Get the reference of the Table's TBODY element.
    var tBody = document.getElementById("tblScore").getElementsByTagName("TBODY")[0];

    //Add Row.
    row = tBody.insertRow(-1);

    //Add EssayName cell.
    var cell = row.insertCell(-1);
    cell.innerHTML = Type;

    //Add Topic cell.
    cell = row.insertCell(-1);
    cell.innerHTML = Score;

    //Add Button cell.
    cell = row.insertCell(-1);
    var btnRemove = document.createElement("INPUT");
    btnRemove.type = "button";
    btnRemove.value = "Remove";
    btnRemove.setAttribute("onclick", "RemoveScore(this);");
    $("#ScoreType option:selected").remove();

    
    cell.appendChild(btnRemove);
}