using DanceRadioSync.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DanceRadioSync.DAL
{
    public class SMSCampaignRepository : ISMSCampaignRepository
    {
        private tubelessMySql context;
        private DbSet<SMSCampaign> dbSet;
        public SMSCampaignRepository(tubelessMySql context)
        {
            this.context = context;
            this.dbSet = context.Set<SMSCampaign>();
        }

        public virtual SMSCampaign GetByID(int id)
        {
            return dbSet.Find(id);
        } 

        public virtual void Update(SMSCampaign smscampaign)
        {
            smscampaign.LAST_MODIFIED_ON = DateTime.Now;
            smscampaign.LastModifiedByStaffID = 78;
            dbSet.Attach(smscampaign);
            context.Entry(smscampaign).State = EntityState.Modified;
        }

        public virtual IEnumerable<SMSCampaignCount> GetSMSLineCountforUpdate()
        {
            IQueryable<SMSCampaignCount> query = (
              from smsCamp in context.SMSCampaigns
              where (smsCamp.Send.Equals(true) &&
                     smsCamp.DateSent == null &&
                     smsCamp.DELETED.Equals(false) &&
                     smsCamp.EDITED.Equals(0) &&
                     smsCamp.NOTACTIVE.Equals(false))
              let lines = (from smsline in context.SMSCampaignLines
                           where smsline.Processed.Equals(false) &&
                                 smsline.SMSCampaignID == smsCamp.ID &&
                                 smsline.DELETED.Equals(false) &&
                                 smsline.EDITED.Equals(0) &&
                                 smsline.NOTACTIVE.Equals(false)
                           select smsline.ID).Count()
              select new SMSCampaignCount
              {
                  ID = smsCamp.ID,
                  LineCount = lines
              }).Distinct();
            return query.ToList();
        }

    }
}