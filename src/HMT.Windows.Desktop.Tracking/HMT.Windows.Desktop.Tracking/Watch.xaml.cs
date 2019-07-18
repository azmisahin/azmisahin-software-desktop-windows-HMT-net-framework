namespace HMT.Windows.Desktop.Tracking {
    using System.Windows;

    /// <summary>
    /// Watch Screen
    /// </summary>
    /// <see cref="https://docs.microsoft.com/tr-tr/dotnet/api/system.windows.window"/>
    public partial class Watch : Window {
        /// <summary>
        /// Watch
        /// </summary>
        public Watch() {
            InitializeComponent();

            InitializeWindowsStartupConfiguration();

            InitializeWindowsStartupEvent();

            InitializeWindowsStyle();
        }

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
    }
}
