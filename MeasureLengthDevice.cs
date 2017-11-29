using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DataLogger
{
    class MeasureLengthDevice : IMeasuringDevice
    {
        DispatcherTimer dispatcherTimer;
        DateTimeOffset startTime;
        DateTimeOffset lastTime;
        DateTimeOffset stopTime;
        int timesTicked = 1;
        int timesToTick = 10;

        private Device device;


        private int data = 0;

        // This field will determine whether the generated measurements 
        // are interpreted in celcius or fahrenheit
        private Units unitsToUse;

        // This field will store a history of a limited set of recently captured measurements.
        // Once the array is full, the class should start overwriting the oldest elements while 
        // continuing to record the newest captures. (You may need some helper fields/variables to go with this one).
        private double[] dataCaptured;

        //  integer - This field will store the most recent measurement captured for convenience of display.
        private double mostRecentMeasure;


        public double CelciusValue(double mostRecentMeasure)
        {
            mostRecentMeasure += 273.15;
            return mostRecentMeasure;
        }

        public double FahrenheitValue(double mostRecentMeasure)
        {
            mostRecentMeasure = 1.8 * (mostRecentMeasure - 273) + 32;
            return mostRecentMeasure;
        }

        public double StartCollecting(Units unit)
        {
            unitsToUse = unit;
            //Device newDevice = new Device();
            device = new Device();
            DispatcherTimerSetup();
            mostRecentMeasure = device.GetMeasurement();
            
            if(unitsToUse == Units.Celcius)
            {
                mostRecentMeasure = CelciusValue(mostRecentMeasure);
            }
            else
            {
                mostRecentMeasure = FahrenheitValue(mostRecentMeasure);
            }

            return mostRecentMeasure;
        }

        bool IMeasuringDevice.StopCollecting()
        {
            dispatcherTimer.Stop();
            return true;
        }

        double[] IMeasuringDevice.GetRawData()
        {
            return dataCaptured;
        }

        public object timeTextBox { get; private set; }

        public void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            //IsEnabled defaults to false
            //historyTextBlock.Text += "dispatcherTimer.IsEnabled = " + dispatcherTimer.IsEnabled + "\n";
            startTime = DateTimeOffset.Now;
            lastTime = startTime;
            // page.history.Text += "Calling dispatcherTimer.Start()\n";
            dispatcherTimer.Start();
            //IsEnabled should now be true after calling start
            // page.history.Text += "dispatcherTimer.IsEnabled = " + dispatcherTimer.IsEnabled + "\n";
        }

        void dispatcherTimer_Tick(object sender, object e)
        {
            //Device device = new Device();
            
            DateTimeOffset time = DateTimeOffset.Now;
            TimeSpan span = time - lastTime;
            lastTime = time;
            mostRecentMeasure = device.GetMeasurement();
            //Time since last tick should be very very close to Interval
            // page.history.Text += timesTicked + "\t time since last tick: " + span.ToString() + "\n";
            timesTicked++;
            if (timesTicked > timesToTick)
            {
                stopTime = time;
                // page.history.Text += "Calling dispatcherTimer.Stop()\n";
                dispatcherTimer.Stop();
                //IsEnabled should now be false after calling stop
                // page.history.Text += "dispatcherTimer.IsEnabled = " + dispatcherTimer.IsEnabled + "\n";
                span = stopTime - startTime;
                // page.history.Text += "Total Time Start-Stop: " + span.ToString() + "\n";
            }
        }
    }
}
