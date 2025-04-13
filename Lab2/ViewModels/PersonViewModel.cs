using Lab2.Models;
using Lab2.Exceptions;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.Input;

namespace Lab2.ViewModels
{
    class PersonViewModel : INotifyPropertyChanged
    {
        private const int MaxAge = 135;

        private string _firstNameInput;
        private string _lastNameInput;
        private string _emailInput;
        private DateTime _birthdayDateInput = DateTime.Today;
        private bool _isEnabled = true;

        private readonly Action<Person> _addPersonAction;
        private readonly Person _existingPerson;

        public PersonViewModel(Action<Person> onSubmit, Person existingPerson = null)
        {
            _addPersonAction = onSubmit;
            CalculateCommand = new AsyncRelayCommand(Calculate, () => CanExecuteProceed);
            _existingPerson = existingPerson;
            if (existingPerson != null)
            {
                _firstNameInput = existingPerson.FirstName;
                _lastNameInput = existingPerson.LastName;
                _emailInput = existingPerson.Email;
                _birthdayDateInput = existingPerson.BirthDate;
            }
        }

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

        public bool CanExecuteProceed =>
            !string.IsNullOrWhiteSpace(FirstNameInput) &&
            !string.IsNullOrWhiteSpace(LastNameInput) &&
            !string.IsNullOrWhiteSpace(EmailInput) &&
            IsEnabled;

        public IRelayCommand CalculateCommand { get; }

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

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (_existingPerson != null)
                        {
                            _existingPerson.FirstName = FirstNameInput;
                            _existingPerson.LastName = LastNameInput;
                            _existingPerson.Email = EmailInput;
                            _existingPerson.BirthDate = BirthdayDateInput;

                            if (_existingPerson.IsBirthday)
                            {
                                MessageBox.Show("Вітаємо з Днем Народження!");
                            }

                            _addPersonAction?.Invoke(_existingPerson);
                        }
                        else
                        {
                            var newPerson = new Person(FirstNameInput, LastNameInput, EmailInput, BirthdayDateInput);

                            if (newPerson.IsBirthday)
                            {
                                MessageBox.Show("Вітаємо з Днем Народження!");
                            }

                            _addPersonAction?.Invoke(newPerson);
                        }


                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window.DataContext == this)
                            {
                                window.Close();
                                break;
                            }
                        }
                    });
                });
            }
            catch (Exception ex) when (ex is InvalidDateInFutureException || ex is InvalidDateInPastException || ex is InvalidEmailException)
            {
                ShowError(ex.Message);
            }

            IsEnabled = true;
            UpdateButtonState();
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void UpdateButtonState()
        {
            OnPropertyChanged(nameof(CanExecuteProceed));
            (CalculateCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
