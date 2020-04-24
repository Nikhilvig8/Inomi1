$(document).ready(function () {
    
    $("#StudentReport").dataTable({

        "bPaginate": true
    });

    $('#btnSearch').click(function () {

        alert();
    });

    $("#btnSearch").click(function () {
        var StrMain = "";
        var ApplicationYear = $('#ApplicationYear').val();
        var Student = $('#Student').val();
        var School = $('#School').val();
        var Product = $('#Product').val();

        if (ApplicationYear != "" && ApplicationYear != null) {
            StrMain = StrMain + "And ApplicationYear=" + "'" + ApplicationYear + "'"
        };
        if (Student != "" && Student != null) {
            StrMain = StrMain + "And FirstName=" + "'" + Student + "'"
        };
        if (School != '' && School != null) {
            StrMain = StrMain + "And School=" + "'" + School + "'"
        };
        if (Product != '' && Product != null) {
            StrMain = StrMain + "And Product=" + "'" + Product + "'"
        };

        $.ajax
            ({
                url: "/ResultReport/BindStudentReport?Str=" + StrMain + "",
                type: "Get",
                success: function (data) {

                    var item = JSON.parse(data);

                    var htm = '';

                    htm += '<thead><tr>';
                    htm += '<th scope="col">Application Year</th>';
                    htm += '<th scope="col">Student Name</th>';
                    htm += '<th scope="col">School</th>';
                    htm += '<th scope="col">Product</th>';
                    htm += '<th scope="col">College Applied to</th>';
                    htm += '<th scope="col">Career</th>';
                    htm += '<th scope="col">Result</th>';
                    htm += '<th scope="col"></th>';
                    htm += '</tr></thead>';


                    for (var i = 0; i < item.length; i++) {
                        htm += '<tbody><tr">';
                        htm += '<td><label id="ApplicationYear" >' + item[i].ApplicationYear + '</label> </td>';
                        htm += '<td><label id="FirstName">' + item[i].FirstName + '</label></td>';
                        htm += '<td><label id="School">' + item[i].School + '</label></td>';
                        htm += '<td><label id="Product">' + item[i].Product + '</label></td>';
                        htm += '<td><label id="CollegeApply">' + item[i].CollegeApply + '</label></td>';
                        htm += '<td><label id="CareerApplying">' + item[i].CareerApplying + '</label></td>';
                        htm += '<td><label id="Result">' + item[i].Result + '</label></td>';
                        htm += '<td><a href="javascript:void;" class="btn red-btn">Pending</a></td>';
                        htm += '</tr></tbody>';
                    }
                    $('#StudentReport').html(htm);

                },
                Error: function (err) {
                    Console.log(err);
                }
            });
    });

});