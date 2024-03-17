using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poligon2024
{
    internal class Tacka
    {
        public double x, y;
        public Tacka(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public Tacka() { }

        public double Get_r()
        {
            double r=Math.Sqrt(x*x + y*y);
            return r;
        }
        public static bool Jednake(Tacka A, Tacka B) 
        {
            if ((A.x == B.x) && (A.y == B.y)) return true;
            else return false;
        }

    }
}
