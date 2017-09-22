/* 
   Name        : contactListCtrl.js
   Version     : 1.0.0
   Author      : Nicky
   Date        : 22-09-2017 
   Description : This Angular JS controller file hold functions for Manage index.html file
*/

contactListApp.controller('contactListCtrl', function ($scope, $http, $filter) {

    var getContactList = function () {
        $http({
            method: "Get",
            url: "http://jsonplaceholder.typicode.com/users"
        }).then(function SentOk(result) {
            $scope.contactList = result.data;
        }, function Error(result) {
            console.log(result);
        });
    };

    getContactList();

    $scope.showDetails = function (a) {
        console.log(a)
    }
})