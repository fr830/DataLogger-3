using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger
{
    interface IMeasuringDevice
    {
        // This method will return a decimal that represents the celcius 
        // value of the most recent measurement that was captured
        double CelciusValue(double mostRecentMeasurement);


        // This method will return a decimal that represents the fahrenheit
        // value of the most recent measurement that was captured
        double FahrenheitValue(double mostRecentMeasurement);


        // This method will start the device running. It will begin collecting measurements and record them.
        double StartCollecting(Units unit);


        // This method will stop the device. It will cease collecting measurements.
        bool StopCollecting();


        //  This method will retrieve a copy of all of the recent data that the measuring 
        // device has captured. The data will be returned as an array of integer values.
        double[] GetRawData();

    }
}
