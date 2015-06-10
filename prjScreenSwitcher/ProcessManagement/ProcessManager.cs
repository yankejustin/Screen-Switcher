using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using prjScreenSwitcher.Utilities;
using System.Linq;

namespace prjScreenSwitcher.ProcessManagement
{
    public class ProcessManager : INotifyPropertyChanged
    {
        #region Event Resources

        public delegate void ProcessChangedEventArgs(Process sender);
        public event ProcessChangedEventArgs SelectedProcessChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Properties

        public List<Process> WindowableProcesses
        {
            get
            {
                List<Process> ValidProcesses = new List<Process>();

                foreach (Process p in Process.GetProcesses())
                {
                    try
                    {
                        if (Win32.IsWindow(p.MainWindowHandle))
                        {
                            ValidProcesses.Add(p);
                        }
                    }
                    // Likely was not able to get the MainWindowHandle for the process.
                    // Just ignore it.
                    catch
                    { }
                }

                return ValidProcesses.OrderBy(proc => proc.ProcessName).ToList();
            }
        }

        private Process _selectedProcess;
        public Process SelectedProcess
        {
            get
            {
                return _selectedProcess;
            }
            set
            {
                if (_selectedProcess != value)
                {
                    _selectedProcess = value;

                    if (SelectedProcessChanged != null)
                    {
                        SelectedProcessChanged(value);
                    }
                }
            }
        }

        #endregion
    }
}