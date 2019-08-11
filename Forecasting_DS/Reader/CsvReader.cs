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

        public static float[,] GetData()
        {
            // init variables
            float[,] forecastMatrix = CreateMatrix();
            // setup Offers Matrix = Points
            SetupOffersMatrix(forecastMatrix);
            return forecastMatrix;
        }

        public static float[,] CreateMatrix()
        {
            //Skip last row and last column //
            return new float[60, 8];

        }

        public static void SetupOffersMatrix(float[,] matrix)
        {
            List<string> list = new List<string>();
            using (StreamReader reader = new StreamReader("../../Data/HoltWintersSeasonal.csv"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line); // Add to clusterPoints.
                    Console.WriteLine(line); // Write to console.
                }
            }

            int row = 0;
            //string[] userArray = { };
            foreach (var item in list)
            {
                // After the the first row comes forcast data
                if (row > 0)
                {

                    // set matrix of  forecast csv table

                    for (int i = 0; i <= matrix.GetLength(1)-1; i++)
                    {
                        string[] forecastData = item.Split(',');
                        if (forecastData[i] == "")
                        {
                            // if  useroffer is empty string then set 0 in matrix
                            matrix[row - 1, i] = 0f;
                        }
                        else
                        {
                            //else useroffer is not empty then set 1 in matrix
                            matrix[row - 1, i] = float.Parse(forecastData[i]);
                            
                        }
                    }
                }
                // set row index to the next line/ the follow up productid of users offers
                row++;
            }
        }


    }
}
