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
            CsvReader.GetData();
            OptimalSmoothingFactor optimalSmoothingFactor = new OptimalSmoothingFactor();
            var data = optimalSmoothingFactor.OptimizeFactors(CsvReader.GetData());
            Application.Run(new ForecastForm(data));
        }
    }
}
