define(["dataContext"], function (dc) {

    var viewModel = function () {
        var rainfallData = ko.observableArray([]);
        var locationData = ko.observableArray([]);
        var selectValue = ko.observable();
        $(document).ajaxStart(function () {
            $("body").addClass("loading");
        });
        $(document).ajaxStop(function () {
            $("body").removeClass("loading");
        });
        dc.LocationData.Get().done(function (locationdataFromServer) {
            $.each(locationdataFromServer, function (index, c) {
                locationData.push(c);
            });
        });

        selectValue.subscribe(function (val) {
            if (val.CityId == 0) {
                dc.RainfallData.Get().done(function (rainfalldataFromServer) {
                    rainfallData.removeAll();
                    $.each(rainfalldataFromServer, function (index, c) {
                        rainfallData.push(c);
                    });


                });

            } else {
                dc.RainfallData.GetRainfallDataByLocation(val.CityId).done(function (locationdataFromServer) {
                    rainfallData.removeAll();
                    $.each(locationdataFromServer, function (index, c) {
                        rainfallData.push(c);
                    });
                });

            };
        });

        var gridViewModel = new ko.simpleGrid.viewModel({
            data: rainfallData,
            columns: [
                { headerText: "Date", rowText: "Date" },
                { headerText: "City", rowText: "City" },
                { headerText: "Precipitation", rowText: function (item) { return item.Precipitation.toFixed(2); } },
                { headerText: "High", rowText: function (item) { return item.TempHigh; } },
                { headerText: "Low", rowText: function (item) { return item.TempLow; } }
            ],
            pageSize: 10
        });


        return {
            GridViewModel: gridViewModel,
            RainfallData: rainfallData,
            LocationData: locationData,
            SelectedValue: selectValue
        };
    }();
    return viewModel;
});