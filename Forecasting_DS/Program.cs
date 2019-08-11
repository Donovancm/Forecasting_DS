using Forecasting_DS.Formulas;
using Forecasting_DS.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting_DS
{
    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            
            CsvReader.GetData();
            SmoothingFormulas sF = new SmoothingFormulas();
            SmoothingVariables smv = sF.CreateEmptySmoothingVariables();
            FactorsVariables fv = sF.CreateEmptyFactorsVariables();

            // set data into variables
            //sT-1 + Bt-1 + alpha * (forecastError/Ct)
            smv.xT = 165f;
            smv.cT = 0.988233399244463f;
            smv.bT = new float[]{ 2.2095f, 3.63860067f };
            smv.current = 1;
            smv.sT = new float[] { 144.42f, 0f };
            smv.forecastError = 20.09583079f;

            fv.alpha = 0.307003546945751f;
            fv.gamma = 0.228914336546831f;
            fv.delta = 0f;

            Console.WriteLine("SmoothLevel: " + sF.SmoothLevelCalc(smv, fv));
            Console.WriteLine("SmoothTrend: " + sF.SmoothTrendCalc(smv, fv));
            Console.WriteLine("SmoothSeasonal: "+sF.SmoothSeasonalCalc(smv, fv));
            Console.ReadLine();
            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
