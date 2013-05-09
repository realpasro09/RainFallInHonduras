define(["dataContext"], function (dc) {
    
    var viewModel = function () {

        var rainfallData = ko.observableArray([]);
        
        var filterOptions = {
            filterText: ko.observable(""),
            useExternalFilter: true
        };
        
        var pagingOptions = {
            pageSizes: ko.observableArray([50, 100, 150]),
            pageSize: ko.observable(50),
            totalServerItems: ko.observable(20),
            currentPage: ko.observable(1)
        };
                
        var setPagingData = function (data, page, pageSize) {
            var pagedData = data.slice((page - 1) * pageSize, page * pageSize);
            rainfallData(pagedData);
            pagingOptions.totalServerItems(data.length);
        };

        var getPagedDataAsync = function (pageSize, page, searchText) {
            setTimeout(function () {
                var data;
                if (searchText) {
                    var ft = searchText.toLowerCase();
                    data = rainfallData.filter(function(item) {
                        return JSON.stringify(item).toLowerCase().indexOf(ft) != -1;
                    });
                    setPagingData(data, page, pageSize);
                } else {
                        setPagingData(rainfallData, page, pageSize);
                }
            }, 100);
        };

        filterOptions.filterText.subscribe(function (data) {
            getPagedDataAsync(pagingOptions.pageSize(), pagingOptions.currentPage(), filterOptions.filterText());
        });

        pagingOptions.pageSizes.subscribe(function (data) {
            getPagedDataAsync(pagingOptions.pageSize(), pagingOptions.currentPage(), filterOptions.filterText());
        });
        pagingOptions.pageSize.subscribe(function (data) {
            getPagedDataAsync(pagingOptions.pageSize(), pagingOptions.currentPage(), filterOptions.filterText());
        });
        pagingOptions.totalServerItems.subscribe(function (data) {
            getPagedDataAsync(pagingOptions.pageSize(), pagingOptions.currentPage(), filterOptions.filterText());
        });
        pagingOptions.currentPage.subscribe(function (data) {
            getPagedDataAsync(pagingOptions.pageSize(), pagingOptions.currentPage(), filterOptions.filterText());
        });

        getPagedDataAsync(pagingOptions.pageSize(), pagingOptions.currentPage());


        var gridOptions = {
            data: rainfallData,
            //enablePaging: true,
            //pagingOptions: pagingOptions,
            filterOptions: filterOptions,
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
