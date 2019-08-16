using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting_DS.Model
{
    public class Forecasting
    {
        public float ActualDemand { get; set; }
        public float Level { get; set; }
        public float Trend { get; set; }
        public float SeasonalAdjustment { get; set; }
        public float OneStepForecast { get; set; }
        public float ForecastError { get; set; }
        public float SquaredError { get; set; }

        public Forecasting()
        {

        }
    }
}
