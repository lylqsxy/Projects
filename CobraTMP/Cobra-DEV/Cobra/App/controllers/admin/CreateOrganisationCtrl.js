//Author: Aaron Bhardwaj
'use strict';
(function () {
    // Controller: CreateOrganisationCtrl
    app.controller('CreateOrganisationCtrl', ['$scope', '$http', '$window', function ($scope, $http, $window) {
        // Initialize scope
        $scope.data = {
            orgData: {
                orgName: '',
                websiteUrl: ''
            },
            msg: ''
        };
        // Create Organisation
        $scope.createOrganisation = function () {
            var req = {
                method: 'POST',
                url: '/Admin/CreateOrganisation',
                data: $scope.data.orgData,
                headers: {
                    'X-XSRF-Token': angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value')
                }
            }
            $http(req).then(
                function (response) {
                    $scope.data.msg = response.data.MsgText;
                    if (response.data.Success) {
                        $window.location.href = response.data.RedirectUrl;
                    }
                }
            );
        };
    }]);
})();