(function () {
    'use strict';
    cobraApp.directive('cobraPopOver', function ($compile, $http) {
        return {

            link: function (scope, element, attrs) {

                var partialViewObject = { partialView: attrs.partialview }
                var url = "/" + attrs.mcontrol + "/CobraPopOver"
                
                $http({
                    method: "Post",
                    url: url,
                    data: partialViewObject,
                }).then(function SentOk(result) {

                    var options = {
                        content: $compile(result.data)(scope),
                        html: true, container: 'body', trigger: 'manual'
                    };

                    $(element).popover(options);
                    element.on('mousedown', function () {
                        var parentId = '#' + attrs.parentid

                        if ($(parentId).attr('value') == 'false') {
                            $(element).popover('show');
                            $(parentId).attr('value', 'true');
                        }
                    });

                }, function Error(result) {


                });

            }
        }

    });
})()