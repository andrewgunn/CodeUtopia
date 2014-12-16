using System.Linq;
using System.Web.Mvc;
using CodeUtopia;
using Library.Frontend.Host.Models;
using Library.Frontend.Queries;

namespace Library.Frontend.Host.Controllers
{
    public class BookController : Controller
    {
        public BookController(IQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor;
        }

        public ActionResult List()
        {
            var projection = _queryExecutor.Execute(new BooksQuery());
            var model = projection.Books.Select(x => new BookModel(x.BookId, x.Title))
                                  .ToList();

            return View(model);
        }

        private readonly IQueryExecutor _queryExecutor;
    }
}