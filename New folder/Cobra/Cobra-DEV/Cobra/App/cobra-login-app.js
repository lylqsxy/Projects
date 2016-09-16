
'use strict';
var cobraApp = angular.module('cobra-app', ['ngRoute', 'angularValidator', 'ui.bootstrap', 'ui.bootstrap.datetimepicker'])

 cobraApp.config(['$routeProvider', function($routeProvider) {
            $routeProvider.
            
            when('/editDetails', {
                templateUrl: 'editDetailsTemplate',
               controller: 'manage/ProfileCtrl'
            }).
            
            when('/editAddress', {
                templateUrl: 'editAddressTemplate',
               controller: 'manage/ProfileCtrl'
            }).
            
            otherwise({
                redirectTo: ''
            });
 }]);


