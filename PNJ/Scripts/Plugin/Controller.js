var app = angular.module("techcombankapp", []);
app.service('getData', ["$http", function (t) {
    this.Data = function (action, data) {
        return t.get("/api/TechcomBank/" + action).error(function () {
            //window.location = "/View/Error/404.html";
        });
    }
    this.TwoPara = function (type, condition) {
        return t.get("/api/TechcomBank/getFilter/" + type + "/" + condition).error(function () {
            //window.location = "/View/Error/404.html";
        });
    }
}])

app.controller("getDomain", ["$scope", "getData", function (s, g) {
    g.Data("getdomain", "").then(function (data) {
        s.getDomain = data.data.data;
    })
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
app.controller("getNavigation", ["$scope", "$http", function (s, t) {
    t.get("/api/TechcomBank/getMenu/TCB").then(function (data) {
        s.SiteMenu = data.data.data
    });

    s.Bam = function()
    {
        console.log("đã bấm vào đây");
    }
}]);

app.controller("getFilterRequest", ["getData", "$scope", function (g, s) {
    g.TwoPara(0, "none").then(function (data) {
        s.status = data.data.data;
    });
    g.TwoPara(1, "none").then(function (data) {
        s.overdue = data.data.data;
    });
    g.TwoPara(2, "none").then(function (data) {
        s.Requesters = data.data.data;
    });
    g.TwoPara(3, "none").then(function (data) {
        s.RequestType = data.data.data;
    });
    g.TwoPara(4, "none").then(function (data) {
        s.Group = data.data.data;
    });
    g.TwoPara(6, "none").then(function (data) {
        s.Prioritys = data.data.data;
    });

    s.Subcategory = {};
    s.Item = {};
    s.Technician = {};
    s.Change = function (type, data) {
        if (data === null || data === '') {
            switch (type) {
                case "category":
                    s.SubToItem = "";
                    s.ItemToFinish = "";
                    s.Subcategory = {};
                    s.Item = {};
                    break;
                case "subcategory":
                    s.ItemToFinish = "";
                    s.Item = {};
                    break;
                case "group":
                    s.Technician = {};
                    break;
            }
        }
        else {
            switch (type) {
                case "category":
                    s.SubToItem = "";
                    s.ItemToFinish = "";
                    g.TwoPara(8, data).then(function (data) {
                        s.Subcategory = data.data.data;
                    });
                    break;
                case "subcategory":
                    s.ItemToFinish = "";
                    g.TwoPara(9, data).then(function (data) {
                        s.Item = data.data.data;
                    });
                    break;
                case "group":
                    s.Technician = {};
                    g.TwoPara(5, data).then(function (data) {
                        s.Technician = data.data.data;
                    });
                    break;
            }
        }
    }

    g.TwoPara(7, "none").then(function (data) {
        s.Category = data.data.data;
    });
}]);
app.controller("getFilterContract", ["getData", "$scope", function (g, s) {
    g.TwoPara(10, "none").then(function (data) {
        s.ContractType = data.data.data;
    });
    g.TwoPara(11, "none").then(function (data) {
        s.ContractStatus = data.data.data;
    });
    g.TwoPara(12, "none").then(function (data) {
        s.Vendor = data.data.data;
    });
}]);