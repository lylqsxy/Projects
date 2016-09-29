(function () {
    "use strict";

    function utils($rootScope, $http) {

        var utilsFactory = {};

        utilsFactory.getApiData = function (param) {
            return $http.get(param)
           .then(function success(response) {
               return response.data;

           }, function error(response) {

               return response;

           });
        };

        utilsFactory.postApiData = function (uri, data, config) {

            return $http.post(uri, data, config).then(
                function success(response) {
                    return [response, true];
                },
                function error(response) {
                    return [response, false];
                });
        };

        utilsFactory.findObjectIndexInArray = function (array, attr, value) {
            for (var i = 0; i < array.length; i++) {
                if (array[i][attr] == value) {
                    return i;
                }
            }
            return -1;
        };

        //Transfer .net Date format to JS format
        utilsFactory.toJsDate = function (item, attr) {
            item[attr] = new Date(item[attr]);
        };

        //transfer the data of an object array to JS format
        utilsFactory.setObjectDateToJS = function (array, attr) {
            array.forEach(function (item) {
                utilsFactory.toJsDate(item, attr);
            });
        };

        //Interpret index to attribute
        utilsFactory.indexToAttribute = function (array, attr, attrArr) {
            array.forEach(function (item) {
                item[attr] = attrArr[item[attr]];
            });
        };

        return utilsFactory;

    }


    angular.module('cobra-app').factory("utils", utils);
})();