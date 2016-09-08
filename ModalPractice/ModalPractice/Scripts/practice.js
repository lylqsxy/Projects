var app = angular.module('myApp', ['ngRoute', 'angularValidator']);

app.controller('appCtrl',
    function ($scope, $http) {
        $scope.login = {};
        $scope.login.RememberMe = false;

        $scope.showModal = function (response) {
            $('#commonModal').modal('show');
            if (response.IfSuccess === false)
            {
                $scope.modalTpl = {
                    modalTitle: "Login Failed",
                    modalPath: 'modalTmp',
                    modalResponse: response
                };
            }
            else
            {
                $scope.modalTpl = {
                    modalTitle: "Login Succeeded",
                    modalPath: 'modalTmp',
                    modalResponse: response
                };
            }
            
        };

        $scope.loginFn = function (form) {

            $http.post("/Account/Login/", $scope.login).success(function (response) {
                console.log(response);
                $scope.showModal(response);
            });
        };

        $scope.close = function () {
            $('#commonModal').modal('hide');
        };
    });