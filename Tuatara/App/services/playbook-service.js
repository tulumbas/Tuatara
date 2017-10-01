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

    Tuatara.playbookService = function ($http, $log, $uibModal) {
        this.getProjects = getProjects;
        this.getWeekFromCurrent = getWeekFromCurrent;
        this.getPlaybookForWeek = getPlaybookForWeek;
        this.openRowEditor = openRowEditor;
        this.saveChanges = saveChanges;

        function getWeekFromCurrent(weekShift) {
            var request = $http.get('/api/calendar/getweek?shift=' + weekShift.toString());
            var pipelined = request.then(function (payload) {
                return new Tuatara.PlaybookWeek(payload.data);
            });
            return pipelined;
        }

        function getPlaybookForWeek(weekShift) {
            return $http
                .get('/api/playbook/get?weekShift=' + weekShift.toString())
                .then(function (payload) {
                    return new Tuatara.Playbook(payload.data);
                })
            ;
        }

        function openRowEditor(rowData) {
            var editor = $uibModal.open({
                animation: false,
                component: 'playbookRowEditor',
                size: 'lg',
                resolve: { rowData: rowData }
            });

            editor.result.then(function (status) {
                $log.debug('row editor status ' + status);
            }, function () {
                $log.info('modal-component dismissed at: ' + new Date());
            });
        }

        function saveChanges(rowDto) {
            
        }

        function getProjects(search) {
            return $http.get('/api/project/findByName?name=' + search)
                .then(function(payload){
                    return payload.data;
                }, function(e){
                    $log.error(e);
                });
            
        }

    }

})();