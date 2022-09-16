using Microsoft.Extensions.Logging;
using DanceRadioSync.BLL;
using DanceRadioSync.Models;
using DanceRadioSync.DAL;

namespace DanceRadioSync
{
    /// <summary>
    /// Process  sends SMS to gateway
    /// </summary>
    public interface ISyncService
    {
        void Process(bool fromQueue = false);
    }

    public class SyncService : ISyncService
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebServiceWorker _webWorker;

        public SyncService(ILogger<SyncService> logger, IUnitOfWork unitOf,  IWebServiceWorker webWorker)
        {
            _logger = logger;
            unitOfWork = unitOf;   
            _webWorker = webWorker;
        }

        public void Process(bool fromQueue = false)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Start Sync from DropBox => DanceRadio  " + (fromQueue ? " from trigger @" : "@")  + DateTime.Now.ToLocalTime());
            Console.ResetColor();
             _logger.LogInformation("SyncService.Process invoked! @" + DateTime.Now.ToLocalTime());

           // StringBuilder test = new StringBuilder();
           // test.AppendLine("Start");
            try
            {
                
                    if (!Global.isProduction)
                        Console.Clear();
                
                

            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
          


        }
    }

    public interface IDoSync
    {
        void PushSync(string number, string message, string Ref);
    }

    public class DoSync : IDoSync
    {
        private readonly ILogger _logger;
        private readonly IWebServiceWorker _webWorker;

        public DoSync(ILogger<DoSync> logger, IWebServiceWorker serviceWorker)
        {
            _logger = logger;
            _webWorker = serviceWorker; 
        }

        public void PushSync(string number, string message, string Ref)
        {
           _webWorker.GetFileListFromDropBox(number, message, Ref);
           // _logger.LogInformation("SampleServiceB.DoIt invoked!");
        }
    }
}
