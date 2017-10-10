namespace Microsoft.Demos.Alexa.Luis.Models
{
    public class Entity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public double Score { get; set; }
    }
}