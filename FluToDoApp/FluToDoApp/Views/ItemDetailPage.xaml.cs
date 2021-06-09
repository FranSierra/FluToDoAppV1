using FluToDoApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace FluToDoApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}