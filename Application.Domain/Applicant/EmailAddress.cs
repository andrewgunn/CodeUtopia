using System;

namespace Application.Domain.Applicant
{
    public class EmailAddress
    {
        public EmailAddress(string emailAddress)
        {
            if (emailAddress == null)
            {
                throw new ArgumentNullException("emailAddress");
            }

            _emailAddress = emailAddress;
        }

        public static implicit operator string(EmailAddress emailAddress)
        {
            return emailAddress._emailAddress;
        }

        public static implicit operator EmailAddress(string emailAddress)
        {
            return new EmailAddress(emailAddress);
        }

        private readonly string _emailAddress;
    }
}