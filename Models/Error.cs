namespace DiscordAyta.Models
{
    public class Error : MagicModel
    {
        public string Code { get; set; }
        public int Status { get; set; }
        public string Details { get; set; }
    }

}
