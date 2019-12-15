using Newtonsoft.Json;

namespace DG.Polly.Contracts
{
    public class DocumentContract
    {
        [JsonProperty("messageId")]
        public string MessageId { get; set; }
    }
}
