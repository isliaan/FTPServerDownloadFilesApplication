using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace DownloadFTPServerFiles
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
//#if DEBUG
//            FTPFilesDownload service = new FTPFilesDownload();
//            service.onDebug();
//#else
            ServiceBase[] ServicesToRun;

            // More than one user Service may run within the same process. To add
            // another service to this process, change the following line to
            // create a second service object. For example,
            //
            //   ServicesToRun = new ServiceBase[] {new FTPFilesDownload(), new MySecondUserService()};
            //
            ServicesToRun = new ServiceBase[] { new FTPFilesDownload() };

            ServiceBase.Run(ServicesToRun);
//#endif

        }
    }
}