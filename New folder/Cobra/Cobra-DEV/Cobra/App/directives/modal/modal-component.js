angular.
  module('cobraModal').
  run(function(editableOptions) {
      editableOptions.theme = 'bs3'; // xeditable
  }).
  component('cModal', {
      templateUrl: '/App/directives/modal/modal.html',
      controller: function CModalController($scope, $rootScope,$timeout,$q, utils) {
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

          // Save modal data to remote database
          $scope.saveModal = function (data, isvalid) {
              var d = $q.defer();

              if (isvalid) {
                  d.resolve();
              } else {
                  d.resolve('Error')
                  return d.promise;
              }
              
              if (isvalid) {

                  // Preparing the data to be posted or sent back
                  if ($scope.modalOption.idValue) {
                      uri += '/' + $scope.modalOption.idValue;
                      data[$scope.modalOption.idVariable] = $scope.modalOption.idValue;
                  }

                  if ($scope.modalOption.controller && $scope.modalOption.action) {
                      var uri = '/' + $scope.modalOption.controller + '/' + $scope.modalOption.action;

                      utils.postApiData(uri, data, $scope.modalOption.httpPostConfig).then(function (respone) {
                          $scope.$emit('modelDone', respone); //tell parent component, save already done, please upadte the page
                      });
                  } else {
                      $scope.$emit('modelDone', data);
                  }
              }
          }

      },
  });