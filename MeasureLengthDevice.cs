using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        private MainPage main;
        


        private double data = 0;

        // This field will determine whether the generated measurements 
        // are interpreted in celcius or fahrenheit
        private Units unitsToUse;

        // This field will store a history of a limited set of recently captured measurements.
        // Once the array is full, the class should start overwriting the oldest elements while 
        // continuing to record the newest captures. (You may need some helper fields/variables to go with this one).
        private double[] dataCaptured;

        //  integer - This field will store the most recent measurement captured for convenience of display.
        private double mostRecentMeasure;

        DateTime measureTime;

        private Timer timer;
        private int limit = 10;



        FixedSizeQueue<double> myQueue = new FixedSizeQueue<double>();

        public double GetMeasurement
        {
            get
            {
                return data;
            }
        }

        public string GetHistory
        {
            get
            {
                return PrintValues(myQueue);
            }
        }

        public DateTime GetTime
        {
            get
            {
                return measureTime;
            }
            
        }


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

        public bool StopCollecting()
        {
            dispatcherTimer.Stop();
            return true;
        }

        public double[] GetRawData()
        {
            return dataCaptured;
        }

        public string historyTextBlock { get; set; }

        public void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            
            startTime = DateTimeOffset.Now;
            lastTime = startTime;

             
            myQueue.Limit = limit;
            dispatcherTimer.Start();

            
        }

        async void dispatcherTimer_Tick(object sender, object e)
        {
            main = new MainPage();
            //mostRecentMeasure = device.GetMeasurement();
            //historyTextBlock.Text = mostRecentMeasure;

            await
                Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                   
                    data = device.GetMeasurement();

                    if (unitsToUse == Units.Celcius)
                    {
                        data = CelciusValue(data);
                    }
                    else
                    {
                        data = FahrenheitValue(data);
                    }

                    measureTime = device.GetTime();
                    myQueue.Enqueue(data);

                    
                });

            main.printQueue();

            timesTicked++;
            if (timesTicked > timesToTick)
            {
                //stopTime = time;
                // page.history.Text += "Calling dispatcherTimer.Stop()\n";
                dispatcherTimer.Stop();
                
            }
        }

        public string PrintValues(FixedSizeQueue<double> myCollection)
        {
            StringBuilder myString = new StringBuilder();
            foreach (var i in myCollection.q)
                myString.AppendLine(i.ToString());
            return myString.ToString();
        }

        public class FixedSizeQueue<T>
        {
            public ConcurrentQueue<T> q = new ConcurrentQueue<T>();
            private object lockObject = new object();

            public int Limit { get; set; }
            public void Enqueue(T obj)
            {
                q.Enqueue(obj);
                lock (lockObject)
                {
                    T overflow;
                    while (q.Count > Limit && q.TryDequeue(out overflow)) ;
                }
            }
        }
    }
}
