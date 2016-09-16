// created by Ty 
// Date : 3/8/16
// geo location

var drawingManager;
var all_overlays = [];
var selectedShape;
var colors = ['#1E90FF', '#FF1493', '#32CD32', '#FF8C00', '#4B0082'];
var selectedColor;
var colorButtons = {};
var path;
var map, coords;
var currentDate = new Date();
var tomorrow = new Date();
tomorrow.setDate(tomorrow.getDate() + 1);
var afterTomorrow = new Date();
afterTomorrow.setDate(tomorrow.getDate() + 2);

cobraApp.controller('GeoLocationCtrl', function ($scope, $http) {

    var that = this;

    $scope.polygons = null;
    $scope.coordinates = coords;
    // min date picker
    this.pickerMin = {
        date: new Date(),
        datepickerOptions: {
            maxDate: null
        }
    };

    // max date picker
    this.pickerMax = {
        date: new Date(),
        datepickerOptions: {
            minDate: null
        }
    };

    // set date for max picker, 10 days in future
    this.pickerMax.date.setDate(this.pickerMax.date.getDate() + 1);

    this.openCalendar = function (e, picker) {
        that[picker].open = true;
    };

    // watch min and max dates to calculate difference
    var unwatchMinMaxValues = $scope.$watch(function () {
        return [that.pickerMin, that.pickerMax];
    }, function () {
        // min max dates
        that.pickerMin.datepickerOptions.maxDate = that.pickerMax.date;
        that.pickerMax.datepickerOptions.minDate = that.pickerMin.date;

        if (that.pickerMin.date && that.pickerMax.date) {
            var diff = that.pickerMin.date.getTime() - that.pickerMax.date.getTime();
            that.dayRange = Math.round(Math.abs(diff / (1000 * 60 * 60 * 24)))
        } else {
            that.dayRange = 'n/a';
        }
    }, true);


    // destroy watcher
    $scope.$on('$destroy', function () {
        unwatchMinMaxValues();
    });

    var polyOptions = {
        strokeColor: '#FF0000',
        strokeOpacity: 0.8,
        strokeWeight: 2,
        fillColor: '#FF0000',
        fillOpacity: 0.35
    };

    //$scope.drawFence = function () {
    //    console.log(JSON.stringify($scope.Geo.StartDate));
    //    $http({
    //        method: 'post',
    //        url: '/Geo/DrawFence',
    //        params: {
    //            'coordinates': JSON.stringify(coords),
    //            'polygons': JSON.stringify(path),
    //            'start': JSON.stringify($scope.Geo.StartDate),
    //            'end': JSON.stringify($scope.Geo.EndDate)
    //        }
    //    }).then(function (response) {
    //        if (response.data.Coordinates && response.data.Polygons)
    //            alert(JSON.stringify(response.data.Coordinates) + '==::==' + JSON.stringify(response.data.Polygons));
    //        else
    //            alert(JSON.stringify(response.data.Result));
    //    })
    //}

    drawingManager = new google.maps.drawing.DrawingManager({
        drawingControlOptions: {
            position: google.maps.ControlPosition.TOP_CENTER,
            drawingModes: ['polygon']
        },
        drawingMode: google.maps.drawing.OverlayType.POLYGON,
        markerOptions: {
            draggable: true
        },
        polylineOptions: {
            editable: true,
        },
        polygonOptions: polyOptions,
        map: map
    });

    //loading current location
    navigator.geolocation.getCurrentPosition(function (position) {
        // map coords
        coords = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
        // map options
        $scope.options = {
            zoom: 13,
            center: coords,
            mapTypeId: google.maps.MapTypeId.TERRAIN
        };
        // map object
        map = new google.maps.Map(document.getElementById('map'), $scope.options);
        // map marker
        $scope.marker = new google.maps.Marker({
            position: coords,
            draggable: true,
            map: map,
            title: "You are here!"
        });
        drawingManager.setMap(map);

    }, function (error) {
        alert('Unable to get location: ' + error.message);
    });

    // load from autocomplete
    $scope.searchForm = { 'SearchBox': '', 'fromLat': '', 'fromLng': '' };
    var options = {
        componentRestrictions: {}
    };
    var inputFrom = document.getElementById('search-box');
    var autocompleteFrom = new google.maps.places.Autocomplete(inputFrom, options);

    // auto complete
    google.maps.event.addListener(autocompleteFrom, 'place_changed', function () {
        var place = autocompleteFrom.getPlace();
        $scope.searchForm.fromLat = place.geometry.location.lat();
        $scope.searchForm.fromLng = place.geometry.location.lng();
        $scope.searchForm.from = place.formatted_address;
        coords = new google.maps.LatLng($scope.searchForm.fromLat, $scope.searchForm.fromLng);
        // map options
        $scope.options = {
            zoom: 13,
            center: coords,
            mapTypeId: google.maps.MapTypeId.TERRAIN
        };
        // map object
        map = new google.maps.Map(document.getElementById('map'), $scope.options);
        // map marker
        $scope.marker = new google.maps.Marker({
            position: coords,
            draggable: true,
            map: map,
            title: "You are here!"
        });

        (function () {
            $scope.polygons = null;
            $scope.coordinates = coords;
        })();
        drawingManager.setMap(map);
        $scope.$apply();

    });
    // drawing is completed
    google.maps.event.addListener(drawingManager, 'overlaycomplete', function (e) {
        all_overlays.push(e);
        if (e.type != google.maps.drawing.OverlayType.MARKER) {
            // Switch back to non-drawing mode after drawing a shape.
            drawingManager.setDrawingMode(null);

            // Add an event listener that selects the newly-drawn shape when the user
            // mouses down on it.
            var newShape = e.overlay;
            newShape.type = e.type;
            google.maps.event.addListener(newShape, 'click', function () {
                setSelection(newShape);
            });
            setSelection(newShape);
        }
    });

    google.maps.event.addListener(drawingManager, 'drawingmode_changed', clearSelection);
    google.maps.event.addDomListener(document.getElementById('delete-button'), 'click', function (e) {
        e.preventDefault();
        deleteSelectedShape();
        if ($scope.polygons != null)
            (function (scope) {
                scope.polygons = null;
                scope.$apply();
            })($scope)
    });

    google.maps.event.addListener(drawingManager, 'polygoncomplete', function (polygon) {
        pol = (polygon.getPath().getArray());
        // not very angularjs 
        (function (coordinates, polygons, scope) {
            scope.polygons = polygons;
            scope.coordinates = coordinates;
            scope.$apply();
        })(coords, pol, $scope)
    });
});

function clearSelection() {
    if (selectedShape) {
        selectedShape.setEditable(false);
        selectedShape = null;
    }
}

function setSelection(shape) {
    clearSelection();
    selectedShape = shape;
    shape.setEditable(true);
}

function deleteSelectedShape() {
    if (selectedShape) {
        selectedShape.setMap(null);
    }
    path = {};
}

function deleteAllShape() {
    for (var i = 0; i < all_overlays.length; i++) {
        all_overlays[i].overlay.setMap(null);
    }
    all_overlays = [];
    path = {};
}

function setGeoFence(coordinates, polygons, controller) {

}




