using System;
using System.Collections.Generic;
using System.Text;

namespace FluToDoApp.Configuration
{
    public interface IConfiguration
    {
        string UrlBase { get; set; }
        string ApiUrl { get; set; }
    }
}
