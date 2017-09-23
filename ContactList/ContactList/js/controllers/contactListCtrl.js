/* 
   Name        : contactListCtrl.js
   Version     : 1.0.0
   Author      : Nicky
   Date        : 22-09-2017 
   Description : This Angular JS controller file hold functions for Manage index.html file
*/

contactListApp.controller('contactListCtrl', function ($scope, $http, $filter) {

    $scope.showTable = false;
    $scope.showLoading = true;
    $scope.showList = false;
    $scope.propertyName = 'id';
    $scope.reverse = false;
    $scope.arrow = $scope.reverse ? '▼' : '▲';

    var getContactList = function () {
        $http({
            method: "Get",
            url: "http://jsonplaceholder.typicode.com/users"
        }).then(function SentOk(result) {
            $scope.contactList = result.data;
            $scope.showTable = true;
            $scope.showLoading = false;
        }, function Error(result) {
            console.log(result);
        });
    };

    getContactList();

    $scope.showDetails = function (data) {
        $scope.$broadcast('showMsgBox', data);
        angular.element('#msgbox').modal('show');
    }

    $scope.sortBy = function (propertyName) {
        $scope.reverse = ($scope.propertyName === propertyName) ? !$scope.reverse : false;
        $scope.propertyName = propertyName;
        $scope.arrow = $scope.reverse ? '▼' : '▲';
    }
})