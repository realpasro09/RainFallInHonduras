define(["dataContext"], function (dc) {
    var viewModel = function () {
        var rainfallData = ko.observableArray();

        dc.RainfallData.Get().done(function(rainfalldataFromServer) {
            $.each(rainfalldataFromServer, function(index, c) {
                rainfallData.push(c);
            });
        });
        
        return {
            RainfallData: rainfallData
        };
    }();

    return viewModel;
});
