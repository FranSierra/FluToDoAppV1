using FluToDoApp.Configuration;
using FluToDoApp.Services;
using FluToDoApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FluToDoApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<TodoApiService>();
            DependencyService.Register<IMessageService, MessageService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
