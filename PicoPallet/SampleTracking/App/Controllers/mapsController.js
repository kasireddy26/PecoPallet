'use strict';
PecoApp.controller('MapsController', ['$scope', function ($scope) {

    $scope.showNotDiv = true;
    $scope.showPushDiv = false;

    function init() {

        // this example has items declared globally. bad javascript. but keeps the example simple.

        var columnDefs = [
            { headerName: "Make", field: "make" },
            { headerName: "Model", field: "model" },
            { headerName: "Price", field: "price", sort: 'desc' }
        ];

        var rowData = [
            { make: "Toyota", model: "Celica", price: 35000 },
            { make: "Ford", model: "Mondeo", price: 32000 },
            { make: "Porsche", model: "Boxter", price: 72000 },
            { make: "Tata", model: "Nano", price: 200000 },
            { make: "Suzuki", model: "Alto", price: 350000 },
            { make: "Honda", model: "City", price: 800000 }
        ];

        var gridOptions = {
            columnDefs: columnDefs,
            rowData: rowData,
            enableSorting: true,
            rowSelection: 'single',
            onSelectionChanged: onSelectionChanged
        };

        // wait for the document to be loaded, otherwise
        // ag-Grid will not find the div in the document.
        //document.addEventListener("DOMContentLoaded", function () {
            var eGridDiv = document.querySelector('#myGrid');
            new agGrid.Grid(eGridDiv, gridOptions);
        //});

        function onSelectionChanged() {
            var selectedRows = gridOptions.api.getSelectedRows();
            var selectedRowsString = '';
            selectedRows.forEach(function (selectedRow, index) {
                if (index != 0) {
                    selectedRowsString += ', ';
                }
                selectedRowsString += selectedRow.model;
            });
            document.querySelector('#selectedRows').innerHTML = selectedRowsString;
        }

        document.querySelector('#selectedRows').innerHTML = 'Hello....123';
        //document.getElementById('selectedRows').innerHTML = 'PTL';


    }

    $scope.mapsData = [
        {
            item: 'Notification 1',
            route: 'Seattle > Tacoma > Olympia',
            point: '47.043050, -122.888117'
        },
        {
            item: 'Pallet Location',
            route: 'Atlanta',
            point: '33.792692, -84.394327'
        },
        {
            item: 'Notification 2',
            route: 'Washington > Canada > North Dakota',
            point: '57.043050, -102.888117'
        }
    ];


    $scope.clickNotification = function (item) {
        //alert('notification');
        if (item.item == 'Notification 1') {
            $scope.showNotDiv = true;
            $scope.showNotDiv2 = false;
            $scope.showPushDiv = false;
            createDirections('n1');
        } else if (item.item == 'Notification 2') {
            $scope.showNotDiv = true;
            $scope.showNotDiv2 = false;
            $scope.showPushDiv = false;
            createDirections('n2');
        } else {
            $scope.showNotDiv = false;
            $scope.showNotDiv2 = false;
            $scope.showPushDiv = true;
            addPushpinWithOptions();
        }

    }

    //$scope.clickPushpin = function () {
    //    //alert('pushpin');
    //    //$scope.showNotDiv = false;
    //    //$scope.showPushDiv = true;
    //    //addPushpinWithOptions();
    //}


    var map = null;
    var directionsManager;
    var directionsErrorEventObj;
    var directionsUpdatedEventObj;

    $scope.getMap = function () {
        map = new Microsoft.Maps.Map(document.getElementById('mapdiv1'),
            { credentials: 'AibtG6FrfI5ME9C8A3iBuehgTiFe1vjEz_9dZjkez4qo4OSwBcN4mv85M6DeBJ73' });
    }

    $scope.getMap2 = function () {
        map = new Microsoft.Maps.Map(document.getElementById('mapdiv2'),
            { credentials: 'AibtG6FrfI5ME9C8A3iBuehgTiFe1vjEz_9dZjkez4qo4OSwBcN4mv85M6DeBJ73' });
    }


    function createDirectionsManager() {
        var displayMessage;
        if (!directionsManager) {
            directionsManager = new Microsoft.Maps.Directions.DirectionsManager(map);
            displayMessage = 'Directions Module loaded\n';
            displayMessage += 'Directions Manager loaded';
        }
        //alert(displayMessage);
        directionsManager.resetDirections();
        directionsErrorEventObj = Microsoft.Maps.Events.addHandler(directionsManager, 'directionsError', function (arg) { alert(arg.message) });
        directionsUpdatedEventObj = Microsoft.Maps.Events.addHandler(directionsManager, 'directionsUpdated', function () {
            //alert('Directions updated')
        });
    }

    function createDrivingRoute(n12) {
        if (!directionsManager) { createDirectionsManager(); }
        directionsManager.resetDirections();

        // Set Route Mode to driving 
        directionsManager.setRequestOptions({ routeMode: Microsoft.Maps.Directions.RouteMode.driving });

        if (n12 == 'n2') {

            var seattleWaypoint = new Microsoft.Maps.Directions.Waypoint({ address: 'Seattle, WA' });
            directionsManager.addWaypoint(seattleWaypoint);

            var tacomaWaypoint = new Microsoft.Maps.Directions.Waypoint({ address: 'Tacoma, WA', location: new Microsoft.Maps.Location(57.255134, -122.441650) });
            directionsManager.addWaypoint(tacomaWaypoint);

            //Olympia, 47.043050, -122.888117
            var lakewoodWaypoint = new Microsoft.Maps.Directions.Waypoint({ address: 'Olympia, WA', location: new Microsoft.Maps.Location(47.043050, -102.888117) });
            directionsManager.addWaypoint(lakewoodWaypoint);
        }
        else {

            var seattleWaypoint = new Microsoft.Maps.Directions.Waypoint({ address: 'Seattle, WA' });
            directionsManager.addWaypoint(seattleWaypoint);

            var tacomaWaypoint = new Microsoft.Maps.Directions.Waypoint({ address: 'Tacoma, WA', location: new Microsoft.Maps.Location(47.255134, -122.441650) });
            directionsManager.addWaypoint(tacomaWaypoint);

            //Olympia, 47.043050, -122.888117
            var lakewoodWaypoint = new Microsoft.Maps.Directions.Waypoint({ address: 'Olympia, WA', location: new Microsoft.Maps.Location(47.043050, -122.888117) });
            directionsManager.addWaypoint(lakewoodWaypoint);
        }

        // Set the element in which the itinerary will be rendered
        //directionsManager.setRenderOptions({ itineraryContainer: document.getElementById('directionsItinerary') });
        //alert('Calculating directions...');
        directionsManager.calculateDirections();
    }

    //var n12 = null;
    function createDirections(n12) {
        //n12 = n12;
        if (!directionsManager) {
            Microsoft.Maps.loadModule('Microsoft.Maps.Directions', { callback: createDrivingRoute });
        }
        else {
            createDrivingRoute(n12);
        }
    }

    //2nd map
    var map2 = null;
    $scope.getMap2 = function () {
        map2 = new Microsoft.Maps.Map(document.getElementById('mapdiv2'),
        { credentials: 'AibtG6FrfI5ME9C8A3iBuehgTiFe1vjEz_9dZjkez4qo4OSwBcN4mv85M6DeBJ73' });
    }
    //$scope.addPushpinWithOptions = function() {
    function addPushpinWithOptions() {
        var offset = new Microsoft.Maps.Point(0, 5);
        var pushpinOptions = { icon: '/Images/pallet2.jpg', width: 30, height: 50 };
        var pushpin = new Microsoft.Maps.Pushpin(new Microsoft.Maps.Location(33.792692, -84.394327), pushpinOptions);
        map2.setView({ center: new Microsoft.Maps.Location(33.792692, -84.394327), zoom: 5 });
        map2.entities.push(pushpin);
    }

    //init();

}]);
