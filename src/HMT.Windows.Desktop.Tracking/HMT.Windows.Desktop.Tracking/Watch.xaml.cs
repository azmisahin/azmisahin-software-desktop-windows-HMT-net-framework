namespace HMT.Windows.Desktop.Tracking {
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
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
        /// Initialize Windows Startup Event
        /// </summary>
        private void InitializeWindowsStartupEvent() {
            // It was detected that the window was closed.
            // see cref="https://docs.microsoft.com/tr-tr/dotnet/api/system.windows.window.closing"
            Closing += Watch_Closing;

            // The window is kept at the highest level.
            // see cref="https://docs.microsoft.com/tr-tr/dotnet/api/system.windows.window.topmost"
            Topmost = true;
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
        private void clearContent() {

            // cref="https://docs.microsoft.com/tr-tr/dotnet/api/system.windows.threading.dispatcher"
            Dispatcher.Invoke(() => {
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

            // Signal Setup
            hardware
                .Signal += Hardware_Signal;

            // Signal Start
            hardware
                .Watcher
                .Watch();
        }

        /// <summary>
        /// Hardware Signal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hardware_Signal(object sender, PrintJobEvent e) {

            // Bind Event Data
            Bind(e);

            #region Check Signal Flag
            switch (e.Flag) {
                case PrintJobEvent.StatusFlag.Paused:
                    break;
                case PrintJobEvent.StatusFlag.Error:
                    break;
                case PrintJobEvent.StatusFlag.Deleting:
                    break;
                case PrintJobEvent.StatusFlag.Spooling:
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
                case PrintJobEvent.StatusFlag.Continue: // Signal CONTINUE
                    break;
                case PrintJobEvent.StatusFlag.Finalize: // Signal END
                    clearContent();
                    break;
                default:                                // Signal START
                    if (e.StatusMask == 0 && e.TotalPages == 0) {
                        // First data
                    }
                    break;
            }
            #endregion

            #region Run Other Task Trigger
            _ = SendAsync(e.Flag);
            #endregion

        }

        /// <summary>
        /// Any Task
        /// </summary>
        private async Task SendAsync(object arg) {

            // Run
            await Task.Run(() => {
                Console
                .WriteLine(
                    $"Run {Thread.CurrentThread.ManagedThreadId} Thread" +
                    $"With {arg} argument");
            });
        }

        #endregion
    }
}
