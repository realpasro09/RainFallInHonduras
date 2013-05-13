define(["dataContext"], function (dc) {

    var viewModel = function () {
        var rainfallData = ko.observableArray([]),
            locationData = ko.observableArray([]),
            selectValue = ko.observable(),
            maxTemperature = ko.observable(0),
            minTemperature = ko.observable(0),
            avgMaxTemperature = ko.observable(0),
            avgMinTemperature = ko.observable(0),
            maxPrecipitation = ko.observable(0),
            minPrecipitacion = ko.observable(0),
            avgPrecipitacion = ko.observable(0);
        
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
                    $.each(rainfalldataFromServer.AlmanacDays, function (index, c) {
                        rainfallData.push(c);
                    });
                    maxTemperature = rainfalldataFromServer.MaxTemperature;
                    minTemperature = rainfalldataFromServer.MinTemperature;
                    avgMaxTemperature = rainfalldataFromServer.AvgMaxTemperature;
                    avgMinTemperature = rainfalldataFromServer.AvgMinTemperature;
                    maxPrecipitation = rainfalldataFromServer.MaxPrecipitation;
                    minPrecipitacion = rainfalldataFromServer.MinPrecipitation;
                    avgPrecipitacion = rainfalldataFromServer.AvgPrecipitation;
                });

            } else {
                dc.RainfallData.GetRainfallDataByLocation(val.CityId).done(function (locationdataFromServer) {
                    rainfallData.removeAll();
                    $.each(locationdataFromServer.AlmanacDays, function (index, c) {
                        rainfallData.push(c);
                    });
                    maxTemperature = locationdataFromServer.MaxTemperature;
                    minTemperature = locationdataFromServer.MinTemperature;
                    avgMaxTemperature = locationdataFromServer.AvgMaxTemperature;
                    avgMinTemperature = locationdataFromServer.AvgMinTemperature;
                    maxPrecipitation = locationdataFromServer.MaxPrecipitation;
                    minPrecipitacion = locationdataFromServer.MinPrecipitation;
                    avgPrecipitacion = locationdataFromServer.AvgPrecipitation;
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
            SelectedValue: selectValue,
            MaxTemperature: maxTemperature,
            MinTemperature: minTemperature,
            AvgMaxTemperature : avgMaxTemperature,
            AvgMinTemperature :avgMinTemperature,
            MaxPrecipitation : maxPrecipitation,
            MinPrecipitation : minPrecipitacion,
            AvgPrecipitation : avgPrecipitacion
        };
    }();
    return viewModel;
});