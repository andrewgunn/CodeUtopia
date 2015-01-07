/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../../scripts/typings/signalr/signalr.d.ts" />
var RegisterBookViewModel = (function () {
    function RegisterBookViewModel(bookHub) {
        var _this = this;
        this.bookHub = bookHub;
        this.title = ko.observable('');
        this.errorMessages = ko.observableArray([]);
        this.hasErrors = ko.computed({
            owner: this,
            read: function () {
                return _this.errorMessages().length > 0;
            }
        });
        bookHub.client.bookError = function (errorMessages) {
            _this.errorMessages(errorMessages);
        };
    }
    RegisterBookViewModel.prototype.registerBook = function () {
        this.bookHub.server.registerBook(this.title());
    };
    return RegisterBookViewModel;
})();
//# sourceMappingURL=RegisterBookViewModel.js.map