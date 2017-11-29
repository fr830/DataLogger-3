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
        public double GetMeasurement()
        {
            
            Random rand = new Random();
            double measurement = rand.Next(1, 10);
            return measurement;
            
        }

    }
}
