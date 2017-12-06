using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Reflection;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DataLogger
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        

        public MainPage()
        {
            this.InitializeComponent();
            
        }

        public Units unit;
        MeasureLengthDevice newDevice = new MeasureLengthDevice();
        Device tempDevice = new Device();
       

        // Build a new device and display the measurements in text boxes
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            newDevice.StartCollecting(unit);
            tempTextBlock.Text = newDevice.GetHistory;
            timeTextBlock.Text = tempDevice.GetTime().ToString();
            timeHistory.Text = newDevice.GetTime.ToString();
            tempHistory.Text = newDevice.GetHistory;

        }

        // Set units to use
        private void celciusRadioButton_Checked(object sender, RoutedEventArgs e)
        {          
            unit = Units.Celcius;            
        }

        // Set units to use
        private void fahrenheitRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            unit = Units.Fahrenheit;
        }

        // Stop app from collecting data
        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            newDevice.StopCollecting();
        }

        // Exit the app
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
