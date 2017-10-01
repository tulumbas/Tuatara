if (!String.format) {
    String.format = function (format) {
        var args = Array.prototype.slice.call(arguments, 1);
        return format.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
              ? args[number]
              : match
            ;
        });
    };
}

(function () {
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    Tuatara.PlaybookWeek = function (dto) {
        this.id = dto.weekID;
        if (this.id) {
            var dt = this.id.toString();
            this.baseDate = new Date(dt.slice(0, 4), dt.slice(4, 6) - 1, dt.slice(6, 8));
            this.baseDateText = this.baseDate.toLocaleDateString();
        } else {
            this.baseDate = null;
        }
        this.weekNo = dto.WeekNo;
    }

    Tuatara.PlaybookWeekRow = function (dto) {
        var _this = this;
        if (dto) {
            Tuatara.PlaybookWeekRow.prototype.copy(dto, _this);
        } else {
            _this.id = _this.resourceID = _this.projectID = 0;
            _this.resource = _this.description = _this.project = _this.status = _this.intraweek = null;
            _this.duration = 0.0;
        }
    }
     
    Tuatara.PlaybookWeekRow.prototype.copy = function (src, dst) {
        dst.id = src.id;
        dst.description = src.description;
        dst.duration = src.duration;

        dst.resourceID = src.resourceID;
        dst.resourceName = src.resourceName;

        dst.whatID = src.whatID;
        dst.whatName = src.whatName;

        dst.priorityID = src.priorityID;
        dst.priorityName = src.priorityName;
        dst.prioritySort = src.prioritySort;

        dst.statusID = src.statusID;
        dst.statusName = src.statusName;
        dst.statusSort = src.statusSort;

        dst.intraweek = src.intraweek;
        dst.intraweekID = src.intraweekID;
        dst.intraweekName = src.intraweekName;

        dst.requestorName = src.requestorName;
    }

    Tuatara.Playbook = function (dto) {
        this.currentWeek = new Tuatara.PlaybookWeek(dto)
        this.rows = [];
        for (var r in dto.rows) {
            this.rows.push(new Tuatara.PlaybookWeekRow(dto.rows[r]));
        }
    }

})();