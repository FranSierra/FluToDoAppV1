using FluToDoApp.Models;
using FluToDoApp.ViewModels;
using FluToDoApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FluToDoApp.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void CheckBox_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Renderer"))
            {
                CheckBox checkBox = sender as CheckBox;
                // Resign the previous event
                checkBox.CheckedChanged -= CheckBox_CheckedChanged;

                checkBox.CheckedChanged += CheckBox_CheckedChanged;
            }
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var cb = (CheckBox)sender;
            var item = (TodoItem)cb.BindingContext;
            _viewModel.DataStore.UpdateItemAsync(item);
        }
    }
}