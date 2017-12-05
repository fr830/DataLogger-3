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

        //private MeasureLengthDevice measure;// = new MeasureLengthDevice();
        //measure = MeasureLengthDevice();
        public Units unit;
        bool appStart = false;
        //Units unit { get; set; }
        MeasureLengthDevice newDevice = new MeasureLengthDevice();
        Device tempDevice = new Device();

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            appStart = true;
            newDevice.StartCollecting(unit);

            object testThis;
            testThis = tempDevice.tempTime();

            //tempTextBlock.Text = propertyInfo.ToString();
            tempTextBlock.Text = newDevice.GetMeasurement.ToString();
            timeTextBlock.Text = newDevice.GetTime.ToString();
            historyTextBlock.Text = newDevice.GetHistory;

        }

        private void celciusRadioButton_Checked(object sender, RoutedEventArgs e)
        {          
            unit = Units.Celcius;            

            if(appStart == true)
            {

            }
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

        public Units printQueue()
        {
            return unit;

        }

        
    }
}
