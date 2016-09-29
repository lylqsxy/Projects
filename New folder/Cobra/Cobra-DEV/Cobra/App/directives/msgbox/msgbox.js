//MsgBox(OK/Cancel)

cobraApp.directive('cMsgbox', function () {
    return {
        restrict: 'E',
        templateUrl: '/App/directives/msgbox/msgbox.html',
        controller: ['$scope', function ($scope) {
            $scope.$on('showMsgBox', function (event, data) {
                $scope.msgboxData = data;
                $scope.okFun = function () {
                    $scope.$emit('msgboxDone', [data, true]);
                };
                $scope.cancelFun = function () {
                    $scope.$emit('msgboxDone', [data, false]);
                };
            });
        }]

    };
});