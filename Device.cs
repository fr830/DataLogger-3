using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DataLogger
{
    class Device
    {
        DateTime measureTime;
        double temp;

        public Device()
        {
            measureTime = DateTime.Now;
            temp = GetMeasurement();
        }
        
        public double GetMeasurement()
        {
            
            Random rand = new Random();
            double measurement = rand.Next(1, 10);

            return measurement;            
        }

        public DateTime GetTime()
        {
            measureTime = DateTime.Now;
            return measureTime;
        }

        public Object tempTime()
        {

            double measurement = GetMeasurement();
            DateTime measureTime = GetTime();
            return measurement + "\t" + measureTime;
        }

    }
}
