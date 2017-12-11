/* Operations: 
 - get week
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
    'use strict';
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    Tuatara.playbookService = function (errorParser, $http, $log) {
        const baseUrl = '/api/playbook/';
        this.getPlaybookForWeek = getPlaybookForWeek;
        this.deleteRow = deleteRow;
        //this.openRowEditor = openRowEditor;
        //this.saveChanges = saveChanges;

        function getPlaybookForWeek(weekShift) {
            return $http
                .get(baseUrl + 'get?weekShift=' + weekShift.toString()).then(function (payload) {
                    return new Tuatara.Playbook(payload.data);
                    //return payload.data;
                }, function (error) {
                    var e = errorParser(error);
                    $log.error('Error loading playbook week ' + e.getAll());
                });
            ;
        }

        function deleteRow(id) {
            return $http.delete(baseUrl + id).catch(function (error) {
                var e = errorParser(error);
                $log.error('Error deleting row ' + id + ': ' + e.getAll());
                throw new Error(e.getAll());
            });
        }
    }

    Tuatara.PlaybookWeek = function (dto) {
        this.id = dto.weekID;
        if (this.id) {
            var dt = this.id.toString();  
            this.baseDate = new Date(dt.slice(0, 4), dt.slice(4, 6) - 1, dt.slice(6, 8));
            this.baseDateText = this.baseDate.toLocaleDateString();
        } else {
            this.baseDate = null;
        }
        this.weekNo = dto.weekNo;
    }

    Tuatara.PlaybookWeekRow = function (dto) {
        this.id = this.resourceID = this.projectID = 0;
        this.resource = this.description = this.project = this.status = this.intraweek = this.priorityName = null;
        this.duration = 0.0;
        if (dto) {
            for (var prop in this) {
                this[prop] = dto[prop];
            }
        }
    }
     
    //Tuatara.PlaybookWeekRow.prototype.copy = function (src, dst) {
    //    dst.id = src.id;
    //    dst.description = src.description;
    //    dst.duration = src.duration;
    //    dst.resourceID = src.resourceID;
    //    dst.resourceName = src.resourceName;
    //    dst.whatID = src.whatID;
    //    dst.whatName = src.whatName;
    //    dst.priorityID = src.priorityID;
    //    dst.priorityName = src.priorityName;
    //    dst.prioritySort = src.prioritySort;
    //
    //    dst.statusID = src.statusID;
    //    dst.statusName = src.statusName;
    //    dst.statusSort = src.statusSort;
    //
    //    dst.intraweek = src.intraweek;
    //    dst.intraweekID = src.intraweekID;
    //    dst.intraweekName = src.intraweekName;
    //
    //    dst.requestorName = src.requestorName;
    //}

    Tuatara.Playbook = function (dto) {
        this.currentWeek = new Tuatara.PlaybookWeek(dto)
        this.rows = [];
        for (var r in dto.rows) {
            this.rows.push(new Tuatara.PlaybookWeekRow(dto.rows[r]));
        }
    }

    angular.module('playbook').service('playbookService', ['errorParser', '$http', '$log', Tuatara.playbookService])
})();