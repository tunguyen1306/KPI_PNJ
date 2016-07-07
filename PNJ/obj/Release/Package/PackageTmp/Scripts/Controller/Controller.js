var app = angular.module("pnjapp", []);
app.service('getData', ["$http", function (t) {
    this.Data = function (action, data) {
        return t.get("/api/PNJ/" + action).error(function () {
            //window.location = "/View/Error/404.html";
        });
    }
    this.TwoPara = function (type, condition) {
        return t.get("/api/PNJ/getFilter/" + type + "/" + condition).error(function () {
            //window.location = "/View/Error/404.html";
        });
    }
}]);
app.controller("getNavigation", ["$scope", "$http", function (s, t) {
    t.get("/api/PNJ/getMenu/PNJ").then(function (data) {
        s.SiteMenu = data.data.data
    });

    s.Bam = function () {
        console.log("đã bấm vào đây");
    }
}]);
app.controller("logouts", ["$http", "$scope", function (h, s) {
    s.logout = function () {
        h.get("/Login/removeSS").then(function (data) {
            if (data.data === "clear") {
                window.location.reload();
            }
        });
    };
}]);

app.controller("getFilterContract", ["$http", "$scope", "getData", function (h, s, g) {
    g.TwoPara(3, "none").then(function (data) {
        s.Type = data.data.data;
    });
}]);
