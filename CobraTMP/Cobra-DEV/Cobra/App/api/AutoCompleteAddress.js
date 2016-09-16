//Using YUI Module pattern to avoid globe variable
var callGooApiAdd = (function () {
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

    //function initialize() {
    //    var input = document.getElementById('searchTextField');
    //    var autocomplete = new google.maps.places.Autocomplete(input);
    //    google.maps.event.addListener(autocomplete, 'place_changed', function () {
    //        var place = autocomplete.getPlace();
    //        document.getElementById('place_id').value = place.place_id;
    //        document.getElementById('place_location').value = place.geometry.location;
    //        document.getElementById('name').value = place.name;
    //        document.getElementById('lname').value = place.long_name;
    //        document.getElementById('address').value = place.formatted_address;
    //        document.getElementById('street_number').value = place.street_number;
    //        document.getElementById('postcode').value = place.postal_code;
    //        document.getElementById('phone').value = place.formatted_phone_number;
    //        document.getElementById('website').value = place.website;
    //        document.getElementById('rating').value = place.rating;
    //        document.getElementById('Lat').value = place.geometry.location.lat();
    //        document.getElementById('Lng').value = place.geometry.location.lng();
    //    });
    //}
    //google.maps.event.addDomListener(window, 'load', initialize);

    function callApi() {
        
        autoPlaces = new google.maps.places.Autocomplete(document.getElementById('input')), { types: ['geocode'] };
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

        })
    };
    google.maps.event.addDomListener(window, 'load', callApi)
    console.log(fillForm);
    console.log(document.readyState);

})();
document.addEventListener("DOMContentLoaded", callGooApiAdd, false); // make sure all scripts and DOM is loaded for the API ready
window.addEventListener("load",callGooApiAdd,false);






