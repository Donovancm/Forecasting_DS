using Forecasting_DS.Formulas;
using Forecasting_DS.Graph;
using Forecasting_DS.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forecasting_DS
{
    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            
            //!!
            //Check smoothing factors value on errors
            //!!
            CsvReader.GetData();
            OptimalSmoothingFactor optimalSmoothingFactor = new OptimalSmoothingFactor();
            var data = optimalSmoothingFactor.OptimizeFactors(CsvReader.GetData());
            Application.Run(new ForecastForm(data));
            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
