using System;

namespace Application.Domain.Applicant
{
    public class FirstName
    {
        public FirstName(string firstName)
        {
            if (firstName == null)
            {
                throw new ArgumentNullException("firstName");
            }

            _firstName = firstName;
        }

        public static implicit operator string(FirstName firstName)
        {
            return firstName._firstName;
        }

        public static implicit operator FirstName(string firstName)
        {
            return new FirstName(firstName);
        }

        private readonly string _firstName;
    }
}