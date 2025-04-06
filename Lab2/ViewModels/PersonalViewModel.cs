using Lab2.Models;
using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using Lab2.Exceptions;

namespace Lab2.ViewModels
{
    class PersonViewModel : INotifyPropertyChanged
    {
        #region Fields
        private const int MaxAge = 135;
        private string _firstNameInput;
        private string _lastNameInput;
        private string _emailInput;
        private DateTime _birthdayDateInput = DateTime.Today;

        private string _firstName;
        private string _lastName;
        private string _email;
        private string _birthdayDate;
        private string _isAdult;
        private string _sunSign;
        private string _chineseSign;
        private string _isBirthday;
        private bool _isEnabled = true;
        #endregion

        #region Properties
        public string FirstNameInput
        {
            get => _firstNameInput;
            set
            {
                _firstNameInput = value;
                OnPropertyChanged(nameof(FirstNameInput));
                UpdateButtonState();
            }
        }

        public string LastNameInput
        {
            get => _lastNameInput;
            set
            {
                _lastNameInput = value;
                OnPropertyChanged(nameof(LastNameInput));
                UpdateButtonState();
            }
        }

        public string EmailInput
        {
            get => _emailInput;
            set
            {
                _emailInput = value;
                OnPropertyChanged(nameof(EmailInput));
                UpdateButtonState();
            }
        }

        public DateTime BirthdayDateInput
        {
            get => _birthdayDateInput;
            set
            {
                _birthdayDateInput = value;
                OnPropertyChanged(nameof(BirthdayDateInput));
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            private set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
                UpdateButtonState();
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));              
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string BirthdayDate
        {
            get => _birthdayDate;
            set
            {
                _birthdayDate = value;
                OnPropertyChanged(nameof(BirthdayDate));
            }
        }

        public string IsAdult
        {
            get => _isAdult;
            private set
            {
                _isAdult = value;
                OnPropertyChanged(nameof(IsAdult));
            }
        }

        public string SunSign
        {
            get => _sunSign;
            private set
            {
                _sunSign = value;
                OnPropertyChanged(nameof(SunSign));
            }
        }

        public string ChineseSign
        {
            get => _chineseSign;
            private set
            {
                _chineseSign = value;
                OnPropertyChanged(nameof(ChineseSign));
            }
        }

        public string IsBirthday
        {
            get => _isBirthday;
            private set
            {
                _isBirthday = value;
                OnPropertyChanged(nameof(IsBirthday));
            }
        }
        #endregion

        public bool CanExecuteProceed =>
            !string.IsNullOrWhiteSpace(FirstNameInput) &&
            !string.IsNullOrWhiteSpace(LastNameInput) &&
            !string.IsNullOrWhiteSpace(EmailInput) &&
            IsEnabled;

 
        public IRelayCommand CalculateCommand { get; }

        public PersonViewModel()
        {
            CalculateCommand = new AsyncRelayCommand(Calculate, () => CanExecuteProceed);
        }

        private async Task Calculate()
        {
            if (!CanExecuteProceed) return;

            IsEnabled = false;
            UpdateButtonState();
            try
            {

                await Task.Run(() =>
                {
                    var person = new Person(FirstNameInput, LastNameInput, EmailInput, BirthdayDateInput);

                    if (person.IsBirthday)
                    {

                        MessageBox.Show("Вітаємо з Днем Народження!");

                    }
                    FirstName = person.FirstName;
                    LastName = person.LastName;
                    Email = person.Email;
                    BirthdayDate = person.BirthDate.ToString("dd/MM/yyyy");
                    IsAdult = person.IsAdult ? "Так" : "Ні";
                    SunSign = person.SunSign;
                    ChineseSign = person.ChineseSign;
                    IsBirthday = person.IsBirthday ? "Так" : "Ні";
                });
            }
            catch (Exception ex) when (ex is InvalidDateInFutureException || ex is InvalidDateInPastException || ex is InvalidEmailException)
            {
                OnExeption(ex.Message);
            }
            IsEnabled = true; 
            UpdateButtonState();
        }

        private void UpdateButtonState()
        {
            OnPropertyChanged(nameof(CanExecuteProceed)); 
            (CalculateCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged(); 
        }

        private bool IsValidDate(DateTime date) =>
            ZodiacUtils.CalculateAge(date) <= MaxAge && date <= DateTime.Today;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void OnExeption(String message)
        {
            FirstName = "";
            LastName = "";
            Email = "";
            BirthdayDate = "";
            IsAdult = "";
            SunSign = "";
            ChineseSign = "";
            IsBirthday = "";
            MessageBox.Show(message);
        }
    }

}
