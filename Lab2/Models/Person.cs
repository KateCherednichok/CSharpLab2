using Lab2.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab2.Models
{
    class Person
    {
        #region Fields
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _birthDate;

        private bool _isAdult;
        private string _sunSign;
        private string _chineseSign;
        private bool _isBirthday;
        private const string EmailPattern = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
        private const int MaxAge = 135;
        #endregion

        #region Properties

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (value > DateTime.Today)
                    throw new InvalidDateInFutureException(value);

                if (ZodiacUtils.CalculateAge(value) > MaxAge)
                    throw new InvalidDateInPastException(value);

                _birthDate = value;

                _isAdult = ZodiacUtils.CalculateAge(_birthDate) >= 18;
                _sunSign = ZodiacUtils.GetWesternZodiacSign(_birthDate);
                _chineseSign = ZodiacUtils.GetChineseZodiacSign(_birthDate);
                _isBirthday = ZodiacUtils.IfItIsBirthdayToday(_birthDate);
            }
        }

        public bool IsAdult
        {
            get { return _isAdult; }
        }

        public string SunSign
        {
            get { return _sunSign; }
        }

        public string ChineseSign
        {
            get { return _chineseSign; }
        }

        public bool IsBirthday
        {
            get { return _isBirthday; }
        }

        #endregion

        [JsonConstructor]
        public Person(string firstName, string lastName, string email, DateTime birthDate)
        {
            if (birthDate > DateTime.Today)
                throw new InvalidDateInFutureException(birthDate);

            if (ZodiacUtils.CalculateAge(birthDate) > MaxAge)
                throw new InvalidDateInPastException(birthDate);

            if (!IsValidEmail(email))
                throw new InvalidEmailException(email);

            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _birthDate = birthDate;
            _isAdult = ZodiacUtils.CalculateAge(_birthDate) >= 18;
            _sunSign = ZodiacUtils.GetWesternZodiacSign(_birthDate);
            _chineseSign = ZodiacUtils.GetChineseZodiacSign(_birthDate);
            _isBirthday = ZodiacUtils.IfItIsBirthdayToday(_birthDate);
        }

        public Person(string firstName, string lastName, string email)
        : this(firstName, lastName, email, DateTime.Today) { }

        public Person(string firstName, string lastName, DateTime birthDate)
            : this(firstName, lastName, string.Empty, birthDate) { }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, EmailPattern);
        }
    }
}
