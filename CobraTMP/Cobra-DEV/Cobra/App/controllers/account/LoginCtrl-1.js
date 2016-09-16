// Login Controller 
// Created by Ty 
// Url: /Account/Login 
// Model: UserLoginViewModel
'use strict';

app.controller('account/LoginCtrl', function ($scope, $timeout, $http, $mdDialog, CommonService) {
    $scope.loading = false;
    $scope.user = {
        email: '',
        password: '',
        rememberMe: false
    };

    $scope.signup = function () {
        window.location.href = "/Account/Register";
    };

    $scope.reset = function () {
        window.location.href = "/Account/ForgetPassword";
    };

    $scope.login = function (user) {
        var model = {
            Email: user.username,
            Password: user.password,
            returnUrl: CommonService.getUrlParameter("ReturnUrl"),
            rememberMe: user.rememberMe
        };

        $scope.loading = true;

        $http({
            method: 'POST',
            url: '/Account/Login',
            data: model,
        }).then(function (response) {
            var result = response.data;

            if (result.Success == true) {

                window.location.href = result.RedirectUrl;
            } else { // login is fail 
                CommonService.showErrorDialog(event, $scope, response, $mdDialog);
                //window.location.pathname = "";

            }
        }, function (response) { // when server is offline or not response
            CommonService.showErrorDialog(event, $scope, response, $mdDialog);
        });

    };
});
