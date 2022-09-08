using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);

        public List<Process> processes { get; set; }
        private DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();

            timer.Tick += Timer_GetProcesses;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void Timer_GetProcesses(object sender, EventArgs e)
        {
            int procId = -1;
            if (processesList.SelectedIndex != -1)
            {
                procId = processes[processesList.SelectedIndex].Id;
            }
            processes = Process.GetProcesses().ToList();
            processesList.ItemsSource = processes;
            if (procId != -1)
            {
                int selected = processes.FindIndex(p => p.Id == procId);
                if(selected != -1)
                    processesList.SelectedIndex = selected;
            }
        }

        private void EndTask_Click(object sender, RoutedEventArgs e)
        {
            if (processesList.SelectedIndex != -1)
            {
                processes[processesList.SelectedIndex].Kill();
            }
            else
            {
                MessageBox(new IntPtr(0), "Task is not selected", "", 0);
            }
        }
    }
}
