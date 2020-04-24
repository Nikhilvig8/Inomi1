if (top.location.pathname === "/HomeQuestionnaire/QuestionSelect") {
    var count = 0;
    $("#Next").on('click', function (e) {
        var countcheck = 0;
        var QuestionsId = "";
        $("#tblSelectQuestion tbody tr").each(function () {
            if ($(this).find('td:nth-child(2)').find('input[type=checkbox]').prop("checked") == true) {
                count++;
                var QuestionId = $(this).find('td:last-child').find('input[id=QuestionId]').val()
                QuestionsId += QuestionId + ","
                countcheck++;
            }
            //var SubStrengthId = $(this).find('td:last-child').find('input[id=SubStrengthId]').val()
            //var SubStrengthValue = $(this).find('td:last-child').find('input[id=SubStrengthValue]').val()
            //alert("QuestionId: " + QuestionId + " SubStrengthId: " + SubStrengthId)
        });

        //alert(count)
        //alert(QuestionsId.slice(0, -1))
        if (countcheck < 30) {
            swal("Alert", "Please select minimum of 30 Questions", "error");
        }
        else {

            $.ajax({
                type: "POST",
                dataType: "json",
                url: "/HomeQuestionnaire/QuestionSelect",
                data: { QuestionSelect: QuestionsId.slice(0, -1) },
                cache: false,
                success: function (result) {
                    if (result.Status == true) {
                        window.location.href = "/HomeQuestionnaire/Questions";
                    }
                },
                error: function (reponse) {

                }
            });

        }
        count = countcheck;
    });

    $(document).on('change', "label[id^='slider']", function () {
        var checkbox = $(this).find('input[type="checkbox"]');
        if ($(checkbox).prop('checked')) {
            count++;
        } else {
            count--;
        }
        $("#questionselected").text(count);
    });
}
if (top.location.pathname === "/HomeQuestionnaire/Questions") {
    $("#Submit").on('click', function (e) {
        var marks = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
        $("#tblQuestion tbody tr").each(function () {
            var QuestionId = $(this).find('td:last-child').find('input[id=QuestionId]').val()
            var SubStrengthId = $(this).find('td:last-child').find('input[id=SubStrengthId]').val()

            var Question = "#" + QuestionId + " option:selected"
            var Ans = $(Question).val();

            marks[SubStrengthId] += parseInt(Ans);
        });
        //alert(marks);

        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/HomeQuestionnaire/Questions",
            data: { Marks: marks },
            cache: false,
            success: function (result) {
                if (result.Status == true) {
                    window.location.href = "/DashBoardQuestionnaire/DashBoard";
                }
            },
            error: function (reponse) {

            }
        });
    });
}

if (window.location.href.indexOf("/AdminQuestionnaire/CareerData/")) {
    $("#addreading").on('click', function (e) {
        var count = $('.row2').length + 1
        $('#reading').append('<div class="row2"><label style = "padding-right:1rem;">' + count + '.</label><input class="form-control col-md-7" value="" placeholder="Name" id="reading' + count + '" /><input class="form-control col-md-3" value="#" placeholder="Link" id="readinglink' + count + '" style="margin-left:1rem;margin-bottom:1rem;" /> <button class="trash" id="readingtrash' + count + '"><i class="fas fa-trash" style="color:red"></i></button> </div>')
    });
    $("#addvideos").on('click', function (e) {
        var count = $('.videos2').length + 1
        $('#videos').append('<div class="videos2"><label style = "padding-right:1rem;">' + count + '.</label><input class="form-control col-md-7" value="" placeholder="Name" id="videos' + count + '" /><input class="form-control col-md-3" value="" placeholder="Link" id="videoslink' + count + '" style="margin-left:1rem;margin-bottom:1rem;" /> <button class="trash" id="videostrash' + count + '"><i class="fas fa-trash" style="color:red"></i></button> </div>')
    });
    $("#addonline").on('click', function (e) {
        var count = $('.online2').length + 1
        $('#online').append('<div class="online2"><label style = "padding-right:1rem;">' + count + '.</label><input class="form-control col-md-7" value="" placeholder="Name" id="online' + count + '" /><input class="form-control col-md-3" value="" placeholder="Link" id="onlinelink' + count + '" style="margin-left:1rem;margin-bottom:1rem;" /> <button class="trash" id="onlinetrash' + count + '"><i class="fas fa-trash" style="color:red"></i></button> </div>')
    });

    $(document).on('click', "button[id^='readingtrash']", function (e) {
        var pa = $(this).parent();
        pa.remove();
    });

    $(document).on('click', "button[id^='videostrash']", function (e) {
        var pa = $(this).parent();
        pa.remove();
    });

    $(document).on('click', "button[id^='onlinetrash']", function (e) {
        var pa = $(this).parent();
        pa.remove();
    });

    $("#update").on('click', function (e) {

        var CareerId = $("#CareerId").val();
        var maindescription = $("#maindescription").val();
        var subdescription = $("#subdescription").val();
        var course = $("#course").val();

        var count = 1;
        var readingarray = [];
        $('.row2').each(function () {
            var name = '#reading' + count
            var link = '#readinglink' + count
            var readingData = {};
            readingData.Name = $(name).val();
            readingData.Link = $(link).val();

            readingarray.push(readingData);
            count++;
        });

        var readingJSON = JSON.stringify(readingarray);

        count = 1;
        var videosarray = [];
        $('.videos2').each(function () {
            var name = '#videos' + count
            var link = '#videoslink' + count
            var videosData = {};
            videosData.Name = $(name).val();
            videosData.Link = $(link).val();

            videosarray.push(videosData);
            count++;
        });

        var videosJSON = JSON.stringify(videosarray);

        count = 1;
        var onlinearray = [];
        $('.online2').each(function () {
            var name = '#online' + count
            var link = '#onlinelink' + count
            var onlineData = {};
            onlineData.Name = $(name).val();
            onlineData.Link = $(link).val();

            onlinearray.push(onlineData);
            count++;
        });

        var onlineJSON = JSON.stringify(onlinearray);

        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/AdminQuestionnaire/UpdateCareer",
            data: { careerId: CareerId, maindescription: maindescription, subdescription: subdescription, course: course, reading: readingJSON, videos: videosJSON, online: onlineJSON },
            cache: false,
            success: function (result) {
                if (result == 1) {
                    alert("Updated Successfully")
                }
                else {
                    alert("Error")
                }
                window.location.href = "/AdminQuestionnaire/Career";

            },
            error: function (reponse) {

            }
        });
    });
}
