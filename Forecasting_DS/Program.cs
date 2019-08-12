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
            PredictionVariables pv = sF.CreateEmptyPredictionVariables() ;

            // set data into variables
            //sT-1 + Bt-1 + alpha * (forecastError/Ct)
            //smv.xT = 165f;
            //smv.sT = 0.988233399244463f;
            //smv.tT = new float[]{ 2.2095f, 3.63860067f };
            smv.current = 1;
            smv.tT = new float[] {5.21470068f, 0f };
            //smv.forecastError = 20.09583079f;
            smv.t = new float[] { 36, 37 };

            //fv.alpha = 0.307003546945751f;
            //fv.gamma = 0.228914336546831f;
            //fv.delta = 0f;

            pv.lTLock = 243.105799f;
            pv.t = 36f;
            pv.sT = new float[] {0.988233399f,1.039459514f,0.932933292f
,0.912597756f
,1.043010605f
,0.906442452f
,0.920837589f
,0.926620944f
,0.988490753f
,1.016201453f
,1.048052656f
,1.204004908f
 };


           // Console.WriteLine("SmoothLevel: " + sF.SmoothLevelCalc(smv, fv));
            //Console.WriteLine("SmoothTrend: " + sF.SmoothTrendCalc(smv, fv));
            //Console.WriteLine("SmoothSeasonal: "+sF.SmoothSeasonalCalc(smv, fv));
            Console.WriteLine("Prediction: " + sF.Prediction(smv, pv, fv));
            Console.ReadLine();
            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
