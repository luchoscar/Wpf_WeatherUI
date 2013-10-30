using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;           //for web request
using System.IO;            //for stream and stream reader
using Newtonsoft.Json;      //for json objects
using System.Diagnostics;   //for debugging


namespace Wpf_WeatherUI
{
    class BackGroundThread
    {
        //public static ObservableCollection<RequestedData> requestedData;
        public static RequestedData requestedData = new RequestedData();
        public static string str_requestedData;
        //public static string urlWeather = "http://api.worldweatheronline.com/free/v1/weather.ashx?q=La+Jolla&format=json&key=jpw23j37mr8n5u5wq5gw4erv";
        public static string urlWeather = "http://api.worldweatheronline.com/free/v1/weather.ashx?q=san+diego&format=json&key=jpw23j37mr8n5u5wq5gw4erv";

        public static async void get(string uri, Action<string> action)
        {
            WebClient wc = new WebClient();
            string result = await BackGroundThread.DownloadAsync(wc, uri);
            action(result);
        }

        public static Task<string> DownloadAsync(WebClient wc, string url)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            DownloadStringCompletedEventHandler completed = null;

            completed = (s, e) =>
            {
                try
                {
                    tcs.SetResult(e.Result);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex.InnerException);
                }
            };

            wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows; U; Windows NT 6.1; de; rv:1.9.2.12) Gecko/20101026 Firefox/3.6.12";
            wc.DownloadStringCompleted += completed;
            wc.DownloadStringAsync(new Uri(url));
            return tcs.Task;
        }
    }
}
