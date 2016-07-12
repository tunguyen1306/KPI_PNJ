


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
//Get Value
function showSelectgroup() {
    var selO = document.getElementsByName('group1')[0];
    var selValues = [];
    for (i = 0; i < selO.length; i++) {
        if (selO.options[i].selected) {
            selValues.push(selO.options[i].value);
        }
    }
    $('#hdgroup').val(selValues);

}

function showSelecttech() {
    var selO = document.getElementsByName('tech1')[0];
    var selValues = [];
    for (i = 0; i < selO.length; i++) {
        if (selO.options[i].selected) {
            selValues.push(selO.options[i].value);
        }
    }
    $('#hdtech').val(selValues);

}

function showSelectsd() {
    var selO = document.getElementsByName('scd1')[0];
    var selValues = [];
    for (i = 0; i < selO.length; i++) {
        if (selO.options[i].selected) {
            selValues.push(selO.options[i].value);
        }
    }
    $('#hdscd').val(selValues);

}

function showSelectcd() {
    var selO = document.getElementsByName('cd1')[0];
    var selValues = [];
    for (i = 0; i < selO.length; i++) {
        if (selO.options[i].selected) {
            selValues.push(selO.options[i].value);
        }
    }
    $('#hdcd').val(selValues);

}

function showSelectsubcd() {
    var selO = document.getElementsByName('subcd1')[0];
    var selValues = [];
    for (i = 0; i < selO.length; i++) {
        if (selO.options[i].selected) {
            selValues.push(selO.options[i].value);
        }
    }
    $('#hdsubcd').val(selValues);

}

function showSelectdepart() {
    var selO = document.getElementsByName('depart1')[0];
    var selValues = [];
    for (i = 0; i < selO.length; i++) {
        if (selO.options[i].selected) {
            selValues.push(selO.options[i].value);
        }
    }
    $('#hddepart').val(selValues);

}

function showSelectrequester() {
    var selO = document.getElementsByName('requester1')[0];
    var selValues = [];
    for (i = 0; i < selO.length; i++) {
        if (selO.options[i].selected) {
            selValues.push(selO.options[i].value);
        }
    }
    $('#hddrequester').val(selValues);

}

function showSelectreQuy() {
    var selO = document.getElementsByName('Quy1')[0];
    var selValues = [];
    for (i = 0; i < selO.length; i++) {
        if (selO.options[i].selected) {
            selValues.push(selO.options[i].value);
        }
    }
    $('#hdQuy').val(selValues);

}
function showSelectredateTime_thang() {
    var selO = document.getElementsByName('dateTime_thang1')[0];
    var selValues = [];
    for (i = 0; i < selO.length; i++) {
        if (selO.options[i].selected) {
            selValues.push(selO.options[i].value);
        }
    }
    $('#hddateTime_thang').val(selValues);

}
function showSelectredateTime_nam() {
    var selO = document.getElementsByName('dateTime_nam1')[0];
    var selValues = [];
    for (i = 0; i < selO.length; i++) {
        if (selO.options[i].selected) {
            selValues.push(selO.options[i].value);
        }
    }
    $('#hddateTime_nam').val(selValues);

}

$('#example-dropUp').change(function () {
    showSelectgroup();
});
$('#example37').change(function () {
    showSelectreQuy();
});
$('#example38').change(function () {
    showSelectredateTime_thang();
});
$('#example39').change(function () {
    showSelectredateTime_nam();
});






$('#example42').change(function () {
    showSelecttech();
});
$('#example43').change(function () {
    showSelectsd();
});
$('#example44').change(function () {
    showSelectcd();
});
$('#example45').change(function () {
    showSelectsubcd();
});
$('#example47').change(function () {
    showSelectdepart();
});
$('#example48').change(function () {
    showSelectrequester();
});