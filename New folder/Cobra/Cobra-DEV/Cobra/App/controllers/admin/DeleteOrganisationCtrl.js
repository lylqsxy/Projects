//Author: Aaron Bhardwaj
'use strict';
(function () {
    // Controller: DeleteOrganisationCtrl
    app.controller('DeleteOrganisationCtrl', ['$scope', '$http', '$window', function ($scope, $http, $window) {
        $scope.deleteOrganisation = function (id) {
            var req = {
                method: 'POST',
                url: '/Admin/DeleteOrganisation',
                data: { 'id': id },
                headers: {
                    'X-XSRF-Token': angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value')
                }
            }
            var isConfirmed = confirm('Are you sure you want to remove this organisation?');
            if (isConfirmed) {
                $http(req).then(
                function (response) {
                    $scope.data.msg = response.data.MsgText;
                    if (response.data.Success) {
                        $window.location.href = response.data.RedirectUrl;
                    }
                }
            );
            }
        };
    }]);
})();