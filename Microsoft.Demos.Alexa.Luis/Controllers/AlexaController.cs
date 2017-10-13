using System.Threading.Tasks;
using System.Text;

using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Demos.Alexa.Luis.Contracts;

namespace Microsoft.Demos.Alexa.Luis.Controllers
{
    [Route("api/[controller]")]
    public class AlexaController : Controller
    {
        public IIntentFinder IntentFinder { get; private set; }
        
        public AlexaController(IIntentFinder intentFinder)
        {
            this.IntentFinder = intentFinder;            
        }

        // POST api/alexa
        [HttpPost]
        public async Task<SkillResponse> Post([FromBody]SkillRequest request)
        {
            var requestType = request.GetRequestType();

            if (requestType == typeof(IntentRequest))
            {
                var intentRequest = request.Request as IntentRequest;
                var phrase = intentRequest.Intent.Slots["PhraseSlot"].Value;

                var intentResponse = await this.IntentFinder.GetIntentAsync(phrase);

                var speechText = new StringBuilder();
                speechText.AppendFormat("Your intent was {0}", intentResponse.Intent);

                if(intentResponse.Entities.Count == 0)
                {
                    speechText.AppendFormat(" and we could not find any entities in the intent.", intentResponse.Intent);
                }
                else
                {
                    speechText.AppendFormat(" and you wanted to talk about ", intentResponse.Intent);
                }

                foreach (var entity in intentResponse.Entities)
                {
                    speechText.AppendFormat("{0},", entity.Name);
                }

                var speechTextString = speechText.ToString();

                return new SkillResponse
                {
                    Version = "1.0",
                    Response = new ResponseBody
                    {                        
                        OutputSpeech = new PlainTextOutputSpeech { Text = speechTextString },
                        Card = new SimpleCard { Title = "App Card Title", Content = speechTextString },
                        ShouldEndSession = true
                    }
                };
            }

            return new SkillResponse
            {
                Version = "1.0",
                Response = new ResponseBody
                {
                    // Use SSML here to make voice more realistic.
                    OutputSpeech = new PlainTextOutputSpeech { Text = $"Hmmm, I did not quite get that. Do you want to rephrase your request or call our customer service instead, at 0345 000 0000?" },
                    Card = new SimpleCard { Title = "App Card Title", Content = "Hmmm, I did not quite get that. Do you want to rephrase your request or call our customer service instead, at 0345 000 0000?" },
                    ShouldEndSession = true
                }
            };
        }
    }
}
