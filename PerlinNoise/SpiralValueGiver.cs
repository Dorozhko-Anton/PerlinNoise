using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerlinNoise
{
    class SpiralValueGiver : IKnotValueParser
    {
        private static readonly Random _random = new Random(DateTime.Now.Millisecond);
        private readonly double _maxValue;
        private readonly double _minValue;
        
        private double[,] _values;
        private int _valuesForSize;

        private void InitValues()
        {
            _valuesForSize = Size;
            _values = new double[Size, Size];
            int sizeX = Size;
            int sizeY = Size;
            int summ = sizeX * sizeY;
            int correctY = 0;
            int correctX = 0;
            int count = 1;
            double step = (_maxValue - _minValue) / (Size * Size);

            while (sizeY > 0)
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < ((sizeX < sizeY) ? sizeY : sizeX); x++)
                    {
                        if (y == 0 && x < sizeX - correctX && count <= summ)
                            _values[y + correctY, x + correctX] = _maxValue - step * count++;
                        if (y == 1 && x < sizeY - correctY && x != 0 && count <= summ)
                            _values[x + correctY, sizeX - 1] = _maxValue - step * count++;
                        if (y == 2 && x < sizeX - correctX && x != 0 && count <= summ)
                            _values[sizeY - 1, sizeX - (x + 1)] = _maxValue - step * count++;
                        if (y == 3 && x < sizeY - (correctY + 1) && x != 0 && count <= summ)
                            _values[sizeY - (x + 1), correctY] = _maxValue - step * count++;
                    }
                }
                sizeY--;
                sizeX--;
                correctY += 1;
                correctX += 1;
            }
        }

        public SpiralValueGiver(double maxValue, double minValue)
        {
            _maxValue = maxValue;
            _minValue = minValue;
        }

        public double GetColor(int x, int y)
        {
            if (_valuesForSize != Size)
            {
                InitValues();
            }
            return _values[x, y] + _random.Next(-50, 50);
        }

        public int Size { get; set; }

    }
}
