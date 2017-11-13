//(function () {
    'use strict';
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    Tuatara.editorwiController = function (workItems, $uibModal, $log) {
        var _this = this;
        this.rows = [];
        this.openRowEditor = openRowEditor;
        this.deleteRecord = deleteRecord;
        this.isLoading = true;

        this.$onInit = function () {
            refresh();
        }

        function refresh() {
            _this.rows = workItems.query(function () { _this.isLoading = false; }, function (error) { $log.error(error); });
        }

        function deleteRecord(project) {
            if (confirm("Are you sure?")) {
                var workItem = new workItems(project);
                workItem.$remove(function () {
                    refresh();
                });
            }
        }

        function openRowEditor(proj) {
            var editor = $uibModal.open({
                animation: false,
                controller: ['core.workItems', '$log', '$scope', '$uibModalInstance', Tuatara.editorwiRowEditorController],
                controllerAs: 'r',
                resolve: { id: proj ? proj.id : 0 },
                size: 'lg',
                templateUrl: '/app/editor-wi/editor-wi-row.html'
            });

            editor.result.then(function (status) {
                $log.info('row editor closed: ' + status.$value.data.name);
                refresh();
            }, function () {
                $log.info('modal-component dismissed at: ' + new Date());
            });
        }
    }
//})();