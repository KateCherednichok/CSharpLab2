using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.ViewModels
{
    class PersonBindingViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public string ChineseSign { get; set; }
        public string WesternSign { get; set; }
        public bool IsAdult { get; set; }
        public bool IsBirthday { get; set; }


        public PersonBindingViewModel(Person person)
        {
            FirstName = person.FirstName;
            LastName = person.LastName;
            Email = person.Email;
            BirthDate = person.BirthDate.ToShortDateString();
            ChineseSign = person.ChineseSign;
            WesternSign = person.SunSign;
            IsAdult = person.IsAdult;
            IsBirthday = person.IsBirthday;
        }
    }
}
