using Newtonsoft.Json;

namespace DG.Polly.Contracts
{
    public class DocumentMetadataContract
    {
        [JsonProperty("messageId")]
        public string MessageId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("additionalInfo")]
        public AdditionalInfoContract AdditionalInfo { get; set; }
    }
}
