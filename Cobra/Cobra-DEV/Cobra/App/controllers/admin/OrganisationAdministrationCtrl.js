//Author: Aaron Bhardwaj
'use strict';
(function () {
    // Controller: OrgAdminCtrl
    app.controller('OrganisationAdministrationCtrl', ['$scope', '$http', '$window', function ($scope, $http, $window) {
        // Initializing scope
        $scope.data = [];
        $http.get('/Admin/OrganisationAdministrationData').then(
                function (response) {
                    $scope.data = response.data;
                }
            );
        $scope.createOrganisation = function () {
            $window.location.href = 'CreateOrganisation';
        }
    }]);
})();