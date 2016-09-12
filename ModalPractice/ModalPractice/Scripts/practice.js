var app = angular.module('myApp', ['ngRoute', 'angularValidator']);

app.controller('appCtrl',
    function ($scope, $http) {

        ////////////////////////
        $scope.showModal = function (response) {
            $('#commonModal').modal('show');
            if (response.IfSuccess === false)
            {
                $scope.modalTpl = {
                    modalTitle: "Login Failed",
                    modalPath: 'modalTmp',
                    modalResponse: response
                };
            }
            else
            {
                $scope.modalTpl = {
                    modalTitle: "Login Succeeded",
                    modalPath: 'modalTmp',
                    modalResponse: response
                };
            }
            
        };

        $scope.loginFn = function (login) {

            $http.post("/Account/Login/", $scope.login).success(function (response) {
                console.log(response);
                $scope.showModal(response);
            });
        };

        $scope.close = function () {
            $('#commonModal').modal('hide');
        };


        //////////////////////////
        
        $scope.data = [];
        
        $scope.data[0] = {
            name: "Nicky",
            address: "4 Rd"
        }

        $scope.data[1] = {
            name: "May",
            address: "5 Rd"
        }

        $scope.data[2] = {
            name: "Eric",
            address: "6 Rd"
        }

        $scope.data[3] = {
            name: "Craig",
            address: "7 Rd"
        }

        $scope.edit = function(i)
        {
            $('#commonModal').modal('show');
            $scope.modalTpl = {
                modalTitle: "Edit",
                modalPath: 'modalEdit',
            };
           // $scope.modal = $scope.data[i];
            $scope.modal = JSON.parse(JSON.stringify($scope.data[i]));
            $scope.index = i;
        }

        $scope.save = function (modal, i) {
            $scope.data[i] = modal;
            $scope.close();
        }

    });