//The MIT License (MIT)
//Copyright (c) 2016 http://www.tyly.co.nz/
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
//to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons 
//to whom the Software is furnished to do so, subject to the following conditions:
//The above copyright notice and this permission notice shall be included in all copies 
//or substantial portions of the Software.
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
//EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

cobraApp.controller('RegisterCtrl', function ($scope, $timeout, $http) {
    $scope.user = {
        username: '',
        password: '',
        confirmPassword: ''
    }

    $scope.submitRegisterForm = function () {
        var registerModel = {
            email: $scope.user.username,
            password: $scope.user.password,
            confirmPassword: $scope.user.confirmPassword
        };

        $http({
            method: 'POST',
            url: '/Account/Register',
            data: registerModel,
        }).then(function (response) {
            var result = response.data;
            if (result.Success === true) {
                window.location.href = result.RedirectUrl;
            } else { // login is fail

            }
        }, function (response) { // when server is offline or not response
            alert("Error. \nPlease contact customer service for further assistnces")
        });
    };

    $scope.passwordValidator = function (password) {

        if (!password) { return; }

        if (password.length < 6) {
            return "Password must be at least " + 6 + " characters long";
        }

        if (!password.match(/[A-Z]/)) {
            return "Password must have at least one capital letter";
        }

        if (!password.match(/[0-9]/)) {
            return "Password must have at least one number";
        }

        return true;
    };
});

cobraApp.run(['$http', function ($http) {
    $http.defaults.headers.common['X-XSRF-Token'] =
        angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value');
}]);

