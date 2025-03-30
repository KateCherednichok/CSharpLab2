using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Lab2.ViewModels;

namespace Lab2.Views
{
    public partial class PersonView : UserControl
    {
        private PersonViewModel _viewModel;
        public PersonView()
        {
            InitializeComponent();
            DataContext = _viewModel = new PersonViewModel();
        }
    }
}
