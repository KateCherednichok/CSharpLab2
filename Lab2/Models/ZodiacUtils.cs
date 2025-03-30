using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Models
{
    class ZodiacUtils
    {
        private static readonly string[] ChineseSigns =
        {
            "Щур", "Бик", "Тигр", "Кролик", "Дракон", "Змія", "Кінь", "Коза", "Мавпа", "Півень", "Собака", "Свиня"
        };

        public static string GetChineseZodiacSign(DateTime birthDate)
        {
            return ChineseSigns[(birthDate.Year - 4) % 12];
        }

        public static string GetWesternZodiacSign(DateTime birthDate)
        {
            int month = birthDate.Month;
            int day = birthDate.Day;

            if ((month == 1 && day >= 20) || (month == 2 && day <= 18))
                return "Водолій";
            else if ((month == 2 && day >= 19) || (month == 3 && day <= 20))
                return "Риби";
            else if ((month == 3 && day >= 21) || (month == 4 && day <= 19))
                return "Овен";
            else if ((month == 4 && day >= 20) || (month == 5 && day <= 20))
                return "Телець";
            else if ((month == 5 && day >= 21) || (month == 6 && day <= 20))
                return "Близнюки";
            else if ((month == 6 && day >= 21) || (month == 7 && day <= 22))
                return "Рак";
            else if ((month == 7 && day >= 23) || (month == 8 && day <= 22))
                return "Лев";
            else if ((month == 8 && day >= 23) || (month == 9 && day <= 22))
                return "Діва";
            else if ((month == 9 && day >= 23) || (month == 10 && day <= 22))
                return "Терези";
            else if ((month == 10 && day >= 23) || (month == 11 && day <= 21))
                return "Скорпіон";
            else if ((month == 11 && day >= 22) || (month == 12 && day <= 21))
                return "Стрілець";
            else if ((month == 12 && day >= 22) || (month == 1 && day <= 19))
                return "Козоріг";
            else
                throw new ArgumentOutOfRangeException("Invalid birth date");
        }

        public static int CalculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age))
                age--;

            return age;
        }


        public static bool IfItIsBirthdayToday(DateTime date)
        {
            return date.Month == DateTime.Today.Month && date.Day == DateTime.Today.Day;
        }

    }
}
