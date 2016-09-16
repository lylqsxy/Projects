
(function () {
    // Controller: ForgetPassword
    cobraApp.controller('account/ForgetPasswordCtrl', ['$scope', '$http', '$window', function ($scope, $http, $window) {
        $scope.hereLink = $window.location.origin + "/Home/Index";
        // Initializing scope
        $scope.data = {
            // object matches with the back-end ViewModel
            userData: { email: '' },
            msg: '',
            hideForm: false,
            showConfirmationMsg: false,
            showServerErrorMsg: false
        };

        $scope.serverErrors = [];
        // POST Email
        $scope.postData = function (valid) {
            if (!valid) { // Tom add function for valid onSubmit button
                console.log('Invalid');
            } else {

                var req = {
                    method: 'POST',
                    url: $window.location.origin + '/Account/ForgetPassword',
                    data: $scope.data.userData
                };
                $http(req).then(
                    function (response) {
                        $scope.data.msg = response.data.MsgText;
                        $scope.data.hideForm = response.data.Success ==true ;
                        $scope.data.userData.email = response.data.Email;
                        if (response.data.Success) {
                            //$scope.data.hideForm = true;
                            $scope.data.showServerErrorMsg = false;
                            $scope.data.showConfirmationMsg = true;
                        }
                        else {
                            $scope.serverErrors = response.data.Errors;
                            $scope.data.showServerErrorMsg = true;
                            $scope.data.showConfirmationMsg = false;

                        }
                    });
            }
        };

    }]);


    cobraApp.run(['$http', function ($http) {
        $http.defaults.headers.common['X-XSRF-Token'] =
            angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value');
    }]);
})();