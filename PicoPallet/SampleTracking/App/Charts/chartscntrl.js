'use strict';
PecoApp.controller('ChartsController', ['$scope', '$uibModal', function ($scope, $uibModal) {

    $scope.pieChartInitData = [];

    var pieChartInitData = [
                { label: "Amazon", value: 55 },
                { label: "Staples", value: 15 },
                { label: "BestBuy", value: 20 },
                { label: "Walmart", value: 10 },
    ];
    $scope.pieChartInitData = pieChartInitData;

    var pie = new d3pie(document.getElementById('pie'), {
        header: {
            title: {
                text: "Online sales for year 2015",
                fontSize: 20
            }
        },
        data: {
            content: $scope.pieChartInitData
        },
        callbacks: {
            onClickSegment: function (a) {
                $scope.open(a);
                $scope.childchartheader = a.data.label;
            }
        }
    });

    //$.ajax({
    //    url: 'http://localhost:1130/Data/PieChartData',
    //    dataType: 'json',
    //    cache: false,
    //    success: function (resp) {
    //        //alert('ajax success');
    //        //console.log(data);
    //        //debugger;
    //        //var pieChartInitData = [
    //        //            { label: "Amazon", value: 55 },
    //        //            { label: "Staples", value: 15 },
    //        //            { label: "BestBuy", value: 20 },
    //        //            { label: "Walmart", value: 10 },
    //        //];
    //        $scope.pieChartInitData = resp.data;
    //        //debugger;

    //        var pie = new d3pie(document.getElementById('pie'), {
    //            header: {
    //                title: {
    //                    text: "Online sales for year 2015",
    //                    fontSize: 20
    //                }
    //            },
    //            data: {
    //                content: $scope.pieChartInitData
    //            },
    //            callbacks: {
    //                onClickSegment: function (a) {
    //                    $scope.open(a);
    //                    $scope.childchartheader = a.data.label;
    //                }
    //            }
    //        });

    //    },
    //    error: function (xhr, status, err) {
    //        debugger;
    //        alert('error:' + err);
    //    }
    //});


    //$scope.authentication = authService.authentication;
    $scope.items = ['item1', 'item2', 'item3'];
    $scope.animationsEnabled = true;

    $scope.open = function (chartData) {
        var size;
        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'myModalContent.html',
            controller: 'ModalInstanceCtrl',
            size: size,
            resolve: {
                items: function () {
                    return $scope.items;
                }
            }
        });

        modalInstance.result.then(function (selectedItem) {
            $scope.selected = selectedItem;
        }, function () {
            //$log.info('Modal dismissed at: ' + new Date());
            console.log('Modal dismissed at: ');
        });

        setTimeout(function () { loadChildChartfunc(chartData); }, 500);

    };

    function loadChildChartfunc(chartData) {

        var amchildchartdata = [
                    { label: "Pallet type 1", value: 10 },
                    { label: "Pallet type 2", value: 40 },
                    { label: "Pallet type 3", value: 25 },
                    { label: "Pallet type 4", value: 5 },
                    { label: "Pallet type 5", value: 20 }
        ];
        var stchildchartdata = [
                    { label: "Pallet type 1", value: 33 },
                    { label: "Pallet type 2", value: 45 },
                    { label: "Pallet type 3", value: 5 },
                    { label: "Pallet type 4", value: 22 }
        ];
        var bbchildchartdata = [
                    { label: "Pallet type 1", value: 15 },
                    { label: "Pallet type 2", value: 85 }
        ];
        var wmchildchartdata = [
                    { label: "Pallet type 1", value: 33 },
                    { label: "Pallet type 2", value: 34 },
                    { label: "Pallet type 3", value: 33 }
        ];

        var childchartdata = null;
        if (chartData.data.label == "Amazon") {
            childchartdata = amchildchartdata;
        }
        else if (chartData.data.label == "Staples") {
            childchartdata = stchildchartdata;
        }
        else if (chartData.data.label == "BestBuy") {
            childchartdata = bbchildchartdata;
        }
        else if (chartData.data.label == "Walmart") {
            childchartdata = wmchildchartdata;
        }

        //pie2.
        var pie123 = new d3pie(document.getElementById('pie123'), {
            header: {
                title: {
                    text: chartData.data.label + ' sales per item',
                    fontSize: 20
                }
            },
            data: {
                content: childchartdata
            }
        });
    }

    $scope.toggleAnimation = function () {
        $scope.animationsEnabled = !$scope.animationsEnabled;
    };
}]);

PecoApp.controller('ModalInstanceCtrl', function ($scope, $uibModalInstance, items) {
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
