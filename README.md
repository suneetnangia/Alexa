# Alexa with Azure Cognitive Services
Project demonstrates how Microsoft's cognitive service LUIS can be used to understand the intent of the users on Alexa. The model extends to other cognitive services as well. LUIS will return both intent and entities which you can potentially use to call your backend systems to return the information to the end user on Alexa.

Give it a spin here-

<a href="https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fsuneetnangia%2FAlexa%2Fmaster%2FMicrosoft.Demos.Alexa.Resources%2Fazuredeploy.json" target="_blank">
    <img src="http://azuredeploy.net/deploybutton.png"/>
</a>

## Flow 
#### REST API App in the diagram below is deployed in your Azure subscription when you click "Deploy" button above. LUIS service endpoint it connects to is the shared service but can be changed to yours via appsettings.json file in the code.

<p align="center">
  <img src="https://github.com/suneetnangia/Alexa/blob/master/Microsoft.Demos.Alexa.Resources/AlexaSkillsScreenshots/ASISFlow.PNG?raw=true" width="350"/>  
</p>

## What Does REST API Do-
1. Provides a RESTful endpoint for Alexa skill to send JSON formatted request with full utterances.
2. Transforms JSON to querystring parameters and sends the request to LUIS API.
3. Transforms JSON response from LUIS API to JSON response for Alexa skill.
Credit: This API uses Alexa serialisation lib from https://github.com/timheuer

## Target Architecture
This depicts what can be achived by this architecture i.e. connecting multiple services in hub and spoke model.

<p align="center">
  <img src="https://github.com/suneetnangia/Alexa/blob/master/Microsoft.Demos.Alexa.Resources/AlexaSkillsScreenshots/TargetFlow.PNG?raw=true" width="400"/>  
</p>

## Design Benefits-
1. Provides a common intelligence layer for multiple channels including but not limited to Voice Services (e.g. Alexa), Bots, IVR.
2. Allows conversations to transit between channels with the same user/session e.g. initiate chat on SMS and continue on Facebook when you get to computer.
3. Follows microservices implementation pattern i.e. separation of intent domain.
4. Provides better intent inference with continuous visibility and opportunity to improve the interpretation model.
5. Exentisble design to allow other cognitive services like sentiment analysis to be plugged in later.

## Integration with Alexa Skill
### Key Points
1. Configure Alexa skill to send full utterances/phrases spoken by the end user to this API, here's how-
    - In Interaction Model tab, create a new custom slot type, call it "PhraseSlotType".
    - In Interaction Model tab, add slot value for this newly created slot type e.g. "sdjhf sahj asdkd alskds disoiew ewds dsjdsiuew dsiuds ouidsfiu ewoi dsoif sswewe". This value is intentionally made up of random characters and words to avoid any weighing towards the value entered when speech is converted to text and it is required to let interaction model know that this slot type can capture multiple words. (thanks Dean Bryen for the top tip here)
    - In Interaction Model tab, create a new intent, call it "GenericIntent".
    - In Interaction Model tab, under "Generic Intent", create a new slot of type "PhraseSlotType", call it "PhraseSlot".
    - In Interaction Model tab, under "Generic Intent", create a new utterance as "{PhraseSlot}". This will let interaction model know that you want to capture everything in this one slot.
2. Configure Alexa skill to send JSON request with utterance to this API.
    - In Configuration tab, select Https endpoint type, and insert the default endpoint which points to your Azure API. Example- https://alexainterfacedemo.azurewebsites.net/api/alexa. Dont forget to specify Https infront of the URL as mentioned in the previous example here.
    - In SSL Certificate tab, select "My development endpoint is a sub-domain of a domain that has a wildcard certificate from a certificate authority".
3. Test the API
    - In Test tab, ensure Test switch is enabled at the top.
    - In Test tab, under "Enter Utterance" input, specify the utterance you want to test e.g. "Send me two packs of chicken nuggets for small dogs"
#### Alexa Development Studio Screenshots https://github.com/suneetnangia/Alexa/tree/master/Microsoft.Demos.Alexa.Resources/AlexaSkillsScreenshots

## What's Next
1. Connect Bot Framework with state management to allow conversational communication on Alexa and other channels.
2. This API uses my LUIS model which is good enough for testing purposes but it will not be specific your domain. Please head over to https://www.LUIS.ai to learn more about LUIS and develop a model which suits your need.
