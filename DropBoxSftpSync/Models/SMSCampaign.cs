using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanceRadioSync.Models
{
    [Table("SMS_CAMPAIGN")]
    public class SMSCampaign : AuditableModel
    {
        [Key]
        [Column("SMS_CAMPAIGN_KEY")]
        public int ID { get; set; }

        [Column("SMS_CAMPAIGN_NAME")]
        public string Name { get; set; } = string.Empty;

        [Column("SEND")]
        public Boolean Send { get; set; }

        [Column("DATE_SENT")]
        public DateTime? DateSent { get; set; }

        [Column("STAFF$SENT_BY")]
        public int? SentByStaffID { get; set; }


        public SMSCampaign()
        {
            Send = false;
        }
    }
}
