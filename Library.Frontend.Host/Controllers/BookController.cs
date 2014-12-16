﻿using System;
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
        public BookController(ICommandSender commandSender, IQueryExecutor queryExecutor)
        {
            _commandSender = commandSender;
            _queryExecutor = queryExecutor;
        }

        public ActionResult List()
        {
            var projection = _queryExecutor.Execute(new BooksQuery());
            var model = projection.Books.Select(x => new BookModel(x.BookId, x.Title))
                                  .ToList();

            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", model);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult Register(string title)
        {
            var command = new RegisterBookCommand
                          {
                              BookId = Guid.NewGuid(),
                              Title = title
                          };

            _commandSender.Send(command);

            return RedirectToAction("List");
        }

        private readonly ICommandSender _commandSender;

        private readonly IQueryExecutor _queryExecutor;
    }
}