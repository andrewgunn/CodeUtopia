using System;

namespace Application.Domain.Applicant
{
    public class LastName
    {
        public LastName(string lastName)
        {
            if (lastName == null)
            {
                throw new ArgumentNullException("lastName");
            }

            _lastName = lastName;
        }

        public static implicit operator string(LastName lastName)
        {
            return lastName._lastName;
        }

        public static implicit operator LastName(string lastName)
        {
            return new LastName(lastName);
        }

        private readonly string _lastName;
    }
}