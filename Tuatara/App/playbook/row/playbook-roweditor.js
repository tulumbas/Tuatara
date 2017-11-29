(function () {
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    Tuatara.playbookRowEditorController = function (playbookService, $log, $scope) {
        var _this = this;
        $scope.isNew = true;
        $scope.ok = saveData;
        $scope.cancel = cancel;
        $scope.getProjects = getProjects;

        this.$onInit = function () {
            var sourceData = _this.resolve.rowData;
            $scope.isNew = (!sourceData || isNaN(sourceData.id) || sourceData.id === 0);
            if (!$scope.isNew) {
                $scope.id = sourceData.id;
            }
            $scope.data = new Tuatara.PlaybookWeekRow(sourceData);
        }

        function saveData() {
            $scope.data.description = _this.description; // copy to initial data
            var promise = playbookService.saveChanges(data);

            promise.then(function () {
                _this.close({ $value: { isCancel: false, data: data } });
            }, function (error) {
                //otput error;
            });            
        }

        function getProjects(search) {
            return playbookService.getProjects(search).then(function (data) {
                console.debug(data);
                return data;
            });
        }

        function cancel() {
            _this.dismiss({ $value: { isCancel: true } });
        }
    }

    angular.module('playbook').component('playbookRowEditor', {
        controller: ['playBookService', '$log', '$scope', Tuatara.playbookRowEditorController],
        controllerAs: 'editor',
        templateUrl: '/app/playbook/row/roweditor.html',
        bindings: {
            resolve: '<',
            close: '&',
            dismiss: '&'
        }
    });
})();