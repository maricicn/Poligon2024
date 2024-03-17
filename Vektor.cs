using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poligon2024
{
    internal class Vektor
    {
        public Tacka pocetak, kraj;
        public Vektor()
        {

        }
        public Vektor(Tacka pocetak, Tacka kraj)
        {
            this.pocetak = pocetak;
            this.kraj = kraj;
        }
        public static Tacka VektorCentriraj(Vektor A)
        {
            Tacka nova = new Tacka();
            nova.x = A.kraj.x - A.pocetak.x;
            nova.y = A.kraj.y - A.pocetak.y;
            return nova;
        }
        public static double Intenzitet(Vektor A)
        {
            Tacka Ac = VektorCentriraj(A);
            return Ac.Get_r();

        }
        public static double Skalarni(Vektor A, Vektor B)
        {
            Tacka A_c = VektorCentriraj(A);
            Tacka B_c = VektorCentriraj(B);
            double skalarni = A_c.x * B_c.x + A_c.y * B_c.y;
            return skalarni;
        }
        public static double VP(Vektor A, Vektor B)
        {
            Tacka A_c = VektorCentriraj(A);
            Tacka B_c = VektorCentriraj(B);
            return A_c.x*B_c.y - A_c.y*B_c.x;
        }
        public static double Ugao(Vektor A, Vektor B)
        {
            Tacka Ac = VektorCentriraj(A);
            Tacka Bc = VektorCentriraj(B);
            double ugaoA = Math.Atan2(Ac.y, Ac.x) * 180 / Math.PI;
            double ugaoB = Math.Atan2(Bc.y, Bc.x) * 180 / Math.PI;
            Console.WriteLine("ugao a={0}", ugaoA);
            Console.WriteLine("ugao b={0}", ugaoB);
            if (ugaoB - ugaoA < 0 )
            {
                return ugaoB - ugaoA + 360;
            }
            return ugaoB - ugaoA;
        }
        public static bool PresekUTemenu(Vektor AB, Tacka T) //Koristi se za Tacka u Poligonu (TPripadaPoligonu)
        {
            double AT = Math.Abs(T.x - AB.pocetak.x);
            double TB = Math.Abs(T.x - AB.kraj.x);
            if (T.y == AB.kraj.y && AT + TB == Intenzitet(AB)) return true;
            else return false;
        }
    }
}
