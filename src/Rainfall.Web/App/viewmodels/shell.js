﻿define(['durandal/plugins/router', 'durandal/app'], function (router, app) {


    return {
        router: router,
        search: function() {
           app.showMessage('Search not yet implemented...');
        },
        activate: function () {
            return router.activate('rainfalldata');
        }
    };
});

