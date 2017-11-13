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

    Tuatara.playbookController = function (playbookService, $log, $scope) {
        var _this = this;
        var currentRow = null;

        // sorting
        this.sortedBy = ["intraweek", "owner"]; // current sorting
        this.sortedZA = false; // a>z or z>a
        this.sortBy = sortBy; // sorting callback
        this.sortIndicator = sortIndicator; // helper, returning css class to show column sorting
        this.setCurrentRow = setCurrentRow;
        this.isCurrentRow = isCurrentRow;
        this.isLoading = true;
        this.openRowEditor = openNewRowEditor;
       
        this.$onInit = function () {
            loadData(defaultWeekShift); // defined in a MVC View
        };

        function isCurrentRow(r) {
            return _this.currentRow && (_this.currentRow.id == r.id);
        }

        function setCurrentRow(r) {
            _this.currentRow = r;
        }

        function loadData(shift) {
            playbookService.getPlaybookForWeek(shift).then(function (data) {
                _this.currentWeek = data.currentWeek;
                _this.rows = data.rows;
                _this.isLoading = false;
            });
        }        

        function sortIndicator(columnName) {
            if (_this.sortedBy[0] === columnName) {
                return _this.sortedZA ? "sorted za" : "sorted";
            }
            return null;
        }

        function sortBy(columnName) {
            if(_this.sortedBy[0] === columnName) {
                _this.sortedZA = !_this.sortedZA;
            } else {
                _this.sortedZA = false;
                var newSort = [columnName];
                for (var i in _this.sortedBy) {
                    if (_this.sortedBy[i] !== columnName) newSort.push(_this.sortedBy[i]);
                    if (newSort.length === 3) break;
                }
                _this.sortedBy = newSort;
            }
        }

        function openNewRowEditor () { 
            playbookService.openRowEditor(); 
        };

    }

})();