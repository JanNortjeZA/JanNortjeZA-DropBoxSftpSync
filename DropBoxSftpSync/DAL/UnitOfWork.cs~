
using Microsoft.Extensions.Configuration;
using DanceRadioSync.Models;
using Renci.SshNet;
using System.Data.SqlClient;
using MySQL.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace DanceRadioSync.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private tubelessMySql tubelessMySql;
        private readonly IConfiguration _config;
      
        public UnitOfWork(IConfiguration config)
        {
            _config = config;
            

            var sshServer = "server01.tubeless.co.za";
            var sshUserName = "jan";
            var sshPassword = "djTubel3ss";
            var databaseServer = "localhost";

            tubelessMySql = new tubelessMySql(_config.GetConnectionString(Global.dbConnectionString));
            //var (sshClient, localPort) = ConnectSsh(sshServer, sshUserName, sshPassword, databaseServer: databaseServer);
            //using (sshClient)
            //{
            //    tubelessMySql = new tubelessMySql(_config.GetConnectionString(Global.dbConnectionString));
               
            //    //MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder
            //    //{
            //    //    Server = "127.0.0.1",
            //    //    Port = localPort,
            //    //    UserID = databaseUserName,
            //    //    Password = databasePassword,
            //    //};

            //    //using var connection = new MySqlConnection(csb.ConnectionString);
            //    //connection.Open();
            //}

        }

        public  (SshClient SshClient, uint Port) ConnectSsh(string sshHostName, string sshUserName, string sshPassword = "",
    string sshKeyFile = "", string sshPassPhrase = "", int sshPort = 22, string databaseServer = "localhost", int databasePort = 3306)
        {
            // check arguments
            if (string.IsNullOrEmpty(sshHostName))
                throw new ArgumentException($"{nameof(sshHostName)} must be specified.", nameof(sshHostName));
            if (string.IsNullOrEmpty(sshHostName))
                throw new ArgumentException($"{nameof(sshUserName)} must be specified.", nameof(sshUserName));
            if (string.IsNullOrEmpty(sshPassword) && string.IsNullOrEmpty(sshKeyFile))
                throw new ArgumentException($"One of {nameof(sshPassword)} and {nameof(sshKeyFile)} must be specified.");
            if (string.IsNullOrEmpty(databaseServer))
                throw new ArgumentException($"{nameof(databaseServer)} must be specified.", nameof(databaseServer));

            // define the authentication methods to use (in order)
            var authenticationMethods = new List<AuthenticationMethod>();
            if (!string.IsNullOrEmpty(sshKeyFile))
            {
                authenticationMethods.Add(new PrivateKeyAuthenticationMethod(sshUserName,
                    new PrivateKeyFile(sshKeyFile, string.IsNullOrEmpty(sshPassPhrase) ? null : sshPassPhrase)));
            }
            if (!string.IsNullOrEmpty(sshPassword))
            {
                authenticationMethods.Add(new PasswordAuthenticationMethod(sshUserName, sshPassword));
            }

            // connect to the SSH server
            var sshClient = new SshClient(new ConnectionInfo(sshHostName, sshPort, sshUserName, authenticationMethods.ToArray()));
            sshClient.Connect();

            // forward a local port to the database server and port, using the SSH server
            var forwardedPort = new ForwardedPortLocal("127.0.0.1", databaseServer, (uint)databasePort);
            sshClient.AddForwardedPort(forwardedPort);
            forwardedPort.Start();

            return (sshClient, forwardedPort.BoundPort);
        }

        //private ISMSCampaignRepository smsCampaignRepository;
        //private ISMSCampaignLineRepository smsCampaignLineRepository;

        //public virtual ISMSCampaignRepository SMSCampaignRepository
        //{
        //    get
        //    {
        //        if (this.smsCampaignRepository == null)
        //        {
        //            this.smsCampaignRepository = new SMSCampaignRepository(tubelessMySql);
        //        }
        //        return smsCampaignRepository;
        //    }
        //}

        //public virtual ISMSCampaignLineRepository SMSCampaignLineRepository
        //{
        //    get
        //    {
        //        if (this.smsCampaignLineRepository == null)
        //        {
        //            this.smsCampaignLineRepository = new SMSCampaignLineRepository(tubelessMySql);
        //        }
        //        return smsCampaignLineRepository;
        //    }
        //}

        public void DbSave()
        {
            tubelessMySql.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    tubelessMySql.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}