


    LoadGroup();
function LoadGroup() {
    $.ajax({
        url: "/api/PNJ/getFilter/4/none",
        success: function (result) {
            $(result.data).each(function (i, value) {
                $("#example-dropUp").append($("<option></option>").val(value.QUEUEID).html(value.QUEUENAME));
            });
            $("#example-dropUp").multiselect('rebuild');

           
        }

    });

}
/*Change group de load technician cac su kien */
$('#example-dropUp').change(function (e) {
    var brands = $('#example-dropUp option:selected');
    var selected = "";
    var parent = "";
  
    if (brands.length == 0) {
        $("#example42").multiselect('destroy');
        $('#example42').multiselect('refresh');
        $('#subtechnician').hide();
    }
    $("#example42").html("");
    $(brands).each(function (index, brand) {
       
        $.ajax({
          
            url: "/api/PNJ/getFilter/5/" + $(this).val(),
            success: function (result) {
              
                parent += "<optgroup id='" + index + "' label='" + brand.text + "'>";
                $.each(result.data, function (i, value) {
                  
                    parent += "<option value='" + value.USER_ID + "'>" + value.FIRST_NAME + "</option>";
                })
                parent += "</optgroup>";
               
                $("#example42").append(parent);
                parent = "";
                $("#example42").multiselect('rebuild');
             
            }
        });
    });
   
});


Servicategory();
function Servicategory() {
    $.ajax({
        url: "/api/PNJ/getFilter/15/none",
        success: function (result) {
            $(result.data).each(function (i, value) {
                $("#example43").append($("<option></option>").val(value.SERVICEID).html(value.NAME));
            });
            $("#example43").multiselect('rebuild');
        }
    });
}
//load category
$('#example43').change(function (e) {
    var brands = $('#example43 option:selected');
    var selected = "";
    var parent = "";

    if (brands.length == 0) {
        $("#example44").multiselect('destroy');
        $('#example44').multiselect('refresh');
        $('#sub_Category').hide();
    }
    $("#example44").html('');
    $(brands).each(function (index, brand) {
       
        $.ajax({

            url: "/api/PNJ/getservicecategorymap/" + $(this).val(),
            success: function (result) {

                parent += "<optgroup id='" + index + "' label='" + brand.text + "'>";
                $.each(result.data2, function (i, value) {

                    parent += "<option value='" + value.IdCategory + "'>" + value.NameCategory + "</option>";
                })
                parent += "</optgroup>";

                $("#example44").append(parent);
                parent = "";
                $("#example44").multiselect('rebuild');

            }
        });
       

    });

});

//Load subcategory 
$('#example44').change(function (e) {
    var brands = $('#example44 option:selected');
    var selected = "";
    var parent = "";
 
    if (brands.length == 0) {
        $("#example45").multiselect('destroy');
        $('#example45').multiselect('refresh');
        $('#sublastCategory').hide();
    }
    $("#example45").html('');
    $(brands).each(function (index, brand) {
        $.ajax({

            url: "/api/PNJ/getFilter_1/2/" + $(this).val(),
            success: function (result) {
                parent += "<optgroup id='" + index + "' label='" + brand.text + "'>";
                $.each(result.data, function (i, value) {

                    parent += "<option value='" + value.SUBCATEGORYID + "'>" + value.NAME + "</option>";
                })
                parent += "</optgroup>";

                $("#example45").append(parent);
                parent = "";
                $("#example45").multiselect('rebuild');

            }
        });
    });
});

//Load Department 
LoadDepartment();
function LoadDepartment() {
    $.ajax({
        url: "/api/PNJ/getFilter_1/0/none",
        success: function (result) {
            $.each(result.data, function (i, value) {
                $("#example47").append($("<option></option>").val(value.DEPTID).html(value.Department));
            });
            $("#example47").multiselect('rebuild');
        }
    });
}




//Load requester 
$('#example47').change(function (e) {
    var brands = $('#example47 option:selected');
    var selected = "";
    var parent = "";

    if (brands.length == 0) {
        $("#example48").multiselect('destroy');
        $('#example48').multiselect('refresh');
        $('#subRequester').hide();
    }
    $("#example48").html('');
    $(brands).each(function (index, brand) {
        $.ajax({

            url: "/api/PNJ/getFilter/16/" + $(this).val(),
            success: function (result) {
                parent += "<optgroup id='" + index + "' label='" + brand.text + "'>";
                $.each(result.data, function (i, value) {

                    parent += "<option value='" + value.Employeeid + "'>" + value.FIRST_NAME + "</option>";
                })
                parent += "</optgroup>";

                $("#example48").append(parent);
                parent = "";
                $("#example48").multiselect('rebuild');

            }
        });
    });
});
