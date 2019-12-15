using Newtonsoft.Json;

namespace DG.Polly.Contracts
{
    public class AdditionalInfoContract
    {
        [JsonProperty("additionalProp1")]
        public string AdditionalProp1 { get; set; }
        [JsonProperty("additionalProp2")]
        public string AdditionalProp2 { get; set; }
        [JsonProperty("additionalProp3")]
        public string AdditionalProp3 { get; set; }
    }
}
