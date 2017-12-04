// Operations: 
//
// 1. Load
// 1.1. Has loading as true
// 1.2. Requests from service playbook with weekShift passed in $routeParams. 
// 1.3. Once populated, loading is false
// 1.4. Has rows, matching service
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
// -8. Delete row
// 8. bulk processing
// 9. week pick
// 10. filtering

describe('pb-list component', function () {

    'use strict';

    const listComponent = 'pbList';
    let testData;

    let bindings, modal;

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

        // restore test data
        testData = new Tuatara.Playbook({
            weekID: 20180101,
            WeekNo: 1,
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
        });

        // playbook service mockup        
        let $rootScope = $injector.get('$rootScope'); 
        let serviceResult = $injector.get("$q").defer();
        let playbookServiceMock = {
            getPlaybookForWeek: function (weekShift) { return serviceResult.promise },
            __resolve: function (result) {
                serviceResult.resolve(result);
                $rootScope.$apply(); // needed to push $q promise chain
            }
        }
        spyOn(playbookServiceMock, "getPlaybookForWeek").and.returnValue(serviceResult.promise);

        // uibModal
        modal = new Tuatara.mocks.uibModal(jasmine, $injector);

        // injectable vars for controller
        bindings = {
            $scope: {},
            $log: $log, // mock the $log to intercept messages from controller
            playbookService: playbookServiceMock,
            $routeParams: { weekShift: -1 },
            $uibModal: modal.uib
        };
    }));

    // check for errors in $log and verify that all http expectations are met
    afterEach(inject(function ($log) {
        if ($log.error.logs.length) {
            console.log('Controller errors: ');
            console.log(angular.mock.dump($log.error.logs));
        }
    }));

    describe('1. Load', function () {

        it('1.1. Has "isLoading" as true. Requests from service playbook with weekShift passed in $routeParams. Once populated, loading is false. Has rows, matching service.',
            inject(function ($componentController) {
                let c = $componentController(listComponent, bindings);
                // initially
                expect(c.isLoading).toBeTruthy('isLoading must be truthy on start');

                // call on init
                c.$onInit();
                expect(bindings.playbookService.getPlaybookForWeek).toHaveBeenCalledWith(bindings.$routeParams.weekShift);
                expect(c.isLoading).toBeTruthy('isLoading still should be true');

                bindings.playbookService.__resolve(testData);
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
    describe('3. current row', function () {
        it('3.1. sets current row, isCurrentRow should return true for the row', inject(function ($componentController) {
            let c = $componentController(listComponent, bindings);
            c.$onInit();
            bindings.playbookService.__resolve(testData);

            // loaded
            expect(c.isCurrentRow(testData.rows[0])).toBeFalsy();
            c.setCurrentRow(testData.rows[0]);
            expect(c.isCurrentRow(testData.rows[0])).toBeTruthy();
        }));
        it('3.2. cssRowClass for current row should be priority + highlighted-row', inject(function ($componentController) {
            let c = $componentController(listComponent, bindings);
            c.$onInit();
            bindings.playbookService.__resolve(testData);
            var row = testData.rows[0];
            c.setCurrentRow(row);
            expect(c.cssRowClass(row)).toEqual('prio-' + row.priorityName.toLowerCase() + ' highlighted-row');
            var row = testData.rows[1];
            expect(c.cssRowClass(row)).toEqual('prio-' + row.priorityName.toLowerCase());
        }));
    });

    // 4. refresh - reloads the data
    describe('4. Refresh should reload the data', function () {

        it('reload', inject(function ($componentController) {
            let c = $componentController(listComponent, bindings);
            expect(c.isLoading).toBeTruthy('isLoading must be truthy on start');
            c.$onInit();
            expect(bindings.playbookService.getPlaybookForWeek).toHaveBeenCalledWith(bindings.$routeParams.weekShift);

            bindings.playbookService.__resolve(testData);
            expect(c.currentWeek).toBeDefined('current week is undef');
            expect(c.currentWeek).not.toBeNull('current week is null');
            expect(c.currentWeek.id).toEqual(testData.currentWeek.id, 'current week is null');
            expect(c.rows).toEqual(testData.rows, 'controller rows do not match with service');
            expect(c.isLoading).toBeFalsy('once populated, isLoading must be false');

            c.refresh();
            expect(bindings.playbookService.getPlaybookForWeek).toHaveBeenCalledWith(bindings.$routeParams.weekShift);

            testData.rows.push(testData[0]);
            bindings.playbookService.__resolve(testData);
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
            let c = $componentController(listComponent, bindings);
            c.$onInit();
            // now service is called for the 1st time
            expect(bindings.playbookService.getPlaybookForWeek.calls.count()).toEqual(1);
            bindings.playbookService.__resolve(testData);

            // call createNew
            c.createNew();
            // check the dialog was created with wizard component and week as a parameter
            expect(modal.openSpy).toHaveBeenCalled();
            expect(modal.getOpenArgs()).toContain(jasmine.objectContaining({ component: 'pbWizard', resolve: { week: c.currentWeek } }));
            // check the list was refreshed after fialog was opened
            // close the dialog
            modal.resolve(true);
            // check the service was called (2nd time)
            expect(bindings.playbookService.getPlaybookForWeek.calls.count()).toEqual(2);
        }));
    });

    xdescribe('10. Filtering in controller', function () {
        it('10.1. search string filters records by full text search',  inject(function ($componentController) {
        }));

        it('10.2. search string ')
    });

});

