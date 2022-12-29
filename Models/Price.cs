using System.Text.Json.Serialization;

namespace DiscordAyta.Models
{
    public class Price : MagicModel
    {
        public string Eur { get; set; }

        [JsonPropertyName("eur_foil")]
        public string EurFoil { get; set; }

        public string Tix { get; set; }

        public string Usd { get; set; }

        [JsonPropertyName("usd_etched")]
        public string UsdEtched { get; set; }

        [JsonPropertyName("usd_foil")]
        public string UsdFoil { get; set; }
    }
}
