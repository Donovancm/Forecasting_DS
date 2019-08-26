using Forecasting_DS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Forecasting_DS.Graph
{
    public partial class ForecastForm : Form
    {
        private double[] demandValues;
        private double[] timeValues;
        //private double _bestAlpha;
        //private double _bestBeta;
        //private double _bestGamma;
        //private double _lowestSSE;

        public ForecastForm(Dictionary<int, Forecasting> data)
        {
            demandValues = GetDemandValues(data);
            timeValues = GetTimeValues(data);


            InitializeComponent();
        }

        private double[] GetDemandValues(Dictionary<int, Forecasting> data)
        {
            var demands = new List<double>();
            foreach (var item in data)
            {
                if (item.Key > 0)
                {
                    demands.Add(item.Value.ActualDemand);
                }
               
            }
            return demands.ToArray();
        }
        private double[] GetTimeValues(Dictionary<int, Forecasting> data)
        {
            var times = new List<double>();
            foreach (var item in data)
            {
                if (item.Key > 0)
                {
                    times.Add(item.Key);
                }
            }
            return times.ToArray();
        }
        private void ForecastForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < demandValues.Length; i++)
            {
                chart1.Series["Demand"].Points.AddXY(timeValues[i], demandValues[i]);
            }
            chart1.Series["Demand"].ChartType = SeriesChartType.FastLine;
            chart1.Series["Demand"].Color = Color.Blue;
        }
    }
}
