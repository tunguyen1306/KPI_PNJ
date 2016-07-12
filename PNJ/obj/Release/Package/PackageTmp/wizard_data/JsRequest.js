

//LoadDepartment();
LoadGroup();
function LoadGroup() {
    $.ajax({
        url: "/api/PNJ/getFilter/4/none",

        success: function (result) {
            $.each(result.data, function (i, value) {
                
                $("#Group").append("<option value= " + value.QUEUEID + ">" + value.QUEUENAME + "</option>")
            })
        }
    });

}

$('#Group').change(function (e) {

    $.ajax({
        url: "/api/PNJ/getFilter/5/" + $('#Group').find("option:selected").val(),
        success: function (result) {
            $('#Technician').children().remove();
            $("#Technician").append("<option value=''>Tất cả</option>");
            $.each(result.data, function (i, value) {
                $("#Technician").append("<option value= " + value.USER_ID + ">" + value.FIRST_NAME + "</option>")
            })
        }
    });
});

LoadCategory();
function LoadCategory()
{
    $.ajax({
        url: "/api/PNJ/getFilter/7/none",

        success: function (result) {
            $.each(result.data, function (i, value) {

                $("#Category").append("<option value= " + value.CATEGORYID + ">" + value.CATEGORYNAME + "</option>")
            })
        }
    });
}


$('#Category').change(function (e) {
    $.ajax({
        url: "/api/PNJ/getFilter_1/2/" + $('#Category').find("option:selected").val(),
        success: function (result) {
            $('#Subcategory').children().remove();
            $("#Subcategory").append("<option value=''>Tất cả</option>");
            $.each(result.data, function (i, value) {
                
                $("#Subcategory").append("<option value= " + value.SUBCATEGORYID + ">" + value.NAME + "</option>")
            })
        }
    });
});

$('#Subcategory').change(function (e) {
    $.ajax({
        url: "/api/PNJ/getFilter_1/3/" + $('#Subcategory').find("option:selected").val(),
        success: function (result) {
            $('#Item').children().remove();
            $("#Item").append("<option value=''>Tất cả</option>");
            $.each(result.data, function (i, value) {
                
                $("#Item").append("<option value= " + value.ITEMID + ">" + value.NAME + "</option>")
            })
        }
    });

});


function LoadDepartment() {
    $.ajax({
        url: "/api/PNJ/getFilter_1/0/none",

        success: function (result) {
            $.each(result.data, function (i, value) {

                $("#Department").append("<option value= " + value.DEPTID + ">" + value.Department + "</option>")
            })
        }
    });
}



