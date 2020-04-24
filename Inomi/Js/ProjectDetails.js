$(document).ready(function () {
    $('#ProjectDetails').DataTable({
        "bSort": false
    });

    $("#ProjectSubmit").click(function () {

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

        var Id = $('#ProjectId').val();
        var Student = $('#ddlProjectStudent').val();
        var ProjectName = $('#ProjectName').val();
        var Description = $('#txtDescription').val();


        if (Student == '' || Student == null || Student == '--Select student--') {
            $('#msgProjectStudent').text('Please select student.');
            $('#msgProjectStudent').show();
            $('#ddlProjectStudent').focus();
            return;
        } else {
            $('#msgProjectStudent').text('');
            $('#msgProjectStudent').hide();
        }

        if (ProjectName == '' || ProjectName == null) {
            $('#msgProjectName').text('Please enter project name.');
            $('#msgProjectName').show();
            $('#ProjectName').focus();
            return;
        } else {
            $('#msgProjectName').text('');
            $('#msgProjectName').hide();
        }

        if (Description == '' || Description == null) {
            $('#msgDescription').text('Please enter description.');
            $('#msgDescription').show();
            $('#txtDescription').focus();
            return;
        } else {
            $('#msgDescription').text('');
            $('#msgDescription').hide();
        }


        ProjectName = escapeHtml(ProjectName);
        Description = escapeHtml(Description);


        data.push({
            Id: Id,
            Student: Student,
            ProjectName: ProjectName,
            Description: Description,

        });

        var i = 0;
        var t = document.getElementById('MilestoneDetails');

        $('#MilestoneDetails tr').each(function () {
            let Milestone = $(t.rows[i].cells[0]).text();
            let Deadline = $(t.rows[i].cells[1]).text();
            //let Define = $(t.rows[i].cells[2]).text();
            let Status = $(t.rows[i].cells[3]).text();


            dataDetails.push({
                Student: Student,
                Milestone: Milestone,
                Deadline: Deadline,
                //Define: Define,
                Status: Status
            });
            i++;
        });

        var json = JSON.stringify(data);
        var jsonDetails = JSON.stringify(dataDetails);

        $("#ProjectSubmit").text('Processing...');
        $("#ProjectSubmit").attr("disabled", true);
        $("#ProjectBack").attr("disabled", true);

        $.ajax
            ({
                url: "/CounsellorAddNew/InsertProjectStatus?json=" + json + "&Id=" + Id + "&jsonDetails=" + jsonDetails + "",
                type: "Get",
                success: function (drr) {

                    $("#ProjectSubmit").text('Save');
                    $("#ProjectSubmit").attr("disabled", false);
                    $("#ProjectBack").attr("disabled", false);

                    var tab = "ProjectGrid";
                    var StrMain = "Record has been save successfully.";
                    var url = "/CounsellorAddNew/CounsellorAddNew/?StrMain=" + StrMain + "&tab=" + tab;
                    window.location.href = url;

                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    });

    $('#ProjectDetails').on('click', '#delete', function () {
        var id = $(this).closest("tr").find('td:eq(8)').text();
        var result = confirm("Do you Want to delete?");
        if (result) {
            $.ajax
                ({
                    url: "/CounsellorAddNew/DeleteProjectDetails?id=" + id + "",
                    type: "Get",
                    success: function (drr) {
                        var tab = "ProjectGrid";
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

    $('#ProjectDetails').on('click', '#edit', function () {
        $('#ProjectGrid').hide();
        $('#project').show();
        $('#ProjectSubmit').text('Update');
        var id = $(this).closest("tr").find('td:eq(8)').text();

        $.ajax
            ({
                url: "/CounsellorAddNew/EditProjectDetails?id=" + id + "",
                type: "Get",
                success: function (data) {
                    var item = JSON.parse(data);
                    for (var i = 0; i < item.length; i++) {
                        $('#ProjectId').val(item[0].ProjectID)
                        $('#ddlProjectStudent').val(item[0].Student)
                        $('#ProjectName').val(item[0].Name)
                        $('#txtDescription').val(item[0].Description)

                    }

                    $.ajax
                        ({
                            url: "/CounsellorAddNew/GetProjectDetails?CID=" + id + "",
                            type: "Get",
                            success: function (data) {
                                var itemCD = JSON.parse(data);
                                var Project = new Array();

                                for (var i = 0; i < itemCD.length; i++) {
                                    Project.push([itemCD[i].Milestone, itemCD[i].deadline]);
                                }

                                for (var i = 0; i < Project.length; i++) {
                                    AddRowP(Project[i][0], Project[i][1], Project[i][2]);
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

    $('#ProjectDetails').on('click', '#ViewModel', function () {
        var id = $(this).closest("tr").find('td:eq(6)').text();
        $('#viewModal').show();
        $.ajax
            ({
                url: "/CounsellorAddNew/MileStoneDetails?id=" + id + "",
                type: "Get",
                success: function (data) {
                    var item = JSON.parse(data);
                    var htm = '';
                    debugger;
                    for (var i = 0; i < item.length; i++) {
                        htm += '<td><img src=' + '/' + item[i].Upload + ' alt="common product" style="width:81px; height:81px" /></td>';
                        htm += '<td><label id="Parameter">' + item[i].Define + '</label></td>';

                    }
                    $('#myModalContent').html(htm);

                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    });

    $('#ProjectDetails').on('click', '#Action', function () {
        var id = $(this).closest("tr").find('td:eq(7)').text();
        var result = confirm("Do you Want to done status ?");
        if (result) {
            $.ajax
                ({
                    url: "/CounsellorAddNew/MileStoneAction?id=" + id + "",
                    type: "Get",
                    success: function (data) {
                        alert('Milestone status have been done.');

                    },
                    Error: function (err) {
                        Console.log(err);
                    }
                });
        }
    });

    $('#close').click(function () {
        $('#viewModal').hide();
    });

    $('#projecttab').click(function () {
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
        $('#TestGrid').hide();
        $('#test').hide();
        $('#FormatGrid').hide();
        $('#format').hide();

        $('#ProjectGrid').show();
        $('#project').hide();
    });
    $('#ProjectBack').click(function () {
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
        $('#TestGrid').hide();
        $('#test').hide();
        $('#FormatGrid').hide();
        $('#format').hide();
        $('#ProjectGrid').show();
        $('#project').hide();

        $('#ddlProjectStudent').val('');
        $('#ProjectName').val('');
        $('#txtDescription').val('');
        $('#ProjectId').val('');
        $('#MilestoneDetails').text('');

    });
    $('#ProjectAddNew').click(function () {
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
        $('#test').hide();
        $('#FormatGrid').hide();
        $('#format').hide();

        $('#ddlProjectStudent').val('');
        $('#ProjectName').val('');
        $('#txtDescription').val('');
        $('#ProjectId').val('');
        $('#MilestoneDetails').text('');

        $('#ProjectGrid').hide();
        $('#project').show();

    });

});

window.onload = function () {
    //Build an array containing Customer records.
    var Project = new Array();
    //customers.push(["John Hammond", "United States"]);
    //customers.push(["Mudassar Khan", "India"]);
    //customers.push(["Suzanne Mathews", "France"]);
    //customers.push(["Robert Schidner", "Russia"]);

    //Add the data rows.
    for (var i = 0; i < Project.length; i++) {
        AddRowP(Project[i][0], Project[i][1]);
    }
};



function Add() {
    var Milestone = document.getElementById("txtMilestone");
    var Deadline = document.getElementById("txtDeadline");
    //var Define = document.getElementById("txtDefine");
    // var Status = document.getElementById("txtStatus");

    if ($('#txtMilestone').val() == '' || $('#txtMilestone').val() == null) {
        $('#msgMilestone').text('Please enter milestone.');
        $('#msgMilestone').show();
        $('#txtMilestone').focus();
        return;
    } else {
        $('#msgMilestone').text('');
        $('#msgMilestone').hide();
    }

    if ($('#txtDeadline').val() == '' || $('#txtDeadline').val() == null) {
        $('#msgDeadline').text('Please enter deadline.');
        $('#msgDeadline').show();
        $('#txtDeadline').focus();
        return;
    } else {
        $('#msgDeadline').text('');
        $('#msgDeadline').hide();
    }

    //if ($('#txtDefine').val() == '' || $('#txtDefine').val() == null) {
    //    $('#msgDefine').text('Please enter define.');
    //    $('#msgDefine').show();
    //    $('#txtDefine').focus();
    //    return;
    //} else {
    //    $('#msgDefine').text('');
    //    $('#msgDefine').hide();
    //}


    AddRowP(Milestone.value, Deadline.value);
    Milestone.value = "";
    Deadline.value = "";
    //Define.value = "";
    //Status.value = "";
};

function Remove(button) {
    //Determine the reference of the Row using the Button.
    var row = button.parentNode.parentNode;
    var name = row.getElementsByTagName("TD")[0].innerHTML;
    if (confirm("Do you want to delete: " + name)) {

        //Get the reference of the Table.
        var table = document.getElementById("tblMilestone");

        //Delete the Table row using it's Index.
        table.deleteRow(row.rowIndex);
    }
};

function AddRowP(Milestone, Deadline) {
    //Get the reference of the Table's TBODY element.
    var tBody = document.getElementById("tblMilestone").getElementsByTagName("TBODY")[0];

    //Add Row.
    row = tBody.insertRow(-1);

    //Add EssayName cell.
    var cell = row.insertCell(-1);
    cell.innerHTML = Milestone;

    //Add Topic cell.
    cell = row.insertCell(-1);
    cell.innerHTML = Deadline;

    //Add WordCount cell.
    //var cell = row.insertCell(-1);
    //cell.innerHTML = Define;


    //Add Button cell.
    cell = row.insertCell(-1);
    var btnRemove = document.createElement("INPUT");
    btnRemove.type = "button";
    btnRemove.value = "Remove";
    btnRemove.setAttribute("onclick", "Remove(this);");
    cell.appendChild(btnRemove);
}