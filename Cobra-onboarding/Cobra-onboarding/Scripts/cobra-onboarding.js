var app = angular.module('myApp', ['ngRoute', 'angularValidator']);

app.config(function ($routeProvider) {
    $routeProvider
    .when("/", {
        templateUrl: "/Home/App"
    })
    .when("/order", {
        templateUrl: "/Order"
    })
    .when("/customer", {
        templateUrl: "/Customer"
    })
    .when("/app", {
        templateUrl: "/Home/App"
    })
    .when("/order", {
        templateUrl: "/Order"
    })
    .when("/customer", {
        templateUrl: "/Customer"
    })

});

app.controller('appCtrl',
    function ($scope, $http, $location, $anchorScroll) {
        $scope.customerListFn = function () {
            $http.get("/Customer/List/").then(function (response) {
                $scope.customerList = response.data;
            });
        };
        $scope.customerListFn();
        window.location.assign("#app");
        $scope.modalPath = '/Customer/CustomerModalContent';

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
            $('#commonModal').modal('show');
            $scope.modalTpl = {
                modalId: "createEditCustomer",
                modalTitle: "Create Customer",
                modalPath: '/Customer/CustomerModalContent'
            };
            $scope.initCustomerToEdit();
        };

        $scope.editCustomer = function (customer) {
            $scope.serverValErr = false;
            $('#commonModal').modal('show');
            $scope.modalTpl = {
                modalId: "createEditCustomer",
                modalTitle: "Edit Customer ID: " + customer.Id,
                modalPath: '/Customer/CustomerModalContent'
            };
            $scope.c = JSON.parse(JSON.stringify(customer));
        };

        $scope.orderCustomer = function (customer) {
            $scope.showOrder = true;
            $scope.scrollToHash("orderAnchor");
            $scope.c = JSON.parse(JSON.stringify(customer));
            $scope.orderListFn($scope.c);
        };

        $scope.deleteCustomer = function (customer) {
            if (confirm("All the customer's orders will be deleted!"))
            {
                var customerDel = { "Id": customer.Id };
                $scope.serverVal = false;
                $http.post("/Customer/Delete/", customerDel).success(function (response) {
                    $scope.customerListFn();
                });
            }
            
        };

        $scope.dataPostCustomer = function (form) {
            if (!form.$invalid) {
                $http.post("/Customer/DataPostCustomer/", $scope.c).success(function (response) {
                    if (response > 0) {
                        $scope.serverValErr = false;
                        $scope.c.Id = response;
                        $scope.customerListFn();
                        $scope.close(form);
                    }
                    else {
                        $scope.serverValErr = true;
                    }
                });
            }
        };

        $scope.orderListFn = function (customer) {
            $http.get("/Order/List/" + customer.Id).then(function (response) {
                $scope.orderList = response.data;
            });
        };

        $scope.initOrderToEdit = function () {
            $scope.o =
            {
                "OrderId": 0,
                "OrderDate": null,
                "PersonId": $scope.c.Id
            };
        };
        $scope.initOrderToEdit();

        $scope.createOrder = function () {
            $scope.serverValErr = false;
            $('#commonModal').modal('show');
            $scope.modalTpl = {
                modalId: "createEdit",
                modalTitle: "Create",
                modalPath: '/Order/OrderModalContent'
            };
            $scope.initOrderToEdit();
        };

        $scope.editOrder = function (order) {
            $scope.serverValErr = false;
            $('#commonModal').modal('show');
            $scope.modalTpl = {
                modalId: "createEdit",
                modalTitle: "Edit  ID: " + order.OrderId,
                modalPath: '/Order/OrderModalContent'
            };
            $scope.o = JSON.parse(JSON.stringify(order));
            $scope.o.OrderDate = new Date($scope.o.OrderDate);
        };

        $scope.dataPostOrder = function (form) {
            if (!form.$invalid) {
                $http.post("/Order/DataPost/", $scope.o).success(function (response) {
                    if (response > 0) {
                        $scope.serverValErr = false;
                        $scope.o.OrderId = response;
                        $scope.orderListFn($scope.c);
                        $scope.customerListFn();                  
                        $scope.close(form);
                    }
                    else {
                        $scope.serverValErr = true;
                    }
                });
            }
        };

        $scope.detailOrder = function (order) {
            $scope.addButtonName = "Add Product >>";
            $('#commonModal').modal('show');
            $scope.modalTpl = {
                modalId: "detail",
                modalTitle: "Customer ID: " + $scope.c.Id + " | " + "Name: " + $scope.c.Name + " | " + "Order  ID: " + order.OrderId,
                modalPath: '/Order/OrderModalContent',
                buttonName: "Detail",
                modalIdDetailAdd : null
            };
            $scope.o = JSON.parse(JSON.stringify(order));
            $scope.o.OrderDate = new Date($scope.o.OrderDate);
            $scope.orderDetailFn(order);
        };

        $scope.deletePost = function (order) {
            if (confirm("All the order details will be deleted!"))
            {
                var orderDel = { "Id": order.OrderId };
                $scope.serverVal = false;
                $http.post("/Order/Delete/", orderDel).success(function (response) {
                    $scope.orderListFn($scope.c);
                    $scope.customerListFn();
                });
            }           
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

        $scope.addProductFromList = function (selectedItem, form) {
            form.selectedProductId.$touched = true;
            if (!form.$invalid) {
                var productNew = { "OrderId": $scope.o.OrderId, "ProductId": selectedItem };
                $http.post("/Order/AddProductModal/", productNew).success(function (response) {
                    if (response > 0) {
                        $scope.serverValErr = false;
                        $scope.orderDetailFn($scope.o);
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
                $scope.orderDetailFn($scope.o);
            });
        };

        $scope.deletePostInDetail = function () {
            $scope.deletePost($scope.o);
            $('#commonModal').modal('hide');
        };

        $scope.close = function (form) {
            form.reset();
            $('#commonModal').modal('hide');
        };

        $scope.scrollToHash = function (hashKey) {
            $location.hash(hashKey);
            $anchorScroll();
        }

        $scope.today = new Date().toISOString().substr(0, 10);

    });
