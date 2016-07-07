$("#logout_sess").click(function () {
    jQuery.ajax({
        url: '/Login/removeSS',
        type: "GET",
        data: "",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data == "clear") {
                window.location.reload();
            }
        }
    });
});