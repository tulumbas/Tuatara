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

    Tuatara.requestService = function ($http) {

        // get all assignments, starting or ending between calndar range
        this.getTasks = function (rangeLeft, rangeRight) {
            var query = string.Format("/api/assignment/get?fr={0}&to={1}", rangeLeft, rangeRight);
            return $http.get(query);
        }


        this.getWeekFromCurrent = function (weekShift) {
            var request = $http.get('/api/playbookapi/GetWeekFromCurrent?shift=' + weekShift.toString());
            var pipelined = request.then(function (payload) {
                return new Tuatara.playbookWeek(payload.data);
            });
            return pipelined;
        }

        this.getCurrentPlaybook = function () {
            return $http.get('/api/playbookapi/GetCurrentPlaybook');
        }
    }

})();