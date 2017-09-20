// playbook controller
/* Operations: 
 - select week (current/back/forward) ... date picker?
 - display assignments ... component?
 - display totals ... component?
 - add row ... popup?
 - edit row inline
 - delete row
 - sorting 
 - filtering by fields
 - move row to another week/reschedule all
 - confirm all
 */


(function () {
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    Tuatara.playbookController = function (playbookService, $log) {
        var _this = this;
        _this.currentWeek = null;

        function selectWeek(shift) {
            playbookService.getWeekFromCurrent(shift)
                .then(function (result) {
                    _this.currentWeek = result;                    
                }, function (errorPayload) {
                    $log.error("Cant get week", errorPayload);
                });
        }

        function loadData(shift) {
            playbookService
                .getPlaybookForWeek(shift)
                .then(function (payload) {
                    _this.currentWeek = new Tuatara.playbookWeek(payload.data.week);
                    _this.rows = payload.data.rows;                    
                }, function (payloadError) { console.log(payloadError); });
        }

        _this.$onInit = function () {
            loadData(0);
        };
    }

})();