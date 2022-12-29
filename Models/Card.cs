using System.Text.Json.Serialization;

namespace DiscordAyta.Models
{
    public class Card : MagicModel
    {
        [JsonPropertyName("all_parts")]
        public Dictionary<string, string>[] AllParts { get; set; }

        [JsonPropertyName("arena_id")]
        public int ArenaId { get; set; }

        [JsonPropertyName("artist_ids")]
        public string[] ArtistIds { get; set; }

        public string Artist { get; set; }

        public bool Booster { get; set; }

        [JsonPropertyName("border_color")]
        public string BorderColor { get; set; }

        [JsonPropertyName("card_back_id")]
        public string CardBackId { get; set; }

        [JsonPropertyName("card_faces")]
        public CardFace[] CardFaces { get; set; }

        [JsonPropertyName("cardmarket_id")]
        public int CardMarketId { get; set; }

        public decimal Cmc { get; set; }

        [JsonPropertyName("collector_number")]
        public string CollectorNumber { get; set; }

        [JsonPropertyName("color_identity")]
        public string[] ColorIdentity { get; set; }

        [JsonPropertyName("color_indicator")]
        public string[] ColorIndicator { get; set; }

        public string[] Colors { get; set; }

        [JsonPropertyName("content_warning")]
        public bool ContentWarning { get; set; }

        public bool Digital { get; set; }

        [JsonPropertyName("edhrec_rank")]
        public int EdhrecRank { get; set; }

        public string[] Finishes { get; set; }

        [JsonPropertyName("flavor_name")]
        public string FlavorName { get; set; }

        [JsonPropertyName("flavor_text")]
        public string FlavorText { get; set; }

        public string Frame { get; set; }

        [JsonPropertyName("frame_effects")]
        public string[] FrameEffects { get; set; }

        [JsonPropertyName("full_art")]
        public bool FullArt { get; set; }

        public string[] Games { get; set; }

        [JsonPropertyName("hand_modifier")]
        public string HandModifier { get; set; }

        [JsonPropertyName("highres_image")]
        public bool HighresImage { get; set; }

        public Guid Id { get; set; }

        [JsonPropertyName("illustration_id")]
        public Guid IllustrationId { get; set; }

        [JsonPropertyName("image_status")]
        public string ImageStatus { get; set; }

        [JsonPropertyName("image_uris")]
        public Dictionary<string, Uri> ImageUris { get; set; }

        public string[] Keywords { get; set; }

        public string Language { get; set; }

        public string Layout { get; set; }

        public Dictionary<string, string> Legalities { get; set; }

        [JsonPropertyName("life_modifier")]
        public string LifeModifier { get; set; }

        public string Loyalty { get; set; }

        [JsonPropertyName("mana_cost")]
        public string ManaCost { get; set; }

        [JsonPropertyName("mtgo_foil_id")]
        public int MtgoFoilId { get; set; }

        [JsonPropertyName("mtgo_id")]
        public int MtgoId { get; set; }

        [JsonPropertyName("multiverse_ids")]
        public int[] MultiverseIds { get; set; }
        
        public string Name { get; set; }

        [JsonPropertyName("oracle_id")]
        public string OracleId { get; set; }

        [JsonPropertyName("oracle_text")]
        public string OracleText { get; set; }

        public bool Oversized { get; set; }

        public string Power { get; set; }

        public Dictionary<string, string> Prewiew { get; set; }

        public Price Prices { get; set; }

        [JsonPropertyName("printed_name")]
        public string PrintedName { get; set; }

        [JsonPropertyName("printed_text")]
        public string PrintedText { get; set; }

        [JsonPropertyName("printed_type_line")]
        public string PrintedTypeLine { get; set; }

        [JsonPropertyName("prints_search_uri")]
        public Uri PrintsSearchUri { get; set; }

        [JsonPropertyName("produced_mana")]
        public string[] ProducedMana { get; set; }

        [JsonPropertyName("promo_types")]
        public string[] PromoTypes { get; set; }

        public bool Promo { get; set; }

        public string Rarity { get; set; }

        [JsonPropertyName("related_uris")]
        public Dictionary<string, Uri> RelatedUris { get; set; }

        [JsonPropertyName("released_at")]
        public string ReleasedAt { get; set; }

        public bool Reprint { get; set; }

        public bool Reserved { get; set; }

        [JsonPropertyName("purchase_uris")]
        public Dictionary<string, Uri> RetailerUris { get; set; }

        [JsonPropertyName("rulings_uri")]
        public Uri RulingsUri { get; set; }

        [JsonPropertyName("scryfall_set_uri")]
        public Uri ScryfallSetUri { get; set; }

        [JsonPropertyName("scryfall_uri")]
        public Uri ScryfallUri { get; set; }

        [JsonPropertyName("security_stamp")]
        public string SecurityStamp { get; set; }

        [JsonPropertyName("set_id")]
        public string SetId { get; set; }

        [JsonPropertyName("set_name")]
        public string SetName { get; set; }

        [JsonPropertyName("set_search_uri")]
        public Uri SetSearchUri { get; set; }

        [JsonPropertyName("set_type")]
        public string SetType { get; set; }

        [JsonPropertyName("set_uri")]
        public string SetUri { get; set; }

        public string Set { get; set; }

        [JsonPropertyName("story_spotlight")]
        public bool StorySpotlight { get; set; }

        [JsonPropertyName("tcgplayer_etched_id")]
        public int TcgplayerEtchedId { get; set; }

        [JsonPropertyName("tcgplayer_id")]
        public int TcgplayerId { get; set; }

        public bool Textless { get; set; }

        public string Toughness { get; set; }

        [JsonPropertyName("type_line")]
        public string TypeLine { get; set; }

        public Uri Uri { get; set; }

        [JsonPropertyName("variation_of")]
        public string VariationOf { get; set; }

        public bool Variation { get; set; }
    }
}
