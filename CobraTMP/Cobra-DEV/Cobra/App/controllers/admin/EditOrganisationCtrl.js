//Author: Aaron Bhardwaj
'use strict';
(function () {
    // Controller: EditOrganisationCtrl
    app.controller('EditOrganisationCtrl', ['$scope', '$http', '$window', '$mdDialog', function ($scope, $http, $window, $mdDialog) {
        // Edit Organisation
        $scope.editOrganisation = function ($event, index) {
            var parentElm = angular.element(document.body);
            $mdDialog.show({
                parent: parentElm,
                targetEvent: $event,
                template: '<md-dialog aria-label="list dialog">' +
                             '	<md-dialog-content>' +
                             '		<div layout="column">' +
                             '			<md-input-container>' +
                             '				<label>Organisation Name</label>' +
                             '				<input type="text" ng-model="OrgName">' +
                             '			</md-input-container>' +
                             '			<md-input-container>' +
                             '				<label>Website URL</label>' +
                             '				<input type="text" ng-model="WebsiteUrl">' +
                             '			</md-input-container>' +
                             '		</div>' +
                             '	</md-dialog-content>' +
                             '   <md-dialog-actions>' +
                             '		<md-button ng-click="closeDialog()" class="md-primary">' +
                             '			Update' +
                             '		</md-button>' +
                             '	</md-dialog-actions>' +
                             '</md-dialog>',
                scope: $scope,
                preserveScope: true,
                controller: DialogController
            });
            // Show current values in Dialog box
            function DialogController($scope, $mdDialog) {
                $scope.OrgName = $scope.data[index].OrgName;
                $scope.WebsiteUrl = $scope.data[index].WebsiteUrl;
                $scope.closeDialog = function () {
                    //assigned values to parent scope 
                    $scope.data[index].OrgName = $scope.OrgName;
                    $scope.data[index].WebsiteUrl = $scope.WebsiteUrl;
                    var req = {
                        method: 'POST',
                        url: '/Admin/EditOrganisation',
                        data: $scope.data[index],
                        headers: {
                            'X-XSRF-Token': angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value')
                        }
                    }
                    $http(req).then(
                        function (response) {
                            if (response.data.Success) {
                                $window.location.href = response.data.RedirectUrl;
                            }
                        }
                    );
                    $mdDialog.hide();
                }
            }
        };
    }]);
})();