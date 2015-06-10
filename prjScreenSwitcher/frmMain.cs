using System;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;
using prjScreenSwitcher.Utilities;
using prjScreenSwitcher.ProcessManagement;
using System.Security.Principal;
using prjScreenSwitcher.Extensions;

namespace prjScreenSwitcher
{
    public partial class frmMain : Form
    {
        #region Fields

        private ManagementEventWatcher ProcStartWatch =
            new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));

        private ManagementEventWatcher ProcStopWatch =
            new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace"));

        private ProcessManager ProcManager = new ProcessManager();

        // Populating the list box for the processes or refreshing its contents.
        // Used to determine if the "SelectedProcessChanged" should be fired.
        private bool IsPopulatingProcList = false;

        private Timer ScreenshotRefreshTimer = new Timer();

        #endregion

        #region Constructors

        public frmMain()
        {
            InitializeComponent();

            cboMonitors.DisplayMember = "DeviceName";
            cboMonitors.DataSource = SystemScreens.ToList();

            lstProcesses.DisplayMember = "ProcessName";
            lstProcesses.DataSource = ProcManager.WindowableProcesses;

            ProcManager.SelectedProcessChanged += new ProcessManager.ProcessChangedEventArgs(frmMain_SelectedProcessChanged);

            ScreenshotRefreshTimer.Interval = 30;
            ScreenshotRefreshTimer.Tick += ScreenshotRefreshTimer_Tick;

            TryInitProcWatchers();
        }

        #endregion

        #region Methods

        private void TryInitProcWatchers()
        {
            // Attempts to initialize the process start watch and the process stop watch.
            // These watchers are used to update the process list and allow a easier-to-use GUI.
            try
            {
                // Using these watchers requires administrator privileges.
                // If the user is not an administrator, then don't bother initializing the process watchers.
                using (WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent())
                {
                    // Get the built-in administrator account.
                    SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);

                    // Compare to the current user.
                    if (windowsIdentity.User == sid)
                    {
                        ProcStartWatch.EventArrived += ProcessWatcher_EventArrived;
                        ProcStopWatch.EventArrived += ProcessWatcher_EventArrived;

                        ProcStartWatch.Start();
                        ProcStopWatch.Start();
                    }
                }
            }
            catch
            {
                try
                {
                    ProcStartWatch.Stop();
                    ProcStopWatch.Stop();
                }
                finally
                {
                    ProcStartWatch.EventArrived -= ProcessWatcher_EventArrived;
                    ProcStopWatch.EventArrived -= ProcessWatcher_EventArrived;

                    ProcStartWatch.SafeDispose();
                    ProcStopWatch.SafeDispose();
                }
            }
        }

        private void PopulateProcessList()
        {
            try
            {
                IsPopulatingProcList = true;

                lstProcesses.DataSource = null;

                lstProcesses.Items.Clear();

                lstProcesses.DisplayMember = "ProcessName";
                lstProcesses.DataSource = ProcManager.WindowableProcesses;
            }
            finally
            {
                IsPopulatingProcList = false;
            }
        }

        private void SetProcessScreenLoc(Screen Destination)
        {
            try
            {
                // Make sure that it the current process has a window.
                if (ProcManager.SelectedProcess != null)
                {
                    if (ProcManager.SelectedProcess.IsWindowable())
                    {
                        // Set the window position by calling the native method.
                        Win32.SetWindowPos(ProcManager.SelectedProcess.MainWindowHandle, this.Handle, Destination.WorkingArea.X,
                                           Destination.WorkingArea.Y, Destination.WorkingArea.Width, Destination.WorkingArea.Height, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to set the location for the process." + Environment.NewLine + ex.Message, "Error");
            }
        }

        private void SetScreenshotImage(Process process)
        {
            this.SuspendLayout();

            try
            {
                if (bmpScreenshot.Image != null)
                {
                    bmpScreenshot.Image.Dispose();
                }

                if (process == null)
                {
                    // If the process is null, stop the timer.
                    ScreenshotRefreshTimer.Stop();
                }
                else
                {
                    bmpScreenshot.Image = process.CaptureScreenshot().Scale(bmpScreenshot.Size);
                }
            }
            catch
            { }
            finally
            {
                this.ResumeLayout(true);
            }
        }

        #endregion

        #region Events

        private void ProcessWatcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            try
            {
                using (Process NewProcess = Process.GetProcessById((int)e.NewEvent.Properties["Id"].Value))
                {
                    if (Win32.IsWindow(NewProcess.MainWindowHandle))
                    {
                        PopulateProcessList();

                        if (lstProcesses.Items.Contains(ProcManager.SelectedProcess) &&
                            ((Process)lstProcesses.SelectedItem != ProcManager.SelectedProcess))
                        {
                            lstProcesses.SelectedItem = ProcManager.SelectedProcess;
                        }
                    }
                }
            }
            catch
            { }
        }

        private void btnGetProcessInfo_Click(object sender, EventArgs e)
        {
            PopulateProcessList();
        }

        private void cboMonitors_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Make sure that that something valid is selected. Stop the method if either is true.
            if ((cboMonitors.SelectedIndex < 0) || (cboMonitors.SelectedItem == null))
            {
                return;
            }

            try
            {
                // Get the selected screen that is stored by cboMonitors.
                Screen SelectedScreen = (cboMonitors.SelectedValue as Screen);

                // Ensure that the screen was able to be casted correctly, then make sure it is a Screen from the system.
                if (SelectedScreen != null && SystemScreens.Any(screen => (screen == cboMonitors.SelectedValue)))
                {
                    // The screen selected is valid and may now be used.
                    if (ProcManager.SelectedProcess != null)
                    {
                        SetProcessScreenLoc(SelectedScreen);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid screen. Please try again.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while handling cboMonitors_SelectedIndexChanged :: " + ex.Message, "Error");
            }
        }

        private void lstProcesses_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ProcManager.SelectedProcess = (lstProcesses.SelectedValue as Process);
            }
            catch
            { }
        }

        private void frmMain_SelectedProcessChanged(Process NewProcess)
        {
            try
            {
                // Only continue if the selected process has changed while not populating the process list.
                // It should be ignored when the process selected is changed when populating the process list.
                if (!IsPopulatingProcList)
                {
                    // Set the text of the "Process Name" TextBox so the user knows
                    // what the currently-selected process is.
                    txtProcessName.Text = NewProcess.ProcessName;

                    // Set the screenshot image to the selected process immediately after the change.
                    SetScreenshotImage(NewProcess);

                    // Start the ScreenshotRefreshTimer to refresh the screenshot of the current
                    // process at regular intervals.
                    ScreenshotRefreshTimer.Start();
                }
            }
            catch
            { }
            finally
            {
                // Reset the state of the buttons and reset other UI.
            }
        }

        private void ScreenshotRefreshTimer_Tick(object sender, EventArgs e)
        {
            SetScreenshotImage(ProcManager.SelectedProcess);
        }

        #endregion

        #region Properties

        private Screen[] SystemScreens
        {
            get
            {
                return Screen.AllScreens;
            }
        }

        #endregion
    }
}