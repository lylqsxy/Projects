//
// 
// 
app.controller('DisplayFence', function ($scope, $http) {
    var polygons = {},
        coords = {};

    $http({
        method: 'GET',
        url: '/Geo/GetLocation',
        params: { "id": 1 }
    }).then(function (response) {
        if (response.data.Location)
            coords = JSON.parse(response.data.Location);
        if (response.data.Polygons)
            polygons = JSON.parse(response.data.Polygons);
        if (coords.length > 0 && polygons.length > 0) {
            alert('coords::' + coords + '||' + polygons)
            initializMap(coords, polygons);

        } else
            alert('Unable to load the geofence');
    });
})

function initializMap(coords, polygons) {
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 14,
        center: (coords),
        mapTypeId: google.maps.MapTypeId.TERRAIN
    });

    // Define the LatLng coordinates for the polygon's path.
    var ploygonCoords = JSON.parse(polygons);

    // Construct the polygon.
    var polygonShape = new google.maps.Polygon({
        paths: (ploygonCoords),
        strokeColor: '#FF0000',
        strokeOpacity: 0.8,
        strokeWeight: 2,
        fillColor: '#FF0000',
        fillOpacity: 0.35
    });

    polygonShape.setMap(map);
}