
(function () {
    // Controller: ForgetPassword
    cobraApp.controller('account/ForgetPasswordCtrl', ['$scope', '$http', '$window', function ($scope, $http, $window, utils) {
        $scope.hereLink = $window.location.origin + "/Home/Index";
        // Initializing scope
        $scope.data = {
            // object matches with the back-end ViewModel
            userData: { email: '' },
            msg: '',
            hideForm: false,
            showConfirmationMsg: false,
            showServerErrorMsg: false
        };


        //Prepare one single order value to Modal
        $scope.toModalObject = function (user) {
            var orderToModal = [
                    { title: 'Email', variableName: 'Email', value: (user ? user.Email : ''), type: 'email', validateon: { minLen: 2, errorText: '* required' } }
            ];

            return orderToModal;
        };
            
        $scope.showModal = function () {
            var modalOption = {
                modalTitle: 'Forget your password?',
                controller: 'account',
                action: 'getPassword',
                idVariable: 'UserId',
                idValue: '12'
            };

            $scope.$broadcast('showModelEvent', [$scope.toModalObject(), modalOption]);
        };

        $scope.$on('modelDone', function (event, data) {
            if (data) {
                console.log('Success');
            } else {
                console.log('error');
            }
        });


        $scope.serverErrors = [];

        //Prepare one single order value to Modal
        $scope.toModalObject = function (order) {
            var orderToModal = [
                    { title: 'Domain', variableName: 'Domain', value: (order ? order.Domain : ''), type: 'text', validation: { minLen: 2, errorText: '* required' } },
                    { title: 'Email', variableName: 'Email', value: (order ? order.Email : ''), type: 'email', validation: { required: true, errorText: '* please input your email' }, regExpVaid: { text: 'Email格式不正确', reg: /^\s*\w*\s*$/ } },
                    { title: 'Phone', variableName: 'Phone', value: (order ? order.Phone : ''), type: 'tel' },
                    { title: 'Order Status', variableName: 'OrderStatus', value: (order ? order.OrderStatus : 0), type: 'select', selectEnum: $scope.orderStatus },
                    { title: 'Order Date', variableName: 'OrderDate', value: (order ? order.OrderDate : new Date()), type: 'date' },
                    { title: 'Marketing Way', variableName: 'MarketingWay', value: (order ? order.MarketingWay : 0), type: 'select', selectEnum: $scope.marketingWay },
                    { title: 'Product Name', variableName: 'ProductName', value: (order ? order.ProductName : ''), type: 'text' },
                    { title: 'Description', variableName: 'Description', value: (order ? order.Description : ''), type: 'textarea' }
            ];

            return orderToModal;
        };

        $scope.showModal = function (id) {
           
            var modalOption = {
                modalTitle: 'Order Detail',
                controller: 'orders', // corrsponding to .net controller
                action: 'index', // index, edit and create, corrsponding to .net backend action
                idVariable: 'OrderId', // Id variale 
                idValue: '12' // the id of the entity, when create new, keep empty
            };

            $scope.$broadcast('showModelEvent', [$scope.toModalObject(), modalOption]);
        };

        $scope.$on('modelDone', function (event, data) {
            if (data) {
                console.log('Success');
            } else {
                console.log('error');
            }
        });

        // POST Email
        $scope.postData = function (valid) {
            if (!valid) { // Tom add function for valid onSubmit button
                console.log('Invalid');
            } else {

                var req = {
                    method: 'POST',
                    url: $window.location.origin + '/Account/ForgetPassword',
                    data: $scope.data.userData
                };
                $http(req).then(
                    function (response) {
                        $scope.data.msg = response.data.MsgText;
                        $scope.data.hideForm = response.data.Success ==true ;
                        $scope.data.userData.email = response.data.Email;
                        if (response.data.Success) {
                            //$scope.data.hideForm = true;
                            $scope.data.showServerErrorMsg = false;
                            $scope.data.showConfirmationMsg = true;
                        }
                        else {
                            $scope.serverErrors = response.data.Errors;
                            $scope.data.showServerErrorMsg = true;
                            $scope.data.showConfirmationMsg = false;

                        }
                    });
            }
        };

    }]);


    cobraApp.run(['$http', function ($http) {
        $http.defaults.headers.common['X-XSRF-Token'] =
            angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value');
    }]);
})();