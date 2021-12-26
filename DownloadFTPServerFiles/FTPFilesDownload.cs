using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Threading;
//using System.Timers;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Net;

namespace DownloadFTPServerFiles
{
    [RunInstaller(true)]
    public partial class FTPFilesDownload : ServiceBase
    {
        //System.Timers.Timer tmrExecutor = new System.Timers.Timer(); // name space(using System.Timers;)
        int ScheduleTime=Convert.ToInt32(ConfigurationSettings.AppSettings["ThreadTime"]);
        public Thread Worker = null; 
        public FTPFilesDownload()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //WriteToFile("Service is started at " + DateTime.Now);
            ////timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            //timer.Interval = 5000; //number in milisecinds  
            //timer.Enabled = true;

            //string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog" + ".txt";
            //File.WriteAllText(filepath, string.Empty);
            //this.WriteToFile("File Download Service started {0}");
            this.ScheduleService();

            //string ResponseDescription = "";

            ////FTP Folder name. Leave blank if you want to Download file from root folder.
            //string ftpFolder = "/FaoCommon/MarketReports/";

            //string LatestFileName = NewFileName("ftp://ftp.connect2nse.com", "faoguest", "FAOGUEST", ftpFolder);

            //string DownloadedFilePath = "E:/Development/FTPServerDownloadFiles" + "/" + LatestFileName;

            //try
            //{
            //    //Create FTP Request.
            //    FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com" + ftpFolder + LatestFileName);
            //    request.Method = WebRequestMethods.Ftp.DownloadFile;

            //    //Enter FTP Server credentials.
            //    request.Credentials = new NetworkCredential("faoguest", "FAOGUEST");
            //    request.UsePassive = true;
            //    request.UseBinary = true;
            //    request.EnableSsl = false;

            //    //Fetch the Response and read it into a MemoryStream object.
            //    FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            //    Stream stream = response.GetResponseStream();
            //    byte[] buffer = new byte[2048];
            //    FileStream fs = new FileStream(DownloadedFilePath, FileMode.Create);
            //    int ReadCount = stream.Read(buffer, 0, buffer.Length);
            //    while (ReadCount > 0)
            //    {
            //        fs.Write(buffer, 0, ReadCount);
            //        ReadCount = stream.Read(buffer, 0, buffer.Length);
            //    }
            //    ResponseDescription = response.StatusDescription;
            //    fs.Close();
            //    stream.Close();
            //}
            //catch (WebException ex)
            //{
            //    throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
            //}


            //try
            //{
            //    this.ScheduleService();
            //    ThreadStart start = new ThreadStart(Working);
            //    Worker = new Thread(start);
            //    Worker.Start();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        //public void Working()
        //{
        //    while (true)
        //    {
        //        WriteToFile("Service is started at " + DateTime.Now);

        //        string ResponseDescription = "";

        //        //FTP Folder name. Leave blank if you want to Download file from root folder.
        //        string ftpFolder = "/FaoCommon/MarketReports/";

        //        string LatestFileName = NewFileName("ftp://ftp.connect2nse.com", "faoguest", "FAOGUEST", ftpFolder);

        //        string DownloadedFilePath = "E:/Development/FTPServerDownloadFiles" + "/" + LatestFileName;

        //        //Create FTP Request.
        //        FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com" + ftpFolder + LatestFileName);
        //        request.Method = WebRequestMethods.Ftp.DownloadFile;

        //        //Enter FTP Server credentials.
        //        request.Credentials = new NetworkCredential("faoguest", "FAOGUEST");
        //        request.UsePassive = true;
        //        request.UseBinary = true;
        //        request.EnableSsl = false;

        //        //Fetch the Response and read it into a MemoryStream object.
        //        FtpWebResponse response = (FtpWebResponse)request.GetResponse();

        //        Stream stream = response.GetResponseStream();
        //        byte[] buffer = new byte[2048];
        //        FileStream fs = new FileStream(DownloadedFilePath, FileMode.Create);
        //        int ReadCount = stream.Read(buffer, 0, buffer.Length);
        //        while (ReadCount > 0)
        //        {
        //            fs.Write(buffer, 0, ReadCount);
        //            ReadCount = stream.Read(buffer, 0, buffer.Length);
        //        }
        //        ResponseDescription = response.StatusDescription;
        //        fs.Close();
        //        stream.Close();

        //        //Thread.Sleep(ScheduleTime*60*1000);
        //    }
        //}

        public void onDebug()
        {
            OnStart(null);
        }

        //public static string NewFileName(string ftp, string StrUserName, string StrPassword, string ftpFolder)
        //{
        //    try
        //    {
        //        //Create FTP Request.
        //        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder);
        //        request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

