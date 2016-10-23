'use strict';

var map = null;
var greenLayer;
var testDataGenerator = new function () {

    /*
    * Example data model that may be returned from a custom web service.
    */
    var exampleDataModel = function (name, latitude, longitude) {
        this.Name = name;
        this.Latitude = latitude;
        this.Longitude = longitude;
    };


    /*
    * This function generates a bunch of random random data with 
    * coordinate information and returns it to a callback function 
    * similar to what happens when calling a web service.
    */
    this.generateData = function (numPoints, callback) {
        var data = [], randomLatitude, randomLongitude;

        for (var i = 0; i < numPoints; i++) {
            randomLatitude = Math.random() * 181 - 90;
            randomLongitude = Math.random() * 361 - 180;
            data.push(new exampleDataModel("Point: " + i, randomLatitude, randomLongitude));
        }

        if (callback) {
            callback(data);
        }
    };
};
function displayInfo(e) {
    if (e.targetType == "pushpin") {
        showInfobox(e.target);
    }
}

function showInfobox(shape) {
    for (var i = map.entities.getLength() - 1; i >= 0; i--) {
        var pushpin = map.entities.get(i);
        if (pushpin.toString() == '[Infobox]') {
            map.entities.removeAt(i);
        };
    }
    var pix = map.tryLocationToPixel(shape.getLocation(), Microsoft.Maps.PixelReference.control);
    var infoboxOptions = { width: 170, height: 80, showCloseButton: true, zIndex: 10, offset: new Microsoft.Maps.Point(10, 0), showPointer: true, title: shape.title, description: shape.description };
    var defInfobox = new Microsoft.Maps.Infobox(shape.getLocation(), infoboxOptions);
    map.entities.push(defInfobox);
}

function customModuleLoaded(pins) {
    greenLayer = new ClusteredEntityCollection(map, {
        singlePinCallback: createPin,
        clusteredPinCallback: createClusteredPin
    });
    console.log(pins);
    requestGreenDataCallback(pins);
    //  setTimeout(function (obj) { testDataGenerator.generateData(200, requestGreenDataCallback); }, 2000, testDataGenerator);
}

function requestGreenDataCallback(response) {
    if (response != null) {
        // console.log('Response');
        //    console.log(response);
        //This method is part of dynamically downloaded clustering Module V7ClientSideClustering.js.
        greenLayer.SetData(response);
    }
}
function createClusteredPin(cluster, latlong) {
    var pin = new Microsoft.Maps.Pushpin(latlong, {
        icon: '/content/images/clusteredpin.png',
        anchor: new Microsoft.Maps.Point(8, 8)
    });
    pin.title = 'Cluster pin';
    pin.description = 'Cluster Size: ' + cluster.length;
    Microsoft.Maps.Events.addHandler(pin, 'click', displayInfo);
    return pin;
}

function createPin(data) {
    var pin = new Microsoft.Maps.Pushpin(data._LatLong, {
        icon: '/content/images/nonclusteredpin.png',
        anchor: new Microsoft.Maps.Point(8, 8)
    });
    pin.title = 'Single pin';
    pin.description = 'GridKey: ' + data.GridKey;
    Microsoft.Maps.Events.addHandler(pin, 'click', displayInfo);
    return pin;
}

function getMap(mapId) {
    map = new Microsoft.Maps.Map(document.getElementById(mapId),
                                {
                                    credentials: 'AnDvM9VHP-ujFKtfLebovTRHr4_eHgoSj5kQgGPIiM6YTJDrbutUpzVCb2Buorpo',
                                    center: new Microsoft.Maps.Location(39.180928, -95.503950),
                                    zoom: 5
                                });
    //load modules
    Microsoft.Maps.registerModule('clusterModule', 'https://www.bingmapsportal.com/scripts/V7ClientSideClustering.js');
    Microsoft.Maps.registerModule("DrawingToolsModule", "/content/DrawingToolsModule.js");
}

function loadClusterModule(loadLocCallback, loadMapCallBack) {
    Microsoft.Maps.loadModule('clusterModule', { callback: loadLocCallback });
    Microsoft.Maps.loadModule("DrawingToolsModule", {
        callback: function () {
            var drawingTools = new DrawingTools.DrawingManager(map, {
                toolbarContainer: document.getElementById('toolbarContainer'),

                events: {
                    drawingEnded: function (s) {
                        loadMapCallBack(s);
                    }
                }
            });
        }
    });
}

PecoApp.controller('GeoFenceController', function ($scope, $http) {
    $scope.firstName = "John";
    $scope.lastName = "Doe";
    console.log($scope);
    $scope.loadLocations = function () {
        console.log('here');
        // customModuleLoaded();
        $http.get("/api/Ping/Locations")
             .then(function (response) {
                 //First function handles success
                 $scope.content = response.data;
                 //  console.log($scope.content);
                 var pinsdata = [];
                 for (var i = 0; i < $scope.content.length; i++) {
                     var temploc = $scope.content[i];
                     var exampleDataModel = function (name, latitude, longitude) {
                         this.Name = name;
                         this.Latitude = latitude;
                         this.Longitude = longitude;
                     };
                     pinsdata.push(new exampleDataModel("Points " + i, temploc.Lat, temploc.Long));
                 }
                 customModuleLoaded(pinsdata);
             }, function (response) {

             });

    }
    $scope.NewShapes = [];
    $scope.saveLocation = function (shape) {
        console.log('shape saved 1');
        console.log(shape);
        $scope.NewShapes.push(shape);
        if (shape.ShapeInfo && shape.ShapeInfo.type != DrawingTools.DrawingMode.pushpin) {


            if (shape.ShapeInfo.type == DrawingTools.DrawingMode.circle) {

                console.log(shape.ShapeInfo.radius + "" + shape.ShapeInfo.center);

            } else {
                var locs = shape.getLocations();
                var segmentLenth, totalLength = 0;


                for (var i = 0; i < locs.length - 1; i++) {

                    console.log(locs[i]);


                }

            }
        }

    }
    angular.element(document).ready(function () {
        getMap('myMapGioFence');
        loadClusterModule($scope.loadLocations, $scope.saveLocation);
    });

    $scope.btnGetDataClick = function () {
        $http({
            url: 'http://localhost:5279/GeoFenceData/GetGeoFenceData',
            method: "GET",
            params: { 'someId': 1234 }
        })
        .then(function (response) {
            alert('success ' + response.data.data);
        },
        function (err) {
            alert('get error');
        });
    }

    $scope.btnPostDataClick = function () {
        var x = $scope.NewShapes;

        var postData = { Name: 'name 1', Id: 100, City: 'Hyd' };
        $http({
            url: 'http://localhost:5279/GeoFenceData/saveGeoFenceData',
            method: "POST",
            data: { 'postdata': postData }
        })
        .then(function (response) {
            alert('success ' + response.data.data);
        },
        function (err) {
            alert('get error');
        });
    }
});