using DanceRadioSync.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DanceRadioSync.DAL
{
    public class SMSCampaignLineRepository : ISMSCampaignLineRepository
    {
        private tubelessMySql context;
        private DbSet<SMSCampaignLine> dbSet;
        public SMSCampaignLineRepository(tubelessMySql context)
        {
            this.context = context;
            this.dbSet = context.Set<SMSCampaignLine>();
        }


        public virtual void Update(SMSCampaignLine smscampaignline)
        {
            smscampaignline.LAST_MODIFIED_ON = DateTime.Now;
            smscampaignline.LastModifiedByStaffID = 78;
            dbSet.Attach(smscampaignline);
            context.Entry(smscampaignline).State = EntityState.Modified;
        }

        public virtual IEnumerable<SMSCampaignLine> GetSMSstoSend()
        {
            IQueryable<SMSCampaignLine> query = (from lines in context.SMSCampaignLines
                                                 where lines.Processed == false &&
                                                       lines.Locked == false
                                                 join camp in context.SMSCampaigns on new { campID = lines.SMSCampaignID }
                                                                                equals new { campID = camp.ID }
                                                 where camp.Send == true
                                                 && camp.NOTACTIVE == false
                                                 select lines);
            return query.Take(500).ToList();
        }

        public virtual IEnumerable<SMSCampaignLine> GetFailedLocks()
        {
            IQueryable<SMSCampaignLine> query = (from lines in context.SMSCampaignLines
                                                 where lines.Processed == false &&
                                                 lines.Locked == true
                                                 join camp in context.SMSCampaigns on new { campID = lines.SMSCampaignID }
                                                                                equals new { campID = camp.ID }
                                                 where camp.Send == true
                                                 select lines);
            return query.Take(500).ToList();
        }

    }
}