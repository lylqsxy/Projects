(function () {
    'use strict';
    cobraApp.directive('cobraPopOver', function ($compile) {
        return {

            link: function (scope, element, attrs) {

                element.on('mousedown', function () {
                    var parentId = '#' + attrs.parentid;

                    if ($(parentId).attr('value') == 'false') {

                        // the popoverbody and popoverfooter allow you to use one body template with differnt buttons. you can skip the footer and just use the popoverbodyid to create the body
                        var popOverBodyId = attrs.popoverbody;    // get popoverbodyid from popoverbody attrs
                        var popOverFooterId;
                        popOverFooterId = attrs.popoverfootover;  // get popoverfooterid from popoverbody attrs

                        if (popOverBodyId.indexOf('#') < 0) {   // if you have not added a # to your popoverbodyid attr add one for you
                            popOverBodyId = "#" + popOverBodyId;
                        }

                        if (popOverFooterId) {
                            if (popOverFooterId.indexOf('#') < 0) {     // if you have not added a # to your popoverfooterid attr add one for you
                                popOverFooterId = "#" + popOverFooterId;
                            }
                        }

                        var popOverBody = $(popOverBodyId).html()           // get html from hidden popoverbodyid div
                        var popOverFooter = $(popOverFooterId).html()       // get html from hidden popoverfooterid div

                        if (popOverFooter) {       // if there is a footer add the body and footer together                            
                            var popOverHtml = popOverBody + popOverFooter;
                        } else {                // if no footer just add the body
                            var popOverHtml = popOverBody;
                        }

                        var options = {             // compile popover into the dom
                            content: $compile(popOverHtml)(scope),
                            html: true, container: 'body', trigger: 'manual'
                        };

                        $(element).popover(options);            // load options into popover
                        $(element).popover('show');             // show popover
                        $(parentId).attr('value', 'true');      // set value in parent div container to true so no other popovers can be shown until current one is closed
                    }

                });

            }
        }

    });
})()