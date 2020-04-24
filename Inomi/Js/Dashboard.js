
$(document).ready(function () {

    var today = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
    $('#startDate').datepicker({
        uiLibrary: 'bootstrap4',
        iconsLibrary: 'fontawesome',
        maxDate: function () {
            return $('#endDate').val();
        }
    });
    $('#endDate').datepicker({
        uiLibrary: 'bootstrap4',
        iconsLibrary: 'fontawesome',
        minDate: function () {
            return $('#startDate').val();
        }
    });

    $('#dashboardStudent').DataTable({
        "bSort": false
    });
    $('#StudentGrid').show();
    $('#student').hide();
    $('#btnSearch').click(function () {



        var StrMain = "";
        var Student = $('#ddlStudent').val();
        var Year = $('#studentApplicationYear').val();
        var School = $('#School').val();

        if (Student != "" && Student != null) {
            StrMain = StrMain + "And Id=" + "'" + Student + "'"
        };
        if (Year != "" && Year != null) {
            StrMain = StrMain + "And ApplicationYear=" + "'" + Year + "'"
        };
        if (School != "" && School != null) {
            StrMain = StrMain + "And School=" + "'" + School + "'"
        };


        document.location = '/Dashboard/Dashboard/?Str=' + StrMain;
    });

    $('#btnReset').click(function () {
        var StrMain = "";
        document.location = '/Dashboard/Dashboard/?Str=' + StrMain;
    });

    $('#dashboardStudent').on('click', '#delete', function () {
        var id = $(this).closest("tr").find('td:eq(8)').text();
        var result = confirm("Do you Want to delete?");
        if (result) {
            $.ajax
                ({
                    url: "/AddNew/DeleteStudentDetails?id=" + id + "",
                    type: "Get",
                    success: function (drr) {
                        var Str = "";
                        var StrMain = "Record has been delete successfully.";
                        var url = "/Dashboard/Dashboard/?Str=" + Str + "&StrMain=" + StrMain;
                        window.location.href = url;
                    },
                    Error: function (err) {
                        Console.log(err);
                    }
                });
        }
    });

    $('#dashboardStudent').on('click', '#edit', function () {
        $('#StudentGrid').hide();
        $('#student').show();
        $('#StudentSubmit').text('Update');
        var id = $(this).closest("tr").find('td:eq(8)').text();
        $.ajax
            ({
                url: "/AddNew/EditStudentDetails?id=" + id + "",
                type: "Get",
                success: function (data) {

                    var item = JSON.parse(data);
                    for (var i = 0; i < item.length; i++) {

                        var k = i;
                        $('#NameId').val(item[0].ID)
                        $('#studentName').val(item[0].FirstName)
                        $('#studentGrade').val(item[0].Grade)
                        $('#studentSchool').val(item[0].School)
                        $('#studentPhone').val(item[0].Phone)
                        $('#studentEmail').val(item[0].Email)
                        $('#studentGender').val(item[0].Gender)
                        $('#studentProduct').val(item[0].Product)
                        $('#txtInstallment').val(item[0].InstallmentCount)
                        if (i == 0) {
                            InstallmentFn();
                        }

                        $('#InstallmentDate' + (k + 1) + '').val(item[i].InsDate);
                        $('#Installment' + (k + 1) + '').val(item[i].InsAmt);

                        $('#studentPicture').val(item[0].Picture)

                        $('#studentParent1Name').val(item[0].Parent1Name)
                        $('#studentParent1Occupation').val(item[0].Parent1Occupation)
                        $('#studentParent1Phone').val(item[0].Parent1Phone)
                        $('#studentParent1Email').val(item[0].Parent1Email)

                        $('#studentParent2Name').val(item[0].Parent2Name)
                        $('#studentParent2Occupation').val(item[0].Parent2Occupation)
                        $('#studentParent2Phone').val(item[0].Parent2Phone)
                        $('#studentParent2Email').val(item[0].Parent2Email)

                        $('#studentCareerApplying').val(item[0].CareerApplying)
                        $('#studentCountryApplying').val(item[0].CountryApplying)
                        $('#studentApplicationYear').val(item[0].ApplicationYear)
                        $('#studentFinacialAidNeeded').val(item[0].FinacialAidNeeded)
                        $('#studentCounsellor').val(item[0].Counsellor)

                        $('#txtConcession').val(item[0].IsConcession);
                        if (item[0].IsConcession == 2) {
                            $('#divConAmt').show();
                        }
                        else {
                            $('#divConAmt').hide();
                        }

                        $('#txtConAmt').val(item[0].ConcessionAmount);

                        $('#studentEmail').attr("disabled", true);
                        $('#studentParent1Email').attr("disabled", true);
                        $('#studentParent2Email').attr("disabled", true);
                    }

                },
                Error: function (err) {
                    Console.log(err);
                }
            });

    });

    function clearflied() {
        $('#StudentSubmit').text('Save');
        $('#Id').val('')
        $('#studentName').val('')
        $('#studentGrade').val('')
        $('#studentSchool').val('')
        $('#studentPhone').val('')
        $('#studentEmail').val('')
        $('#studentGender').val('--Select--')
        $('#studentProduct').val('')
        $('#studentPicture').val('')
        $('#studentParent1Name').val('')
        $('#studentParent1Occupation').val('')
        $('#studentParent1Phone').val('')
        $('#studentParent1Email').val('')
        $('#studentParent2Name').val('')
        $('#studentParent2Occupation').val('')
        $('#studentParent2Phone').val('')
        $('#studentParent2Email').val('')
        $('#studentCareerApplying').val('')
        $('#studentCountryApplying').val('')
        $('#studentApplicationYear').val('')
        $('#studentFinacialAidNeeded').val('')
        $('#studentCounsellor').val('')

        $('#txtInstallment').val('--Select--')
        $('#InstallmentDate').html('')
        $('#txtConcession').val('1')
        $('#txtConAmt').val('')
    }


    $('#StudentBack').click(function () {
        $('#StudentGrid').show();
        $('#student').hide();
        $('#StudentSubmit').text('Save');
    });

    $("#StudentSubmit").click(function () {

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
        let Insdata = [];

        var Id = $('#NameId').val();
        var Name = $('#studentName').val();
        var Grade = $('#studentGrade').val();
        var School = $('#studentSchool').val();
        var Phone = $('#studentPhone').val();
        var Email = $('#studentEmail').val();
        var Gender = $('#studentGender').val();
        var Product = $('#studentProduct').val();

        var InstallmentCount = $('#txtInstallment').val();
        var Concession = $('#txtConcession').val();
        var ConcessionAmount = $('#txtConAmt').val();

        var Picture = $('#studentPicture').val();

        var Parent1Name = $('#studentParent1Name').val();
        var Parent1Occupation = $('#studentParent1Occupation').val();
        var Parent1Phone = $('#studentParent1Phone').val();
        var Parent1Email = $('#studentParent1Email').val();

        var Parent2Name = $('#studentParent2Name').val();
        var Parent2Occupation = $('#studentParent2Occupation').val();
        var Parent2Phone = $('#studentParent2Phone').val();
        var Parent2Email = $('#studentParent2Email').val();

        var CareerApplying = $('#studentCareerApplying').val();
        var CountryApplying = $('#studentCountryApplying').val();
        var ApplicationYear = $('#studentApplicationYear').val();
        var FinacialAidNeeded = $('#studentFinacialAidNeeded').val();
        var Counsellor = $('#studentCounsellor').val();

        var filter = /^[7-9][0-9]{9}$/;
        var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;

        if (Name == '' || Name == null) {
            $('#msgStudent').text('Please enter Name.');
            $('#msgStudent').show();
            $('#studentName').focus();
            return;
        }
        else {
            $('#msgStudent').text('');
            $('#msgStudent').hide();
        }

        if (Grade == '' || Grade == null) {
            $('#msgGrade').text('Please enter Grade.');
            $('#msgGrade').show();
            $('#studentGrade').focus();
            return;
        }
        else {
            $('#msgGrade').text('');
            $('#msgGrade').hide();
        }
        if (School == '' || School == null) {
            $('#msgSchool').text('Please enter School.');
            $('#msgSchool').show();
            $('#studentSchool').focus();
            return;
        }
        else {
            $('#msgSchool').text('');
            $('#msgSchool').hide();
        }
        if (Phone == '' || Phone == null) {
            $('#msgPhone').text('Please enter Phone No.');
            $('#msgPhone').show();
            $('#studentPhone').focus();
            return;
        }
        else {
            $('#msgPhone').text('');
            $('#msgPhone').hide();
        }

        if (filter.test(Phone)) {
            $('#msgPhone').text('');
            $('#msgPhone').hide();
        }
        else {
            $('#msgPhone').text("Please enter valid mobile no.");
            $('#msgPhone').show();
            return;
        }


        if (Email == '' || Email == null) {
            $('#msgEmail').text('Please enter Email.');
            $('#msgEmail').show();
            $('#studentEmail').focus();
            return;
        }
        else {
            $('#msgEmail').text('');
            $('#msgEmail').hide();
        }
        if (testEmail.test(Email)) {
            $('#msgEmail').text('');
            $('#msgEmail').hide();
        }
        else {
            $('#msgEmail').text('Please enter valid email.');
            $('#msgEmail').show();
            $('#studentEmail').focus();
            return;
        }


        if (Gender == '' || Gender == null || Gender == '--Select--') {
            $('#msgGender').text('Please select Gender.');
            $('#msgGender').show();
            $('#studentGender').focus();
            return;
        }
        else {
            $('#msgGender').text('');
            $('#msgGender').hide();
        }
        if (Product == '' || Product == null) {
            $('#msgProduct').text('Please enter Product.');
            $('#msgProduct').show();
            $('#studentProduct').focus();
            return;
        }
        else {
            $('#msgProduct').text('');
            $('#msgProduct').hide();
        }

        if (InstallmentCount == '' || InstallmentCount == null || InstallmentCount == '--Select--') {
            $('#msgInstallment').text('Please select Installment No.');
            $('#msgInstallment').show();
            $('#txtInstallment').focus();
            return;
        }
        else {
            $('#msgInstallment').text('');
            $('#msgInstallment').hide();
        }

        if (Concession == 2) {
            if (ConcessionAmount == '' || ConcessionAmount == null) {
                $('#msgConAmt').text('Please select Installment No.');
                $('#msgConAmt').show();
                $('#txtConAmt').focus();
                return;
            }
            else {
                $('#msgConAmt').text('');
                $('#msgConAmt').hide();
            }
        }


        if (Picture == '' || Picture == null) {
            $('#msgPicture').text('Please choose Picture.');
            $('#msgPicture').show();
            $('#studentPicture').focus();
            return;
        }
        else {
            $('#msgPicture').text('');
            $('#msgPicture').hide();
        }
        if (Parent1Name == '' || Parent1Name == null) {
            $('#msgParent1Name').text('Please enter Parent Name.');
            $('#msgParent1Name').show();
            $('#studentParent1Name').focus();
            return;
        }
        else {
            $('#msgParent1Name').text('');
            $('#msgParent1Name').hide();
        }
        if (Parent1Occupation == '' || Parent1Occupation == null) {
            $('#msgParent1Occupation').text('Please enter Parent Occupation.');
            $('#msgParent1Occupation').show();
            $('#studentParent1Occupation').focus();
            return;
        }
        else {
            $('#msgParent1Occupation').text('');
            $('#msgParent1Occupation').hide();
        }
        if (Parent1Phone == '' || Parent1Phone == null) {
            $('#msgParent1Phone').text('Please enter Parent Phone.');
            $('#msgParent1Phone').show();
            $('#studentParent1Phone').focus();
            return;
        }
        else {
            $('#msgParent1Phone').text('');
            $('#msgParent1Phone').hide();
        }

        if (filter.test(Parent1Phone)) {
            $('#msgParent1Phone').text('');
            $('#msgParent1Phone').hide();
        }
        else {
            $('#msgParent1Phone').text("Please enter valid mobile no.");
            $('#msgParent1Phone').show();
            return;
        }


        if (Parent1Email == '' || Parent1Email == null) {
            $('#msgParent1Email').text('Please enter Parent Email.');
            $('#msgParent1Email').show();
            $('#studentParent1Email').focus();
            return;
        }
        else {
            $('#msgParent1Email').text('');
            $('#msgParent1Email').hide();
        }
        if (Parent1Email == '' || Parent1Email == null) {
            $('#msgParent1Email').text('Please enter Parent Email.');
            $('#msgParent1Email').show();
            $('#studentParent1Email').focus();
            return;
        }
        else {
            $('#msgParent1Email').text('');
            $('#msgParent1Email').hide();
        }

        if (Parent2Name == '' || Parent2Name == null) {
            $('#msgParent2Name').text('Please enter Parent Name.');
            $('#msgParent2Name').show();
            $('#studentParent2Name').focus();
            return;
        }
        else {
            $('#msgParent2Name').text('');
            $('#msgParent2Name').hide();
        }
        if (Parent2Occupation == '' || Parent2Occupation == null) {
            $('#msgParent2Occupation').text('Please enter Parent Occupation.');
            $('#msgParent2Occupation').show();
            $('#studentParent2Occupation').focus();
            return;
        }
        else {
            $('#msgParent2Occupation').text('');
            $('#msgParent2Occupation').hide();
        }
        if (Parent2Phone == '' || Parent2Phone == null) {
            $('#msgParent2Phone').text('Please enter Parent Phone.');
            $('#msgParent2Phone').show();
            $('#studentParent2Phone').focus();
            return;
        }
        else {
            $('#msgParent2Phone').text('');
            $('#msgParent2Phone').hide();
        }

        if (filter.test(Parent2Phone)) {
            $('#msgParent2Phone').text('');
            $('#msgParent2Phone').hide();
        }
        else {
            $('#msgParent2Phone').text("Please enter valid mobile no.");
            $('#msgParent2Phone').show();
            return;
        }

        if (Parent2Email == '' || Parent2Email == null) {
            $('#msgParent2Email').text('Please enter Parent Email.');
            $('#msgParent2Email').show();
            $('#studentParent2Email').focus();
            return;
        }
        else {
            $('#msgParent2Email').text('');
            $('#msgParent2Email').hide();
        }


        if (testEmail.test(Parent2Email)) {
            $('#msgParent2Email').text('');
            $('#msgParent2Email').hide();
        }
        else {
            $('#msgParent2Email').text('Please enter valid email.');
            $('#msgParent2Email').show();
            $('#studentParent2Email').focus();
            return;
        }

        if (CareerApplying == '' || CareerApplying == null) {
            $('#msgCareerApplying').text('Please enter Career Applying.');
            $('#msgCareerApplying').show();
            $('#studentCareerApplying').focus();
            return;
        }
        else {
            $('#msgCareerApplying').text('');
            $('#msgCareerApplying').hide();
        }
        if (CountryApplying == '' || CountryApplying == null) {
            $('#msgCountryApplying').text('Please enter Country Applying.');
            $('#msgCountryApplying').show();
            $('#studentCountryApplying').focus();
            return;
        }
        else {
            $('#msgCountryApplying').text('');
            $('#msgCountryApplying').hide();
        }
        if (ApplicationYear == '' || ApplicationYear == null) {
            $('#msgApplicationYear').text('Please enter Application Year.');
            $('#msgApplicationYear').show();
            $('#studentApplicationYear').focus();
            return;
        }
        else {
            $('#msgApplicationYear').text('');
            $('#msgApplicationYear').hide();
        }
        if (FinacialAidNeeded == '' || FinacialAidNeeded == null) {
            $('#msgFinacialAidNeeded').text('Please enter Finacial Aid Needed.');
            $('#msgFinacialAidNeeded').show();
            $('#studentFinacialAidNeeded').focus();
            return;
        }
        else {
            $('#msgFinacialAidNeeded').text('');
            $('#msgFinacialAidNeeded').hide();
        }
        if (Counsellor == '' || Counsellor == null || Counsellor == '--Select Counsellor--') {
            $('#msgCounsellorList').text('Please select Counsellor.');
            $('#msgCounsellorList').show();
            $('#studentCounsellor').focus();
            return;
        }
        else {
            $('#msgCounsellorList').text('');
            $('#msgCounsellorList').hide();
        }


        Name = escapeHtml(Name);
        Grade = escapeHtml(Grade);
        School = escapeHtml(School);
        Phone = escapeHtml(Phone);
        Email = escapeHtml(Email);
        Gender = escapeHtml(Gender);
        Product = escapeHtml(Product);
        Picture = escapeHtml(Picture);

        Parent1Name = escapeHtml(Parent1Name);
        Parent1Occupation = escapeHtml(Parent1Occupation);
        Parent1Phone = escapeHtml(Parent1Phone);
        Parent1Email = escapeHtml(Parent1Email);

        Parent2Name = escapeHtml(Parent2Name);
        Parent2Occupation = escapeHtml(Parent2Occupation);
        Parent2Phone = escapeHtml(Parent2Phone);
        Parent2Email = escapeHtml(Parent2Email);

        CareerApplying = escapeHtml(CareerApplying);
        CountryApplying = escapeHtml(CountryApplying);
        ApplicationYear = escapeHtml(ApplicationYear);
        FinacialAidNeeded = escapeHtml(FinacialAidNeeded);
        Counsellor = escapeHtml(Counsellor);
        InstallmentCount = escapeHtml(InstallmentCount);
        IsConcession = escapeHtml(Concession);
        ConcessionAmount = escapeHtml(ConcessionAmount);

        data.push({
            Name: Name,
            Grade: Grade,
            School: School,
            Phone: Phone,
            Email: Email,
            Gender: Gender,
            Product: Product,
            Picture: Picture,

            Parent1Name: Parent1Name,
            Parent1Occupation: Parent1Occupation,
            Parent1Phone: Parent1Phone,
            Parent1Email: Parent1Email,

            Parent2Name: Parent2Name,
            Parent2Occupation: Parent2Occupation,
            Parent2Phone: Parent2Phone,
            Parent2Email: Parent2Email,

            CareerApplying: CareerApplying,
            CountryApplying: CountryApplying,
            ApplicationYear: ApplicationYear,
            FinacialAidNeeded: FinacialAidNeeded,
            Counsellor: Counsellor,
            InstallmentCount: InstallmentCount,
            IsConcession: Concession,
            ConcessionAmount: ConcessionAmount
        });

        for (var i = 1; i <= InstallmentCount; i++) {

            let InsDate = $('#InstallmentDate' + i + '').val();
            let InsAmt = $('#Installment' + i + '').val();

            if (InsAmt == '' || InsAmt == null) {
                $('#msgInstallment' + i + '').text('Please enter intallment.');
                $('#msgInstallment' + i + '').show();
                $('#Installment' + i + '').focus();
                return;
            }
            else {
                $('#msgInstallment' + i + '').text('');
                $('#msgInstallment' + i + '').hide();
            }


            if (InsDate == '' || InsDate == null) {
                $('#msgInstallmentDate' + i + '').text('Please enter intallment Date.');
                $('#msgInstallmentDate' + i + '').show();
                $('#InstallmentDate' + i + '').focus();
                return;
            }
            else {
                $('#msgInstallmentDate' + i + '').text('');
                $('#msgInstallmentDate' + i + '').hide();
            }

            Insdata.push({
                InsDate: InsDate,
                InsAmt: InsAmt
            });
        }


        var fileUpload = $("input[name='studentFichier']").get(0);
        var files = fileUpload.files;

        var fileData = new FormData();

        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        var json = JSON.stringify(data);

        var jsonIns = JSON.stringify(Insdata);

        $("#StudentSubmit").text('Processing...');
        $("#StudentSubmit").attr("disabled", true);
        $("#StudentBack").attr("disabled", true);

        $.ajax
            ({
                url: "/AddNew/InsertStudentStatus?json=" + json + "&Id=" + Id + "&jsonIns=" + jsonIns + "",
                contentType: false,
                processData: false,
                type: "POST",
                data: fileData,
                success: function (data) {

                    $("#StudentSubmit").text('Update');
                    $("#StudentSubmit").attr("disabled", false);
                    $("#StudentBack").attr("disabled", false);

                    var Str = "";
                    var StrMain = "";

                    var item = JSON.parse(data);
                    for (var i = 0; i < item.length; i++) {

                        if (item[0].InsertStatus == 'Student email id already exist.') {
                            $('#msgEmail').text(item[0].InsertStatus);
                            $('#msgEmail').show();
                            $('#studentEmail').focus();
                            return;
                        }
                        else {
                            $('#msgEmail').text('');
                            $('#msgEmail').hide();
                        }
                        if (item[0].InsertStatus == 'Father email id already exist.') {
                            $('#msgParent1Email').text(item[0].InsertStatus);
                            $('#msgParent1Email').show();
                            $('#studentParent1Email').focus();
                            return;
                        }
                        else {
                            $('#msgParent1Email').text('');
                            $('#msgParent1Email').hide();
                        }

                        if (item[0].InsertStatus == 'Mother email id already exist.') {
                            $('#msgParent2Email').text(item[0].InsertStatus);
                            $('#msgParent2Email').show();
                            $('#studentParent2Email').focus();
                            return;
                        }
                        else {
                            $('#msgParent2Email').text('');
                            $('#msgParent2Email').hide();
                        }


                        StrMain = (item[0].InsertStatus)

                    }



                    var url = "/Dashboard/Dashboard/?Str=" + Str+"&StrMain=" + StrMain;
                    clearflied();
                    window.location.href = url;
                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    });

    $("#txtInstallment").change(function () {
        
        InstallmentFn();
    });

    $('#divConAmt').hide();
    $("#txtConcession").change(function () {
        var cons = $("#txtConcession").val();
        if (cons == 1) {
            $('#divConAmt').hide();
            $('#txtConAmt').val('0');
        }
        else {
            $('#divConAmt').show();
            $('#txtConAmt').val('');
        }
    });

    function InstallmentFn() {
        var Count = $("#txtInstallment").val();
        var InsCount = parseInt(Count);
        var htm = '';
        for (var i = 1; i <= InsCount; i++) {
            htm += '<div class="form-group col-sm-3 col-12 date"><label>Installment ' + i + ' <sup>*</sup></label><input type="text" id="Installment' + i + '" class="form-control"><label style="display:none;color:red" id="msgInstallment' + i + '"></label></div><div class="form-group col-sm-3 col-12 date"><label>Installment Deadline ' + i + ' <sup>*</sup></label><input type="text" id="InstallmentDate' + i + '" class="form-control InsDate" readonly><label style="display:none;color:red" id="msgInstallmentDate' + i + '"></label></div>';
        }
        $('#InstallmentDate').html(htm);
        for (var i = 1; i <= InsCount; i++) {
            var today = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
            $('#InstallmentDate' + i + '').datepicker({
                uiLibrary: 'bootstrap4',
                iconsLibrary: 'fontawesome',
                minDate: today,
                maxDate: function () {
                    return $('#endDate').val();
                }
            });
        }
    }

    //$('#txtInstallment').keyup(function () {
    //    if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
    //        this.value = this.value.replace(/[^0-9\.]/g, '');
    //    }
    //});
    $('#txtConAmt').keyup(function () {
        if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
            this.value = this.value.replace(/[^0-9\.]/g, '');
        }
    });

    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }

    $(window).on('load', function () {
        setTimeout(function () { $("#msgErroStudent").hide(); }, 4000);
    });

});


$(document).ready(function () {
    var item = [];
    var itemC = [];
    reload_cart('', 'Product');
    CounselorStatus();
    TotalApplication();
    CartCounsellerPendingTask();

    function reload_cart(ProductId, TabId) {

        $.ajax
            ({
                url: "/Dashboard/GetProductChart?ProductId=" + ProductId + "&TabId=" + TabId + "",
                type: "Get",
                success: function (dataP) {

                    item = '';
                    item = JSON.parse(dataP);
                    google.charts.load('current', { 'packages': ['bar'] });
                    google.charts.setOnLoadCallback(drawChart);
                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    }

    $("#Product").click(function () {
        var ProductId = $('#ProductId').val();
        var TabId = 'Product';
        reload_cart(ProductId, TabId);
    });

    $("#Student").click(function () {
        var ProductId = $('#ProductId').val();
        var TabId = 'Student';
        reload_cart(ProductId, TabId);
    });

    $("#ProductId").change(function () {
        var ProductId = $('#ProductId').val();
        var TabId = $('#Product').text().trim();
        reload_cart(ProductId, TabId);
    });

    function drawChart() {
        var data = google.visualization.arrayToDataTable([
            ['', ''],
            ['Jan', item[0].ProductCount],
            ['Feb', item[1].ProductCount],
            ['Mar', item[2].ProductCount],
            ['Apr', item[3].ProductCount],
            ['May', item[4].ProductCount],
            ['Jun', item[5].ProductCount],
            ['Jul', item[6].ProductCount],
            ['Aug', item[7].ProductCount],
            ['Sept', item[8].ProductCount],
            ['Oct', item[9].ProductCount],
            ['Nov', item[10].ProductCount],
            ['Dec', item[11].ProductCount]

        ]);

        var options = {
            legend: { position: 'none' },
            chart: {
            }
        };

        var chart = new google.charts.Bar(document.getElementById('columnchart_material'));

        chart.draw(data, google.charts.Bar.convertOptions(options));
    }


    function CounselorStatus() {
        google.charts.load("current", { packages: ["corechart"] });
        google.charts.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = google.visualization.arrayToDataTable([
                ['', ''],
                ['Task', 2],
                ['Essay', 2],
                ['Messages', 2],

            ]);

            var options = {
                title: '',
                pieHole: 0.7,
                pieSliceTextStyle: {
                    color: 'black',
                },
            };

            var chart = new google.visualization.PieChart(document.getElementById('CounsellorChart'));
            chart.draw(data, options);
        }
    }

    function TotalApplication() {
        google.charts.load("current", { packages: ["corechart"] });
        google.charts.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = google.visualization.arrayToDataTable([
                ['', ''],
                ['Accepted', 2],
                ['Rejected', 2],
            ]);

            var options = {
                title: '',
                pieHole: 0.7,
                pieSliceTextStyle: {
                    color: 'black',
                },
            };

            var chart = new google.visualization.PieChart(document.getElementById('TotalApplications'));
            chart.draw(data, options);
        }
    }


    function CartCounsellerPendingTask() {

        $.ajax
            ({
                url: "/Dashboard/GetCounsellerPendingTask",
                type: "Get",
                success: function (dataC) {

                    itemC = '';
                    itemC = JSON.parse(dataC);

                    for (var i = 0; i < item.length; i++) {
                        $('#lblTotalTaks').text(itemC[0].TotalCount)
                        $('#lblPendingTaks').text(itemC[0].TotalTask)
                        $('#lblPendingEssay').text(itemC[0].TotalEssay)
                        $('#lblPendingMessage').text(itemC[0].TotalMessage)
                    }

                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    }


});

