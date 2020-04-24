$(document).ready(function () {

    $('#ProductDetails').DataTable({
        "bSort": false
    });


    $("#ProductSubmit").click(function () {

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
        var Id = $('#ProductId').val();
        var Name = $('#ProductName').val();
        var Fee = $('#ProductFee').val();

        var ProductDetails = $('#txtProductDetails').val();
        var Link = $('#ProductLink').val();
        var Picture = $('#ProductPicture').val();

        if (Name == '' || Name == null) {
            $('#msgProductName').text('Please enter Product Name.');
            $('#msgProductName').show();
            $('#ProductName').focus();
            return;
        }
        else {
            $('#msgProductName').text('');
            $('#msgProductName').hide();
        }


        if (Fee == '' || Fee == null) {
            $('#msgProductFee').text('Please enter Fee.');
            $('#msgProductFee').show();
            $('#ProductFee').focus();
            return;
        }
        else {
            $('#msgProductFee').text('');
            $('#msgProductFee').hide();
        }

        if (Link == '' || Link == null) {
            $('#msgProductLink').text('Please enter Link.');
            $('#msgProductLink').show();
            $('#ProductLink').focus();
            return;
        } else {
            $('#msgProductLink').text('');
            $('#msgProductLink').hide();
        }

        if (Picture == '' || Picture == null) {
            $('#msgProductPicture').text('Please Select Picture.');
            $('#msgProductPicture').show();
            $('#ProductPicture').focus();
            return;
        } else {
            $('#msgProductPicture').text('');
            $('#msgProductPicture').hide();
        }

        if (ProductDetails == '' || ProductDetails == null) {
            $('#msgProductDetails').text('Please enter product details.');
            $('#msgProductDetails').show();
            $('#txtProductDetails').focus();
            return;
        } else {
            $('#msgProductDetails').text('');
            $('#msgProductDetails').hide();
        }

        debugger;

        Id = escapeHtml(Id);
        Name = escapeHtml(Name);
        Fee = escapeHtml(Fee);

        ProductDetails = escapeHtml(ProductDetails);
        Link = escapeHtml(Link);
        Picture = escapeHtml(Picture);


        data.push({
            Id: Id,
            Name: Name,
            Fee: Fee,
            Link: Link,
            Picture: Picture,
            ProductDetails: ProductDetails

        });

        var fileUpload = $("input[name='productFicher']").get(0);
        var files = fileUpload.files;

        var fileData = new FormData();

        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        $("#ProductSubmit").text('Processing...');
        $("#ProductSubmit").attr("disabled", true);
        $("#ProductBack").attr("disabled", true);
        var json = JSON.stringify(data);
        $.ajax
            ({
                url: "/AddNew/InsertProductStatus?json=" + json + "&Id=" + Id + "",
                contentType: false,
                processData: false,
                type: "POST",
                data: fileData,
                success: function (drr) {
                    $("#ProductSubmit").text('Save');
                    $("#ProductSubmit").attr("disabled", false);
                    $("#ProductBack").attr("disabled", false);

                    var tab = "ProductGrid";
                    var StrMain = "Record has been save successfully.";
                    var url = "/AddNew/Student/?StrMain=" + StrMain + "&tab=" + tab;
                    window.location.href = url;
                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    });

    $('#ProductDetails').on('click', '#delete', function () {
        var id = $(this).closest("tr").find('td:eq(3)').text();
        var result = confirm("Do you Want to delete?");
        if (result) {
            $.ajax
                ({
                    url: "/AddNew/DeleteProductDetails?id=" + id + "",
                    type: "Get",
                    success: function (drr) {
                        var tab = "ProductGrid";
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

    $('#ProductDetails').on('click', '#edit', function () {
        $('#ProductGrid').hide();
        $('#product').show();
        $('#ProductSubmit').text('Update');
        var id = $(this).closest("tr").find('td:eq(3)').text();
        $.ajax
            ({
                url: "/AddNew/EditProductDetails?id=" + id + "",
                type: "Get",
                success: function (data) {

                    var item = JSON.parse(data);
                    debugger;
                    for (var i = 0; i < item.length; i++) {
                        $('#ProductId').val(item[0].Id)
                        $('#ProductName').val(item[0].Name)
                        $('#ProductFee').val(item[0].Fee)
                        $('#ProductLink').val(item[0].Link)
                        $('#ProductPicture').val(item[0].Attribute2)
                        $('#txtProductDetails').val(item[0].ProductDtls)
                    }
                },
                Error: function (err) {
                    Console.log(err);
                }
            });

    });

    $('#producttab').click(function () {
        $('#ProductGrid').show();
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
    });
    $('#ProductBack').click(function () {
        $('#ProductGrid').show();
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
        $('#ProductPicture').val('');
    });
    $('#ProductAddNew').click(function () {
        $('#ProductGrid').hide();
        $('#product').show();
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

        $('#ProductSubmit').val('Save');
        $('#ProductId').val('')
        $('#ProductName').val('')
        $('#ProductFee').val('')
        $('#ProductInstallment1').val('')
        $('#ProductDate1').val('')
        $('#ProductInstallment2').val('')
        $('#ProductDate2').val('')
        $('#ProductInstallment3').val('')
        $('#ProductDate3').val('')
        $('#ProductInstallment4').val('')
        $('#ProductDate4').val('')
        $('#ProductLink').val('')
        $('#Attribute1').val('')
        //$('.ck-blurred').html('');
        $('#ProductPicture').val('');
        $('.ck-blurred').html('');
    });

});