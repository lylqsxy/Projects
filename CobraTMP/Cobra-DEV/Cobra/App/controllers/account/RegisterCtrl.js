

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

