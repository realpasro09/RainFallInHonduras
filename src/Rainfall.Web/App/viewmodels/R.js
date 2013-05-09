define(function (require) {
    
    //var rainFallData = require("server");
    var data = function () {
        this.features = $.getJSON("/RainfallData/Get", {
            format: "json"
        }).done(function() {
            alert("Done!! :D");
        });
    };

    return data;
});
