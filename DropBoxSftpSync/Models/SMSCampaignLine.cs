using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanceRadioSync.Models
{
    [Table("SMS_CAMPAIGN_LINE")]
    public class SMSCampaignLine : AuditableModel
    {
        [Key]
        [Column("SMS_CAMPAIGN_LINE_KEY")]
        public int ID { get; set; }

        [Column("SMS_CAMPAIGN$")]
        public int SMSCampaignID { get; set; }

        [Column("MEMBER$")]
        public int MemberID { get; set; }

        [Column("PHONE_NUMBER")]
        public string PhoneNumber { get; set; }

        [Column("SMS_MESSAGE")]
        public string Message { get; set; }

        [Column("PROCESSED")]
        public Boolean Processed { get; set; }

        [Column("LOCKED")]
        public Boolean Locked { get; set; }


        public SMSCampaignLine()
        {
            Processed = false;
            Locked = false;
        }
    }
}
