'use strict';
PecoApp.controller('PushpinbyZipController', ['$scope', '$http', 'mapsFactory', function ($scope, $http, mapsFactory) {
    var map = null;
    $scope.polyPoints = [];
    $scope.InitMap = function() {
        map = new Microsoft.Maps.Map(document.getElementById('myMap'),
            { credentials: 'AibtG6FrfI5ME9C8A3iBuehgTiFe1vjEz_9dZjkez4qo4OSwBcN4mv85M6DeBJ73', center: new Microsoft.Maps.Location(37.09024, -95.712891), zoom: 4 });
    }

    $scope.GetGeoFence = function () {
        $scope.GetAllPushpin();
        var response;
        $.ajax({
            url: 'http://localhost:5279/data/GetAllPolygons?zipcode=' + $scope.txtSearch,
            dataType: 'json',
            cache: false,
            success: function (resp) {
                response = resp;                                     
                var mapCenter;
                for (var i = 0; i < resp.data.length; i++)
                {
                    var poly = resp.data[i];
                  
                    var polyPoints = [];
                    for (var j = 0; j < poly.Points.length; j++) {
                        if (i == 0 && j == 0) {
                            mapCenter = new Microsoft.Maps.Location(poly.Points[j].Latitude, poly.Points[j].Longitude);                            
                        }
                        
                            polyPoints.push(new Microsoft.Maps.Location(poly.Points[j].Latitude, poly.Points[j].Longitude));                        
                    }                    
                 
                    map.entities.push(new Microsoft.Maps.Polygon(polyPoints, null));
                    map.setView({ center: mapCenter, zoom: 5 });                    
                }                
            },
            error: function (xhr, status, err) {
                //debugger;
                alert('error at GetPolygons:' + err);
            }
        });      
    }

    $scope.GetPushpinbyZip = function () {

        $.ajax({
            url: 'http://localhost:5279/data/GetPushPinDataByZip?zipcode=' + $scope.txtSearch,
            dataType: 'json',
            cache: false,
            success: function (resp) {

                for (var i = map.entities.getLength() - 1; i >= 0; i--) {
                    var pushpin = map.entities.get(i);
                    if (pushpin instanceof Microsoft.Maps.Pushpin) {
                        map.entities.removeAt(i);
                    };
                }

                for (var i = 0; i < resp.data.length; i++) {
                    var pushpin = new Microsoft.Maps.Pushpin(map.getCenter(), null);
                    map.entities.push(pushpin);
                    pushpin.setLocation(new Microsoft.Maps.Location(resp.data[i].Latitude, resp.data[i].Longitude));
                }
                map.setView({ center: new Microsoft.Maps.Location(resp.data[0].Latitude, resp.data[0].Longitude), zoom: 6 });
                
            },
            error: function (xhr, status, err) {
                //debugger;
                alert('error at GetPushPinDataByZip:' + err);
            }
        });             
    }

    $scope.GetAllPushpin = function () {

        $.ajax({
            url: 'http://localhost:5279/data/GetAllPushPinsData',
            dataType: 'json',
            cache: false,
            success: function (resp) {

                for (var i = map.entities.getLength() - 1; i >= 0; i--) {
                    var pushpin = map.entities.get(i);
                    if (pushpin instanceof Microsoft.Maps.Pushpin) {
                        map.entities.removeAt(i);
                    };
                }

                for (var i = 0; i < resp.data.length; i++) {
                    var pushpin = new Microsoft.Maps.Pushpin(map.getCenter(), null);
                    map.entities.push(pushpin);
                    pushpin.setLocation(new Microsoft.Maps.Location(resp.data[i].Latitude, resp.data[i].Longitude));
                }
                //map.setView({ center: new Microsoft.Maps.Location(resp.data[0].Latitude, resp.data[0].Longitude), zoom: 8 });
            },
            error: function (xhr, status, err) {
                //debugger;
                alert('error at GetAllPushpin:' + err);
            }
        });
    }

    //angular.element(document).ready(function () {
    //    getMap('myMap');
    //    loadClusterModule($scope.loadLocations, $scope.saveLocation);
    //});

}]);