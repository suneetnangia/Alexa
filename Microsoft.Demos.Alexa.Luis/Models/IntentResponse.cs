namespace Microsoft.Demos.Alexa.Luis.Models
{
    using System.Collections.Generic;

    public class IntentResponse
    {
        public IntentResponse()
        {
            this.Entities = new List<Entity>();
        }

        public string Intent { get; set; }

        public IList<Entity> Entities { get; set; }
    }
}