using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace MessagesWinAPI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("USER32.DLL")]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint Msg, int wParam, string lParam);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        IntPtr window = new IntPtr(0);
        const uint WM_SETTEXT = 0x0C; // встановлення тексту заголовку
        const uint WM_CLOSE = 0x0010; // закрити вікно

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            window = FindWindow(null, windowName.Text.ToString());
            if (window.ToInt32() == 0)
                windowInfo.Content = "Window not founded";
            else
                windowInfo.Content = "Focused on: " + windowName.Text;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (window.ToInt32() == 0)
                windowInfo.Content = "Window not focused";
            else
            {
                SendMessage(window, WM_SETTEXT, 0, windowName.Text);
                windowInfo.Content = "Focused on: " + windowName.Text;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (window.ToInt32() == 0)
                windowInfo.Content = "Window not focused";
            else
            {
                SendMessage(window, WM_CLOSE, 0, windowName.Text);
                windowInfo.Content = "Window is closed";
            }
        }

        private void Sound_Click(object sender, RoutedEventArgs e)
        {
            int frequency = 400;
            while(frequency <= 1000)
            {
                Console.Beep(frequency, 500);
                frequency+=100;
            }
            while (frequency > 400)
            {
                frequency -= 100;
                Console.Beep(frequency, 450);
            }
        }
    }
}
