var app = angular.module('myApp', ['ngAnimate', 'ui.bootstrap', 'ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider
    .when("/", {
        templateUrl: "/"
    })
    .when("/create", {
        templateUrl: "/Order/ModalContent",
        controller: "ModalContentCtrl"
    })
    .when("/edit", {
        templateUrl: "/Order/ModalContent",
        controller: "ModalContentCtrl"
    })
    .when("/detail", {
        templateUrl: "/Order/ModalContent",
        controller: "ModalContentCtrl"
    });
});

app.controller('appCtrl',
       function ($scope, $rootScope, $http, $uibModal, $log) {
           $scope.listFn = function () {
               $http.get("/Order/List/").then(function (response) {
                   $scope.list = response.data;
               });
           };
           $scope.listFn();
           $http.get("/Order/PersonInfo/").then(function (response) {
               $scope.peopleList = response.data;
           });
           $scope.orderToEdit =
            {
                "OrderId": 0,
                "OrderDate": undefined,
                "PersonId": null
            };

           $scope.createOrder = function () {
               $('#templateModal').modal('show');
               $scope.modalTpl = {
                   modalId: "createEdit",
                   modalTitle: "Create",
                   buttonName: "Create"
               };

               document.location = '#create';
           };

           $scope.editOrder = function (o) {
               $('#templateModal').modal('show');
               $scope.modalTpl = {
                   modalId: "createEdit",
                   modalTitle: "Edit  ID: " + o.OrderId,
                   buttonName: "Edit"
               };
               $scope.orderToEdit = o;
               document.location = '#edit';
           };

           $scope.detailOrder = function (o) {
               $('#templateModal').modal('show');
               $scope.modalTpl = {
                   modalId: "detail",
                   modalTitle: "Detail  ID: " + o.OrderId,
                   buttonName: "Detail"
               };
               $scope.orderToEdit = o;
               document.location = '#edit';
           };

           $scope.deletePost = function (o) {
               var orderDel = { "Id": o.OrderId };
               $scope.serverVal = false;
               $http.post("/Order/Delete/", orderDel).success(function (response) {
                   $scope.listFn();
               });
           };
       
       });

app.controller('ModalContentCtrl',
    function ($scope, $http) {
        $scope.OrderId = $scope.orderToEdit.OrderId;
        $scope.InputOrderDate = new Date($scope.orderToEdit.OrderDate);
        $scope.SeletedPersonId = $scope.orderToEdit.PersonId;
        $scope.dataPost = function () {
            $scope.createEditForm.personId.$touched = true;
            $scope.createEditForm.orderDate.$touched = true;
            if (!$scope.createEditForm.personId.$invalid && !$scope.createEditForm.orderDate.$invalid) {
                var orderNew = { "OrderId": $scope.OrderId, "OrderDate": $scope.InputOrderDate, "PersonId": $scope.SeletedPersonId };
                $http.post("/Order/DataPost/", orderNew).success(function (response) {
                    if (response === "True") {
                        $scope.serverValErr = false;
                        $scope.listFn();
                        $scope.close();
                    }
                    else {
                        $scope.serverValErr = true;
                    }
                });
            }
        };
        $scope.productListFn = function () {
            $http.get("/Order/Details/" + $scope.OrderId).then(function (response) {
                $scope.productDetails = response.data;
            });
        };
        $scope.productListFn();

        $scope.addProduct = function () {

            $http.get("/Order/ProductInfo/").then(function (response) {
                $scope.productList = response.data;
            });
        };

        $scope.addProductFromList = function (selectedItem) {
            var productNew = { "OrderId": $scope.OrderId, "ProductId": selectedItem };
            $http.post("/Order/AddProductModal/", productNew).success(function (response) {
                if (response === "True") {
                    $scope.serverValErr = false;
                    $scope.productListFn();
                }
                else {
                    $scope.serverValErr = true;
                }
            });
        };

        $scope.removeProduct = function (orderDetailId) {
            var productDel = { "id": orderDetailId };
            $scope.serverVal = false;
            $http.post("/Order/DelOrderDetails/", productDel).success(function (response) {
                $scope.productListFn();
            });
        };

        $scope.deletePostInDetail = function () {
            $scope.deletePost($scope.orderToEdit);
            $scope.close();
        };

        $scope.close = function () {
            $('#templateModal').modal('hide');
        };

    });
