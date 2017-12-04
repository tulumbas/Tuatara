// tests the row editor controller for workforce
// 1. Load. inputDisabled is true until load finishes. isLoading true > false
// 1.1. with correct id - loads data, isNew = false, inputDisabled == false
// 1.2. with wrong id - populates an error, inputDisabled == false
// 1.3. with 0 - creates new (empty), isNew = true, inputDisabled == false
// 2. calling getworkforce returns array of workforce
// 3. Calling OK for exisitng record
// 3.1. sends PUT on server with new data and closes the dialog ($uibModalInstance.close )
// 3.2. on save error displays the error message and not close the dialog
// 4. Calling OK for new record
// 4.1. sends POST on server and closes the dialog ($uibModalInstance.close )
// 4.2. on error save should display the error message and not close the dialog
// 5. Calling Cancel: calles dismiss

describe('workforce-row controller', function () {

    console.log("Test capturing console");

    // test data used in mocked up service calls
    var testProj = [
        { id: 2, name: 'Another', parentID: null, parentName: null },
        { id: 3, name: 'Project 1', parentID: 1, parentName: 'Parent' },
        { id: 1, name: 'Parent', parentID: null, parentName: null }
    ], testData_3 = testProj[1];

    const serviceUrl = '/api/workforce', rowController = 'workforceRowController';

    // objects reused by all tests
    var $httpBackend, bindings, $uibModalInstance, $log;

    // Load the module that contains the component before each test
    beforeEach(module('workforce'));

    // wrapper to simplify test stetup
    function loadRecordById(id, response, $controller) {
        // prepare the mock
        $httpBackend.expectGET(serviceUrl + '/' + id).respond(response); // project to be edited

        // create test scope to be populated with data by controller
        bindings.$scope = { $resolve: { id: id } };

        // resolve controller
        var ctrl = $controller(rowController, bindings, {});

        // default value for flags
        expect(ctrl.isNew).toBeTruthy('default for isNew should be truth');
        expect(ctrl.isInputDisabled).toBeTruthy('default for isInputDisabled should be truth');
        expect(ctrl.isLoading).toBeTruthy('isLoading is true on start');

        // manually call initialization
        ctrl.$onInit();
        $httpBackend.flush();

        return ctrl;
    }

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

        // since angular UI bootstrap modal is used with a controller 
        // (as opposed to component), it creates an adhoc modalInstance object
        // and injects it directly into the controller, 
        // so I need to manually mock the modal instance 
        // it's made global, so spies can be acessed inside tests
        $uibModalInstance = {
            //result: modalResultDeferred.promise,
            //opened: modalOpenedDeferred.promise,
            //closed: modalClosedDeferred.promise,
            //rendered: modalRenderDeferred.promise,
            close: function (result) { },
            dismiss: function (reason) { }
        };

        // spy on modal methods
        spyOn($uibModalInstance, 'close');
        spyOn($uibModalInstance, 'dismiss');

        // injectable vars for controller
        bindings = {
            // initially I would include api service manually - because it was a clash in names
            // but if the service name matches the injected variable name (WTF!!!!) it is resolved by injector without my help       
            $uibModalInstance: $uibModalInstance,
            // mock the $log to intercept messages from controller
            $log: $log
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
    }));

    describe('1. Load', function () {
        it('1.1. with correct id - loads data, isNew = false, inputDisabled == false',
            inject(function ($injector, $controller) {
                var testParent = testProj[2];

                // initialize controller for record 3
                var ctrl = loadRecordById(3, testData_3, $controller);

                // edit mode - isNew is false
                expect(ctrl.isNew).toBeFalsy('isNew is false for existing records');
                expect(ctrl.isInputDisabled).toBeFalsy('isInputDisabled is false for existing records');
                expect(ctrl.isLoading).toBeFalsy('isLoading is false on load');

                expect(ctrl.id).toBe(testData_3.id, 'Wrong record id');
                expect(ctrl.name).toBe(testData_3.name, 'Wrong name');
                expect(ctrl.parent.id).toBe(testData_3.parentID, 'wrong parentID');
                expect(ctrl.parent.name).toBe(testParent.name, 'wrong parentName');
            }));

        it('1.2. with wrong id - populates an error, inputDisabled == false',
            inject(function ($injector, $controller) {
                var ctrl = loadRecordById(100, null, $controller);

                // edit mode - isNew is false
                expect(ctrl.isInputDisabled).toBeTruthy('isInputDisabled is true for absent records');
                expect(ctrl.isLoading).toBeFalsy('isLoading is false on load');
                expect(ctrl.error).toBeGreaterThan('', 'error message must be non empty');
            }));

        it('1.3. with 0 - creates new (empty), isNew = true, inputDisabled == false',
              inject(function ($injector, $controller) {

                  // create test scope to be populated with data by controller
                  bindings.$scope = { $resolve: { id: 0 } };

                  // resolve controller
                  var ctrl = $controller(rowController, bindings, {});

                  // default value for flags
                  expect(ctrl.isNew).toBeTruthy('default for isNew should be truth');
                  expect(ctrl.isInputDisabled).toBeTruthy('default for isInputDisabled should be truth');
                  expect(ctrl.isLoading).toBeTruthy('isLoading is true on start');

                  // manually call initialization
                  ctrl.$onInit();

                  // edit mode - isNew is false
                  expect(ctrl.isLoading).toBeFalsy('isLoading is false on load');
                  expect(ctrl.isNew).toBeTruthy('isNew is true for new records');
                  expect(ctrl.isInputDisabled).toBeFalsy('isInputDisabled is false for new records');

                  expect(ctrl.id).toBe(0, 'Wrong record id');
                  expect(ctrl.name).toBeNull('Name must be null');
                  expect(ctrl.parent).toBeNull('Parent must be null');
              }));
    });

    describe('2. getParents', function () {
        it('calling getParents returns array of workforce',
            inject(function ($injector, $controller) {
                // initialize controller for record 3
                var ctrl = loadRecordById(3, testData_3, $controller);

                // expect a query searching for workforce containing 'a'
                $httpBackend.expectGET(serviceUrl + '?name=*a').respond(testProj);

                // try to send a keystroke to search for parent project
                // because of typeahead inability to work with reources, 
                // need to handle promises myself
                var result = ctrl.getParents('*a');
                result.then(function (data) {
                    // check results
                    expect(data).toEqual(testProj);
                })

                $httpBackend.flush();
            }));
    });

    describe('3. Calling OK for an exisitng record', function () {
        it('3.1. sends PUT on server with new data and closes the dialog', inject(function ($injector, $controller) {
            var ctrl = loadRecordById(3, testData_3, $controller);
            var newData = { id: 3, name: 'Abracadabra', parentID: '4' };
            var savedData = null;

            expect(ctrl.error).toBeNull('Initial error state is wrong');

            $httpBackend.expectPUT(serviceUrl + '/3', newData).respond(function (method, url, dataToSave) {
                savedData = angular.fromJson(dataToSave);
                return [200, dataToSave];
                // data – {string|Object} – The response body transformed with the transform functions.
                // status – {number} – HTTP status code of the response.
                // headers – {function([headerName])} – Header getter function.
                // config – {Object} – The configuration object that was used to generate the request.
                // statusText – {string} – HTTP status text of the response.
                // xhrStatus – {string} – Status of the XMLHttpRequest (complete, error, timeout or abort).
            });

            ctrl.name = newData.name;
            ctrl.parent = { id: newData.parentID, name: 'Muz Zambela' };

            // pressing ok and sending request
            ctrl.ok();
            $httpBackend.flush();

            expect(savedData).toEqual(newData, "Wrong data sent");
            expect(ctrl.error).toBeNull('Error must be null');
            expect($uibModalInstance.close).toHaveBeenCalled();
        }));


        it('3.2. on error save should display the error message and not close the dialog ', inject(function ($injector, $controller) {
            var ctrl = loadRecordById(3, testData_3, $controller);
            var testError = "Test error";
            expect(ctrl.error).toBeNull('Initial error state is wrong');

            $httpBackend.expectPUT(serviceUrl + '/3').respond(400, { message: testError });

            // pressing ok and sending request
            ctrl.ok();
            $httpBackend.flush();

            expect(ctrl.error).toMatch(testError);
            expect($uibModalInstance.close).not.toHaveBeenCalled();
        }));
    });

    describe('4. Calling OK for a new record', function () {
        it('4.1. sends POST on server and closes the dialog ', inject(function ($injector, $controller) {
            // prepare and load the controller
            var newData = { id: 0, name: 'Abracadabra', parentID: '4' };
            var savedData = null;

            bindings.$scope = { $resolve: { id: 0 } };
            var ctrl = $controller(rowController, bindings, {});
            ctrl.$onInit();

            expect(ctrl.error).toBeNull('Initial error state is wrong');

            // api works with 0 in the end
            $httpBackend.expectPOST(serviceUrl + '/0', newData).respond(function (method, url, dataToSave) {
                savedData = angular.fromJson(dataToSave);
                return [200, dataToSave];
            });

            ctrl.name = newData.name;
            ctrl.parent = { id: newData.parentID, name: 'Muz Zambela' };

            // pressing ok and sending request
            ctrl.ok();
            $httpBackend.flush();

            expect(savedData).toEqual(newData, "Wrong data sent");
            expect(ctrl.error).toBeNull('Error must be null');
            expect($uibModalInstance.close).toHaveBeenCalled();
        }));


        it('4.2. on error save should display the error message and not close the dialog ', inject(function ($injector, $controller) {
            var testError = "Test error";
            var newData = { id: 0, name: 'Abracadabra', parentID: '4' };
            bindings.$scope = { $resolve: { id: 0 } };
            var ctrl = $controller(rowController, bindings, {});
            ctrl.$onInit();
            // api works with 0 in the end
            $httpBackend.expectPOST(serviceUrl + '/0').respond(400, { message: testError });
            expect(ctrl.error).toBeNull('Initial error state is wrong');

            ctrl.name = newData.name;
            ctrl.parent = { id: newData.parentID, name: 'Muz Zambela' };
            // pressing ok and sending request
            ctrl.ok();
            $httpBackend.flush();

            expect(ctrl.error).toMatch(testError);
            expect($uibModalInstance.close).not.toHaveBeenCalled();
        }));
    });


    describe('5. Cancel', function () {
        it('Calling Cancel: calles dismiss', inject(function ($controller) {
            // initialize controller for record 3
            var ctrl = loadRecordById(3, testData_3, $controller);

            ctrl.cancel();
            expect($uibModalInstance.dismiss).toHaveBeenCalled();
        }));
    });
});