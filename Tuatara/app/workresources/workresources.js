
(function () {
    'use strict';
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    Tuatara.resourcesController = function (workresources, $uibModal, $log) {
        var _this = this;
        this.rows = [];
        this.openRowEditor = openRowEditor;
        this.deleteRecord = deleteRecord;
        this.isLoading = true;

        this.$onInit = function () {
            refresh();
        }

        function refresh() {
            _this.rows = workresources.query(function () { _this.isLoading = false; }, function (error) { $log.error(error); });
        }

        function deleteRecord(project) {
            if (confirm("Are you sure?")) {
                var workItem = new workresources(project);
                workItem.$remove(function () {
                    refresh();
                });
            }
        }

        function openRowEditor(rowData) {
            var editor = $uibModal.open({
                animation: false,
                controller: ['core.workitems', '$log', '$scope', '$uibModalInstance', Tuatara.editorwiRowEditorController],
                resolve: { id: rowData.id },
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
})();