using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PerlinNoise
{
    interface IKnotValueParser
    {
        double GetColor(int x, int y);
        
        int Size { get; set; }
    }
}
