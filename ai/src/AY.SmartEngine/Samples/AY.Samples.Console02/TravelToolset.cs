using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AY.Samples.Console02
{
    record WeatherReport(string City, int TemperatureCelsius, bool WillRain);

    internal class TravelToolset
    {
        [Description("查询指定城市的实时天气")]
        public WeatherReport QueryWeather([Description("城市名称")] string city)
        {
            int temperature = Random.Shared.Next(-5, 36);
            bool willRain = Random.Shared.NextDouble() > 0.6;
            return new WeatherReport(city, temperature, willRain);
        }

        [Description("根据天气提供穿搭建议")]
        public string SuggestOutfit(string city)
        {
            var weather = QueryWeather(city);
            return weather switch
            {
                { WillRain: true } => $"{city} 可能会下雨，建议携带雨具并穿防水外套。",
                { TemperatureCelsius: < 5 } => $"{city} 温度 {weather.TemperatureCelsius}℃，请穿冬装并注意保暖。",
                { TemperatureCelsius: > 28 } => $"{city} 今天很热（{weather.TemperatureCelsius}℃），可以选择短袖和透气面料。",
                _ => $"{city} 气温 {weather.TemperatureCelsius}℃，穿上舒适的日常装束即可。"
            };
        }
    }
}
