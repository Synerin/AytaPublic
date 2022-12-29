using System.Text.Json.Serialization;

namespace DiscordAyta.Models
{
    public class Rulings : MagicModel
    {
        [JsonPropertyName("oracle_id")]
        public string OracleId { get; set; }

        public string Source { get; set; }

        [JsonPropertyName("published_at")]
        public string PublishedAt { get; set; }

        public string Comment { get; set; }
    }
}
