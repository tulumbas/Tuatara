(function () {
    'use strict';
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    Tuatara.resourcesRowController = function (workItems, $log, $scope, $uibModalInstance) {
        var _this = this;
        $scope.isNew = true;
        $scope.ok = ok;
        $scope.cancel = cancel;
        $scope.getProjects = getProjects;

        this.$onInit = function () {
            var sourceData = $scope.$resolve.rowData;
            $scope.isNew = (!sourceData || isNaN(sourceData.id) || sourceData.id === 0);
            if (!$scope.isNew) {
                $scope.id = sourceData.id;
                $scope.name = sourceData.name;
                if (sourceData.parentID) {
                    workItems.get({ id: sourceData.parentID }, function (parent) {
                        $scope.parent = parent; // don't know why, but uib typeahead is nnot happy about promise, so need to resolve it
                    }, function (error) {
                        $log.error(error.data.message);
                    });
                } else {
                    $scope.parent = null;
                }
            } else {
                $scope.id = 0;
                $scope.name = null;
                $scope.parent = null;
            }
        }

        function getProjects(search) {
            return workItems.query({ name: search }).$promise; // $promise needed for uib
        }

        function ok() {
            var parentID = $scope.parent ? $scope.parent.id : null;
            var data = { id: $scope.id, name: $scope.name, parentID: parentID };
            if (!data.name || !data.name.trim().length) {
                $scope.error = 'Please enter non-empty name';
                $log.error('No project name provided');
            } else {
                $scope.error = null;
                var workItem = new workItems(data);
                var action = $scope.isNew ? workItem.$save : workItem.$update;
                var promise = action.call(workItem,
                    function () {
                        $uibModalInstance.close({ $value: { isCancel: false, data: data } });
                    }, function (error) {
                        //otput error;
                        $log.error('Error saing project: ' + (error.data ? error.data.message : 'unknown'));
                        if (error.data) {
                            var err = error.data;
                            if (err.exceptionMessage) {
                                $log.error(err.exceptionMessage);
                            }
                            if (err.exceptionType) {
                                $log.error(err.exceptionType);
                            }
                            if (err.stackTrace) {
                                $log.error(err.stackTrace);
                            }
                        }
                    });
            }
        }

        function cancel() {
            $uibModalInstance.dismiss({ $value: { isCancel: true } });
        }
    }
})();