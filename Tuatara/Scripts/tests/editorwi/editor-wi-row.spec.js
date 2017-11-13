describe('editor of work item row', function () {

    // Load the module that contains the component before each test
    beforeEach(module('editor-wi'));
    beforeEach(module('ui.bootstrap'));

    var testProj = [
        { id: 3, name: 'Project 1', parentID: 1, parentName: 'Parent' },
        { id: 1, name: 'Parent', parentID: null, parentName: null }
    ];
    

    // Test the controller
    describe('editorWi component', function () {

        it('should create a model with 3 work items',
            inject(function ($injector, $controller) {

                // prepare mmock request of the project data
                var $httpBackend = $injector.get('$httpBackend');
                $httpBackend.when('GET', '/api/projects/3').respond(testProj[0]);

                // resolve resource wrapping service - for some reason it's not found by $injector
                var workItems = $injector.get('core.workItems');

                // since angular UI bootstrap modal is used with a controller 
                // (as opposed to component), it creates an adhoc modalInstance object
                // and injects it directly into the controller, 
                // so I need to manually mock the modal instance 
                var $uibModalInstance = {
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

                // create test scope to be populated with data by controller
                var testScope = { $resolve: { id: 3}};

                // resolve controller
                var ctrl = $controller(Tuatara.editorwiRowEditorController, {
                    $scope: testScope,
                    workItems: workItems,
                    $uibModalInstance: $uibModalInstance
                }, {});

                // default value for isNew
                expect(ctrl.isNew).toBeTruthy();

                // manually call initialization
                ctrl.$onInit();
                $httpBackend.flush();

                // edit mode - isNew is false
                var data = testProj[0];
                var parent = testProj[1];
                expect(ctrl.isNew).toBeFalsy();                
                expect(ctrl.id).toBe(data.id);
                expect(ctrl.name).toBe(data.name);
                expect(ctrl.parent.id).toBe(data.parentID);
                expect(ctrl.parent.name).toBe(parent.name);


            }));

    });
});