(function () {
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    Tuatara.pbRowController = function (playbookService, $log, $scope) {
        var _this = this;
        this.selectRow = function () { $scope.rowData.selected = !$scope.rowData.selected; };
        this.openRowEditor = openRowEditor;
        $scope.rowData.selected = false;

        function openRowEditor() {
            var editor = $uibModal.open({
                animation: false,
                component: 'playbookRowEditor',
                size: 'lg',
                resolve: { rowData: $scope.rowData }
            });

            editor.result.then(function (status) {
                $log.debug('row editor status ' + status);
            }, function () {
                $log.info('modal-component dismissed at: ' + new Date());
            });
        }
    }

    angular.module('playbook').directive('pbRow', function () {
        return {
            restrict: 'A',
            controller: ['playBookService', '$log', '$scope', Tuatara.playbookRowController],
            controllerAs: 'r',
            templateUrl: '/app/playbook/row/pb-row.html',
            scope: {
                row: "=",
                onUpdate: "&"
            }
        }
    });

})();