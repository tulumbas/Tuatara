(function () {
    'use strict';
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    Tuatara.projectsListController = function (projectsService, userConfirmation, $uibModal, $log) {
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
            _this.rows = projectsService.query(function () {
                _this.isLoading = false;
            }, function (error) {
                _this.isLoading = false;
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
                _this.isLoading = true;
                var workItem = new projectsService(project);
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
                controller: 'projectsRowController',
                controllerAs: 'r',
                resolve: { id: proj ? proj.id : 0 },
                size: 'lg',
                templateUrl: '/app/projects/projects-row.html'
            });

            editor.result.then(function (status) {
                refresh();
            }, function () {
                $log.info('modal-component dismissed at: ' + new Date());
            });
        }
    }

    angular.module('projects')
        .component('projectsList', {
            controller: ['projectsService', 'userConfirmation', '$uibModal', '$log', Tuatara.projectsListController],
            controllerAs: 'v',
            templateUrl: '/app/projects/projects-list.html'
        })
})();