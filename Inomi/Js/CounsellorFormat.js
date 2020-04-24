$(document).ready(function () {

    $('#FormatDetails').DataTable({
        "bSort": false
    });


    $("#FormatSubmit").click(function () {

        $("#FormatSubmit").text('Processing...');
        $("#FormatSubmit").attr("disabled", true);
        $("#FormatBack").attr("disabled", true);

        let data = [];

        let dataC = [];
        let dataS = [];

        var Id = $('#FormatId').val();
        var Format = $('#Format').val();
        var ddlFiletype = $('#ddlFiletype').val();
        var Emailsubject = $('#txtEmailsubject').val();
        //var emailBody = $('.ck-blurred').html();
        var emailBody = CKEDITOR.instances.editor.getData();
        var emailBodyValidation = $('.ck-blurred').text();

        function escapeHtml(emailBody) {
            return emailBody
                .replace(/&/g, "~amp;")
                .replace(/</g, "~lt;")
                .replace(/>/g, "~gt;")
                .replace(/"/g, "~quot;")
                .replace(/'/g, "~039;")
                .replace(/#/g, "~040;");
        }

        var EmailText = escapeHtml(emailBody);


        if (ddlFiletype == '0') {
            $('#msgFileType').text('Please choose file type.');
            $('#msgFileType').show();
            $('#ddlFiletype').focus();
            return;
        } else {
            $('#msgFileType').text('');
            $('#msgFileType').hide();
        }
        if (Format == '' || Format == null) {
            $('#msgFormat').text('Please choose Format.');
            $('#msgFormat').show();
            $('#Format').focus();
            $("#FormatSubmit").text('Submit');
            $("#FormatSubmit").attr("disabled", false);
            $("#FormatBack").attr("disabled", false);
            return;
        } else {
            $('#msgFormat').text('');
            $('#msgFormat').hide();
        }

        if (emailBody == '' || emailBody == null) {
            $('#msgeditor').text('Please enter email text.');
            $('#msgeditor').show();
            $("#FormatSubmit").text('Submit');
            $("#FormatSubmit").attr("disabled", false);
            $("#FormatBack").attr("disabled", false);
            return;
        }
        else {
            $('#msgeditor').text('');
            $('#msgeditor').hide();
        }

        data.push({
            Format: Format,
            ddlFiletype: ddlFiletype
        });

        $('#ddlCounsellor :selected').each(function () {
            debugger;
            let CounsellorId = $(this).val();
            dataC.push({
                CounsellorId: CounsellorId
            });
        });

        $('#ddlStudent :selected').each(function () {
            let StudentId = $(this).val();
            dataS.push({
                StudentId: StudentId
            });
        });

        var fileUpload = $("input[name='FormatFichier']").get(0);
        var files = fileUpload.files;

        var fileData = new FormData();

        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        var json = JSON.stringify(data);

        var jsonC = JSON.stringify(dataC);
        var jsonS = JSON.stringify(dataS);
        $.ajax
            ({
                url: "/AddNew/InsertFormatStatus?json=" + json + "&Id=" + Id + "&EmailText=" + EmailText + "&jsonC=" + jsonC + "&jsonS=" + jsonS + "&Emailsubject=" + Emailsubject + "",
                contentType: false,
                processData: false,
                type: "POST",
                data: fileData,
                success: function (drr) {
                    $("#FormatSubmit").text('Submit');
                    $("#FormatSubmit").attr("disabled", false);
                    $("#FormatBack").attr("disabled", false);
                    var tab = "FormatGrid";
                    var StrMain = "Email have been send successfully.";
                    var url = "/Format/Format/?StrMain=" + StrMain;
                    window.location.href = url;
                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    });

    $('#FormatDetails').on('click', '#delete', function () {
        var id = $(this).closest("tr").find('td:eq(1)').text().trim();
        var result = confirm("Do you Want to delete?");
        if (result) {
            $.ajax
                ({
                    url: "/AddNew/DeleteFormatDetails?id=" + id + "",
                    type: "Get",
                    success: function (drr) {
                        var tab = "FormatGrid";
                        var StrMain = "Record has been delete successfully.";
                        var url = "/Format/Format/?StrMain=" + StrMain;
                        window.location.href = url;
                    },
                    Error: function (err) {
                        Console.log(err);
                    }
                });

        }
    });

    $('#FormatDetails').on('click', '#edit', function () {
        $('#FormatGrid').hide();
        $('#format').show();
        $('#FormatSubmit').text('Update');
        var id = $(this).closest("tr").find('td:eq(1)').text();

        $.ajax
            ({
                url: "/AddNew/EditFormatDetails?id=" + id + "",
                type: "Get",
                success: function (data) {

                    var item = JSON.parse(data);

                    for (var i = 0; i < item.length; i++) {
                        $('#FormatId').val(item[0].Id)
                        $('#Format').val(item[0].Format)
                    }
                },
                Error: function (err) {
                    Console.log(err);
                }
            });

    });

});

$(window).on('load', function () {
    //$('#MddlCounsellor_checklist').css('position', 'relative');
    //$('#MddlCounsellor_checklist').css('height', '100px');
    //$('#MddlCounsellor_checklist').css('width', '100%');

    //$('#MddlCounsellor_findInList').css('width', '100%');
    //$('#MddlCounsellor').css('width', '100%');

    //$('#MddlStudent_checklist').css('position', 'relative');
    //$('#MddlStudent_checklist').css('height', '100px');
    //$('#MddlStudent_checklist').css('width', '100%');

    //$('#MddlStudent_findInList').css('width', '100%');
    //$('#MddlStudent').css('width', '100%');

    setTimeout(function () { $("#msgErroStudent").hide(); }, 4000);
});

