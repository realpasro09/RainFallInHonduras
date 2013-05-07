define(["config"], function (jquery, configuration) {

    var server = function ($, config) {

        var get = function (resource, data) {
            return sendAjaxRequest("GET", resource, data);
        };

        var post = function (resource, data) {
            return sendAjaxRequest("POST", resource, data);
        };

        var put = function (resource, data) {
            return sendAjaxRequest("PUT", resource, data);
        };

        var del = function (resource, data) {
            return sendAjaxRequest("DELETE", resource, data);
        };

        var sendAjaxRequest = function (type, resource, data) {
            var operator = "?";
            if (resource.indexOf("?") !== -1) {
                operator = "&";
            }

            var fullUrl = config.ApiUrl + resource + operator;

            //append timestamp so browsers won't cache requests
            fullUrl = fullUrl + "&_ts=" + new Date().getTime();

            var promise = $.ajax({
                url: fullUrl,
                dataType: "json",
                type: type,
                data: data
            });

            //error handling
            promise.pipe(function (response, textStatus, jqXhr) {
                if (response && response.Status == "error") {
                    if (console) {
                        console.log(response.Message);
                    } else {
                        alert(response.Message);
                    }

                    var deferred = new $.Deferred();
                    return deferred.reject(response);
                }
                return jqXhr;
            });

            return promise;
        };

        return {
            Get: get,
            Put: put,
            Delete: del,
            Post: post,
        };
    }(jquery, configuration);

    return server;
});