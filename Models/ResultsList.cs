using System.Text.Json.Serialization;

namespace DiscordAyta.Models
{
    public class ResultsList : MagicModel
    {   
        [JsonPropertyName("has_more")]
        public bool HasMore { get; set; }
        
        public Object[] Data { get; set; }
    }
}
