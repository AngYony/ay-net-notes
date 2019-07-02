namespace Ay.ConfigurationOptions.Frame.Sample.OptionsModel
{
    public class AyOption
    {
        public AyOption()
        {
            Option1 = "Hello Options";
        }

        public string Option1 { get; set; }
        public int Option2 { get; set; } = 333;
    }
}