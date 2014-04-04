using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PerlinNoise
{
    class Program
    {
        public static int NormalizeColorValue(double value)
        {
            var result = (int)value;
            if (result > 255)
            {
                result = 255;
            }
            if (result < 0)
            {
                result = 0;
            }
            return result;
        }

        static void Main(string[] args)
        {
            var random = new Random();
            var size = 256;
            var bitMap = new Bitmap(size, size);

            int baseFrequency = 3;
            double[] persistance = { 0.5, 0.35, 0.15};

            var gridList = new List<Tuple<GridArgbWrapper, double>>();

            for (int i = 1; i <= 3; i++)
            {
                var freq = (int) Math.Pow(baseFrequency, i);
                var grid = new GridArgbWrapper(size, freq)//,  new SpiralValueGiver(random.Next(120, 255), random.Next(0, 120)))
                    { TurnColor = Color.White};

                gridList.Add(new Tuple<GridArgbWrapper, double>(grid, persistance[i - 1]));
            }

            for (int i = 0; i < bitMap.Size.Width; i++)
            {
                for (int j = 0; j < bitMap.Size.Height; j++)
                {
                    
                    bitMap.SetPixel(i, j , Color.FromArgb(
                                             NormalizeColorValue(gridList.Sum(tuple => tuple.Item1.GetAlpha(i, j) * tuple.Item2)),
                                             NormalizeColorValue(gridList.Sum(tuple => tuple.Item1.GetRed(i, j) * tuple.Item2)),
                                             NormalizeColorValue(gridList.Sum(tuple => tuple.Item1.GetGreen(i, j) * tuple.Item2)),
                                             NormalizeColorValue(gridList.Sum(tuple => tuple.Item1.GetBlue(i, j) * tuple.Item2))
                                           )
                                           );
                    
                }
            }

            bitMap.Save("perlinWithTurnColor3.bmp");
        }
    }



}
