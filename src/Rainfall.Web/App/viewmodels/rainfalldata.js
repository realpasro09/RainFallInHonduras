define(["dataContext"], function (dc) {
    
    var viewModel = function () {
        var self = this;
        this.rainfallData = ko.observableArray([]);
        
        this.filterOptions = {
            filterText: ko.observable(""),
            useExternalFilter: true
        };
        this.pagingOptions = {
            pageSizes: ko.observableArray([50, 100, 150]),
            pageSize: ko.observable(50),
            totalServerItems: ko.observable(20),
            currentPage: ko.observable(1)
        };
        
        
        this.setPagingData = function (data, page, pageSize) {
            var pagedData = data.slice((page - 1) * pageSize, page * pageSize);
            self.rainfallData(pagedData);
            self.pagingOptions.totalServerItems(data.length);
        };

        this.getPagedDataAsync = function (pageSize, page, searchText) {
            setTimeout(function () {
                var data;
                if (searchText) {
                    var ft = searchText.toLowerCase();
                    data = rainfallData.filter(function(item) {
                        return JSON.stringify(item).toLowerCase().indexOf(ft) != -1;
                    });
                    self.setPagingData(data, page, pageSize);
                } else {
                        self.setPagingData(rainfallData, page, pageSize);
                }
            }, 100);
        };

        self.filterOptions.filterText.subscribe(function (data) {
            self.getPagedDataAsync(self.pagingOptions.pageSize(), self.pagingOptions.currentPage(), self.filterOptions.filterText());
        });

        self.pagingOptions.pageSizes.subscribe(function (data) {
            self.getPagedDataAsync(self.pagingOptions.pageSize(), self.pagingOptions.currentPage(), self.filterOptions.filterText());
        });
        self.pagingOptions.pageSize.subscribe(function (data) {
            self.getPagedDataAsync(self.pagingOptions.pageSize(), self.pagingOptions.currentPage(), self.filterOptions.filterText());
        });
        self.pagingOptions.totalServerItems.subscribe(function (data) {
            self.getPagedDataAsync(self.pagingOptions.pageSize(), self.pagingOptions.currentPage(), self.filterOptions.filterText());
        });
        self.pagingOptions.currentPage.subscribe(function (data) {
            self.getPagedDataAsync(self.pagingOptions.pageSize(), self.pagingOptions.currentPage(), self.filterOptions.filterText());
        });

        self.getPagedDataAsync(self.pagingOptions.pageSize(), self.pagingOptions.currentPage());


        this.gridOptions = {
            data: rainfallData,
            //enablePaging: true,
            //pagingOptions: self.pagingOptions,
            filterOptions: self.filterOptions,
            jqueryUITheme: true,
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
