(function () {
    'use strict';
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    Tuatara.editorwiRowEditorController = function (workItems, $log, $scope, $uibModalInstance) {
        var _this = this;
        this.isNew = true;
        this.ok = ok;
        this.cancel = cancel;
        this.getProjects = getProjects;
        this.loading = true;
        this.error = null;

        this.$onInit = function () {
            var id = $scope.$resolve ? $scope.$resolve.id : 0;
            loadData(id);
        }

        function loadData(id) {
            _this.loading = true;
            _this.error = null;
            _this.id = 0;
            _this.name = null;
            _this.parent = null;
            _this.isNew = !id;

            if (!_this.isNew) {
                workItems.get({ id: id }, function (data) {
                    _this.id = data.id;
                    _this.name = data.name;
                    _this.parent = data.parentID ? { id: data.parentID, name: data.parentName } : null;
                    _this.loading = false;
                }, function (error) {
                    $log.error(error.data.message);
                    _this.loading = false;
                    _this.error = error.data.message;
                });
            } else {
                _this.loading = false;
            }
        }

        function getProjects(search) {
            return workItems.query({ name: search }).$promise; // $promise needed for uib
        }

        function ok() {
            var parentID = _this.parent ? _this.parent.id : null;
            var data = { id: _this.id, name: _this.name, parentID: parentID };
            if (!data.name || !data.name.trim().length) {
                _this.error = 'Please enter non-empty name';
                $log.error('No project name provided');
            } else {
                _this.error = null;
                saveAndClose(data);
            }
        }

        function saveAndClose(data) {
            var workItem = new workItems(data);
            var action = _this.isNew ? workItem.$save : workItem.$update;
            var promise = action.call(workItem,
                function () {
                    $uibModalInstance.close({ $value: { isCancel: false, data: data } });
                }, function (error) {
                    //otput error;
                    _this.error = error.data ? error.data.message : 'error saving record';
                    $log.error('Error saing project: ' + _this.error);
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

        function cancel() {
            $uibModalInstance.dismiss({ $value: { isCancel: true } });
        }
    }
})();