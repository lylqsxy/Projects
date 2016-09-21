// Author: Aaron Bhardwaj
(function () {
    'use strict';
    // Directive: cvvTooltip
    cobraApp.directive('cvvTooltip', function () {
        return {
            restrict: 'A',
            link: function (scope, element, attrs, controller) {
                $(element).popover({
                    content:
                        '<div class="row">' +
                        '   <div class="col-xs-5">' +
                        '       <img src="../Content/Images/Payment/card_visa_cvv.jpg"/>' +
                        '   </div>' +
                        '   <div class="col-xs-7 hidden-xs">' +
                        '       <div>Visa, Mastercard or Discover</div>' +
                        '   </div>' +
                        '</div>' +
                        '<div class="row" style="margin-top:3%">' +
                        '   <div class="col-xs-5">' +
                        '       <img src="../Content/Images/Payment/card_amex_cvv.jpg"/>' +
                        '   </div>' +
                        '   <div class="col-xs-7 hidden-xs">' +
                        '       <div>American Express</div>' +
                        '   </div>' +
                        '</div>',
                    html: true,
                    placement: "auto right",
                    trigger: "hover",
                    container: "div.cvvpop"
                });
            }
        };
    });
})();