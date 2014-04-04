using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PerlinNoise
{
    class Grid
    {
        private double[,] _grid;
        private int _baseSize;
        private int _divisionOrder;

        private const int Padding = 3;

        private static readonly int[,] M = new int[16,16]
            {
                { 0 , 0 , 0 , 0 , 0 , 36 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 } ,
                {0 , -12 , 0 , 0 , 0 , -18 , 0 , 0 , 0 , 36 , 0 , 0 , 0 , -6 , 0 , 0 } ,
                { 0 , 18 , 0 , 0 , 0 , -36 , 0 , 0 , 0 , 18 , 0 , 0 , 0 , 0 , 0 , 0 } ,
                { 0 , -6 , 0 , 0 , 0 , 18 , 0 , 0 , 0 , -18 , 0 , 0 , 0 , 6 , 0 , 0 } ,
                { 0 , 0 , 0 , 0 , -12 , -18 , 36 , -6 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 } ,
                { 4 , 6 , -12 , 2 , 6 , 9 , -18 , 3 , -12 , -18 , 36 , -6 , 2 , 3 , -6 , 1 } ,
                { -6 , -9 , 18 , -3 , 12 , 18 , -36 , 6 , -6 , -9 , 18 , -3 , 0 , 0 , 0 , 0 } ,
                { 2 , 3 , -6 , 1 , -6 , -9 , 18 , -3 , 6 , 9 , -18 , 3 , -2 , -3 , 6 , -1 } ,
                { 0 , 0 , 0 , 0 , 18 , -36 , 18 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 } ,
                { -6 , 12 , -6 , 0 , -9 , 18 , -9 , 0 , 18 , -36 , 18 , 0 , -3 , 6 , -3 , 0 } ,
                { 9 , -18 , 9 , 0 , -18 , 36 , -18 , 0 , 9 , -18 , 9 , 0 , 0 , 0 , 0 , 0 } ,
                { -3 , 6 , -3 , 0 , 9 , -18 , 9 , 0 , -9 , 18 , -9 , 0 , 3 , -6 , 3 , 0 } ,
                { 0 , 0 , 0 , 0 , -6 , 18 , -18 , 6 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 } ,
                { 2 , -6 , 6 , -2 , 3 , -9 , 9 , -3 , -6 , 18 , -18 , 6 , 1 , -3 , 3 , -1 } ,
                { -3 , 9 , -9 , 3 , 6 , -18 , 18 , -6 , -3 , 9 , -9 , 3 , 0 , 0 , 0 , 0 } ,
                { 1 , -3 , 3 , -1 , -3 , 9 , -9 , 3 , 3 , -9 , 9 , -3 , -1 , 3 , -3 , 1 } 
            };

        private static readonly double NormCoeff = 1.0/36;
        private static readonly Random randomGenerator = new Random();


        public Grid(int size, int divisionOrder)
        {
            _baseSize = size;
            _divisionOrder = divisionOrder;
            _grid = new double[divisionOrder + Padding, divisionOrder + Padding];


            for (var i = 0; i < divisionOrder + Padding; i++)
            {
                for (var j = 0; j < divisionOrder + Padding; j++)
                {
                    _grid[i, j] = randomGenerator.Next(100, 255);
                }
            }   
        }

        public Grid(int size, int divisionOrder, IKnotValueParser valueParser)
        {
            _baseSize = size;
            _divisionOrder = divisionOrder;
            _grid = new double[divisionOrder + Padding, divisionOrder + Padding];
            valueParser.Size = divisionOrder + Padding;

            for (var i = 0; i < divisionOrder + Padding; i++)
            {
                for (var j = 0; j < divisionOrder + Padding; j++)
                {
                    _grid[i, j] = valueParser.GetColor(i, j);
                }
            } 
        }

        public double GetColor(int x, int y)
        {
            var result = 0.0;

            var innerX = (double) x*(_divisionOrder)/_baseSize + 1;
            var innerY = (double) y * (_divisionOrder) / _baseSize + 1;
            var innerXint = (int) innerX;
            var innerYint = (int) innerY;
            var relativeX = innerX - innerXint;
            var relativeY = innerY - innerYint;

            var alpha = new double[16];
            var gamma = new double[16];

            for (int i = -1; i < 3; i++)
            {
                for (int j = -1; j < 3; j++)
                {
                    gamma[(i + 1) * 4 + j + 1] = _grid[innerXint + j, innerYint + i];
                }
            }

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    alpha[i] += M[i, j]*gamma[j];
                }
                alpha[i] *= NormCoeff;
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result += alpha[i*4 + j]*Math.Pow(relativeX, i)*Math.Pow(relativeY, j);
                }
            }

            return result;
        }




    }
}
