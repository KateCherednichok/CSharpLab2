using Lab2.Models;
using Lab2.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab2.Views
{
    /// <summary>
    /// Interaction logic for AllPersonsView.xaml
    /// </summary>
    public partial class AllPersonsView : UserControl
    {
        private AllPersonsViewModel _viewModel;

        public AllPersonsView()
        {
            InitializeComponent();
            _viewModel = new AllPersonsViewModel();
            DataContext = _viewModel;
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            string filterField = (FilterComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string filterText = FilterTextBox.Text.Trim().ToLower();
            _viewModel.ApplyFilter(filterField, filterText);
        }

        private void ResetFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterTextBox.Text = "";
            _viewModel.ResetFilter();
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            string sortField = (SortComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            _viewModel.ApplySort(sortField);
        }

        private void EditPerson_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is PersonBindingViewModel selectedPersonVM)
            {
                var oldPerson = new Person(
                    selectedPersonVM.FirstName,
                    selectedPersonVM.LastName,
                    selectedPersonVM.Email,
                    DateTime.ParseExact(selectedPersonVM.BirthDate, "dd.MM.yyyy", null)
                );

                int index = _viewModel.GetIndex(oldPerson);
                if (index == -1)
                {
                    MessageBox.Show("Не вдалося знайти особу для редагування.");
                    return;
                }

                var personWindow = new PersonWindow();

                var viewModel = new PersonViewModel(editedPerson =>
                {
                    _viewModel.UpdatePerson(index, editedPerson);
                }, oldPerson);

                personWindow.DataContext = viewModel;
                personWindow.Owner = Window.GetWindow(this);
                personWindow.ShowDialog();
            }
        }

        private void DeletePerson_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is PersonBindingViewModel selectedPersonVM)
            {
                if (MessageBox.Show($"Видалити {selectedPersonVM.FirstName}?", "Підтвердження", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var personToDelete = _viewModel.Persons.FirstOrDefault(p => p.FirstName == selectedPersonVM.FirstName && p.LastName == selectedPersonVM.LastName);
                    if (personToDelete != null)
                    {
                        _viewModel.DeletePerson(personToDelete);
                    }
                }
            }
        }

        private void CreatePerson_Click(object sender, RoutedEventArgs e)
        {
            var personWindow = new PersonWindow();

            var viewModel = new PersonViewModel(person =>
            {
                _viewModel.AddPerson(person);
            });

            personWindow.DataContext = viewModel;
            personWindow.Owner = Window.GetWindow(this); 
            personWindow.ShowDialog();
        }
    }
}
