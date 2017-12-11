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

    Tuatara.pbListController = function (playbookService, userConfirmation, $log, $scope, $routeParams, $uibModal) {
        var _this = this;
        var currentRow = null;
        var weekShift = $routeParams.weekShift ? $routeParams.weekShift : 0;

        // sorting
        this.sortedBy = ["intraweekSort", "owner"]; // current sorting
        this.sortedZA = false; // a>z or z>a
        this.sortBy = sortBy; // sorting callback
        this.sortIndicator = sortIndicator; // helper, returning css class to show column sorting
        this.setCurrentRow = setCurrentRow;
        this.isCurrentRow = isCurrentRow;
        this.isLoading = true;
        this.rows = [];
        this.createNew = createNew;
        this.cssRowClass = cssRowClass;
        this.refresh = loadData;
        this.onRowsKeyUp = onRowsKeyUp;
        this.currentWeek = null;
        this.deleteRows = deleteRows;

        this.$onInit = function () {
            loadData(); // defined in a MVC View
        };

        function isCurrentRow(r) {
            return currentRow && (currentRow.id == r.id);
        }

        function setCurrentRow(r) {
            currentRow = r;
        }

        function onRowsKeyUp(e) {
            if (!e.altKey && !e.ctrlKey) {
                switch (e.keyCode) {
                    case 38: // up arrow
                        //if (currentRow) {

                        //} else if(_this.rows.length) {
                        //    currentRow = _this.rows[_this.rows.length - 1];
                        //}
                        console.log('Key up');
                        break; 
                    case 40: // down arrow
                        console.log('Key down');
                        break;
                    default: return;
                }
            }
        }

        function loadData() {
            console.log("Loading week: " + weekShift)
            playbookService.getPlaybookForWeek(weekShift).then(function (data) {
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
            if (_this.sortedBy[0] === columnName) {
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

        function createNew() {
            var editor = $uibModal.open({
                animation: false,
                component: 'pbWizard',
                size: 'lg',
                resolve: { week: _this.currentWeek }
            });

            editor.result.then(function (status) {
                $log.debug('row editor status ' + status);
                loadData();
            }, function () {
                $log.info('modal-component dismissed at: ' + new Date());
            });
        }

        function cssRowClass(rowData) {
            return 'prio-' + rowData.priorityName.toLowerCase() + (_this.isCurrentRow(rowData) ? ' highlighted-row' : '');
        }

        function deleteRows(rowsToDelete) {
            if (rowsToDelete.length == 1) {
                userConfirmation(function () {
                    playbookService.deleteRow(rowsToDelete[0].id)
                }, "Are you sure you want to delete '" + rowsToDelete[0].description + "'");
            } else if (rowsToDelete.length > 1) {
                userConfirmation(function () {
                    playbookService.deleteRows(rowsToDelete.map(function (r) { return r.id; }));
                }, 'Are you sure you want to delete ' + rowsToDelete.length + ' rows');
            }
            loadData();
        }
    }

    angular.module('playbook')
        .component('pbList', {
            controller: ['playbookService', 'userConfirmation', '$log', '$scope', '$routeParams', '$uibModal', Tuatara.pbListController],
            controllerAs: 'v',
            templateUrl: '/app/playbook/pb-list.html'
        });
})();