using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_WeatherUI
{
    class WeatherConditions
    {
        public struct WeatherDescriptors
        {
            public string value { set; get; }
        }

        //share variables
        public float precipMM { set; get; }
        public int weatherCode { set; get; }
        public WeatherDescriptors weatherDesc;
        public WeatherDescriptors weatherIconUrl;
        public string winddir16Point { set; get; }
        public string winddirDegree { set; get; }
        public float windspeedKmph { set; get; }
        public float windspeedMiles { set; get; }

        //current condition variables
        public float cloudcover { set; get; }
        public float humidity { set; get; }
        public string observation_time { set; get; }
        public float pressure { set; get; }
        public float temp_C { set; get; }
        public float temp_F { set; get; }
        public float visibility { set; get; }
        
        //request variables
        public DateTime date { set; get; }
        public float tempMaxC { set; get; }
        public float tempMaxF { set; get; }
        public float tempMinC { set; get; }
        public float tempMinF { set; get; }
        public string winddirection { set; get; }
    }
}
