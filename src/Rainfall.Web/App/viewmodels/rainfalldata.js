define(["dataContext"], function (dc) {
    
    var viewModel = function () {
        this.rainfallData = ko.observableArray([]);
        this.locationData = ko.observableArray([]);
        this.selectValue = ko.observable();
       
        
        this.selectValue.subscribe(function(val) {
            dc.RainfallData.GetRainfallDataByLocation(val.CityId).done(function (locationdataFromServer) {
                $.each(locationdataFromServer, function (index, c) {
                    console.log(c);
                });
            });
        });
        
        dc.RainfallData.Get().done(function (rainfalldataFromServer) {
            $.each(rainfalldataFromServer, function (index, c) {
                rainfallData.push(c);
            });
        });
        
        dc.RainfallData.GetLocations().done(function (locationdataFromServer) {
            $.each(locationdataFromServer, function (index, c) {
                locationData.push(c);
            });
        });
        
        this.gridOptions = {
            data: rainfallData,
            columnDefs: [{ field: 'Date', width: 90 },
                            { field: 'City', width: 130 },
                            { field: 'Precipitation', width: 110 },
                            { field: 'TempHigh' , displayName:'Higher Temperature', width:150 },
                            { field: 'TempLow', displayName: "Lower Temperature", width: 150 }
                        ]
        };

        return {
            RainfallData: rainfallData,
            RainfallDatagridOption: gridOptions,
            LocationData: locationData,
            SelectedValue: selectValue
        };
    }();
    return viewModel;
});
