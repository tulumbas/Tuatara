// Operations: 
//
// 1. Load
// 1.1. Component has loading as true
// 1.2. Requests from service playbook with weekShift passed in $routeParams. 
// 1.3. Once populated, loading is false
// 1.4. Has rows, matching testData
// 2. Sorting
// 2.1. Initially has sortedby = ['intraweekSort', 'owner']
// 2.2. Initially has sortZA is false
// 2.3. sortBy('duration') changes sortedby to ['duration', 'intraweekSort', 'owner']
// 2.4. sortBy('statusSort') changes sortedBy to ['statusSort', 'duration', 'intraweekSort'] 
// 2.5. sortBy('intraweekSort') changes sortdBy to ['intraweekSort', 'statusSort', 'duration']
// 2.6. 2x sortBy('intraweekSort') sets sortZA to true
// 3. current row
// 3.1. sets current row, isCurrentRow should return true for the row
// 3.2. cssRowClass for current row should be priority + highlighted-row
// 4. refresh - reloads the data
// 5. totals ? separatecomponent
// 6. new recordswizard 
// 6.1. call createNew
// -7. openroweditor
// -7.1. openRowEditor is calling uibModal.open with { resolve : { row: rowdata }}
// -7.2. when dialog is resolved, should refresh the view
// 8. Row deletion - applied to set of selected rows
// 8.1. DeleteRows with 1 row as argument calls for confirmation with question "Are you sure you want to delete " + description
// 8.2. Select 3 rows.  deleteRows with null calls for confirmation with quesion "Are you sure you want to delete 3 rows?"
// 8.3. DeleteRows call pb Service deleteRows with array of row ids
// 10. Bulk edit of selected rows (Complete/Cancel/Reschedule/Move) ?
// 11. week pick
// 12. filtering

