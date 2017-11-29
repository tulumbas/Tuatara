(function () {
    'use strict';
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    Tuatara.workforceListController = function (workforceService, userConfirmation, $uibModal, $log) {
        var _this = this;
        this.rows = []
        this.openRowEditor = openRowEditor;
        this.deleteRecord = deleteRecord;
        this.isLoading = true;
        this.error = null;

        refresh();

        //this.$onInit = function () {
        //    refresh();
        //}

        function refresh() {
            _this.rows = workforceService.query(function () {
                _this.isLoading = false;
            }, function (error) {
                if (error && error.data) {
                    _this.error = error.data.message;
                } else if (error) {
                    _this.error = error.message;
                }
                $log.error(error);
            });
        }

        function deleteRecord(project) {
            userConfirmation(function () {
                var workItem = new workforceService(project);
                workItem.$remove(function () {
                    refresh();
                }, function (error) {
                    if (error && error.data) {
                        _this.error = error.data.message;
                    } else if (error) {
                        _this.error = error.message;
                    }
                    $log.error(error);
                    // nevertheless, refresh the list
                    // (the record could have gone already)
                    refresh();
                });
            });
        }

        function openRowEditor(proj) {
            var editor = $uibModal.open({
                animation: false,
                controller: 'workforceRowController',
                controllerAs: 'r',
                resolve: { id: proj ? proj.id : 0 },
                size: 'lg',
                templateUrl: '/app/workforce/workforce-row.html'
            });

            editor.result.then(function (status) {
                refresh();
            }, function () {
                $log.info('modal-component dismissed at: ' + new Date());
            });
        }
    }

    angular.module('workforce')
        .component('workforceList', {
            controller: ['workforceService', 'userConfirmation', '$uibModal', '$log', Tuatara.workforceListController],
            controllerAs: 'v',
            templateUrl: '/app/workforce/workforce-list.html'
        })
})();