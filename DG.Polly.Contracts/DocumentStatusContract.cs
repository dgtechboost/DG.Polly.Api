using Newtonsoft.Json;

namespace DG.Polly.Contracts
{
    public class DocumentStatusContract
    {
        [JsonProperty("messageId")]
        public string MessageId { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
