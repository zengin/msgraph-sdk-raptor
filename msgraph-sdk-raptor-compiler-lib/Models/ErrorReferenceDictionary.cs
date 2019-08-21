using Newtonsoft.Json;

namespace MsGraphSDKSnippetsCompiler.Models
{
    public class ErrorReferenceDictionary
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Error")]
        public string Error { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }
    }

    public class ErrorReferenceDictionaryStats
    {
        public string Id { get; set; }
        public string Error { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
    }

    public class ErrorGroup
    {
        public string Key { get; set; }
        public int Count { get; set; }
    }
}