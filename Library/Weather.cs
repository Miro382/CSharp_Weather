using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

/*
 * 
 * Miroslav Murin
 * CSharp Weather
 * http://openweathermap.org
 * Version 1.0.0
 */

namespace CSharp_Weather
{

    public class WeatherInfo
    {
        public string CityName = "", CityLon = "", CityLat = "",CityID="", CountryCode = "";
        public string Temperature = "", TemperatureMin = "", TemperatureMax="";
        public string Humidity = "",HumidityUnit="",Pressure="",PressureUnit="";
        public string WindSpeed = "", WindName = "", WindDirection = "", WindDirectionCode = "", WindDirectionName = "";
        public string Weather = "",WeatherNumber="";
    }


    public class Weather
    {
        public const string Kelvin = "",Celsius = "&units=metric", Fahrenheit = "&units=imperial";

        public string APPID = "";
        public string Format = "";
        private string lang = "";
        private bool getweather = false;

        public WeatherInfo weatherinfo = new WeatherInfo();

        public Weather(string appID)
        {
            APPID = appID;
        }

        public Weather(string appID, string UnitFormat)
        {
            APPID = appID;
            Format = UnitFormat;
        }

        public string UnitSymbol()
        {
            if (Format == "&units=imperial")
                return "F";
            else if (Format == "&units=metric")
                return "°C";
            else
                return "K";
        }


        ///<summary>
        ///return true when all is working.
        ///call only after "GetWeather" function
        ///</summary>
        public bool GetWeatherIsSuccessful()
        {
            return getweather;
        }


        ///<summary>
        ///Change language
        ///SetCustomLanguage must be true else it will return to default (English)
        ///</summary>
        public void SetLanguage(string CountryCode,bool SetCustomLanguage)
        {
            if (SetCustomLanguage)
                lang = "&lang=" + CountryCode;
            else
                lang = "";
        }



        private string GETHtml(string Url)
        {
            HttpWebRequest myRequest = (HttpWebRequest) WebRequest.Create(Url);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();

            return result;
        }




        ///<summary>
        ///Return Country English name
        ///</summary>
        public string GetCountry()
        {
                string culture = CultureInfo.CurrentCulture.EnglishName;
                return culture.Substring(culture.IndexOf('(') + 1, culture.LastIndexOf(')') - culture.IndexOf('(') - 1);
        }


        ///<summary>
        ///Get Weather by City Name. 
        ///Example1 = London  |
        ///Example2 = London,uk
        ///</summary>
        public bool GetWeather(string CityName)
        {
            try
            {
                getweather = false;
                string weather = GETHtml("http://api.openweathermap.org/data/2.5/weather?q="+CityName+"&APPID="+APPID+Format+lang+"&mode=xml");
                ParseXML(weather);
                getweather = true;

                return true;
            } catch (Exception ex)
            {
                Debug.WriteLine(""+ex);
                getweather = false;

                return false;
            }
        }


        ///<summary>
        ///Get Weather By geographic coordinates.
        ///</summary>
        public bool GetWeather(float Latitude, float Longtitude)
        {
            try
            {
                getweather = false;
                string weather = GETHtml("http://api.openweathermap.org/data/2.5/weather?lat="+Latitude+"&lon=" + Longtitude + "&APPID=" + APPID + Format + lang + "&mode=xml");
                ParseXML(weather);
                getweather = true;

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
                getweather = false;

                return false;
            }
        }


        ///<summary>
        ///Get Weather By city ID.
        ///</summary>
        public bool GetWeather(int CityID)
        {
            try
            {
                getweather = false;
                string weather = GETHtml("http://api.openweathermap.org/data/2.5/weather?id=" + CityID + "&APPID=" + APPID + Format + lang + "&mode=xml");
                ParseXML(weather);
                getweather = true;

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
                getweather = false;

                return false;
            }
        }


        private void ParseXML(string XML)
        {
            try
            {

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(XML);

                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/current/city");
                XmlNodeList nodes2 = doc.DocumentElement.SelectNodes("/current/temperature");
                XmlNodeList nodes3 = doc.DocumentElement.SelectNodes("/current/humidity");
                XmlNodeList nodes4 = doc.DocumentElement.SelectNodes("/current/pressure");
                XmlNodeList nodes5 = doc.DocumentElement.SelectNodes("/current/wind");
                XmlNodeList nodes6 = doc.DocumentElement.SelectNodes("/current/weather");

                foreach (XmlNode node in nodes)
                {
                    weatherinfo.CountryCode = node.SelectSingleNode("country").InnerText;
                    weatherinfo.CityID = node.Attributes["id"].Value;
                    weatherinfo.CityName = node.Attributes["name"].Value;
                    weatherinfo.CityLon = node.SelectSingleNode("coord").Attributes["lon"].Value;
                    weatherinfo.CityLat = node.SelectSingleNode("coord").Attributes["lat"].Value;
                }

                foreach (XmlNode node in nodes2)
                {
                    weatherinfo.Temperature = node.Attributes["value"].Value;
                    weatherinfo.TemperatureMin = node.Attributes["min"].Value;
                    weatherinfo.TemperatureMax = node.Attributes["max"].Value;
                }

                foreach (XmlNode node in nodes3)
                {
                    weatherinfo.Humidity = node.Attributes["value"].Value;
                    weatherinfo.HumidityUnit = node.Attributes["unit"].Value;
                }

                foreach (XmlNode node in nodes4)
                {
                    weatherinfo.Pressure = node.Attributes["value"].Value;
                    weatherinfo.PressureUnit = node.Attributes["unit"].Value;
                }

                foreach (XmlNode node in nodes5)
                {
                    weatherinfo.WindSpeed = node.SelectSingleNode("speed").Attributes["value"].Value;
                    weatherinfo.WindName = node.SelectSingleNode("speed").Attributes["name"].Value;
                    weatherinfo.WindDirection = node.SelectSingleNode("direction").Attributes["value"].Value;
                    weatherinfo.WindDirectionCode = node.SelectSingleNode("direction").Attributes["code"].Value;
                    weatherinfo.WindDirectionName = node.SelectSingleNode("direction").Attributes["name"].Value;
                }

                foreach (XmlNode node in nodes6)
                {
                    weatherinfo.Weather = node.Attributes["value"].Value;
                    weatherinfo.WeatherNumber = node.Attributes["number"].Value;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
            }
        }


    }
}
