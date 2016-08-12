using CSharp_Weather;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WeatherApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            Weather weather = new Weather("79f4bd16a8425b5ba8153d93e67a7c21", Weather.Fahrenheit);
            weather.SetLanguage("en",true);
            if(weather.GetWeather(weather.GetCountry()))
            {
                if(weather.GetWeatherIsSuccessful())
                {
                    label2.Text = weather.weatherinfo.Temperature +weather.UnitSymbol();
                    label4.Text = weather.weatherinfo.Humidity + " " + weather.weatherinfo.HumidityUnit;
                    label6.Text = weather.weatherinfo.WindSpeed;
                    label8.Text = weather.weatherinfo.Pressure + " " + weather.weatherinfo.PressureUnit;
                    label10.Text = weather.weatherinfo.CityName + "  ( " + weather.weatherinfo.CountryCode+" )";
                }
            }
            */

            
            CityLocator cityloc = new CityLocator();
            cityloc.GetGeoCoordByCityName("Michalovce");

            WeatherMET weather = new WeatherMET();
            if(weather.GetWeatherData(cityloc.Latitude,cityloc.Longtitude))
            {
                pictureBox1.Image = weather.GetIcon(0);

                label2.Text = weather.weatherinfo.Temperature + "°C";
                label4.Text = weather.weatherinfo.Humidity + " " + weather.weatherinfo.HumidityUnit;
                label6.Text = weather.weatherinfo.WindSpeed;
                label8.Text = weather.weatherinfo.Pressure + " " + weather.weatherinfo.PressureUnit;
                label10.Text = cityloc.PlaceDisplayName;
            }
            


        }


    }
}
