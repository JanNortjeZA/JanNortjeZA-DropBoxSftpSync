using System.ComponentModel.DataAnnotations.Schema;

namespace DanceRadioSync.Models
{
    [Serializable()]
    public class AuditableModel
    {
        [Column("STAFF$CREATED_BY")]
        public int CreatedByStaffID { get; set; }

        [Column("STAFF$LAST_MODIFIED_BY")]
        public int LastModifiedByStaffID { get; set; }

        [Column("CREATED_ON")]
        public DateTime CREATED_ON { get; set; }
        [Column("LAST_MODIFIED_ON")]
        public DateTime LAST_MODIFIED_ON { get; set; }
        [Column("MASTER")]
        public int MASTER { get; set; }
        [Column("NOTACTIVE")]
        public Boolean NOTACTIVE { get; set; }
        [Column("EDITED")]
        public short EDITED { get; set; }
        [Column("DELETED")]
        public Boolean DELETED { get; set; }
        [Column("HISTORY")]
        public short HISTORY { get; set; }


        public AuditableModel()
        {
            CREATED_ON = DateTime.Now;
            LAST_MODIFIED_ON = DateTime.Now;
            MASTER = 0;
            NOTACTIVE = false;
            EDITED = 0;
            DELETED = false;
            HISTORY = 0;
        }
    }
}