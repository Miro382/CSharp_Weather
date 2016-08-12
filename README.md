# CSharp_Weather
C# weather api

Very easy code for receive weather from http://openweathermap.org/ or from http://met.no/
with few lines of code you can get temperature, wind speed, humidity, pressure...

<h4>Open Weather Map</h4>
using CSharp_Weather;

            Weather weather = new Weather("123456789101112131415", Weather.Celsius);
            if(weather.GetWeather("London"))
            {
                if(weather.GetWeatherIsSuccessful())
                {
                    string temp = weather.weatherinfo.Temperature + weather.UnitSymbol();
                }
            }


Open Weather Map is free for commercial use.


If you want weather of your country capital city use

     if(weather.GetWeather(weather.GetCountry()))
     {
          ...  
     }


<h4>The Norwegian Meteorological Institute</h4>

using CSharp_Weather;

            CityLocator cityloc = new CityLocator();
            cityloc.GetGeoCoordByCityName("Bratislava");

            WeatherMET weather = new WeatherMET();
            if(weather.GetWeatherData(cityloc.Latitude,cityloc.Longtitude))
            {
                string temp = weather.weatherinfo.Temperature;
            }
 
 
 <br>
 MET Norway is free for commercial use if you attribute them: http://api.met.no/license_data.html           
 
 <br>
 CityLocator is for receive latitude and longtitude from city name.      
 
<b>This version only work for current weather. No forecast.<b>

Current version: 1.0.0
