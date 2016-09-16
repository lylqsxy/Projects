
cobraApp.service('CommonService', function () {
    this.showSucessDialog = function (event, $scope, response, $mdDialog) {
        var confirm = $mdDialog.confirm({
            controller: function ($scope, $mdDialog) {
                $scope.closeDialog = function () {
                    $mdDialog.hide();
                };

                $scope.goToLogin = function () {
                    window.location.href = "/Account/Login";
                };
            },
            template:
                '<md-dialog aria-label="Lucky day">' +
                '  <md-toolbar> ' +
                '     <div class="md-toolbar-tools" layout="row" layout-align="center center">' +
                '         <h2>Congratulation!</h2></div> ' +
                '  </md-toolbar>' +
                '  <md-dialog-content class="md-padding">' +
                '    Please check your email to activate your account' +
                '  </md-dialog-content>' +
                '  <md-dialog-actions>' +
                '    <md-button class="md-primary md-raised" ng-click="closeDialog()">Close</md-button>' +
                //'    <md-button ng-click="goToLogin()" class="md-primary md-raised">Login</md-button>' +
                '  </md-dialog-actions>' +
                '</md-dialog>',
            parent: angular.element(document.body),
            targetEvent: event,
        });
        $mdDialog.show(confirm).then(function () {
            window.location.href = response.data.RedirectUrl;
            $scope.status = 'You decided to keep your debt.';
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    }

    this.showErrorDialog = function (event, $scope, response, $mdDialog) {
        var confirm = $mdDialog.confirm({
            controller: function ($scope, $mdDialog) {
                $scope.closeDialog = function () {
                    $mdDialog.hide();
                };
            },
            template:
                '<md-dialog aria-label="Lucky day">' +
                '  <md-toolbar class="md-warn"> ' +
                '     <div layout="row" layout-align="center center">' +
                '         <h2>Error!</h2></div> ' +
                '  </md-toolbar>' +
                '  <md-dialog-content class="md-padding">' +
                    response.data.Message +
                '  </md-dialog-content>' +
                '  <md-dialog-actions>' +
                '    <md-button ng-click="closeDialog()">Close</md-button>' +
                '  </md-dialog-actions>' +
                '</md-dialog>',
            parent: angular.element(document.body),
            targetEvent: event,
        });

        $mdDialog.show(confirm).then(function () {
            $scope.loading = false;
            if (response.data.RedirectUrl.length > 0) {
                var urlDecoded = decodeURIComponent(response.data.RedirectUrl);
                window.location.pathname = urlDecoded;
            }
        }, function () { });
    }

    this.showDialog = function () {
        //..
    }

    this.getUrlParameter = function (param) {
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
});