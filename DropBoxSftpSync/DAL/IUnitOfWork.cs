using Renci.SshNet;

namespace DanceRadioSync.DAL
{
   public interface IUnitOfWork
    {
        public  (SshClient SshClient, uint Port) ConnectSsh(string sshHostName, string sshUserName, string sshPassword = "",
    string sshKeyFile = "", string sshPassPhrase = "", int sshPort = 22, string databaseServer = "localhost", int databasePort = 3306);


        void DbSave();
       // ISMSCampaignRepository SMSCampaignRepository { get;  }
       // ISMSCampaignLineRepository SMSCampaignLineRepository { get; }
    }
}
