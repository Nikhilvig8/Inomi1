$(document).ready(function () {

    $('#FAQDetails').DataTable({
        "bSort": false,
        "scrollX": true
    });

    $("#btnSubmit").click(function () {

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
        var Id = $('#IdQuestion').val();
        var Question = $('#txtQuestion').val();
        var Answer = $('#txtAnswer').val();


        if (Question == '' || Question == null) {
            $('#txtQuestion').focus();
            $('#lblQuestion').text('Please enter question.');
            $('#lblQuestion').show();
            return;
        }
        else {
            $('#lblQuestion').text('');
            $('#lblQuestion').hide();
        }
        if (Answer == '' || Answer == null) {
            $('#txtAnswer').focus();
            $('#lblAnswer').text('Please enter answer.');
            $('#lblAnswer').show();
            return;
        }
        else {
            $('#lblAnswer').text('');
            $('#lblAnswer').hide();
        }

        Question = escapeHtml(Question);
        Answer = escapeHtml(Answer);

        data.push({
            Question: Question,
            Answer: Answer
        });

        $("#btnSubmit").text('Processing...');
        $("#btnSubmit").attr("disabled", true);
        $("#btnReset").attr("disabled", true);

        var json = JSON.stringify(data);
        $.ajax
            ({
                url: "/FAQ/FAQInsertData?json=" + json + "&Id=" + Id + "",
                type: "Get",
                success: function (drr) {
                    
                    $("#btnSubmit").text('Save');
                    $("#btnSubmit").attr("disabled", false);
                    $("#btnReset").attr("disabled", false);
                    $('#txtQuestion').val('');
                    $('#txtAnswer').val('');

                    var StrMain = "Record has been update successfully.";
                    var url = "/FAQ/FAQMaster/?StrMain=" + StrMain;
                    window.location.href = url;
                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    });

    $('#FAQDetails').on('click', '#delete', function () {
        var id = $(this).closest("tr").find('td:eq(2)').text();
        var result = confirm("Do you Want to delete?");
        if (result) {
            $.ajax
                ({
                    url: "/FAQ/DeleteFAQDetails?id=" + id + "",
                    type: "Get",
                    success: function (drr) {
                        var StrMain = "Record has been delete successfully.";
                        var url = "/FAQ/FAQMaster/?StrMain=" + StrMain;
                        window.location.href = url;
                    },
                    Error: function (err) {
                        Console.log(err);
                    }
                });
        }
    });

    $('#FAQDetails').on('click', '#edit', function () {

        $('#btnSubmit').text('Update');
        var id = $(this).closest("tr").find('td:eq(2)').text();
        debugger;
        $.ajax
            ({
                url: "/FAQ/EditFAQDetails?id=" + id + "",
                type: "Get",
                success: function (data) {

                    var item = JSON.parse(data);
                    debugger;
                    for (var i = 0; i < item.length; i++) {
                        $('#IdQuestion').val(item[0].Id)
                        $('#txtQuestion').val(item[0].Question)
                        $('#txtAnswer').val(item[0].Answer)
                    }

                },
                Error: function (err) {
                    Console.log(err);
                }
            });

    });

    $('#btnReset').click(function () {
        $('#txtQuestion').val('');
        $('#txtAnswer').val('');
        var url = "/FAQ/FAQMaster";
        window.location.href = url;
    });

});

$(window).on('load', function () {
    setTimeout(function () { $("#msgErroStudent").hide(); }, 4000);
});