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
        public float cT;
        public float l;
        public float oneStepForecastError;
        public float forecastError;
        public float[] sT;
        public float[] bT;
        public int current;
    }

    public class FactorsVariables
    {
        public float alpha;
        public float delta;
        public float gamma;
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

        public float ForecastErrorCalc(float xT, float oneStepError) {
            return xT - oneStepError;
        }
        public float OneStepErrorCalc(float sT, float bT, float cT) {
            return (sT + bT) * cT; 
        }
        public float SmoothLevelCalc(SmoothingVariables levelVariables, FactorsVariables factorsVariables)
        {
            //sT-1 + Bt-1 + alpha * (forecastError/Ct)
            levelVariables.oneStepForecastError = OneStepErrorCalc(levelVariables.sT[levelVariables.current - 1], levelVariables.bT[levelVariables.current - 1], levelVariables.cT);
            levelVariables.forecastError = ForecastErrorCalc(levelVariables.xT, levelVariables.oneStepForecastError);

            float sT = levelVariables.sT[levelVariables.current - 1] + levelVariables.bT[levelVariables.current - 1] + factorsVariables.alpha * (levelVariables.forecastError / levelVariables.cT);
            return sT;
        }

        public float SmoothTrendCalc(SmoothingVariables trendVariables, FactorsVariables factorsVariables)
        {
            // Bt = bt-1 + gemma * alpha * (forecastError/ct)
            trendVariables.oneStepForecastError = OneStepErrorCalc(trendVariables.sT[trendVariables.current - 1], trendVariables.bT[trendVariables.current - 1], trendVariables.cT);
            trendVariables.forecastError = ForecastErrorCalc(trendVariables.xT, trendVariables.oneStepForecastError);

            float bT = trendVariables.bT[trendVariables.current - 1] + factorsVariables.gamma * factorsVariables.alpha * (trendVariables.forecastError / trendVariables.cT);
            return bT;
        }

        public float SmoothSeasonalCalc(SmoothingVariables seasonalVariables, FactorsVariables factorsVariables)
        {
            // Ct = cT + delta * (1 - alpha ) * ForecastError /(sT-1 + bT-1)
            seasonalVariables.oneStepForecastError = OneStepErrorCalc(seasonalVariables.sT[seasonalVariables.current-1],seasonalVariables.bT[seasonalVariables.current-1],seasonalVariables.cT);
            seasonalVariables.forecastError = ForecastErrorCalc(seasonalVariables.xT, seasonalVariables.oneStepForecastError);

            float cT = seasonalVariables.cT + factorsVariables.delta * (1 - factorsVariables.alpha) * seasonalVariables.forecastError / (seasonalVariables.sT[seasonalVariables.current - 1] + seasonalVariables.bT[seasonalVariables.current - 1]);
            return cT;
        }
    }
}