        //        //Enter FTP Server credentials.
        //        request.Credentials = new NetworkCredential(StrUserName, StrPassword);
        //        request.UsePassive = true;
        //        request.UseBinary = true;
        //        request.EnableSsl = false;

        //        //Fetch the Response and read it using StreamReader.
        //        FtpWebResponse response = (FtpWebResponse)request.GetResponse();

        //        StreamReader streamReader = new StreamReader(response.GetResponseStream());
        //        streamReader.DiscardBufferedData();

        //        List<string> directories = new List<string>();

        //        string line = streamReader.ReadLine();
        //        DataTable dtFiles = new DataTable();
        //        dtFiles.Columns.Add("Name");
        //        dtFiles.Columns.Add("Size");
        //        dtFiles.Columns.Add("Date");

        //        while (!string.IsNullOrEmpty(line))
        //        {
        //            if(line.Contains("CM_MTM_Prices_"))
        //            {
        //                string[] lineArr = line.Split(' ');
        //                line = lineArr[lineArr.Length - 1];
        //                directories.Add(line);
        //                directories.Sort();

        //                DataRow row = dtFiles.NewRow();

        //                row["Name"] = lineArr[lineArr.Length - 1];
        //                row["Size"] = lineArr[lineArr.Length - 2];
        //                row["Date"] = Convert.ToDateTime(lineArr[0]).ToString("yyyy-MM-dd") + " " + lineArr[2];
        //                dtFiles.Rows.Add(row);
        //            }
        //            line = streamReader.ReadLine();
        //        }

        //        string expression;
        //        string sortOrder;
        //        //expression = "Name like '%F_CTM_CONTRACTS_%' ";
        //        expression = "Name like '%CM_MTM_Prices_%' ";
        //        sortOrder = "Date DESC";
        //        DataRow[] foundRows = dtFiles.Select(expression, sortOrder, DataViewRowState.Added);
        //        string foundlastestfilename = Convert.ToString(foundRows[0].ItemArray[0]);

        //        return foundlastestfilename;

        //    }
        //    catch (WebException ex)
        //    {
        //        throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
        //    }
        //}

        //public static string NewFileDateTime(string ftp, string StrUserName, string StrPassword, string ftpFolder)
        //{
        //    try
        //    {
        //        //Create FTP Request.
        //        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder);
        //        request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

        //        //Enter FTP Server credentials.
        //        request.Credentials = new NetworkCredential(StrUserName, StrPassword);
        //        request.UsePassive = true;
        //        request.UseBinary = true;
        //        request.EnableSsl = false;

        //        //Fetch the Response and read it using StreamReader.
        //        FtpWebResponse response = (FtpWebResponse)request.GetResponse();

        //        StreamReader streamReader = new StreamReader(response.GetResponseStream());
        //        streamReader.DiscardBufferedData();

        //        List<string> directories = new List<string>();

        //        string line = streamReader.ReadLine();
        //        DataTable dtFiles = new DataTable();
        //        dtFiles.Columns.Add("Name");
        //        dtFiles.Columns.Add("Size");
        //        dtFiles.Columns.Add("Date");

        //        while (!string.IsNullOrEmpty(line))
        //        {
        //            if (line.Contains("CM_MTM_Prices_"))
        //            {
        //                string[] lineArr = line.Split(' ');
        //                line = lineArr[lineArr.Length - 1];
        //                directories.Add(line);
        //                directories.Sort();

        //                DataRow row = dtFiles.NewRow();

        //                row["Name"] = lineArr[lineArr.Length - 1];
        //                row["Size"] = lineArr[lineArr.Length - 2];
        //                row["Date"] = Convert.ToDateTime(lineArr[0]).ToString("yyyy-MM-dd") + " " + lineArr[2];
        //                dtFiles.Rows.Add(row);
        //            }
        //            line = streamReader.ReadLine();
        //        }

        //        string expression;
        //        string sortOrder;
        //        //expression = "Name like '%F_CTM_CONTRACTS_%' ";
        //        expression = "Name like '%CM_MTM_Prices_%' ";
        //        sortOrder = "Date DESC";
        //        DataRow[] foundRows = dtFiles.Select(expression, sortOrder, DataViewRowState.Added);
        //        string foundlastestfiledatetime = Convert.ToString(foundRows[0].ItemArray[2]);

        //        return foundlastestfiledatetime;

        //    }
        //    catch (WebException ex)
        //    {
        //        throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
        //    }
        //}


