namespace DanceRadioSync.Models
{
    public class SMS
    {
        public string ID { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int? MemberID { get; set; }
    }
}
