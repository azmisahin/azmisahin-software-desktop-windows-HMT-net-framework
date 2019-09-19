namespace HMT.Windows.Desktop.Tracking {
    using System;
    using System.Configuration;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Script.Serialization;
    using System.Windows;
    using System.Windows.Threading;
    using HMT.Hardware;

    /// <summary>
    /// Watch Screen
    /// </summary>
    /// <see cref="https://docs.microsoft.com/tr-tr/dotnet/api/system.windows.window"/>
    public partial class Watch : Window {

        #region Startup Algoritm
        /// <summary>
        /// Watch
        /// </summary>
        public Watch() {

            InitializeComponent();

            InitializeWindowsStartupConfiguration();

            InitializeWindowsStartupEvent();

            InitializeWindowsStyle();

            InitializeHMT();
        }
        #endregion

        #region Window Design
        /// <summary>
        /// Initalize Windows Startup Configuration
        /// <see cref="https://docs.microsoft.com/tr-tr/dotnet/api/system.windows.window.windowstartuplocation"/>
        /// </summary>
        private void InitializeWindowsStartupConfiguration() {
            // Define workspace dimensions
            // see cref="https://docs.microsoft.com/tr-tr/dotnet/api/system.windows.systemparameters.workarea"
            double workAreaWidth = SystemParameters.WorkArea.Width;
            double workAreaHeight = SystemParameters.WorkArea.Height;

            // Windows startup location Reset
            // see cref="https://docs.microsoft.com/tr-tr/dotnet/api/system.windows.window.windowstartuplocation"
            WindowStartupLocation = WindowStartupLocation.Manual;

            // Top - Left
            // see cref="https://docs.microsoft.com/tr-tr/dotnet/api/system.windows.window.left"
            // see cref="https://docs.microsoft.com/tr-tr/dotnet/api/system.windows.window.top"
            Left = 0; Top = 0;

            // Configures the window position as [ bottom - right ]
            Left = workAreaWidth - Width;
            Top = workAreaHeight - Height;

            // Disable windows resizing.
            // see cref="https://docs.microsoft.com/tr-tr/dotnet/api/system.windows.window.resizemode"
            ResizeMode = ResizeMode.NoResize;

            // Title bar and border are not shown.
            // see cref="https://docs.microsoft.com/tr-tr/dotnet/api/system.windows.window.windowstyle"
            WindowStyle = WindowStyle.None;
        }

        /// <summary>
        /// DispatcherTimer setup
        /// </summary>
        DispatcherTimer dispatcherTimer;

        /// <summary>
        /// Initialize Windows Startup Event
        /// </summary>
        private void InitializeWindowsStartupEvent() {
            // It was detected that the window was closed.
            // see cref="https://docs.microsoft.com/tr-tr/dotnet/api/system.windows.window.closing"
            Closing += Watch_Closing;

            // The window is kept at the highest level.
            // see cref="https://docs.microsoft.com/tr-tr/dotnet/api/system.windows.window.topmost"
            Topmost = true;

            // Hide 10 seconds after you start.
            // see cref="https://docs.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatchertimer"
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }

        /// <summary>
        /// Timer Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dispatcherTimer_Tick(object sender, EventArgs e) {

            // Hide
            finalAndClose();

            //  DispatcherTimer setup
            dispatcherTimer.Stop();
        }

        /// <summary>
        /// Closing block
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Watch_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            e.Cancel = true;
        }

        /// <summary>
        /// Initialize Windows Style
        /// </summary>
        private void InitializeWindowsStyle() {
            // Window transparency enabled
            // see cref="https://docs.microsoft.com/en-us/dotnet/api/system.windows.window.allowstransparency"
            AllowsTransparency = true;

            // Min 0.0 - Max 1.0
            Opacity = 0.80;
        }
        #endregion

        #region Window Bind
        /// <summary>
        /// Bind this Window
        /// </summary>
        /// <param name="e"></param>
        private void Bind(PrintJobEvent e) {


            #region Define Event Data
            // Job Status       [ For example: Spooling ] 
            string stringJobStatus = $"{e.JobStatus}";

            // Priority         [ For example: 1 ] 
            string stringPriority = $"{e.Priority}";

            // Document         [ For example: test.pdf ]
            string stringDocument = $"{e.Document}";

            // User             [ For example: Jack     ]
            string stringUser = $"{e.Owner}";

            // Host             [ For example: desktop-computer-100 ]
            string stringHost = $"{e.HostPrintQueue}";

            // JobId            [ For example: 50 ] This day print count
            string stringJobId = $"{e.JobId}";

            // Total Pages      [ For example: 5 ] Print document pages count
            string stringTotalPages = $"{e.TotalPages}";

            // TimeSubmitted    [ For example: 201907181459 ] Print send datetime
            string stringTimeSubmitted = $"{e.TimeSubmitted}";

            #endregion

            // cref="https://docs.microsoft.com/tr-tr/dotnet/api/system.windows.threading.dispatcher"
            Dispatcher.Invoke(() => {

                // Show on his terminating mission
                if (WindowState == WindowState.Minimized)
                    WindowState = WindowState.Normal;

                lblDocument.Content = stringDocument;
                lblHost.Content = stringHost;
                lblJobId.Content = stringJobId;
                lblJobStatus.Content = stringJobStatus;
                lblPriority.Content = stringPriority;
                lblTimeSubmitted.Content = stringTimeSubmitted;
                lblTotalPages.Content = stringTotalPages;
                lblUser.Content = stringUser;
            });
        }

        /// <summary>
        /// Clear Content
        /// </summary>
        private void finalAndClose() {

            // cref="https://docs.microsoft.com/tr-tr/dotnet/api/system.windows.threading.dispatcher"
            Dispatcher.Invoke(() => {

                // Hide on his terminating mission.
                this.WindowState = WindowState.Minimized;

                lblJobStatus.Content = $"";
                lblPriority.Content = $"";
                lblDocument.Content = $"";
                lblUser.Content = $"";
                lblHost.Content = $"";
                lblJobId.Content = $"";
                lblTotalPages.Content = $"";
                lblTimeSubmitted.Content = $"";
            });

        }
        #endregion

        #region Manager Controllers

        /// <summary>
        /// Printer Job Hardware
        /// </summary>
        private PrinterJob hardware;

        /// <summary>
        /// Initialize Hardware Managemnent Tools
        /// </summary>
        /// <see cref="https://azmisahin.github.io/azmisahin-software-hardware-tools-HMT-net-framework/"/>
        private void InitializeHMT() {

            // Job Initalize
            hardware = new PrinterJob();

            // Hardware Watch Authentication
            hardwareWatchAuthentication();

            // Signal Setup
            hardware
                .Signal += Hardware_Signal;

            // Signal Start
            hardware
                .Watcher
                .Watch();
        }

        /// <summary>
        /// Hardware Authentication
        /// </summary>
        private void hardwareWatchAuthentication() {

            #region Define Authentication Parameters

            // Computer User Name
            string user = getAppSetting("User");

            // Computer Password
            string password = getAppSetting("Password");

            // Computer Name or Ip Address Or Local Connetection "."
            string computer = getAppSetting("Computer");

            // Active Directory Domain Name or Workgorup Name
            string domain = getAppSetting("Domain");

            #endregion

            // check app data
            bool isAuthentication = (
                string.IsNullOrEmpty(user) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(computer) ||
                string.IsNullOrEmpty(domain)
                ) && true ? false : true;

            // Hardwate Watch Authentication Set
            if (isAuthentication) {
                hardware
                    .Watcher
                    .Authentication(user, password, computer, domain);
            }
        }

        /// <summary>
        /// Get Application Setting
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// System.Configuration 4.0.0.0
        /// <see cref="https://docs.microsoft.com/tr-tr/dotnet/api/system.configuration.configurationmanager.appsettings/>
        private static string getAppSetting(string key) {
            // Return Application Setting Value
            string result = "";

            try {
                // Read Application Settings
                System.Collections.Specialized.NameValueCollection appSettings = ConfigurationManager.AppSettings;

                // Read Key
                result = appSettings[key] ?? "Not Found";
            }
            catch (ConfigurationErrorsException) {
                Console.WriteLine("Error reading app settings");
                result = null;
            }

            // Return Result
            return result;
        }

        /// <summary>
        /// Hardware Signal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hardware_Signal(object sender, PrintJobEvent e) {

            // Bind Event Data
            Bind(e);

            Console.WriteLine($"Flag -> {e.Flag} - {e.Size}");

            #region Check Signal Flag
            switch (e.Flag) {
                case PrintJobEvent.StatusFlag.Paused:
                    break;
                case PrintJobEvent.StatusFlag.Error:
                    break;
                case PrintJobEvent.StatusFlag.Deleting:
                    break;
                case PrintJobEvent.StatusFlag.Spooling:
                    
                    if (e.StatusMask == 8 && e.TotalPages == 0) {
                        finalAndClose();
                        Console.WriteLine($"1 Pages Printed.[{e.Size}]");

                        #region Run Other Task Trigger
                        _ = SendAsync(e);
                        #endregion
                    }
                    break;
                case PrintJobEvent.StatusFlag.Printing:
                    break;
                case PrintJobEvent.StatusFlag.Offline:
                    break;
                case PrintJobEvent.StatusFlag.Paperout:
                    break;
                case PrintJobEvent.StatusFlag.Printed:
                    break;
                case PrintJobEvent.StatusFlag.Deleted:
                    break;
                case PrintJobEvent.StatusFlag.Blocked_DevQ:
                    break;
                case PrintJobEvent.StatusFlag.User_Intervention_Req:
                    break;
                case PrintJobEvent.StatusFlag.Restart:
                    break;
                case PrintJobEvent.StatusFlag.Idled:
                    finalAndClose();
                    Console.WriteLine($"{e.PagesPrinted} Pages Printed.[{e.Size}] Total : {e.TotalPages}");

                    #region Run Other Task Trigger
                    _ = SendAsync(e);
                    #endregion

                    break;
                case PrintJobEvent.StatusFlag.Continue:
                    break;
                case PrintJobEvent.StatusFlag.Finalize:
                    if (e.Size == 0) {
                        finalAndClose();
                        Console.WriteLine($"{e.PagesPrinted} Pages Printed.[{e.Size}] Total : {e.TotalPages}");

                        #region Run Other Task Trigger
                        _ = SendAsync(e);
                        #endregion
                    }
                    break;
                default:
                    break;
            }
            #endregion
        }

        /// <summary>
        /// Any Task
        /// </summary>
        private async Task SendAsync(PrintJobEvent arg) {

            // Run
            await Task.Run(() => {
                Console
                .WriteLine(
                    $"Run {Thread.CurrentThread.ManagedThreadId} Thread" +
                    $"With {arg.Flag} argument: {arg.StatusMask}");

                #region Send Remote Web Api Connection

                // String Data
                string data = "";
                // Api Adres
                string api = "";
                // Remote Uri
                Uri uri;

                try {
                    // Initialize Data Serializer
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    // data set
                    data = serializer.Serialize(arg);
                    // api set
                    api = getAppSetting("api");
                    // Remote Web Service
                    uri = new Uri(api);
                    // Create a request using a URL that can receive a post. 
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
                    // Request method
                    request.Method = "post";
                    // Convert POST data to a byte array.
                    byte[] byteArray = Encoding.UTF8.GetBytes(data);
                    // Set the ContentType property of the WebRequest.
                    request.ContentType = "application/json; charset=UTF-8";
                    request.Accept = "application/json";
                    // Set the ContentLength property of the WebRequest.
                    request.ContentLength = byteArray.Length;
                    // Get the request stream.
                    Stream dataStream = request.GetRequestStream();
                    // Write the data to the request stream.
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    // Close the Stream object.
                    dataStream.Close();
                }
                catch (Exception) {

                    Console.WriteLine($"Connection Fail {api}");
                }
                #endregion

            });
        }

        #endregion
    }
}