        public static string[] NewFileName(string ftp, string StrUserName, string StrPassword, string ftpFolder)
        {
            try
            {
                //Create FTP Request.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                //Enter FTP Server credentials.
                request.Credentials = new NetworkCredential(StrUserName, StrPassword);
                request.UsePassive = true;
                request.UseBinary = true;
                request.EnableSsl = false;

                //Fetch the Response and read it using StreamReader.
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                streamReader.DiscardBufferedData();

                List<string> directories = new List<string>();

                string line = streamReader.ReadLine();
                DataTable dtFiles = new DataTable();
                dtFiles.Columns.Add("Name");
                dtFiles.Columns.Add("Size");
                dtFiles.Columns.Add("Date");

                while (!string.IsNullOrEmpty(line))
                {
                    if (line.Contains("CM_MTM_Prices_"))
                    {
                        string[] lineArr = line.Split(' ');
                        line = lineArr[lineArr.Length - 1];
                        directories.Add(line);
                        directories.Sort();

                        DataRow row = dtFiles.NewRow();

                        row["Name"] = lineArr[lineArr.Length - 1];
                        row["Size"] = lineArr[lineArr.Length - 2];
                        row["Date"] = Convert.ToDateTime(lineArr[0]).ToString("yyyy-MM-dd") + " " + lineArr[2];
                        dtFiles.Rows.Add(row);
                    }
                    line = streamReader.ReadLine();
                }

                string expression;
                string sortOrder;
                //expression = "Name like '%F_CTM_CONTRACTS_%' ";
                expression = "Name like '%CM_MTM_Prices_%' ";
                sortOrder = "Date DESC";
                DataRow[] foundRows = dtFiles.Select(expression, sortOrder, DataViewRowState.Added);
                string[] foundlastestfilename = new string[2];
                foundlastestfilename[0] = Convert.ToString(foundRows[0].ItemArray[0]);
                foundlastestfilename[1] = Convert.ToString(foundRows[0].ItemArray[2]);

                return foundlastestfilename;

            }
            catch (WebException ex)
            {
                throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
            }
        }

        protected override void OnStop()
        {
            //if ((Worker != null) & Worker.IsAlive)
            //{
            //    WriteToFile("Service is stopped at " + DateTime.Now);
            //    Worker.Abort();
            //}
            //this.WriteToFile("File Download stopped: " + Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()).ToString("dd-MM-yyyy"));
            this.WriteToFile("File Download stopped: " + DateTime.Now);
            this.Schedular.Dispose();
        }

