(function () {
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    Tuatara.playbookRowEditorController = function (playbookService, $log) {
        var _this = this;
        this.data = null;
        this.isNew = true;
        this.ok = saveData;
        this.cancel = cancel;
        this.getProjects = getProjects;


        this.$onInit = function () {
            var sourceData = _this.resolve.rowData;
            _this.isNew = (sourceData === null || isNaN(sourceData.id) || sourceData.id === 0);
            _this.data = new Tuatara.PlaybookWeekRow(sourceData);
        }

        function saveData() {
            _this.data.description = _this.description; // copy to initial data
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
})();