describe('pb-list component', function () {

    'use strict';

    const listComponent = 'pbList', serviceBaseURL = '/api/playbook/';
    let testRawData, testData, bindings, userConfirmation, $httpBackend;

    function getC($componentController) {
        let c = $componentController(listComponent, bindings);
        $httpBackend.expectGET(serviceBaseURL + 'get?weekShift=' + bindings.$routeParams.weekShift).respond(testRawData);
        c.$onInit();
        //bindings.playbookService.__resolve(testData);
        $httpBackend.flush();
        return c;
    }

    beforeEach(module('playbook'));

    //
    // setup of all tests in a file
    // - create/mock up dependencies
    // - set/reset global vars so they're ready for each test
    // - make sure the environment is reset for next test
    beforeEach(inject(function ($injector, $log) {
        // Add a custom equality tester before 
        // each test for Equals comparator to exclude angular's own stuff
        jasmine.addCustomEqualityTester(angular.equals);

        // restore source data
        testRawData = {
            weekID: 20180101,
            weekNo: 1,
            rows: [{
                id: 1,
                description: "Task 1",
                duration: 0.5,
                resourceID: 1, resourceName: "QA",
                whatID: 21, whatName: "Zurich",
                priorityID: 1, priorityName: "Red", prioritySort: 1,
                statusID: 1, statusName: "Booked", statusSort: 1,
                intraweekSort: 1, intraweekID: 1, intraweekName: "Mon",
                requestorName: "Jo"
            }, {
                id: 2,
                description: "Task 2",
                duration: 1.5,
                resourceID: 2, resourceName: "Dev",
                whatID: 22, whatName: "Aviva",
                priorityID: 2, priorityName: "Amber", prioritySort: 2,
                statusID: 3, statusName: "In Progress", statusSort: 3,
                intraweekSort: 2, intraweekID: 2, intraweekName: "Tue",
                requestorName: "Quinn"
            }, {
                id: 3,
                description: "Task 3",
                duration: 2.5,
                resourceID: 4, resourceName: "Qv dev",
                whatID: 23, whatName: "Financial-Clarity",
                priorityID: 3, priorityName: "BAU", prioritySort: 3,
                statusID: 4, statusName: "Cancelled", statusSort: 4,
                intraweekSort: 3, intraweekID: 3, intraweekName: "Wed",
                requestorName: "Sofia"
            }]
        };

        //// restore test data
        testData = new Tuatara.Playbook(testRawData);


        // TODO: rewrite mockup
        userConfirmation = new Tuatara.mocks.userConfirmation(true);

        $httpBackend = $injector.get('$httpBackend');

        // injectable vars for controller
        bindings = {
            $scope: {},
            $log: $log, // mock the $log to intercept messages from controller
            $routeParams: { weekShift: -1 },
            //playbookService: new Tuatara.mocks.playbookServiceMock(jasmine, $injector),
            $uibModal: new Tuatara.mocks.uibModal(jasmine, $injector),
            userConfirmation: userConfirmation.service
        };
    }));

    // check for errors in $log and verify that all http expectations are met
    afterEach(inject(function ($log) {
        if ($log.error.logs.length) {
            console.log('Controller errors: ');
            console.log(angular.mock.dump($log.error.logs));
        }

        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();

    }));

    describe('PB service', function () {
        it('getPlaybookForWeek calls the backend and returns typed model', inject(function ($injector) {
            let args = arguments;
            let service = $injector.get('playbookService');
            $httpBackend.expectGET(serviceBaseURL + 'get?weekShift=' + bindings.$routeParams.weekShift).respond(testRawData);
            service.getPlaybookForWeek(bindings.$routeParams.weekShift).then(function (data) {
                expect(data.currentWeek).toEqual(testData.currentWeek);
                expect(data.rows).toEqual(data.rows);
            }, function (error) {
                fail('error returned from backend');
            });
            $httpBackend.flush();

        }));
    });

    describe('1. Load', function () {
        it('1.1. Has "isLoading" as true. Requests from service playbook with weekShift passed in $routeParams. Once populated, loading is false. Has rows, matching service.',
            inject(function ($componentController) {
                let c = $componentController(listComponent, bindings);
                $httpBackend.expectGET(serviceBaseURL + 'get?weekShift=' + bindings.$routeParams.weekShift).respond(testRawData);
                c.$onInit();
                //expect(bindings.playbookService.getPlaybookForWeek).toHaveBeenCalledWith(bindings.$routeParams.weekShift);
                expect(c.isLoading).toBeTruthy('isLoading still should be true');

                $httpBackend.flush();
                //bindings.playbookService.__resolve(testData);
                expect(c.currentWeek).toBeDefined('current week is undef');
                expect(c.currentWeek).not.toBeNull('current week is null');
                expect(c.currentWeek.id).toEqual(testData.currentWeek.id, 'current week is null');
                expect(c.rows).toEqual(testData.rows, 'controller rows do not match with service');
                expect(c.isLoading).toBeFalsy('once populated, isLoading must be false');
            }));
    });

    // 2. Sorting
    // 2.1. Initially has sortedby = ['intraweekSort', 'owner']
    // 2.2. Initially has sortZA is false
    // 2.3. sortBy('duration') changes sortedby to ['duration', 'intraweekSort', 'owner']
    // 2.4. sortBy('statusSort') changes sortedBy to ['statusSort', 'duration', 'intraweekSort'] 
    // 2.5. sortBy('intraweekSort') changes sortdBy to ['intraweekSort', 'statusSort', 'duration']
    // 2.6. 2x sortBy('intraweekSort') sets sortZA to true
    describe('2. Sorting', function () {
        it('2.1.-2.2. Initially has sortedby = ["intraweekSort", "owner"]', inject(function ($componentController) {
            let c = $componentController(listComponent, bindings);
            expect(c.sortedBy).toEqual(['intraweekSort', 'owner'], 'initial sorting is wrong');
            expect(c.sortedZA).toEqual(false, 'initial sorting order is wrong');
        }));

        it('2.3.-2.6. Change sort order by calling sortBy', inject(function ($componentController) {
            let c = $componentController(listComponent, bindings);
            c.sortBy('duration');
            expect(c.sortedBy).toEqual(['duration', 'intraweekSort', 'owner'], 'sorting 2.3. is wrong');
            c.sortBy('statusSort');
            expect(c.sortedBy).toEqual(['statusSort', 'duration', 'intraweekSort'], 'sorting 2.4. is wrong');
            c.sortBy('intraweekSort');
            expect(c.sortedBy).toEqual(['intraweekSort', 'statusSort', 'duration'], 'sorting 2.5. is wrong');
            expect(c.sortedZA).toEqual(false, 'initial sorting order is wrong');
            c.sortBy('intraweekSort');
            expect(c.sortedBy).toEqual(['intraweekSort', 'statusSort', 'duration'], 'sorting 2.6. is wrong');
            expect(c.sortedZA).toEqual(true, 'change of order is wrong');
        }));

    });

    // 3. current row
    // 3.1. sets current row, isCurrentRow should return true for the row
    // 3.2. cssRowClass for current row should be priority + highlighted-row
    describe('3. Current row', function () {
        it('3.1. sets current row, isCurrentRow should return true for the row', inject(function ($componentController) {
            let c = getC($componentController);

            // loaded
            expect(c.isCurrentRow(testData.rows[0])).toBeFalsy();
            c.setCurrentRow(testData.rows[0]);
            expect(c.isCurrentRow(testData.rows[0])).toBeTruthy();
        }));
        it('3.2. cssRowClass for current row should be priority + highlighted-row', inject(function ($componentController) {
            let c = getC($componentController);

            let row = testData.rows[0];
            c.setCurrentRow(row);
            expect(c.cssRowClass(row)).toEqual('prio-' + row.priorityName.toLowerCase() + ' highlighted-row');
            row = testData.rows[1];
            expect(c.cssRowClass(row)).toEqual('prio-' + row.priorityName.toLowerCase());
        }));
    });

    // 4. refresh - reloads the data
    describe('4. Refresh should reload the data', function () {

        it('reload', inject(function ($componentController) {
            let c = getC($componentController);

            expect(c.currentWeek).toBeDefined('current week is undef');
            expect(c.currentWeek).not.toBeNull('current week is null');
            expect(c.currentWeek.id).toEqual(testData.currentWeek.id, 'current week is null');
            expect(c.rows).toEqual(testData.rows, 'controller rows do not match with service');
            expect(c.isLoading).toBeFalsy('once populated, isLoading must be false');

            testRawData.rows.push(testRawData.rows[0]);
            $httpBackend.expectGET(serviceBaseURL + 'get?weekShift=' + bindings.$routeParams.weekShift).respond(testRawData);
            c.refresh();
            //expect(bindings.playbookService.getPlaybookForWeek).toHaveBeenCalledWith(bindings.$routeParams.weekShift);


            testData.rows.push(testData.rows[0]);
            // bindings.playbookService.__resolve(testData);
            $httpBackend.flush();
            expect(c.currentWeek).toBeDefined('current week is undef');
            expect(c.currentWeek).not.toBeNull('current week is null');
            expect(c.currentWeek.id).toEqual(testData.currentWeek.id, 'current week is null');
            expect(c.rows).toEqual(testData.rows, 'controller rows do not match with service');
            expect(c.isLoading).toBeFalsy('once populated, isLoading must be false');
        }));
    });

    describe('6. Create new records', function () {
        it('6.1. Calls createNew and starts pbWizard with week parameter. On the end, refreshes the view',  inject(function ($componentController) {
            // setup component
            let c = getC($componentController);

            // now service is called for the 1st time
            // expect(bindings.playbookService.getPlaybookForWeek.calls.count()).toEqual(1);
            // call createNew
            c.createNew();
            // check the dialog was created with wizard component and week as a parameter
            expect(bindings.$uibModal.open).toHaveBeenCalled();
            expect(bindings.$uibModal.__getOpenArgs()).toContain(jasmine.objectContaining({ component: 'pbWizard', resolve: { week: c.currentWeek } }));
            // check the list was refreshed after fialog was opened
            // close the dialog
            $httpBackend.expectGET(serviceBaseURL + 'get?weekShift=' + bindings.$routeParams.weekShift).respond(testRawData);
            bindings.$uibModal.__resolve(true);
            $httpBackend.flush();
            // check the service was called (2nd time)
            //expect(bindings.playbookService.getPlaybookForWeek.calls.count()).toEqual(2);
        }));
    });

    // 8. Row deletion - applied to set of selected rows
    // 8.1. DeleteRows with 1 row as argument calls for confirmation with question "Are you sure you want to delete " + description
    // 8.2. Select 3 rows.  deleteRows with null calls for confirmation with quesion "Are you sure you want to delete 3 rows?"
    // 8.3. DeleteRows call pb Service deleteRows with array of row ids
    describe('Row deletion - applied to set of selected rows', function () {
        it('DeleteRows with 1 row as argument calls for confirmation with question ' +
            '"Are you sure you want to delete " + description', inject(function ($componentController) {

                let c = getC($componentController);
                let data1 = testData.rows[0];

                $httpBackend.expectDELETE(serviceBaseURL + data1.id);
                $httpBackend.expectGET(serviceBaseURL + 'get?weekShift=' + bindings.$routeParams.weekShift).respond(testRawData);

                c.deleteRows([data1]);
                expect(userConfirmation.__getQuestion()).toEqual("Are you sure you want to delete '" + data1.description + "'");

                $httpBackend.flush();
                //expect(bindings.playbookService.deleteRows).toHaveBeenCalledWith([data1.id]);
                
        }));

    });

    xdescribe('10. Filtering in controller', function () {
        it('10.1. search string filters records by full text search',  inject(function ($componentController) {
        }));

        it('10.2. search string ')
    });

});

