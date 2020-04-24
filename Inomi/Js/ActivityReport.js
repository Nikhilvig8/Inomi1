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

    $('#ActivityReprot').DataTable({
        "bSort": false
    });
    $('#btnSearch').click(function () {

        var StrMain = "";
        var Student = $('#Student').val();
        //var startDate = $('#startDate').val();
        //var endDate = $('#endDate').val();
        var Year = $('#studentApplicationYear').val();
        var School = $('#School').val();

        if (Student != "" && Student != null) {
            StrMain = StrMain + "And FirstName=" + "'" + Student + "'"
        };
        if (Year != "" && Year != null) {
            StrMain = StrMain + "And ApplicationYear=" + "'" + Year + "'"
        };
        if (School != "" && School != null) {
            StrMain = StrMain + "And School=" + "'" + School + "'"
        };


        document.location = '/ActivityReport/ActivityReport/?Str=' + StrMain;
    });

    $('#btnReset').click(function () {
        var StrMain = "";
        document.location = '/ActivityReport/ActivityReport/?Str=' + StrMain;
    });
});


