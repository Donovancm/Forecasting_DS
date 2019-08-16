using Forecasting_DS.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting_DS.Reader
{


    public class CsvReader
    {
        enum ForcastingFormat
        {
            ActualDemand = 1,
            Level = 2,
            Trend = 3,
            SeasonalAdjustment = 4,
            OneStepError = 5,
            ForecastError = 6,
            SquaredError = 7,
        }

        public class AddForecastingValue
        {
            public int index;
            public float value;
            public int key;
        }
        public static Dictionary<int, Forecasting> data;

        public static Dictionary<int, Forecasting>  GetData()
        {
            // init variables
            data = new Dictionary<int, Forecasting>();
            DictionaryForecast();
            return data;
        }
        public static void DictionaryForecast()
        {
            List<string> list = new List<string>();
            using (StreamReader reader = new StreamReader("../../Data/HoltWintersSeasonal.csv"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line); 
                    Console.WriteLine(line); 
                }
            }

            int row = 0;
            
            foreach (var item in list)
            {
                // After the the first row comes forecast data
                if (row > 0)
                {
                    for (int i = 1; i <= list.Count - 1; i++)
                    {
                        string[] forecastData = item.Split(',');
                        int key = 0;
                        int time = 0;
                        for (int index = 0; index < forecastData.Length; index++)
                        {
                            if (index == 0)
                            {
                                time = int.Parse(forecastData[index]);
                                key = time;

                                if (data.ContainsKey(time))
                                {
                                    AddForecastingToDictionary(data, CreateAddForecastingValue(time, ConvertToFloat(forecastData[index]), index));
                                }
                                else { data.Add(time, new Forecasting()); };
                            }
                            else
                            {
                                AddForecastingToDictionary(data, CreateAddForecastingValue(time, ConvertToFloat(forecastData[index]), index));
                            }
                        }
                    }
                }
                row++;
            }
        }
        
        private static AddForecastingValue CreateAddForecastingValue(int key, float value, int index)
        {
            var values = new AddForecastingValue();
            values.key = key;
            values.value = value;
            values.index = index;
            return values;
        }

        private static void AddForecastingToDictionary(Dictionary<int, Forecasting> data, AddForecastingValue values)
        {
            //TODO duidelijk maken
            switch (values.index)
            {
                case (int) ForcastingFormat.ActualDemand:
                    data[values.key].ActualDemand = values.value; 
                    break;
                case (int)ForcastingFormat.Level:
                    data[values.key].Level = values.value;
                    break;
                case (int)ForcastingFormat.Trend:
                    data[values.key].Trend = values.value;
                    break;
                case (int)ForcastingFormat.SeasonalAdjustment:
                    data[values.key].SeasonalAdjustment = values.value;
                    break;
                case (int)ForcastingFormat.OneStepError:
                    data[values.key].OneStepForecast = values.value;
                    break;
                case (int)ForcastingFormat.ForecastError:
                    data[values.key].ForecastError = values.value;
                    break;
                case (int)ForcastingFormat.SquaredError:
                    data[values.key].SquaredError = values.value;
                    break;
                default:
                    break;
            }
        }

        private static float ConvertToFloat(string value)
        {
            if (value == "")
            {
                return 0f;
            }
            return float.Parse(value);
        }


    }
}
