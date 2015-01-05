﻿using System;

namespace Library.Frontend.ProjectionStore.Book
{
    public class BookEntity
    {
        public Guid BookId { get; set; }

        public bool IsBorrowed { get; set; }

        public string Title { get; set; }
    }
}