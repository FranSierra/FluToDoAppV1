using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluToDoApp.Services
{
    public class MessageService : IMessageService
    {
        public async Task ShowAsync(string appName, string message, string btnText)
        {
            await App.Current.MainPage.DisplayAlert(appName, message, btnText);
        }
              
    }
}
