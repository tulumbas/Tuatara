xdescribe('editor of work items', function () {

    // Load the module that contains the component before each test
    beforeEach(module('tuatara.core'));
    beforeEach(module('editor-wi'));

    // Test the controller
    describe('editorWi component', function () {

        it('should create a model with 3 work items',
            inject(function ($componentController) {

                console.log('here');
                var ctrl = $componentController('editorWi', { $scope: {}}, {});

                console.log('here 2');
                expect(ctrl.rows.length).toBe(3);
            }));

    });
});