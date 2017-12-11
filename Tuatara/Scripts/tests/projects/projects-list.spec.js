// test the project list component
// 1. Load
// 1.1. contains all test data from mock query. isLoading true > false
// 2. Delete project
// 2.1. sends DELETE with id if confirmed
// 2.2. doesn't send requests if not confirmed
// 2.3. should display an error when delete fails
// 3. Edit 
// 3.1. openRowEditor is calling uibModal.open with { resolve : { id: id }}
// 3.2. when dialog is resolved, should refresh the view

describe('projects-list component', function () {
    const testData = [
        { id: 2, name: 'Another', parentID: null, parentName: null },
        { id: 3, name: 'Project 1', parentID: 1, parentName: 'Parent' },
        { id: 1, name: 'Parent', parentID: null, parentName: null }
    ];
    const serviceUrl = '/api/projects', listComponent = 'projectsList';

    // objects reused by all tests
    let $httpBackend, bindings, $log, userConfirmation;

    // Load the module that contains the component before each test
    beforeEach(module('projects'));

    //
    // setup of all tests in a file
    // - create/mock up dependencies
    // - set/reset global vars so they're ready for each test
    // - make sure the environment is reset for next test
    beforeEach(inject(function ($injector, $log) {
        // prepare mock request of the project data
        $httpBackend = $injector.get('$httpBackend');

        // Add a custom equality tester before 
        // each test for Equals comparator to exclude angular's own stuff
        jasmine.addCustomEqualityTester(angular.equals);

        // mock up uib modal for now only open method is used, which returns 
        // a modal instance with promise result property 
        // https://angular-ui.github.io/bootstrap/
        let modal = new Tuatara.mocks.uibModal(jasmine, $injector);

        // mock the user confirmation service
        userConfirmation = new Tuatara.mocks.userConfirmation(true);

        // injectable vars for controller
        bindings = {
            $scope: {},
            userConfirmation: userConfirmation.service,
            $uibModal: modal,
            $log: $log // mock the $log to intercept messages from controller
        }
    }));

    // check for errors in $log and verify that all http expectations are met
    afterEach(inject(function ($log) {
        if ($log.error.logs.length) {
            console.log('Controller errors: ');
            console.log(angular.mock.dump($log.error.logs));
        }

        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    }))

    // Test the controller
    describe('1. Load', function () {
        it('contains all test data from mock query. isLoading true > false', inject(function ($componentController) {
            $httpBackend.expectGET(serviceUrl).respond(testData);
            var ctrl = $componentController(listComponent, bindings);

            expect(ctrl.isLoading).toBeTruthy('isLoading should be true on start');
            $httpBackend.flush();

            expect(ctrl.rows).toEqual(testData, 'component data doesn\'t match service data');
            expect(ctrl.isLoading).toBeFalsy('isLoading should be false in the end');
        }));
    });

    describe('2. Delete project', function () {
        it('2.1. sends DELETE with id if confirmed; refreshes screen on success', inject(function ($componentController) {
            // prepare to DELETE service call, no answer            
            $httpBackend.expectDELETE(serviceUrl + '/' + testData[0].id).respond(200);
            // new data, returned by refresh
            var newData = [testData[1], testData[2]];
            $httpBackend.expectGET(serviceUrl).respond(newData);
            // user agrees to delete record
            userConfirmation.answer = true;
            // delete project
            var ctrl = $componentController(listComponent, bindings);
            ctrl.deleteRecord(testData[0]);
            $httpBackend.flush();
            // it should call refresh upon successful delete
            expect(ctrl.rows).toEqual(newData, 'refreshed rows should have returned new data');
        }));

        it('2.2. doesn\'t send requests if not confirmed', inject(function ($componentController) {
            // user disagrees to delete record
            userConfirmation.answer = false;
            // try to delete project
            var ctrl = $componentController(listComponent, bindings);
            // absence of requests is checked in afterEach
            ctrl.deleteRecord(testData[0]);
        }));

        it('2.3. displays an error when delete fails; refreshes the list', inject(function ($componentController) {
            // prepare to DELETE service call, error answer
            var testError = "Test error";
            $httpBackend.expectDELETE(serviceUrl + '/' + testData[0].id).respond(400, { message: testError });
            // new data, returned by refresh
            var newData = [testData[1], testData[2]];
            $httpBackend.expectGET(serviceUrl).respond(newData);
            // user agrees to delete record
            userConfirmation.answer = true;
            // delete project
            var ctrl = $componentController(listComponent, bindings);
            expect(ctrl.error).toBeNull('initial error state is wrong');

            ctrl.deleteRecord(testData[0]);
            $httpBackend.flush();

            expect(ctrl.error).toMatch(testError, 'it should display an error if delete fails');
            // it should call refresh anyways
            expect(ctrl.rows).toEqual(newData, 'refreshed rows should have returned new data');
        }));

    });

    describe('3. Edit', function () {
        it('3.1. openRowEditor is calling uibModal.open with { resolve : { id: id }}', inject(function ($componentController) {
            var testRecord = testData[0];
            var ctrl = $componentController(listComponent, bindings);
            ctrl.openRowEditor(testRecord);

            // uib modal open method was not called
            expect(bindings.$uibModal.open).toHaveBeenCalled();
            // argsFor returns argument array for call, toContain - checks array for presence
            expect(bindings.$uibModal.__getOpenArgs()).toContain(jasmine.objectContaining({ resolve: { id: testRecord.id } }));
        }));

        it('3.2. when dialog is resolved, should refresh the view', inject(function ($componentController) {
            $httpBackend.expectGET(serviceUrl).respond(testData);
            var ctrl = $componentController(listComponent, bindings);

            // call openEditorRow with project
            ctrl.openRowEditor(testData[0]);

            // new data, returned by refresh
            var newData = [testData[1], testData[2]];
            $httpBackend.expectGET(serviceUrl).respond(newData);
            // imitate dialog closing with sme idiotic uibModal response
            bindings.$uibModal.__resolve(testData[0]);
            $httpBackend.flush();

            // it should call refresh anyways
            expect(ctrl.rows).toEqual(newData, 'refreshed rows should have returned new data');
        }));
    });
});