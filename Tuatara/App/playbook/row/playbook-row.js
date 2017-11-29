(function () {
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    Tuatara.playbookRowController = function (playbookService, $log, $scope) {
        var _this = this;
        this.selectRow = function () { $scope.rowData.selected = !$scope.rowData.selected; };
        this.openRowEditor = function () { playbookService.openRowEditor($scope.rowData); };
        $scope.rowData.selected = false;
    }

    angular.module('playbook').directive('playbookRow', function () {
        return {
            restrict: 'A',
            controller: ['playBookService', '$log', '$scope', Tuatara.playbookRowController],
            controllerAs: 'r',
            templateUrl: '/app/playbook/row/row.html'
        }
    });

})();