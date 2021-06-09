using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluToDoApp.Services
{
    public interface IMessageService
    {
        Task ShowAsync(string appName, string message, string btnText);
    }
}
