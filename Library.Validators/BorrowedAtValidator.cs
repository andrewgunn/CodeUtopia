using System;
using System.Collections.Generic;
using CodeUtopia;
using CodeUtopia.Validators;

namespace Library.Validators
{
    public class BorrowedAtValidator : IValidator<DateTime>
    {
        public BorrowedAtValidator()
        {
            _defaultDateTimeProvider = new UtcNowDateTimeProvider();
            _dateTimeProvider = _defaultDateTimeProvider;
        }

        public IReadOnlyCollection<IValidationError> Validate(DateTime candidate)
        {
            var validationErrors = new List<IValidationError>();

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

        private IDateTimeProvider _dateTimeProvider;

        private readonly IDateTimeProvider _defaultDateTimeProvider;
    }
}