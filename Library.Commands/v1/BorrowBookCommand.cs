﻿using System;

namespace Library.Commands.v1
{
    [Obsolete]
    public class BorrowBookCommand
    {
        public Guid BookId { get; set; }
    }
}