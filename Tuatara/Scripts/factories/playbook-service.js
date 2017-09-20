/* Operations: 
 - get wee
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

    Tuatara.playbookWeek = function (jsonData) {
        this.id = jsonData.id;
        if (jsonData.id) {
            var dt = jsonData.id.toString();
            this.baseDate = new Date(dt.slice(0, 4), dt.slice(4, 6) - 1, dt.slice(6, 8));
            this.baseDateText = this.baseDate.toLocaleDateString();
        } else {
            this.baseDate = null;
        }
        this.weekNo = jsonData.WeekNo;
    }

    Tuatara.playbookService = function ($http) {
        this.getWeekFromCurrent = function (weekShift) {
            var request = $http.get('/api/calendar/getweek?shift=' + weekShift.toString());
            var pipelined = request.then(function (payload) {
                return new Tuatara.playbookWeek(payload.data);
            });
            return pipelined;
        }

        this.getPlaybookForWeek = function (weekShift) {
            return $http.get('/api/playbook/get?weekShift=' + weekShift.toString());
        }
    }

})();