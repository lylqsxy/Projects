/* 
   Name        : msgbox.js
   Version     : 1.0.0
   Author      : Nicky
   Date        : 22-09-2017 
   Description : This Angular JS directive file hold functions for Manage msgbox.html file
                 The file is used for common MsgBox.
*/

contactListApp.directive('cMsgbox', function () {
    return {
        restrict: 'E',
        templateUrl: './views/msgbox.html',
        controller: ['$scope', function ($scope) {
            $scope.$on('showMsgBox', function (event, data) {
                $scope.msgboxData = data;
                $scope.okFun = function () {
                    $scope.$emit('msgboxDone', data);
                };
            });
        }]

    };
});