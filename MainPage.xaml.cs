using System;
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

        //private MeasureLengthDevice measure;// = new MeasureLengthDevice();
        //measure = MeasureLengthDevice();
        Units unit;

        public TextBlock history { get { return historyTextBlock; } }
        MeasureLengthDevice newDevice = new MeasureLengthDevice();

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            newDevice.StartCollecting(unit);

            //tempTextBlock.Text = newDevice.StartCollecting(unit).ToString();
            tempTextBlock.Text = newDevice.GetHistory;

            timeTextBlock.Text = newDevice.GetTime.ToString();

            historyTextBlock.Text = newDevice.GetHistory;

        }

        private void celciusRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            
            unit = Units.Celcius;
            
        }

        private void fahrenheitRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            unit = Units.Fahrenheit;
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            newDevice.StopCollecting();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        public void printQueue()
        {
            
            tempTextBlock.Text = newDevice.GetHistory;
            timeTextBlock.Text = "this"; //newDevice.GetTime.ToString();
            historyTextBlock.Text = newDevice.GetHistory;
        }
    }
}
