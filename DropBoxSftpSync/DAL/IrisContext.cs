using System.Data.Entity;
using DanceRadioSync.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
//using MySQL.Data.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

namespace DanceRadioSync.DAL
{
    public class tubelessMySql : DbContext
    {
        public tubelessMySql(string dbConnection) : base(dbConnection)
        {
            Database.SetInitializer<tubelessMySql>(null);

            SetTimeOut();
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.Database.Log = s => Debug.WriteLine(s);
            }
        }

        private void SetTimeOut()
        {
            this.Database.CommandTimeout = 0;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    optionsBuilder.UseMySQL("server=localhost;database=library;user=user;password=password");

        //}

        // public DbSet<SMSCampaign> SMSCampaigns { get; set; }
        // public DbSet<SMSCampaignLine> SMSCampaignLines { get; set; }

    }
}