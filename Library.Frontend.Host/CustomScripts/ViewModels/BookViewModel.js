/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../../scripts/typings/signalr/signalr.d.ts" />
var BookViewModel = (function () {
    function BookViewModel(bookHub, bookId, title, isBorrowed) {
        var _this = this;
        this.bookHub = bookHub;
        this.bookId = ko.observable(bookId);
        this.title = ko.observable(title);
        this._isBorrowed = ko.observable(isBorrowed);
        this.canBorrow = ko.computed({
            owner: this,
            read: function () {
                return !_this._isBorrowed();
            }
        });
        this.canReturn = ko.computed({
            owner: this,
            read: function () {
                return _this._isBorrowed();
            }
        });
    }
    BookViewModel.prototype.borrowBook = function () {
        return this.bookHub.invoke('borrowBook', this.bookId());
    };
    BookViewModel.prototype.returnBook = function () {
        this.bookHub.invoke('returnBook', this.bookId());
    };
    return BookViewModel;
})();
//# sourceMappingURL=BookViewModel.js.map