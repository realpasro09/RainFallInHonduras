define(["config","server"], function(config, server) {
    
    return {
        RainfallData: {
            GetRainfallData: function (locationId, periodId) {
                return server.Get("rainfalldata/getrainfalldata", { locationId: locationId, periodId: periodId });
            },
            GetRainfallSummary: function(locationId, periodId) {
                return server.Get("rainfalldata/getrainfallsummary", {locationId :locationId, periodId: periodId});
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