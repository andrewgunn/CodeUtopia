using System;
using System.Collections.Generic;
using CodeUtopia;
using CodeUtopia.Validators;

namespace Library.Validators
{
    public class ReturnedAtValidator : IValidator<DateTime>
    {
        public ReturnedAtValidator(DateTime borrowedAt)
        {
            _borrowedAt = borrowedAt;

            _defaultDateTimeProvider = new UtcNowDateTimeProvider();
            _dateTimeProvider = _defaultDateTimeProvider;
        }

        public IReadOnlyCollection<IValidationError> Validate(DateTime candidate)
        {
            var validationErrors = new List<IValidationError>();

            if (candidate < _borrowedAt)
            {
                validationErrors.Add(new BookCannotBeReturnedItWasBeingBorrowed(_borrowedAt, candidate));
            }

            var now = DateTimeProvider.Value;

            if (candidate > now)
            {
                validationErrors.Add(new BookCannotBeReturnedInTheFuture(now, candidate));
            }

            return validationErrors;
        }

        public IDateTimeProvider DateTimeProvider
        {
            get
            {
                return _dateTimeProvider;
            }
            set
            {
                _dateTimeProvider = value ?? _defaultDateTimeProvider;
            }
        }

        private readonly DateTime _borrowedAt;

        private IDateTimeProvider _dateTimeProvider;

        private readonly IDateTimeProvider _defaultDateTimeProvider;
    }
}