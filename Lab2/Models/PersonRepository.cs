using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab2.Models
{
    class PersonRepository
    {
        private static readonly string FileStorage = "allPersons.json";

        private static readonly string[] Names = {
            "Андрій", "Анна", "Антон", "Артем", "Арсен",
            "Богдан", "Валерій", "Василь", "Вікторія", "Віталій",
            "Владислав", "Ганна", "Григорій", "Дарина", "Дмитро",
            "Євген", "Іван", "Ігор", "Інна", "Ірина",
            "Катерина", "Кирило", "Ксенія", "Лідія", "Людмила",
            "Максим", "Марина", "Марія", "Микола", "Мирослав",
            "Назар", "Наталія", "Ніна", "Оксана", "Олег",
            "Олена", "Ольга", "Петро", "Роман", "Руслан",
            "Світлана", "Сергій", "Софія", "Степан", "Тарас",
            "Тетяна", "Юлія", "Юрій", "Ярослав", "Зоряна"
        };

        private static readonly string[] Surnames = {
            "Антонюк", "Бойко", "Бондаренко", "Василенко", "Гаврилюк",
            "Герасименко", "Гончар", "Гриценко", "Дяченко", "Заблоцький",
            "Захарченко", "Зоряний", "Іваненко", "Іванчук", "Клименко",
            "Коваленко", "Коваль", "Ковальчук", "Козак", "Копиленко",
            "Кравченко", "Кравчук", "Кузьменко", "Кириленко", "Левченко",
            "Литвин", "Лютий", "Мельник", "Мороз", "Мартинюк",
            "Микитенко", "Николайчук", "Олійник", "Павленко", "Паламарчук",
            "Петренко", "Піддубний", "Поляшук", "Пархоменко", "Рибак",
            "Романенко", "Рябченко", "Савченко", "Семененко", "Сиворенко",
            "Стеценко", "Ткаченко", "Хоменко", "Цимбалюк", "Юрченко"
        };

        private static readonly Random _randomGanerater = new Random();

        private static readonly int _currentYear = DateTime.Now.Year;


        public static List<Person> GetDefault()
        {
            if (File.Exists(FileStorage))
            {
                return Load();
            }

            var newPeople = GeneratePersons();
            Save(newPeople);
            return newPeople;
        }

        private static List<Person> GeneratePersons()
        {
            var persons = new List<Person>();

            for (int i = 0; i < 50; i++)
            {
                var firstName = Names[i % Names.Length];
                var lastName = Surnames[i % Surnames.Length];
                var email = $"person{i}@gmail.com";
                var birthDate = new DateTime(_randomGanerater.Next(_currentYear - 130, _currentYear - 1), _randomGanerater.Next(1, 13), _randomGanerater.Next(1, 28));

                try
                {
                    var person = new Person(firstName, lastName, email, birthDate);
                    persons.Add(person);
                    Console.WriteLine(person);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return persons;
        }

   
        public static void Save(List<Person> persons)
        {
            try
            {
                string json = JsonSerializer.Serialize(persons);
                File.WriteAllText(FileStorage, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не вийшло зберегти JSON: {ex.Message}");
            }
        }


        private static List<Person> Load()
        {
            try
            {
                string json = File.ReadAllText(FileStorage);
                return JsonSerializer.Deserialize<List<Person>>(json) ?? new List<Person>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не вийшло зчитати JSON: {ex.Message}");
                return new List<Person>();
            }
        }
    }


}
    
