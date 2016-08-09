# CSharp_Weather
C# weather api

Very easy code for retrieve weather from http://openweathermap.org/
With few lines of code you can get temperature, wind, humidity, pressure...


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



Current version: 1.0.0
