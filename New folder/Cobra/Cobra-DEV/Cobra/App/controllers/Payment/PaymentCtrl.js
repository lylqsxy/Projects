//Author: Aaron Bhardwaj
(function () {
    'use strict';

    cobraApp.config(function ($locationProvider) {
        $locationProvider.html5Mode({ enabled: true, requireBase: false });
    });

    // Controller: PaymentCtrl
    cobraApp.controller('PaymentCtrl', ['$scope', '$http', '$window', '$location', function ($scope, $http, $window, $location) {
        // Initialize scope
        $scope.data = {
            userData: {
                "CreditCard": { "CardNumber": null, "ExpiryMonth": null, "ExpiryYear": null, "FirstName": null, "LastName": null, "CVV2": null, "CardType": null }
            },
            msg: '',
            showError: false
        };
        
        $scope.disableButton = false;
        $scope.serverErrors = [];
        $scope.err = false;

        // Create card Payment
        $scope.payByCard = function () {
            $scope.disableButton = true;
            var req = {
                method: 'POST',
                url: '/Payment/DoDirectPayment',
                data: {
                    cardModel: $scope.data.userData.CreditCard,
                    etoken: $location.search().etoken,
                    email: $location.search().email
                },
                headers: {
                    'X-XSRF-Token': angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value')
                }
            }
            $http(req).then(
                function (response) {
                    $scope.data.msg = response.data.MsgText;
                    debugger;
                    if (response.data.Success) {
                        $window.location.href = response.data.RedirectUrl;
                    }
                    else {
                        $scope.disableButton = false;
                        $scope.serverErrors = response.data.Errors;
                        $scope.data.showError = true;
                    }
                }
            );
        };

        // Create PayPal Payment
        $scope.dataP = {
            msg: '',
            showError: false
        };
        $scope.payByPayPal = function () {
            $scope.dataP = {
                msg: [],
                showError: false
            };
            var req1 = {
                method: 'POST',
                url: '/Payment/SetExpressCheckout',
                data: {
                    etoken: $location.search().etoken,
                    email: $location.search().email
                    },
                headers: {
                    'X-XSRF-Token': angular.element(document.querySelector('input[name="__RequestVerificationToken"]')).attr('value')
                }
            }
            $http(req1).then(
                function (response) {
                    $scope.dataP.msg = response.data.MsgText;
                    debugger;
                    if (response.data.Success) {
                        $window.location.href = response.data.RedirectUrl;
                    }
                    else {
                        $scope.disableButton = false;
                        $scope.serverErrors = response.data.Errors;
                        $scope.dataP.showError = true;
                    }
                }
            );
        };

        $scope.month = null;
        $scope.year = null;
        $scope.invalidMonth = false;
        $scope.invalidYear = false;
        $scope.invalidDate = false;
        $scope.checkDate = function () {
            var monthP = new Date().getMonth() + 1; //January is 0!
            var yearP = new Date().getFullYear();
            var yr = '20' + $scope.year;

            // Date validation i.e. date less than or equal to current date
            if (parseInt($scope.month, 10) <= monthP && parseInt(yr, 10) == yearP) {
                $scope.invalidDate = true;
            }
            else {
                $scope.invalidDate = false;
                $scope.invalidMonth = false;
                $scope.invalidYear = false;
            }

            // Month validation
            if (parseInt($scope.month, 10) > 0 && parseInt($scope.month, 10) <= 12 || $scope.month == null) {
                $scope.data.userData.CreditCard.ExpiryMonth = $scope.month;
                $scope.invalidMonth = false;
            }
            else {
                $scope.data.userData.CreditCard.ExpiryMonth = null;
                $scope.invalidMonth = true;
            }

            // Year validation
            if (parseInt(yr, 10) >= yearP || $scope.year == null) {
                $scope.data.userData.CreditCard.ExpiryYear = $scope.year == null ? null : '20' + $scope.year;
                $scope.invalidYear = false;
            }
            else {
                $scope.data.userData.CreditCard.ExpiryYear = null;
                $scope.invalidYear = true;
            }
        };

        $scope.cardMaxLength = 16;
        $scope.cardCVVLength = 3;
        $scope.checkCard = function () {
            // DISCOVER
            // BIN Series: 6011, 65, 644-649, 622126-622925
            if (/^(6011|65|64[4-9]|62212[6-9]|6221[3-9][0-9]|622[2-8][0-9][0-9]|6229[01][0-9]|62292[0-5])/.test($scope.data.userData.CreditCard.CardNumber)) {
                $scope.data.userData.CreditCard.CardType = 'DISCOVER';
                $scope.cardMaxLength = 19;
                $scope.cardCVVLength = 3;
            }
            // MASTERCARD
            // BIN Series: 222100-222999, 223000-229999, 230000-269999, 270000-271999, 272000-272099
            // future BIN series 222100-272099
            // Add following regex to if condition for 'mastercard'
            // Regex: 222[1-9][0-9][0-9]|22[3-9][0-9][0-9][0-9]|2[3-6][0-9][0-9][0-9][0-9]|27[01][0-9][0-9][0-9]|2720[0-9][0-9]
            else if (/^5[1-5]/.test($scope.data.userData.CreditCard.CardNumber)) {
                $scope.data.userData.CreditCard.CardType = 'MASTERCARD';
                $scope.cardMaxLength = 16;
                $scope.cardCVVLength = 3;
            }
            // VISA
            // BIN Series: 4
            else if (/^4/.test($scope.data.userData.CreditCard.CardNumber)) {
                $scope.data.userData.CreditCard.CardType = 'VISA';
                $scope.cardMaxLength = 19;
                $scope.cardCVVLength = 3;
            }
            // AMERICAN EXPRESS
            // BIN Series: 34, 37
            else if (/^3[47]/.test($scope.data.userData.CreditCard.CardNumber)) {
                $scope.data.userData.CreditCard.CardType = 'AMEX';
                $scope.cardMaxLength = 15;
                $scope.cardCVVLength = 4;
            }
            else {
                $scope.data.userData.CreditCard.CardType = 'unknown';
                $scope.cardMaxLength = 16;
                $scope.cardCVVLength = 3;
            }
        }
    }]);
})();
