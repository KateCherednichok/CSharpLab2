using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Lab2.ViewModels
{
    class AllPersonsViewModel : INotifyPropertyChanged
    {
        private List<Person> _allPersons;
        public ObservableCollection<PersonBindingViewModel> Persons { get; set; }

        public AllPersonsViewModel()
        {
            _allPersons = PersonRepository.GetDefault();
            Persons = new ObservableCollection<PersonBindingViewModel>(_allPersons.Select(p => new PersonBindingViewModel(p)));
        }

        public void ApplyFilter(string field, string value)
        {
            var filtered = _allPersons.Where(p =>
            {
                var prop = GetPropertyValue(p, field);
                return prop != null && prop.ToLower().Contains(value.ToLower());
            }).ToList();

            Persons.Clear();
            foreach (var p in filtered)
                Persons.Add(new PersonBindingViewModel(p));
        }

        public void ResetFilter()
        {
            Persons.Clear();
            foreach (var p in _allPersons)
                Persons.Add(new PersonBindingViewModel(p));
        }

        public void AddPerson(Person person)
        {
            _allPersons.Add(person); 
            PersonRepository.Save(_allPersons);
            ResetFilter();
        }

        public void UpdatePerson(int index, Person person)
        {
            _allPersons[index] = person;
            var updatedVM = new PersonBindingViewModel(person);
            Persons[index] = updatedVM;
            PersonRepository.Save(_allPersons);
        }

        public int GetIndex(Person person)
        {
            for (int i = 0; i < _allPersons.Count; i++)
            {
                var p = _allPersons[i];
                if (p.FirstName == person.FirstName &&
                    p.LastName == person.LastName &&
                    p.Email == person.Email &&
                    p.BirthDate.Date == person.BirthDate.Date)
                {
                    return i;
                }
            }
            return -1;
        }

        public void ApplySort(string field)
        {
            IEnumerable<Person> sorted;
            if (field == "Дата народження")
            {
                sorted = _allPersons.OrderBy(p => p.BirthDate);
            }
            else
            {
                sorted = _allPersons.OrderBy(p => GetPropertyValue(p, field));
            }

            _allPersons = sorted.ToList();

            Persons.Clear();
            foreach (var p in sorted)
                Persons.Add(new PersonBindingViewModel(p));
        }

        public void DeletePerson(PersonBindingViewModel personVM)
        {
            var personToDelete = _allPersons.FirstOrDefault(p =>
                p.FirstName == personVM.FirstName &&
                p.LastName == personVM.LastName &&
                p.Email == personVM.Email &&
                p.BirthDate.ToShortDateString() == personVM.BirthDate &&
                p.ChineseSign == personVM.ChineseSign &&
                p.SunSign == personVM.WesternSign &&
                p.IsAdult == personVM.IsAdult &&
                p.IsBirthday == personVM.IsBirthday);
            if (personToDelete != null)
            {
                _allPersons.Remove(personToDelete);
                Persons.Remove(personVM);
                PersonRepository.Save(_allPersons);
            }
        }

        private string GetPropertyValue(Person person, string field)
        {
            return field switch
            {
                "Ім'я" => person.FirstName,
                "Прізвище" => person.LastName,
                "Email" => person.Email,
                "Дата народження" => person.BirthDate.ToShortDateString(),
                "Знак (Кит.)" => person.ChineseSign,
                "Знак (Зах.)" => person.SunSign,
                "Повнолітній" => person.IsAdult.ToString(),
                "Іменинник" => person.IsBirthday.ToString(),
                _ => ""
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
