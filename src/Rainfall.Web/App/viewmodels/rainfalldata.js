define(["dataContext"], function (dc) {


    /* The dataTable binding */
    (function ($) {
        ko.bindingHandlers.dataTable = {
            init: function (element, valueAccessor) {
                var binding = ko.utils.unwrapObservable(valueAccessor());

                // If the binding is an object with an options field,
                // initialise the dataTable with those options. 
                if (binding.options) {
                    $(element).dataTable(binding.options);
                }
            },
            update: function (element, valueAccessor) {
                var binding = ko.utils.unwrapObservable(valueAccessor());

                // If the binding isn't an object, turn it into one. 
                if (!binding.data) {
                    binding = { data: valueAccessor() };
                }

                // Clear table
                $(element).dataTable().fnClearTable();

                // Rebuild table from data source specified in binding
                $(element).dataTable().fnAddData(binding.data());
            }
        };
    })(jQuery);
    
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
            avgPrecipitacion = ko.observable(0),
            totalPrecipitation = ko.observable(0);
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

        var getRainfallDataSummary = function() {
            dc.RainfallData.GetRainfallSummary(locationValue,periodValue).done(function(dataFromServer) {
                maxTemperature(dataFromServer.MaxTemp);
                minTemperature(dataFromServer.MinTemp);
                avgTemperature(dataFromServer.AvgTemp);
                avgPrecipitacion(dataFromServer.AvgPrecipitation);
                totalPrecipitation(dataFromServer.TotalPrecipitation);
            });
        };
        getRainfallDataSummary();
        selectedLocation.subscribe(function (val) {
            locationValue(val.CityId);
            getRainfallDataSummary();
        });
        
        selectedPeriod.subscribe(function (val) {
            periodValue(val.PeriodId);
            getRainfallDataSummary();
        });
        
        var gridDataTable = {
            data: rainfallData,
            options:
            {
                bServerSide: true,  
                bProcessing: true,
                sPaginationType: "full_numbers",
                sAjaxSource: "/RainfallData/GetRainfallData",
                fnServerData: function(sSource, aoData, fnCallback, oSettings) {
                    oSettings.jqXHR = $.ajax({
                            "dataType": 'json',
                            "type": "POST",
                            "url": sSource,
                            "data": aoData,
                            "success": fnCallback
                        }
                    );
                },
                fnServerParams: function ( aoData )   
                {  
                    aoData.push({ name: "locationId", value: locationValue() });
                    aoData.push({ name: "periodId", value: periodValue() });
                },
                aoColumns:
                [
                    { sTitle: 'Date', mData: 'Date' },
                    { sTitle: 'City', mData: 'City' },
                    { sTitle: 'High Temp', mData: 'TempHigh' },
                    { sTitle: 'Low Temp', mData: 'TempLow' },
                    { sTitle: 'Precipitation', mData: 'Precipitation' }
                ]
            }
        };

        return {
            GridViewModel: gridDataTable,
            RainfallData: rainfallData,
            LocationData: locationData,
            PeriodData: periodData,
            SelectedLocation: selectedLocation,
            SelectedPeriod: selectedPeriod,
            MaxTemperature: maxTemperature,
            MinTemperature: minTemperature,
            AvgTemperature :avgTemperature,
            AvgPrecipitation: avgPrecipitacion,
            TotalPrecipitation: totalPrecipitation
        };
    }();
    return viewModel;
});