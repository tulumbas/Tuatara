describe('errorParser service', function () {

    const testMessage = "This is test error message";

    beforeEach(module('tuatara.core'));

    it('returns message for messages inside error itself', inject(function (errorParser) {        
        let testError = { message: testMessage };
        expect(errorParser(testError).getMessage()).toEqual(testMessage);
    }));

    it('returns message for webapi errors', inject(function (errorParser) {
        let testError = { data: { message: testMessage } };
        expect(errorParser(testError).getMessage()).toEqual(testMessage);
    }));

    it('returns message for exceptions', inject(function (errorParser) {
        let testError = {  exceptionMessage: testMessage };
        expect(errorParser(testError).getMessage()).toEqual(testMessage);
    }));

    it('returns full description of an error', inject(function (errorParser) {
        let testError = { message: "foo", data: { message: testMessage } };
        expect(errorParser(testError).getAll()).toContain(testMessage);
        expect(errorParser(testError).getAll()).toContain("foo");
    }));

});