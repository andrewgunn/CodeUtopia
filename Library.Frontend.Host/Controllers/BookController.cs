using System;
using System.Linq;
using System.Web.Mvc;
using CodeUtopia;
using CodeUtopia.Messaging;
using Library.Commands;
using Library.Frontend.Host.Models;
using Library.Frontend.Queries;

namespace Library.Frontend.Host.Controllers
{
    public class BookController : Controller
    {
        public BookController(IBus bus, IQueryExecutor queryExecutor)
        {
            _bus = bus;
            _queryExecutor = queryExecutor;
        }

        //[HttpName]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public RedirectToRouteResult Borrow([Bind(Prefix = "id")] Guid bookId)
        //{
        //    var command = new BorrowBookCommand
        //                  {
        //                      BookId = bookId,
        //                  };

        //    _bus.Send(command);
        //    _bus.Commit();

        //    return RedirectToAction("List");
        //}

        public ActionResult List()
        {
            var projection = _queryExecutor.Execute(new BooksQuery());
            var model = projection.Books.Select(x => new BookModel(x.BookId, x.Title, x.IsBorrowed))
                                  .ToList();

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", model);
            }

            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public RedirectToRouteResult Register(string title)
        //{
        //    var command = new RegisterBookCommand
        //                  {
        //                      BookId = Guid.NewGuid(),
        //                      Title = title
        //                  };

        //    _bus.Send(command);
        //    _bus.Commit();

        //    return RedirectToAction("List");
        //}

        //[HttpName]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public RedirectToRouteResult Return([Bind(Prefix = "id")] Guid bookId)
        //{
        //    var command = new ReturnBookCommand
        //                  {
        //                      BookId = bookId,
        //                  };

        //    _bus.Send(command);
        //    _bus.Commit();

        //    return RedirectToAction("List");
        //}

        private readonly IBus _bus;

        private readonly IQueryExecutor _queryExecutor;
    }
}