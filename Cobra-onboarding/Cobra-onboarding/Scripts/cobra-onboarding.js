var app = angular.module('myApp', ['ngRoute']);

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
       function ($scope, $http) {
           $scope.customerListFn = function () {
               $http.get("/Customer/List/").then(function (response) {
                   $scope.customerList = response.data;
               });
           };
           $scope.customerListFn();

           $scope.orderListFn = function () {
               $http.get("/Order/List/").then(function (response) {
                   $scope.orderList = response.data;
               });
           };
           $scope.orderListFn();
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
                   $scope.orderListFn();
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
                        $scope.orderListFn();
                        $scope.close();
                    }
                    else {
                        $scope.serverValErr = true;
                    }
                });
            }
        };
        $scope.orderDetailFn = function () {
            $http.get("/Order/Details/" + $scope.OrderId).then(function (response) {
                $scope.orderDetails = response.data;
            });
        };
        
        $scope.productListFn = function () {
            $http.get("/Order/ProductInfo/").then(function (response) {
                $scope.productList = response.data;
            });
        };

        $scope.orderDetailFn();
        $scope.productListFn();
        $scope.addButtonName = "Add Product >>";

        $scope.addProduct = function () {
            if ($scope.modalTpl.modalIdDetailAdd === "detailAdd")
            {
                $scope.modalTpl.modalIdDetailAdd = null;
                $scope.addButtonName = "Add Product >>";
            }
            else
            {
                $scope.modalTpl.modalIdDetailAdd = "detailAdd";
                $scope.SeletedProductId = null;
                $scope.addButtonName = "<< Close";
            }     
        };

        $scope.closeAddForm = function () {
            if ($scope.modalTpl.modalIdDetailAdd === "detailAdd") {
                $scope.modalTpl.modalIdDetailAdd = null;
                $scope.addButtonName = "Add Product >>";
            }
        };

        $scope.addProductFromList = function (selectedItem) {
            $scope.addForm.selectedProductId.$touched = true;
            if (!$scope.addForm.selectedProductId.$invalid) {
                var productNew = { "OrderId": $scope.OrderId, "ProductId": selectedItem };
                $http.post("/Order/AddProductModal/", productNew).success(function (response) {
                    if (response === "True") {
                        $scope.serverValErr = false;
                        $scope.orderDetailFn();
                    }
                    else {
                        $scope.serverValErr = true;
                    }
                });
            }
        };

        $scope.removeProduct = function (orderDetailId) {
            var productDel = { "id": orderDetailId };
            $scope.serverVal = false;
            $http.post("/Order/DelOrderDetails/", productDel).success(function (response) {
                $scope.orderDetailFn();
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
