using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PerlinNoise
{
    class GridArgbWrapper
    {
        private Grid _alpha;
        private Grid _red;
        private Grid _green;
        private Grid _blue;

        public Color TurnColor { get; set; }



        public GridArgbWrapper(int size, int divisionOrder)
        {
            _alpha = new Grid(size, divisionOrder);
            _red = new Grid(size, divisionOrder);
            _green = new Grid(size, divisionOrder);
            _blue = new Grid(size, divisionOrder);
            TurnColor = Color.White;
        }

        public GridArgbWrapper(int size, int divisionOrder, IKnotValueParser valueParser)
        {
            _alpha = new Grid(size, divisionOrder,valueParser);
            _red = new Grid(size, divisionOrder, valueParser);
            _green = new Grid(size, divisionOrder, valueParser);
            _blue = new Grid(size, divisionOrder, valueParser);
            TurnColor = Color.White;
        }

        public double GetAlpha(int x, int y)
        {
            return _alpha.GetColor(x, y)*TurnColor.A/255;
        }

        public double GetRed(int x, int y)
        {
            return _red.GetColor(x, y) * TurnColor.R / 255;
        }

        public double GetGreen(int x, int y)
        {
            return _green.GetColor(x, y) * TurnColor.G / 255;
        }

        public double GetBlue(int x, int y)
        {
            return _blue.GetColor(x, y) * TurnColor.B / 255;
        }
    }
}
