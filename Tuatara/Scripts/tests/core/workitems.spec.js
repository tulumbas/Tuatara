'use strict';

describe('core services:', function () {

    var projectData = [
        { id: 1, name: 'All Products', parentID: null },
        { id: 2, name: 'All Projects', parentID: null },
        { id: 3, name: 'Financial-Clarity', parentID: 1 },
    ];

    var $httpBackend, workItems;

    // Add a custom equality tester before 
    // each test for Equals comparator to exclude angular's own stuff
    beforeEach(function () {
        jasmine.addCustomEqualityTester(angular.equals);
    });

    // load services
    beforeEach(module('tuatara.core'));

    // Instantiate the service and "train" `$httpBackend` before each test
    beforeEach(inject(function ($injector) {
        $httpBackend = $injector.get('$httpBackend');
        $httpBackend.when('GET', '/api/projects').respond(projectData);
        $httpBackend.when('GET', '/api/projects/3').respond(projectData[2]);
        workItems = $injector.get('core.workItems');
    }));

    // Verify that there are no outstanding expectations or requests after each test
    afterEach(function () {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });

    describe('workItems', function () {
        it('should fetch 3 projects from query', function () {

            var rows = workItems.query();
            expect(rows).toEqual([]);

            $httpBackend.flush();
            expect(rows).toEqual(projectData);
        });

        it('should get details for 1 project', function () {
            var data = workItems.get({ id: 3 });
            $httpBackend.flush();
            expect(data).toEqual(projectData[2]);
        });
    });
});