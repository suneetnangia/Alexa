namespace Microsoft.Demos.Alexa.Luis.Models
{
    using System.Collections.Generic;

    public class LuisResponse
    {
        public string Query { get; set; }

        public LuisIntent TopScoringIntent { get; set; }

        public IList<LuisIntent> Intents { get; set; }

        public IList<LuisEntity> Entities { get; set; }
    }
}
