(function () {
    'use strict';
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    Tuatara.projectsRowController = function (projectsService, errorParser, $log, $scope, $uibModalInstance) {
        var _this = this;
        this.isNew = true;
        this.ok = ok;
        this.cancel = cancel;
        this.getProjects = getProjects;
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
                projectsService.get({ id: id }, function (data) {
                    _this.isLoading = false;
                    if (!data.id) { // this means not found, because data is an instance of resource (projectService)
                        _this.isInputDisabled = true;
                        _this.error = 'Record with id ' + id + ' was not found';
                    } else {
                        _this.id = data.id;
                        _this.name = data.name;
                        _this.parent = data.parentID ? { id: data.parentID, name: data.parentName } : null;
                        _this.isInputDisabled = false;
                    }
                }, function (error) {
                    _this.isLoading = false;
                    _this.error = errorParser(error).getMessage();
                    _this.isInputDisabled = true;
                });
            } else {
                _this.isLoading = false;
                _this.isInputDisabled = false;
            }
        }

        function getProjects(search) {
            return projectsService.query({ name: search }).$promise;  // $promise needed for uib
            //return projectsService.query({ name: search }, function (data) {
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
            var workItem = new projectsService(data);
            var action = _this.isNew ? workItem.$create : workItem.$update;
            var promise = action.call(workItem,
                function () {
                    $uibModalInstance.close({ $value: { isCancel: false, data: data } });
                }, function (error) {
                    var e = errorParser(error);
                    _this.error = e.getMessage('Unsupported error while saving record');
                    $log.error('Error saving project: ' + e.getAll());
                });
        }

        function cancel() {
            $uibModalInstance.dismiss({ $value: { isCancel: true } });
        }
    }

    angular.module('projects')
        .controller('projectsRowController', ['projectsService', 'errorParser', '$log', '$scope', '$uibModalInstance', Tuatara.projectsRowController]);
})();