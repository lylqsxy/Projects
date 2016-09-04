var app = angular.module('myApp', ['ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider
    .when("/", {
        templateUrl: "/"
    });
});

app.controller('appCtrl',
    function ($scope, $http) {
        $scope.customerListFn = function () {
            $http.get("/App/List/").then(function (response) {
                $scope.customerList = response.data;
            });
        };
        $scope.customerListFn();

        $scope.initCustomerToEdit = function () {
            $scope.c =
            {
                "Id": 0,
                "Name": null,
                "Address1": null,
                "Address2": null,
                "Town_City": null
            };
        };
        $scope.initCustomerToEdit();

        $scope.createCustomer = function () {
            $scope.serverValErr = false;
            $('#templateModalOut').modal('show');
            $scope.modalTpl = {
                modalId: "createEditCustomer",
                modalTitle: "Create Customer",
                buttonName: "Create"
            };
            $scope.initCustomerToEdit();
        };

        $scope.editCustomer = function (customer) {
            $scope.serverValErr = false;
            $('#templateModalOut').modal('show');
            $scope.modalTpl = {
                modalId: "createEditCustomer",
                modalTitle: "Edit Customer ID: " + customer.Id,
                buttonName: "Edit"
            };
            $scope.c = JSON.parse(JSON.stringify(customer));
        };

        $scope.dataPostCustomer = function (form) {
            form.Name.$touched = true;
            form.Address1.$touched = true;
            form.Address2.$touched = true;
            form.Town_City.$touched = true;
            if (!form.Name.$invalid &&
                !form.Address1.$invalid &&
                !form.Address2.$invalid &&
                !form.Town_City.$invalid) {
                form.Name.$touched = false;
                form.Address1.$touched = false;
                form.Address2.$touched = false;
                form.Town_City.$touched = false;
                $http.post("/App/DataPostCustomer/", $scope.c).success(function (response) {
                    if (response > 0) {
                        $scope.serverValErr = false;
                        $scope.c.Id = response;
                        $scope.customerListFn();
                        $scope.addOrder();
                    }
                    else {
                        $scope.serverValErr = true;
                    }
                });
            }
        };

        $scope.addOrder = function () {
            $scope.modalTpl = {
                modalId: "customerOrderDetail",
                modalTitle: "Customer ID: " + $scope.c.Id + " | " + "Name: " + $scope.c.Name,
                buttonName: "Create"
            };
            $scope.orderListFn($scope.c);  
        };

        $scope.orderListFn = function (c) {
            $http.get("/Order/List/" + c.Id).then(function (response) {
                $scope.orderList = response.data;
            });
        };

        $scope.initOrderToEdit = function () {
            $scope.o =
            {
                "OrderId": 0,
                "OrderDate": undefined,
                "PersonId": $scope.c.Id
            };
        };
        $scope.initOrderToEdit();

        $scope.createOrder = function () {
            $scope.serverValErr = false;
            $('#templateModal').modal('show');
            $scope.modalTplIn = {
                modalId: "createEdit",
                modalTitle: "Create",
                buttonName: "Create"
            };
            $scope.initOrderToEdit();
        };

        $scope.editOrder = function (order) {
            $scope.serverValErr = false;
            $('#templateModal').modal('show');
            $scope.modalTplIn = {
                modalId: "createEdit",
                modalTitle: "Edit  ID: " + order.OrderId,
                buttonName: "Edit"
            };
            $scope.o = JSON.parse(JSON.stringify(order));
            $scope.o.OrderDate = new Date($scope.o.OrderDate);
        };

        $scope.dataPostOrder = function (form) {
            form.orderDate.$touched = true;
            if (!form.orderDate.$invalid) {
                form.orderDate.$touched = false;
                console.log($scope.o);
                $http.post("/Order/DataPost/", $scope.o).success(function (response) {
                    if (response === "True") {
                        $scope.serverValErr = false;
                        $scope.orderListFn($scope.c);
                        $scope.customerListFn();
                        $scope.closeIn();
                    }
                    else {
                        $scope.serverValErr = true;
                    }
                });
            }
        };

        $scope.detailOrder = function (order) {
            $('#templateModal').modal('show');
            $scope.modalTplIn = {
                modalId: "detail",
                modalTitle: "Customer ID: " + $scope.c.Id + " | " + "Name: " + $scope.c.Name + " | " + "Order  ID: " + order.OrderId,
                buttonName: "Detail"
            };
            $scope.orderDetailFn(order);
        };

        $scope.deletePost = function (order) {
            var orderDel = { "Id": order.OrderId };
            $scope.serverVal = false;
            $http.post("/Order/Delete/", orderDel).success(function (response) {
                $scope.orderListFn();
                $scope.customerListFn();
            });
        };

        $scope.orderDetailFn = function (order) {
            $http.get("/Order/Details/" + order.OrderId).then(function (response) {
                $scope.orderDetails = response.data;
            });
        };
        
        $scope.productListFn = function () {
            $http.get("/Order/ProductInfo/").then(function (response) {
                $scope.productList = response.data;
            });
        };
      
        $scope.productListFn();
        $scope.addButtonName = "Add Product >>";

        $scope.addProduct = function () {
            if ($scope.modalTplIn.modalIdDetailAdd === "detailAdd")
            {
                $scope.modalTplIn.modalIdDetailAdd = null;
                $scope.addButtonName = "Add Product >>";
            }
            else
            {
                $scope.modalTplIn.modalIdDetailAdd = "detailAdd";
                $scope.SeletedProductId = null;
                $scope.addButtonName = "<< Close";
            }     
        };

        //$scope.closeAddForm = function () {
        //    if ($scope.modalTpl.modalIdDetailAdd === "detailAdd") {
        //        $scope.modalTpl.modalIdDetailAdd = null;
        //        $scope.addButtonName = "Add Product >>";
        //    }
        //};

        //$scope.addProductFromList = function (selectedItem) {
        //    $scope.addForm.selectedProductId.$touched = true;
        //    if (!$scope.addForm.selectedProductId.$invalid) {
        //        var productNew = { "OrderId": $scope.OrderId, "ProductId": selectedItem };
        //        $http.post("/Order/AddProductModal/", productNew).success(function (response) {
        //            if (response === "True") {
        //                $scope.serverValErr = false;
        //                $scope.orderDetailFn();
        //            }
        //            else {
        //                $scope.serverValErr = true;
        //            }
        //        });
        //    }
        //};

        //$scope.removeProduct = function (orderDetailId) {
        //    var productDel = { "id": orderDetailId };
        //    $scope.serverVal = false;
        //    $http.post("/Order/DelOrderDetails/", productDel).success(function (response) {
        //        $scope.orderDetailFn();
        //    });
        //};

        //$scope.deletePostInDetail = function () {
        //    $scope.deletePost($scope.orderToEdit);
        //    $scope.close();
        //};

        $scope.close = function () {
            $('#templateModalOut').modal('hide');
        };

        $scope.closeIn = function () {
            $('#templateModal').modal('hide');
        };

    });
