/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../Scripts/typings/knockout/knockout.d.ts" />
/// <reference path="../../scripts/typings/signalr/signalr.d.ts" />
class BooksViewModel {
    books: KnockoutObservableArray<BookViewModel>;

    constructor(bookHub: any) {
        var self = this;
        this.books = ko.observableArray([]);

        var booksSubscription = this.books.subscribe(onBooksChanged);

        function onBooksChanged() {
            booksSubscription.dispose();
            booksSubscription = null;

            self.books.sort((left, right) => left.title().toLowerCase() === right.title().toLowerCase() ? 0 : (left.title().toLowerCase() < right.title().toLowerCase() ? -1 : 1));

            booksSubscription = self.books.subscribe(onBooksChanged);
        }

        bookHub.client.loadBooks = (books: any) => {
            for (var i = 0; i < books.length; i++) {
                var book = books[i];

                this.books.push(new BookViewModel(bookHub, book.BookId, book.Title, book.IsBorrowed, book.ReturnBy));
            }
        };

        bookHub.client.bookBorrowed = (bookId: string, returnBy: string) => {
            var book = this.getBook(bookId);

            if (book) {
                book._isBorrowed(true);
                book.returnBy(returnBy);
            }
        };

        bookHub.on('bookRegistered', (bookId: string, title: string) => {
            this.books.push(new BookViewModel(bookHub, bookId, title, false, null));
        });

        bookHub.client.bookReturned = (bookId: string) => {
            var book = this.getBook(bookId);

            if (book) {
                book._isBorrowed(false);
                book.returnBy(null);
            }
        };
    }

    private getBook(bookId: string) {
        return ko.utils.arrayFirst(this.books(), book => book.bookId() === bookId);
    }
}