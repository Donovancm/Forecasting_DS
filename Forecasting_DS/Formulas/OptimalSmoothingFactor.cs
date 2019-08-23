using Forecasting_DS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting_DS.Formulas
{
    class OptimalSmoothingFactor
    {
        float containSSE = float.MaxValue;
        float bestAlpha = float.MaxValue;
        float bestBeta = float.MaxValue;
        float bestGamma = float.MaxValue;
        float alpha = float.MaxValue;
        float beta = float.MaxValue;
        float gamma = float.MaxValue;
        float stepValue = 0.1f;
        //SSEbewaar=MAXINT
        public void OptimizeFactors(Dictionary<int, Forecasting> data)
        {
            
            for (float a = 0; a <= 1; a = a + stepValue)
            {
                for (float b = 0; b <= 1; b = b + stepValue)
                {
                    for (float c = 0; c <= 1; c = c + stepValue)
                    {
                        alpha = a;
                        beta = b;
                        gamma = c;
                        ImproveSmoothingFactor(data);
                        float sse = CalculateSSE(data);
                        if (sse < containSSE)
                        {
                            containSSE = sse;
                            bestAlpha = a;
                            bestBeta = b;
                            bestGamma = c;

                        }
                       
                    }
                }
            }

            float newStepValue = stepValue / 10;
            for (float a = (bestAlpha - stepValue); a <= (bestAlpha + stepValue); a = a + newStepValue)
            {
                for (float b = (bestBeta - stepValue); b <= (bestBeta + stepValue); b = b + newStepValue)
                {
                    for (float c = (bestGamma - stepValue); c <= (bestGamma + stepValue); c = c + newStepValue)
                    {
                        alpha = a;
                        beta = b;
                        gamma = c;
                        ImproveSmoothingFactor(data);
                        float sse = CalculateSSE(data);
                        if (sse < containSSE)
                        {
                            containSSE = sse;
                            bestAlpha = a;
                            bestBeta = b;
                            bestGamma = c;

                        }
                    }
                }

            }

        }

        private float CalculateSSE(Dictionary<int, Forecasting> data)
        {
            float sse = 0f;
            foreach (var item in data)
            {
                sse = sse + item.Value.SquaredError;
            }
           
            return sse;
        }

        private void ImproveSmoothingFactor(Dictionary<int, Forecasting> data)
        {
            var formulas = new SmoothingFormulas();
            FactorsVariables newFactorsVariables = new FactorsVariables();
            newFactorsVariables.alpha = alpha;
            newFactorsVariables.delta = beta;
            newFactorsVariables.gamma = gamma;

            SmoothingVariables newSmoothingVariables = new SmoothingVariables();
            int key = -11;
            foreach (var item in data)
            {
                
                if (item.Key == 1)
                {
                    //magic sequence
                    item.Value.OneStepForecast = formulas.OneStepErrorCalc(data[0].Level, data[0].Trend, data[key].SeasonalAdjustment);
                    item.Value.ForecastError = formulas.ForecastErrorCalc(item.Value.ActualDemand, item.Value.OneStepForecast);
                    item.Value.SquaredError = formulas.SquardErrorCalc(item.Value.ForecastError);// debug

                    newSmoothingVariables.forecastError = item.Value.ForecastError;
                    newSmoothingVariables.oneStepForecastError = item.Value.OneStepForecast;
                    newSmoothingVariables.xT = data[item.Key -1].ActualDemand;
                    newSmoothingVariables.lT = data[item.Key - 1].Level;
                    newSmoothingVariables.sT = data[key].SeasonalAdjustment;
                    newSmoothingVariables.tT = data[item.Key - 1].Trend;


                    //TODO make formulas simple
                    newSmoothingVariables.lT = item.Value.Level = formulas.SmoothLevelCalc(newSmoothingVariables, newFactorsVariables);
                    newSmoothingVariables.tT = item.Value.Trend = formulas.SmoothTrendCalc(newSmoothingVariables, newFactorsVariables);
                    newSmoothingVariables.sT =  item.Value.SeasonalAdjustment = formulas.SmoothSeasonalCalc(newSmoothingVariables, newFactorsVariables);
                    key++;
                }
                else if(item.Key > 1 && item.Key <= 36)
                {
                    //magic all
                    //Paint
                    item.Value.OneStepForecast = formulas.OneStepErrorCalc(data[item.Key-1].Level, data[item.Key - 1].Trend, data[key].SeasonalAdjustment);
                    item.Value.ForecastError = formulas.ForecastErrorCalc(item.Value.ActualDemand, item.Value.OneStepForecast);
                    item.Value.SquaredError = formulas.SquardErrorCalc(item.Value.ForecastError); 

                    newSmoothingVariables.forecastError = item.Value.ForecastError;
                    newSmoothingVariables.oneStepForecastError = item.Value.OneStepForecast;
                    newSmoothingVariables.xT = data[item.Key - 1].ActualDemand;
                    newSmoothingVariables.lT = data[item.Key - 1].Level;
                    newSmoothingVariables.sT = data[key].SeasonalAdjustment;
                    newSmoothingVariables.tT = data[item.Key - 1].Trend;
                    //TODO make formulas simple
                    newSmoothingVariables.lT = item.Value.Level = formulas.SmoothLevelCalc(newSmoothingVariables, newFactorsVariables);
                    newSmoothingVariables.tT = item.Value.Trend = formulas.SmoothTrendCalc(newSmoothingVariables, newFactorsVariables);
                    newSmoothingVariables.sT = item.Value.SeasonalAdjustment = formulas.SmoothSeasonalCalc(newSmoothingVariables, newFactorsVariables);
                    key++;
                }
            }
        }

    }

}
