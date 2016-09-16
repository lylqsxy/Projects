
// Controller: ResetPassword
app.controller('account/ResetPasswordCtrl', function ($scope, $http, $window, $location) {
    $scope.hereLink = $window.location.origin + "/Home/Index";
    // Initialize scope
    $scope.data = {
        // object matches with the back-end ViewModel
        userData: {
            email: '',
            password: '',
            confirmPassword: '',
            token: getUrlParameter("token")
        },
        msg: '',
        hideForm: false,
        showConfirmationMsg: false,
        showServerErrorMsg: false
    };
    $scope.serverErrors = [];
    // Reset Password
    $scope.resetPassword = function () {
        var req = {
            method: 'POST',
            url: $window.location.origin + '/Account/ResetPassword',
            data: $scope.data.userData
        }
        $http(req).then(
            function (response) {
                $scope.data.msg = response.data.MsgText;
                $scope.data.hideForm = (response.data.Success == true);
                $scope.data.userData.email = response.data.Email;
                if (response.data.Success) {
                    $scope.data.showServerErrorMsg = false;
                    $scope.data.showConfirmationMsg = true;
                }
                else {
                    $scope.serverErrors = response.data.Errors;
                    $scope.data.showServerErrorMsg = true;
                }
            }
        );
    };

});

function getUrlParameter(param) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split(/[&||?]/),
        res;

    for (var i = 0; i < sURLVariables.length; i += 1) {
        var paramName = sURLVariables[i],
            sParameterName = (paramName || '').split('=');

        if (sParameterName[0] === param) {
            res = sParameterName[1];
        }
    }
    return res;
}