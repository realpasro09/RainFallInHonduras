define(["config","server"], function(config, server) {
    
    return {
        RainfallData: {
            Get: function () {
                return server.Get("rainfalldata/get");
            },
            GetRainfallDataByLocation: function (locationId){
                return server.Get("rainfalldata/getrainfalldatabylocation", {locationId : locationId});
            },
            GetRainfallData: function (locationId, periodId) {
                return server.Get("rainfalldata/getrainfalldata", { locationId: locationId, periodId: periodId });
            }
        },
        LocationData: {
            Get: function () {
                return server.Get("locationdata/get");
            },
        },
        PeriodData: {
            Get: function() {
                return server.Get("perioddata/get");
            }
        }
    };
});