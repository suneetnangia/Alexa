namespace Microsoft.Demos.Alexa.Luis.Contracts
{
    using System.Threading.Tasks;
    using Microsoft.Demos.Alexa.Luis.Models;    

    public interface IIntentFinder
    {
        Task<IntentResponse> GetIntentAsync(string phrase);
    }
}