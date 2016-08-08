using CSharp_Weather;
using System;
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
            Weather weather = new Weather("1234567891011121345", Weather.Celsius);
            weather.SetLanguage("en",true);
            if(weather.GetWeather("London"))
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
        }


    }
}
