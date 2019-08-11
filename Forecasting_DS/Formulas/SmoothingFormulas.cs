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
        public float[] sT;
        public float[] bT;
        public int current;
    }

    public class FactorsVariables
    {
        public float alpha;
        public float beta;
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

        public float SmoothLevelCalc(SmoothingVariables levelVariables, FactorsVariables factorsVariables)
        {
            // St = alpha * (xT/ (cT- l)) + (1 - alpha) * (sT-1 + bT-1)
            float sT = factorsVariables.alpha * (levelVariables.xT / (levelVariables.cT - levelVariables.l)) + (1 - factorsVariables.alpha) * (levelVariables.sT[levelVariables.current - 1] + levelVariables.bT[levelVariables.current - 1]);

            return sT;
        }

        public float SmoothTrendFormulaCalc(SmoothingVariables trendVariables, FactorsVariables factorsVariables)
        {
            // Bt = beta * (sT - ST-1) + (1 - beta) * Bt-1
            float bT = factorsVariables.beta * (trendVariables.sT[trendVariables.current] - trendVariables.sT[trendVariables.current - 1]) + (1 - factorsVariables.beta) * trendVariables.bT[trendVariables.current - 1];
            return bT;
        }

        public float SmoothSeasonalFormulaCalc(SmoothingVariables seasonalVariables, FactorsVariables factorsVariables)
        {
            // Ct = gamma * (xT / sT) + (1 - gamma) * cT - l
            float cT = factorsVariables.gamma * (seasonalVariables.xT / seasonalVariables.sT[seasonalVariables.current]) + (1 - factorsVariables.gamma) * seasonalVariables.cT - seasonalVariables.l;
            return cT;
        }
    }
}
