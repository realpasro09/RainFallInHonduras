define(["dataContext"], function (dc) {
    
    var viewModel = function () {
        var self = this;
        this.rainfallData = ko.observableArray([]);
        
        
        
        this.gridOptions = {
            data: rainfallData,
            //enablePaging: true,
            //pagingOptions: self.pagingOptions,
            //filterOptions: self.filterOptions
        };
        return {
            RainfallData: rainfallData,
            RainfallDatagridOption: gridOptions,
            activate: function() {
                return dc.RainfallData.Get().done(function (rainfalldataFromServer) {
                    $.each(rainfalldataFromServer, function (index, c) {
                        rainfallData.push(c);
                    });
                });
            }
        };
    }();
    return viewModel;
});