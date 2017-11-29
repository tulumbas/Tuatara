(function () {
    'use strict';
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    Tuatara.workforceRowController = function (workforceService, $log, $scope, $uibModalInstance) {
        var _this = this;
        this.isNew = true;
        this.ok = ok;
        this.cancel = cancel;
        this.getParents = getParents;
        this.isLoading = true;
        this.error = null;
        this.isInputDisabled = true;
        //this.parentSearchResult = [];

        this.$onInit = function () {
            var id = $scope.$resolve ? $scope.$resolve.id : 0;
            loadData(id);
        }

        function loadData(id) {
            _this.isLoading = true;
            _this.error = null;
            _this.id = 0;
            _this.name = null;
            _this.parent = null;
            _this.isNew = !id;

            if (!_this.isNew) {
                workforceService.get({ id: id }, function (data) {
                    _this.isLoading = false;
                    if (!data.id) { // this means not found, because data is an instance of resource (workforceervice)
                        _this.isInputDisabled = true;
                        _this.error = 'Record with id ' + id + ' was not found';
                    } else {
                        _this.id = data.id;
                        _this.name = data.name;
                        _this.parent = data.parentID ? { id: data.parentID, name: data.parentName } : null;
                        _this.isInputDisabled = false;
                    }
                }, function (error) {
                    $log.error(error.data.message);
                    _this.isLoading = false;
                    _this.error = error.data.message;
                    _this.isInputDisabled = true;
                });
            } else {
                _this.isLoading = false;
                _this.isInputDisabled = false;
            }
        }

        function getParents(search) {
            return workforceService.query({ name: search }).$promise;  // $promise needed for uib
            //return workforceService.query({ name: search }, function (data) {
            //    if(angular.isArray(data) || data === null) _this.parentSearchResult = data;
            //}, function (error) {
            //    if (error && error.data) {
            //        _this.error = error.data.message;
            //        _this.parentSearchResult = [];
            //    }
            //}); // $promise needed for uib
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
            var workItem = new workforceService(data);
            var action = _this.isNew ? workItem.$create : workItem.$update;
            var promise = action.call(workItem,
                function () {
                    $uibModalInstance.close({ $value: { isCancel: false, data: data } });
                }, function (error) {
                    //otput error;
                    _this.error = error.data ? error.data.message : (error.message ? error.message : 'Unsupported error while saving record');
                    $log.error('Error saving project: ' + _this.error);
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

    angular.module('workforce')
        .controller('workforceRowController', ['workforceService', '$log', '$scope', '$uibModalInstance', Tuatara.workforceRowController]);
})();