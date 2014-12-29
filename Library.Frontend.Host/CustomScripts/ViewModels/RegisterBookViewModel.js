/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../../scripts/typings/signalr/signalr.d.ts" />
var RegisterBookViewModel = (function () {
    function RegisterBookViewModel(bookHub) {
        this.bookHub = bookHub;
        this.title = ko.observable('');
    }
    RegisterBookViewModel.prototype.registerBook = function () {
        this.bookHub.server.registerBook(this.title());
        this.title('');
    };
    return RegisterBookViewModel;
})();
//# sourceMappingURL=RegisterBookViewModel.js.map