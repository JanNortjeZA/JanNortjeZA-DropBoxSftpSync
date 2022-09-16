using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using DanceRadioSync.Models;
using System;

namespace DanceRadioSync
{
    public class Functions
    {
        private readonly ISyncService _syncService;
        private readonly IDoSync _doSync;

        public Functions(ISyncService syncService, IDoSync doSync)
        {
            _syncService = syncService;
            _doSync = doSync;   
        }

       

        [FunctionName("MessageQueue")]
       // [return: Queue("sms-queue-poison")]
        public void ProcessServiceMessage([QueueTrigger("sync-service")] string message, ILogger logger)
        {
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sync triggered : " + DateTime.Now.ToLocalTime() + Environment.NewLine + message);
            Console.ResetColor();
            if (message == "sync")
            {
                _syncService.Process(true);
            }
           // return message;
        }

        [FunctionName("MessageSyncQueue")]
        // [return: Queue("sms-queue-poison")]
        public void ProcessSyncPushMessage([QueueTrigger("sync-push")] ILogger logger)
        {
            try
            {
                Console.WriteLine("Push Sync now");
              //  _doSync.PushSync(sms.To, sms.Message, sms.MemberID == null ? "Iris" : sms.MemberID.Value.ToString());
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
               // Console.WriteLine("Instant Message to:  " + sms.To + " => " + sms.Message);
                Console.ResetColor();
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            
            //if (message == "send")
            //{
            //    _syncService.Process(true);
            //}
            // return message;
        }

        ///  CRON expression
        /// {second} {minute} {hour} {day} {month} {day-of-week}

        [FunctionName("TimerService")]
        public  void TimerTick([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger logger)
        {
            _syncService.Process();
        }


    }
}
