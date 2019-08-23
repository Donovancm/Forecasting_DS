using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting_DS.Formulas
{
    public class SmoothingVariables
    {
        public float xT;
        public float sT;
        public float lT;
        public float tT;
        public float oneStepForecastError;
        public float forecastError;
    }

    public class FactorsVariables
    {
        public float alpha;
        public float delta;
        public float gamma;
    }

    public class PredictionVariables
    {
        public float t;
        public float lTLock;
        public float[] sT;

    }

    public class SmoothingFormulas
    {
        public SmoothingVariables CreateEmptySmoothingVariables()
        {
            return new SmoothingVariables();
        }

        public FactorsVariables CreateEmptyFactorsVariables()
        {
            return new FactorsVariables();
        }

        public PredictionVariables CreateEmptyPredictionVariables()
        {
            return new PredictionVariables();
        }

        public float ForecastErrorCalc(float xT, float oneStepError) {
            return xT - oneStepError;
        }
        public float OneStepErrorCalc(float lT, float tT, float sT) {
            return (lT + tT) * sT; 
        }

        public float SquardErrorCalc(float forecastError)
        {
            return float.Parse(Math.Pow(Double.Parse(forecastError.ToString()), 2).ToString());
        }

        public float SmoothLevelCalc(SmoothingVariables levelVariables, FactorsVariables factorsVariables)
        {
            //lT-1 + Bt-1 + alpha * (forecastError/Ct)
            //levelVariables.oneStepForecastError = OneStepErrorCalc(levelVariables.lT, levelVariables.tT, levelVariables.sT);
            //levelVariables.forecastError = ForecastErrorCalc(levelVariables.xT, levelVariables.oneStepForecastError);

            float lT = levelVariables.lT + levelVariables.tT + factorsVariables.alpha * (levelVariables.forecastError / levelVariables.sT);
            return lT;
        }

        public float SmoothTrendCalc(SmoothingVariables trendVariables, FactorsVariables factorsVariables)
        {
            // Bt = bt-1 + gemma * alpha * (forecastError/ct)
            //trendVariables.oneStepForecastError = OneStepErrorCalc(trendVariables.lT, trendVariables.tT, trendVariables.sT);
            //trendVariables.forecastError = ForecastErrorCalc(trendVariables.xT, trendVariables.oneStepForecastError);

            float tT = trendVariables.tT + factorsVariables.gamma * factorsVariables.alpha * (trendVariables.forecastError / trendVariables.sT);
            return tT;
        }

        public float SmoothSeasonalCalc(SmoothingVariables seasonalVariables, FactorsVariables factorsVariables)
        {
            // Ct = sT + delta * (1 - alpha ) * ForecastError /(lT-1 + tT-1)
           // seasonalVariables.oneStepForecastError = OneStepErrorCalc(seasonalVariables.lT,seasonalVariables.tT,seasonalVariables.sT);
           // seasonalVariables.forecastError = ForecastErrorCalc(seasonalVariables.xT, seasonalVariables.oneStepForecastError);

            float sT = seasonalVariables.sT + factorsVariables.delta * (1 - factorsVariables.alpha) * seasonalVariables.forecastError / (seasonalVariables.lT + seasonalVariables.tT);
            return sT;
        }
        //public float Prediction(SmoothingVariables smoothVariables, PredictionVariables predictionVariables, FactorsVariables factorsVariables)
        //{
        //    //(lT-1 + (t.current - t-1) * Tt-1 ) * S(vorige 12 maanden)

        //    //float prediction = predictionVariables.lTLock + ((smoothVariables.t[smoothVariables.current] - predictionVariables.t) * smoothVariables.tT) * predictionVariables.sT[12 - 12];
        //    return prediction;
        // }
    }
}
