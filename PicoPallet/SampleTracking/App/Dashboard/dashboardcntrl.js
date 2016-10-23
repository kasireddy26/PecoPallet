'use strict';
PecoApp.controller('DashboardController', ['$scope', '$http', 'mapsFactory', function ($scope, $http, mapsFactory) {

    var pushPoint = null;
    $scope.polygonName;
    $scope.lstPushPoint = [];
	
	var polygonPoint = null;
	$scope.lstPolygonPoint = [];

    
	$scope.savePushPindata = function () {
        
	    if ($scope.lstPushPoint.length > 0) {
	        $http({
	            url: 'http://localhost:5279/Data/SavePushPinData',
	            method: "POST",
	            data: $scope.lstPushPoint
	        })
            .then(function (response) {
                alert('Pushpin data saved.');
            },
            function (error) {
                alert('error while saving pushpins data - ' + error);
            });
	    }
	    else if ($scope.lstPolygonPoint.length > 0) 
	    {
	        for (var i = 0; i < $scope.lstPolygonPoint.length; i++) {
	            $scope.lstPolygonPoint[i].Name = $scope.polygonName;
	        }
	        
	        $http({
	            url: 'http://localhost:5279/Data/SavePolygonData',
	            method: "POST",
	            data: $scope.lstPolygonPoint
	        })
            .then(function (response) {
                alert('Polygon data saved.');
            },
            function (error) {
                alert('error while saving pushpins data - ' + error);
            });
	    }
    }

    $scope.saveLocation = function (shape) {        
        if (shape.ShapeInfo && shape.ShapeInfo.type != DrawingTools.DrawingMode.pushpin) {
            if (shape.ShapeInfo.type == DrawingTools.DrawingMode.circle) {
                console.log(shape.ShapeInfo.radius + "" + shape.ShapeInfo.center);
            } else {
                var locs = shape.getLocations();
                var segmentLenth, totalLength = 0;              
				
				polygonPoint = null;
				polygonPoint = mapsFactory.PolygonCreate();
				var polygonpoints = [];
				
				for (var i = 0; i < locs.length - 1; i++) {                    
					polygonpoints.push({
						pid: i+1,
						Latitude: locs[i].latitude,
						Longitude: locs[i].longitude						
					});
                }
				
				polygonPoint.Points = polygonpoints;
				polygonPoint.Name = $scope.polygonName;

				var reqUrl = 'http://dev.virtualearth.net/REST/v1/Locations/' +
                     locs[0].latitude + ',' + locs[0].longitude +
                    '?key=AibtG6FrfI5ME9C8A3iBuehgTiFe1vjEz_9dZjkez4qo4OSwBcN4mv85M6DeBJ73&jsonp=JSON_CALLBACK';

				CallRestService(reqUrl, GetZipandCallback);
            }
        }
        else {
            pushPoint = null;
            pushPoint = mapsFactory.PushpinCreate();
            pushPoint.Name = $scope.polygonName;
             
            pushPoint.Latitude = shape._location.latitude;
            pushPoint.Longitude = shape._location.longitude;

            var reqUrl = 'http://dev.virtualearth.net/REST/v1/Locations/' +
                pushPoint.Latitude + ',' + pushPoint.Longitude +
                '?key=AibtG6FrfI5ME9C8A3iBuehgTiFe1vjEz_9dZjkez4qo4OSwBcN4mv85M6DeBJ73&jsonp=JSON_CALLBACK';

            CallRestService(reqUrl, GetZipCodeandAddCallback);
        }

    }

    function GetZipandCallback(result) {
        polygonPoint.Zip = result.resourceSets[0].resources[0].address.postalCode;
        $scope.lstPolygonPoint.push(polygonPoint);
    }

    function GetZipCodeandAddCallback(result) {
        pushPoint.Zip = result.resourceSets[0].resources[0].address.postalCode;
        $scope.lstPushPoint.push(pushPoint);
    }

    function CallRestService(request, callback) {
        $http.jsonp(request)
            .success(function (r) {
                callback(r);
            })
        .error(function (data, status, error, thing) {
            alert(error);
        });
    }

    angular.element(document).ready(function () {
        getMap('myMap');
        loadClusterModule($scope.loadLocations, $scope.saveLocation);
    });
	
	$scope.toolbar = false;
    $scope.hidecontrols = function () {
        if ($scope.toolbar === true) {
            $scope.toolbar = false;
        }
        else
            $scope.toolbar = true;
    }
	
    $scope.loadLocations = function () {
        console.log('here');
        // customModuleLoaded();

        //$http.get("/api/Ping/Locations")
        //     .then(function (response) {
        //         //First function handles success
        //         $scope.content = response.data;
        //         //  console.log($scope.content);
        //         var pinsdata = [];
        //         for (var i = 0; i < $scope.content.length; i++) {
        //             var temploc = $scope.content[i];
        //             var exampleDataModel = function (name, latitude, longitude) {
        //                 this.Name = name;
        //                 this.Latitude = latitude;
        //                 this.Longitude = longitude;
        //             };
        //             pinsdata.push(new exampleDataModel("Points " + i, temploc.Lat, temploc.Long));
        //         }
        //         customModuleLoaded(pinsdata);
        //     }, function (response) {

        //     });

    }


}]);