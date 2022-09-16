using System;
using DanceRadioSync.Models;
using System.Collections.Generic;


namespace DanceRadioSync.DAL
{
    public interface ISMSCampaignRepository
    {
        SMSCampaign GetByID(int id);
        void Update(SMSCampaign smscampaign);
        IEnumerable<SMSCampaignCount> GetSMSLineCountforUpdate();
    }
}
