using System;
using DanceRadioSync.Models;
using System.Collections.Generic;

namespace DanceRadioSync.DAL
{
    public interface ISMSCampaignLineRepository
    {
        void Update(SMSCampaignLine smscampaignlign);
        IEnumerable<SMSCampaignLine> GetSMSstoSend();
        IEnumerable<SMSCampaignLine> GetFailedLocks();
    }
}
