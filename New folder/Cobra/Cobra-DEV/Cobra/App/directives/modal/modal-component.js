angular.
  module('cobraModal').
  run(function(editableOptions) {
      editableOptions.theme = 'bs3'; // xeditable
  }).
  component('cModal', {
      templateUrl: '/App/directives/modal/modal.html',
      controller: function CModalController($scope, $rootScope,$timeout, utils) {
          var ws = this;
          $scope.formShown=false;
          $scope.modalOption = {};
          $scope.modalData = [];

          // data is a object array
          $scope.$on('showModelEvent', function (event, data) {

              $scope.modalData = data[0];
              $scope.modalOption = data[1];
              if ($scope.modalOption.action !== 'index') {
                  $timeout(function () {
                      angular.element('#show-form-button').triggerHandler('click');
                      $scope.formShown = true;
                  });
              } else if ($scope.formShown == true) {
                  $timeout(function () {
                      angular.element('#cancel-form-button').triggerHandler('click');
                      $scope.formShown = false;
                  });
              }
          });

          $scope.modalEdit = function () {
              $scope.formShown = true;
          };

          $scope.saveModal = function (data) {
              var uri = $scope.modalOption.controller + '/' + $scope.modalOption.action;
              if ($scope.modalOption.idValue) {
                  uri += '/' + $scope.modalOption.idValue;
                  data[$scope.modalOption.idVariable] = $scope.modalOption.idValue;
              }

              utils.postApiData(uri, data).then(function (respone) {
                  $scope.$emit('modelDone', respone);
              });
          }
         
      },
  });