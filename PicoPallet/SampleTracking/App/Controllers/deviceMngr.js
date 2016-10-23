'use strict';
PecoApp.controller('DeviceMngrController', ['$scope', '$uibModal', function ($scope, $uibModal) {

        var columnDefs = [
            { headerName: "Transponder", field: "transponder", sort: 'Asc' },
            { headerName: "Gateway", field: "gateway" },
            { headerName: "Batterylevel", field: "batterylevel", width: 130 },
            { headerName: "Temperature", field: "temperature" },
            { headerName: "Lastseen", field: "lastseen" },
            { headerName: "Location", field: "locationname" }
        ];

        var gridOptions = {
            columnDefs: columnDefs,
            enableSorting: true,
            rowSelection: 'single',
            onSelectionChanged: onSelectionChanged
        };

        function onFilterChanged(value) {
            //gridOptions.api.setQuickFilter(value);
            alert(value);
        }

        //gridOptions.api.setQuickFilter(value);
        function onFilterChanged(value) {
            //gridOptions.api.setQuickFilter(value);
        }

        function onSelectionChanged() {
            var selectedRows = gridOptions.api.getSelectedRows();
            function openPopup(dmData) {
                var size;
                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: 'devmngrModalContent.html',
                    controller: 'DevMngrModalCntrl',
                    size: size,
                    resolve: {
                        items: function () {
                            return dmData;
                        }
                    }
                });

                modalInstance.result.then(function (selectedItem) {
                    $scope.selected = selectedItem;
                }, function () {
                    //$log.info('Modal dismissed at: ' + new Date());
                    console.log('Modal dismissed at: ');
                });

                //setTimeout(function () { loadChildChartfunc(chartData); }, 500);
            };
            //open popup
            openPopup(selectedRows[0]);
        }

        //function callBackDataFunction(respdata) {
        //    var gridOptions = {
        //        columnDefs: columnDefs,
        //        rowData: respdata,
        //        enableSorting: true,
        //        rowSelection: 'single',
        //        onSelectionChanged: onSelectionChanged
        //    };
        //    var eGridDiv = document.querySelector('#myGrid');
        //    new agGrid.Grid(eGridDiv, gridOptions);
        //}


        $.ajax({
            url: 'http://localhost:5279/Data/GetDeviceManagerData',
            dataType: 'json',
            cache: false,
            success: function (resp) {
                //alert(resp.data);
                //callBackDataFunction(resp.data);
                gridOptions.api.setRowData(resp.data);
            },
            error: function (xhr, status, err) {
                debugger;
                alert('error:' + err);
            }
        });

        function onFilterChanged(value) {
            gridOptions.api.setQuickFilter(value);
        }

        $scope.txtSearch = '';      
        $scope.txtchange = function() {
            gridOptions.api.setQuickFilter($scope.txtSearch);
        }

        var eGridDiv = document.querySelector('#myGrid');
        new agGrid.Grid(eGridDiv, gridOptions);

}]);


PecoApp.controller('DevMngrModalCntrl', function ($scope, $uibModalInstance, items) {
    $scope.showmap = false;
    $scope.showMap = function () {
        $scope.showmap = !$scope.showmap;
    }
    var map2 = null;
    $scope.getMap = function () {
        map2 = new Microsoft.Maps.Map(document.getElementById('mapdiv1'),
            { credentials: 'AibtG6FrfI5ME9C8A3iBuehgTiFe1vjEz_9dZjkez4qo4OSwBcN4mv85M6DeBJ73' });

        addPushpinWithOptions();
    }

    function addPushpinWithOptions() {
        var lat = $scope.items.location.split(",")[0];
        var lon = $scope.items.location.split(",")[1];

        var offset = new Microsoft.Maps.Point(0, 5);
        var pushpinOptions = { icon: '\images\clusteredpin.png', width: 30, height: 50 };
        var pushpin = new Microsoft.Maps.Pushpin(new Microsoft.Maps.Location(lat, lon), pushpinOptions);
        map2.setView({ center: new Microsoft.Maps.Location(lat, lon), zoom: 5 });
        map2.entities.push(pushpin);
    }

    $scope.items = items;
    $scope.selected = {
        item: $scope.items[0]
    };
    $scope.ok = function () {
        $uibModalInstance.close($scope.selected.item);
    };
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});
