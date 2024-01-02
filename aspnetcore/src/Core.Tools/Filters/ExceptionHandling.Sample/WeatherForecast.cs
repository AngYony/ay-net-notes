using System;
using System.ComponentModel.DataAnnotations;

namespace ExceptionHandling.Sample
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }

   public class Student
    {
        public int Age { get; set; }
        
 
        [StringLength(3,ErrorMessage ="Name不能超过3个字")]
        public string Name { get; set; }
    }
}
