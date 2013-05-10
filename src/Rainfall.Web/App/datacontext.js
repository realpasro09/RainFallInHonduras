define(["config","server"], function(config, server) {
    
    return {
        RainfallData: {
            Get: function () {
                return server.Get("rainfalldata/get");
            },
            GetRainfallDataByLocation: function (locationId){
                return server.Get("rainfalldata/getrainfalldatabylocation", {locationId : locationId});
            },
        },
        LocationData: {
            Get: function () {
                return server.Get("locationdata/get");
            },
        }
    };
});