define(["dataContext"], function (dc) {

    var viewModel = function () {
        var rainfallData = ko.observableArray([]),
            locationData = ko.observableArray([]),
            periodData = ko.observableArray([]),
            selectedLocation = ko.observable(),
            locationValue = ko.observable(0),
            periodValue = ko.observable(0),
            selectedPeriod = ko.observable(),
            maxTemperature = ko.observable(0),
            minTemperature = ko.observable(0),
            avgTemperature = ko.observable(0),
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

        dc.PeriodData.Get().done(function (perioddataFromServer) {
            $.each(perioddataFromServer, function (index, c) {
                periodData.push(c);
            });
        });
        
        selectedLocation.subscribe(function (val) {
            locationValue(val.CityId);
            dc.RainfallData.GetRainfallData(locationValue, periodValue)
                .done(function(rainfalldataFromServer) {
                    rainfallData.removeAll();
                    $.each(rainfalldataFromServer.AlmanacDays, function(index, c) {
                        rainfallData.push(c);
                    });
                    maxTemperature(rainfalldataFromServer.MaxTemperature);
                    minTemperature(rainfalldataFromServer.MinTemperature);
                    avgTemperature(rainfalldataFromServer.AvgTemperature.toFixed(2));
                    avgPrecipitacion(rainfalldataFromServer.AvgPrecipitation.toFixed(2));
                });
            gridViewModel.currentPageIndex(0);
        });
        
        selectedPeriod.subscribe(function (val) {
            periodValue(val.PeriodId);
            dc.RainfallData.GetRainfallData(locationValue, periodValue)
                .done(function (rainfalldataFromServer) {
                    rainfallData.removeAll();
                    $.each(rainfalldataFromServer.AlmanacDays, function (index, c) {
                        rainfallData.push(c);
                    });
                    maxTemperature(rainfalldataFromServer.MaxTemperature);
                    minTemperature(rainfalldataFromServer.MinTemperature);
                    avgTemperature(rainfalldataFromServer.AvgTemperature.toFixed(2));
                    avgPrecipitacion(rainfalldataFromServer.AvgPrecipitation.toFixed(2));
                });
            gridViewModel.currentPageIndex(0);
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
            PeriodData: periodData,
            SelectedLocation: selectedLocation,
            SelectedPeriod: selectedPeriod,
            MaxTemperature: maxTemperature,
            MinTemperature: minTemperature,
            AvgTemperature :avgTemperature,
            AvgPrecipitation : avgPrecipitacion
        };
    }();
    return viewModel;
});