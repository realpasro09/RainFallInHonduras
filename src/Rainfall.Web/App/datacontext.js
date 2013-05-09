define(["config","server"], function(config, server) {
    
    return {
        RainfallData: {
            Get: function () {
                return server.Get("rainfalldata/get");
            },
            GetLocations: function () {
                return server.Get("rainfalldata/getlocations");
            }
        }
    };
});