using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;                   //for web request
using System.IO;                    //for stream and stream reader
using Newtonsoft.Json;              //for json objects
using System.Threading;             //for threads
using System.Diagnostics;           //for debugging
using System.Windows.Threading;     //for DispatcherTimer

namespace Wpf_WeatherUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    //public void ProcessThread(string In_data)
    //{
    //    cstm_TempCity.lbl_TopCity.Content = In_data;
    //}

    public partial class MainWindow : Window
    {
        //public delegate void DataCallBack(string weatherData);

        public MainWindow()
        {
            //Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            //Debug.AutoFlush = true;

            InitializeComponent();

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            RetrieveData();
        }

        private void RetrieveData()
        {
            BackGroundThread.get(BackGroundThread.urlWeather, (json) =>
            {
                Console.WriteLine("Json string");
                Console.WriteLine(json);
                BackGroundThread.str_requestedData = json;
                
                Console.WriteLine("Requested data");
                Console.WriteLine(BackGroundThread.str_requestedData);

                String temp = BackGroundThread.str_requestedData;
                temp = temp.Replace("[", "");
                temp = temp.Replace("]", "");

                Console.WriteLine("Deserializing json");
                Console.WriteLine(temp);

                //deserialize json and set label values
                BackGroundThread.requestedData = JsonConvert.DeserializeObject<RequestedData>(temp);
                UpdateUI();
            });
        }

        private void UpdateUI()
        {
            string city = BackGroundThread.requestedData.data.request.query;
            int index = city.IndexOf(",");
            city = city.Substring(0, index);

            //*** top grid ************************

            // left side
            cstm_TempCity.lbl_TopTempVal.Content = BackGroundThread.requestedData.data.current_condition.temp_C;
            //cstm_TempCity.lbl_TopTempUnit.Content = "Cel";
            cstm_TempCity.lbl_TopCity.Content = city;// BackGroundThread.requestedData.data.request.query;

            //right side
            Uri pictureUri = new Uri(BackGroundThread.requestedData.data.current_condition.weatherIconUrl.value);
            BitmapImage image = new BitmapImage(pictureUri);
            cstm_TempCity.img_Weather.Source = image;

            cstm_TempCity.cstm_humidSpeed.lbl_humVal.Content = BackGroundThread.requestedData.data.current_condition.humidity;
            cstm_TempCity.cstm_humidSpeed.lbl_windVal.Content = BackGroundThread.requestedData.data.current_condition.windspeedMiles;
            cstm_TempCity.cstm_humidSpeed.lbl_windUnits.Content = "MPH";

            //bottom grid
            Uri pictureUri2 = new Uri(BackGroundThread.requestedData.data.current_condition.weatherIconUrl.value);
            BitmapImage image2 = new BitmapImage(pictureUri2);
            cstm_BottomDataInfo.lbl_weatherIcon.Source = image2;
            cstm_BottomDataInfo.lbl_temp.Content = BackGroundThread.requestedData.data.current_condition.temp_C;
            cstm_BottomDataInfo.lbl_city.Content = city;// BackGroundThread.requestedData.data.request.query;
            cstm_BottomDataInfo.lbl_date.Content = BackGroundThread.requestedData.data.weather.date.ToString("MM.dd.yyyy");
            cstm_BottomDataInfo.lbl_winDir.Content = BackGroundThread.requestedData.data.current_condition.winddir16Point;

        }

        private void WeatherWindow_Initialized(object sender, EventArgs e)
        {
            RetrieveData();
        }
    }
}
