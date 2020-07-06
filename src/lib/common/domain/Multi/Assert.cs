using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Common.Domain.Multi
{
    public static class Assert
    {
        public static void AreEqual(object obj1, object obj2, string message)
        {
            if (!obj1.Equals(obj2))
            {
                throw new DomainException(message);
            }
        }

        public static void AreNotEqual(object obj1, object obj2, string message)
        {
            if (obj1.Equals(obj2))
            {
                throw new DomainException(message);
            }
        }

        public static void Empty(string value, string message)
        {
            if (!string.IsNullOrEmpty(value))
            {
                throw new DomainException(message);
            }
        }

        public static void NotEmpty(string value, string message)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new DomainException(message);
            }
        }

        public static void IsMatch(string pattern, string value, string message)
        {
            var regex = new Regex(pattern);

            if (!regex.IsMatch(value))
            {
                throw new DomainException(message);
            }
        }

        public static void IsNull(object obj, string message)
        {
            if (obj != null)
            {
                throw new DomainException(message);
            }
        }

        public static void IsNotNull(object obj, string message)
        {
            if (obj == null)
            {
                throw new DomainException(message);
            }
        }

        public static void Length(string value, int length, string message)
        {
            if (value == null || value.Length != length)
            {
                throw new DomainException(message);
            }
        }

        public static void MinLength(string value, int length, string message)
        {
            if (value == null || value.Length < length)
            {
                throw new DomainException(message);
            }
        }

        public static void MaxLength(string value, int length, string message)
        {
            if (value == null || value.Length > length)
            {
                throw new DomainException(message);
            }
        }

        public static void IsEmail(string value, string message)
        {
            try
            {
                new MailAddress(value);
            }
            catch
            {
                throw new DomainException(message);
            }
        }
    }
}
