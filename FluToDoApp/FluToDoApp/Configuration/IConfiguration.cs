using System;
using System.Collections.Generic;
using System.Text;

namespace FluToDoApp.Configuration
{
    public interface IConfiguration
    {
        string ApiUrlBase { get; set; }
    }
}
