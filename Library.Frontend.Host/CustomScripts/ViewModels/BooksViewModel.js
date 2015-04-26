/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../../scripts/typings/signalr/signalr.d.ts" />
var BooksViewModel = (function () {
    function BooksViewModel(bookHub) {
        var _this = this;
        var self = this;
        this.books = ko.observableArray([]);

        var booksSubscription = this.books.subscribe(onBooksChanged);

        function onBooksChanged() {
            booksSubscription.dispose();
            booksSubscription = null;

            self.books.sort(function (left, right) {
                return left.title().toLowerCase() === right.title().toLowerCase() ? 0 : (left.title().toLowerCase() < right.title().toLowerCase() ? -1 : 1);
            });

            booksSubscription = self.books.subscribe(onBooksChanged);
        }

        bookHub.client.loadBooks = function (books) {
            for (var i = 0; i < books.length; i++) {
                var book = books[i];

                _this.books.push(new BookViewModel(bookHub, book.BookId, book.Title, book.IsBorrowed));
            }
        };

        bookHub.client.bookBorrowed = function (bookId) {
            var book = _this.getBook(bookId);

            if (book) {
                book._isBorrowed(true);
            }
        };

        bookHub.on('bookRegistered', function (bookId, title) {
            _this.books.push(new BookViewModel(bookHub, bookId, title, false));
        });

        bookHub.client.bookReturned = function (bookId) {
            var book = _this.getBook(bookId);

            if (book) {
                book._isBorrowed(false);
            }
        };
    }
    BooksViewModel.prototype.getBook = function (bookId) {
        return ko.utils.arrayFirst(this.books(), function (book) {
            return book.bookId() === bookId;
        });
    };
    return BooksViewModel;
})();
//# sourceMappingURL=BooksViewModel.js.map
