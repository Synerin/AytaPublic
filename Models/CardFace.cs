using System.Text.Json.Serialization;

namespace DiscordAyta.Models
{
    public class CardFace : MagicModel
    {
        [JsonPropertyName("artist_id")]
        public string ArtistId { get; set; }

        public string Artist { get; set; }

        public decimal Cmc { get; set; }

        [JsonPropertyName("color_indicator")]
        public string[] ColorIndicator { get; set; }

        public string[] Colors { get; set; }

        [JsonPropertyName("flavor_name")]
        public string FlavorName { get; set; }

        [JsonPropertyName("flavor_text")]
        public string FlavorText { get; set; }

        [JsonPropertyName("illustration_id")]
        public string IllustrationId { get; set; }

        [JsonPropertyName("image_uris")]
        public Dictionary<string, Uri> ImageUris { get; set; }

        public string Layout { get; set; }

        public string Loyalty { get; set; }

        [JsonPropertyName("mana_cost")]
        public string ManaCost { get; set; }

        public string Name { get; set; }

        [JsonPropertyName("oracle_id")]
        public string OracleId { get; set; }

        [JsonPropertyName("oracle_text")]
        public string OracleText { get; set; }

        public string Power { get; set; }

        [JsonPropertyName("printed_name")]
        public string PrintedName { get; set; }

        [JsonPropertyName("printed_text")]
        public string PrintedText { get; set; }

        [JsonPropertyName("printed_type_line")]
        public string PrintedTypeLine { get; set; }

        public string Toughness { get; set; }

        [JsonPropertyName("type_line")]
        public string TypeLine { get; set; }

        public string Watermark { get; set; }
    }
}
