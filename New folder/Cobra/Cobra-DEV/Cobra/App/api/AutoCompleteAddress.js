//Using YUI Module pattern to avoid globe variable

//var callAFunc = (function () {
//    var autoPlaces, place, val; // this is globle variable need to be wrapped
//    var fillForm = { // element for the fill form according to google autocomplete document


//        street_number: 'short_name',
//        route: 'long_name',
//        locality: 'long_name',
//        administrative_area_level_1: 'long_name',
//        administrative_area_level_2: 'long_name',
//        country: 'long_name',
//        postal_code: 'short_name'
//    }

//     function callApi() {

//        //var input = document.getElementsByTagName('script');
//        //var input = document.querySelector('script[id="idEditAddressTemplate"]');
//        var input = document.getElementById('input123456');
//        //autoPlaces = new google.maps.places.Autocomplete(input, { types: ['geocode'] });
//        autoPlaces = new google.maps.places.Autocomplete(input);
//        debugger;
//        google.maps.event.addListener(autoPlaces, 'place_changed', function () {
//            place = autoPlaces.getPlace();
//            //finished capture information of the address input
//            for (var element in fillForm) {
//                document.getElementById(element).value = '';
//                document.getElementById(element).disable = false;
//            }
//            // finished checking variable of each fields 
//            for (var i = 0; i < place.address_components.length; i++) {
//                var addressType = place.address_components[i].types[0];
//                if (fillForm[addressType]) {
//                    val = place.address_components[i][fillForm[addressType]];
//                    document.getElementById(addressType).value = val;
//                    console.log(val);
//                }
//            }
//            //finished filling element for each feild

//        })
//    };
//    callApi();
//    //google.maps.event.addDomListener(window, 'load', callApi)
//    console.log(fillForm);
//    console.log(document.readyState);

//});


var btnStatus = false;
function callAFunc() {
    cobraApp.controller('tempC', function ($scope, $timeout, $document) {
        $scope.callGooApiAdd = function () {
            var autoPlaces, place, val; // this is globle variable need to be wrapped
            var fillForm = { // element for the fill form according to google autocomplete document


                street_number: 'short_name',
                route: 'long_name',
                locality: 'long_name',
                administrative_area_level_1: 'long_name',
                administrative_area_level_2: 'long_name',
                country: 'long_name',
                postal_code: 'short_name'
            }


            $scope.callApi = function () {

                //var input = document.getElementsByTagName('script');
                //var input = document.querySelector('script[id="idEditAddressTemplate"]');
                var input = document.getElementById('input123456');
                //autoPlaces = new google.maps.places.Autocomplete(input, { types: ['geocode'] });
                autoPlaces = new google.maps.places.Autocomplete(input);
                google.maps.event.addListener(autoPlaces, 'place_changed', function () {
                    place = autoPlaces.getPlace();
                    //finished capture information of the address input
                    for (var element in fillForm) {
                        document.getElementById(element).value = '';
                        document.getElementById(element).disable = false;
                    }
                    // finished checking variable of each fields 
                    for (var i = 0; i < place.address_components.length; i++) {
                        var addressType = place.address_components[i].types[0];
                        if (fillForm[addressType]) {
                            val = place.address_components[i][fillForm[addressType]];
                            document.getElementById(addressType).value = val;
                            console.log(val);
                        }
                    }
                    //finished filling element for each feild
                    scope.$on('$destroy', function () {
                        $(".pac-container").remove();
                        autocomplete = null;
                    });

                })
            };
            $scope.callApi();
            //google.maps.event.addDomListener(window, 'load', callApi)
            console.log(fillForm);
            console.log(document.readyState);

        };
        $scope.callGooApiAdd();
    });
};

//document.addEventListener("DOMContentLoaded", callGooApiAdd, false); // make sure all scripts and DOM is loaded for the API ready
//window.addEventListener("load",callGooApiAdd,false);