        public void WriteToFile(string Message)
        {
            //throw new Exception("The method or operation is not implemented.");
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog" + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }

        private Timer Schedular;

        public void ScheduleService()
        {
            try
            {
                Schedular = new Timer(new TimerCallback(SchedularCallback));
                string mode = ConfigurationSettings.AppSettings["Mode"].ToUpper();
                this.WriteToFile("File Download Mode: " + mode);

                //Set the Default Time.
                DateTime scheduledTime = DateTime.MinValue;

                if (mode == "DAILY")
                {
                    this.WriteToFile("File Download Mode: " + mode);
                    //Get the Scheduled Time from AppSettings.
                    scheduledTime = DateTime.Parse(System.Configuration.ConfigurationSettings.AppSettings["ScheduledTime"]);
                    if (DateTime.Now > scheduledTime)
                    {
                        //If Scheduled Time is passed set Schedule for the next day.
                        scheduledTime = scheduledTime.AddDays(1);
                    }
                }

                if (mode.ToUpper() == "INTERVAL")
                {
                    //Get the Interval in Minutes from AppSettings.
                    int intervalMinutes = Convert.ToInt32(ConfigurationSettings.AppSettings["IntervalMinutes"]);

                    //Set the Scheduled Time by adding the Interval to Current Time.
                    scheduledTime = DateTime.Now.AddMinutes(intervalMinutes);
                    if (DateTime.Now > scheduledTime)
                    {
                        //If Scheduled Time is passed set Schedule for the next Interval.
                        scheduledTime = scheduledTime.AddMinutes(intervalMinutes);                        
                    }
                }

                string ResponseDescription = "";

                //FTP Folder name. Leave blank if you want to Download file from root folder.
                //string ftpFolder = "/FaoCommon/MarketReports/";
                string ftpFolder = "/common/clearing/";

                //string LatestFileName = NewFileName("ftp://ftp.connect2nse.com", "faoguest", "FAOGUEST", ftpFolder);
                //string LatestFileDateTime = NewFileDateTime("ftp://ftp.connect2nse.com", "faoguest", "FAOGUEST", ftpFolder);
                string[] returnvalue = NewFileName("ftp://ftp.connect2nse.com", "ftpguest", "FTPGUEST", ftpFolder);
                string LatestFileName = returnvalue[0];
                string LatestFileDateTime = returnvalue[1];
                string CorrectFileName = LatestFileName.Replace(".csv", ".txt");

                string DownloadedFilePath = "E:/Development/FTPServerDownloadFiles" + "/" + CorrectFileName;
                string NewDownloadedFilePath = "E:/Development/FTPServerDownloadFiles" + "/" + "New_"+CorrectFileName;

                string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog" + ".txt";

                string PreviousFileInfo = "";
                string searchForFileInfo = "CM_MTM_Prices_";
                string[] FileLinesInfo = File.ReadAllLines(filepath);

                for (int i = FileLinesInfo.Length - 1; i >= 0; i--)
                {
                    if (FileLinesInfo[i].Contains(searchForFileInfo))
                    {
                        PreviousFileInfo = FileLinesInfo[i].ToString();
                    }
                }

                File.WriteAllText(filepath, string.Empty);
                //this.WriteToFile("File Download Service Started: " + Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()).ToString("dd-MM-yyyy"));
                this.WriteToFile("File Download Service Started: " + DateTime.Now);
                this.WriteToFile("File Download Mode: " + mode);

                if (PreviousFileInfo != (CorrectFileName + " " + LatestFileDateTime))
                {
                    try
                    {
                        //Create FTP Request.
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com" + ftpFolder + LatestFileName);
                        request.Method = WebRequestMethods.Ftp.DownloadFile;

                        //Enter FTP Server credentials.
                        request.Credentials = new NetworkCredential("ftpguest", "FTPGUEST");
                        request.UsePassive = true;
                        request.UseBinary = true;
                        request.EnableSsl = false;

                        //Fetch the Response and read it into a MemoryStream object.
                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                        Stream stream = response.GetResponseStream();
                        byte[] buffer = new byte[2048];
                        FileStream fs = new FileStream(DownloadedFilePath, FileMode.Create);
                        int ReadCount = stream.Read(buffer, 0, buffer.Length);
                        while (ReadCount > 0)
                        {
                            fs.Write(buffer, 0, ReadCount);
                            ReadCount = stream.Read(buffer, 0, buffer.Length);
                        }
                        ResponseDescription = response.StatusDescription;

                        fs.Close();

                        DataTable dtFilesData = new DataTable();
                        dtFilesData.Columns.Add("Description");
                        //string searchFor = "ACC";
                        string searchFor = "TB";
                        string[] lines = File.ReadAllLines(DownloadedFilePath);

                        for (int i = lines.Length - 1; i >= 0; i--)
                        {
                            if (lines[i].Contains(searchFor))
                            {
                                DataRow row = dtFilesData.NewRow();

                                row["Description"] = lines[i].ToString();
                                dtFilesData.Rows.Add(row);
                            }
                        }
                        if (dtFilesData.Rows.Count > 0)
                        {
                            int columnIndex = 0; //single-column DataTable
                            string[] userArr = new string[dtFilesData.Rows.Count];
                            for (int i = 0; i < dtFilesData.Rows.Count; i++)
                            {
                                userArr[i] = dtFilesData.Rows[i][columnIndex].ToString();
                            }
                            File.WriteAllLines(NewDownloadedFilePath, userArr);
                            //File.Delete(DownloadedFilePath);
                        }

                        stream.Close();
                    }
                    catch (WebException ex)
                    {
                        throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
                    }
                }

                TimeSpan timeSpan = scheduledTime.Subtract(DateTime.Now);
                string schedule = string.Format("{0} day(s) {1} hour(s) {2} minute(s) {3} seconds(s)", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                this.WriteToFile("File Download scheduled to run after: " + schedule);
                //this.WriteToFile("The File Name is: " + CorrectFileName +" "+LatestFileDateTime+ " {0}");
                this.WriteToFile(CorrectFileName + " " + LatestFileDateTime);
                //this.WriteToFile("File Download Service Ended: " + Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()).ToString("dd-MM-yyyy"));
                this.WriteToFile("File Download Service Ended: " + DateTime.Now);

                //Get the difference in Minutes between the Scheduled and Current Time.
                int dueTime = Convert.ToInt32(timeSpan.TotalMilliseconds);

                //Change the Timer's Due Time.
                Schedular.Change(dueTime, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                WriteToFile("File Download Error on: " + ex.Message + ex.StackTrace);

                //Stop the Windows Service.
                using (System.ServiceProcess.ServiceController serviceController = new System.ServiceProcess.ServiceController("SimpleService"))
                {
                    serviceController.Stop();
                }
            }
        }

        private void SchedularCallback(object e)
        {
            this.WriteToFile("File Download Log");
            this.ScheduleService();
        }
    }
}
