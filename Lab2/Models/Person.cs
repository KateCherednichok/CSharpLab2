using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private readonly bool _isAdult;
        private readonly string _sunSign;
        private readonly string _chineseSign;
        private readonly bool _isBirthday;

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
            set { _birthDate = value; }
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

        public Person(string firstName, string lastName, string email, DateTime birthDate)
        {
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

    }
}
