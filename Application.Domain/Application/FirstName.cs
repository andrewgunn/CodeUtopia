using System;

namespace Application.Domain.Application
{
    public class FirstName
    {
        public FirstName(string firstName)
        {
            if (firstName == null)
            {
                throw new ArgumentNullException("firstName");
            }

            _firstName = string.Format("{0}{1}", char.ToUpper(firstName[0]), firstName.Substring(1));
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