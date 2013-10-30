using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_WeatherUI
{
    class WeatherData
    {
        public struct RequestData
        {
            public string query { set; get; }
            public string type { set; get; }
        }

        public WeatherConditions current_condition;
        public RequestData request;
        public WeatherConditions weather; 
        
    }
}
