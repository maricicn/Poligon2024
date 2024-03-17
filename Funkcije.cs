using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Poligon2024
{
    internal class Funkcije
    {
        public static char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        public static Poligon Unos()
        {
            
            Console.WriteLine("Unesite broj temena: ");
            int n;
            n = Convert.ToInt32(Console.ReadLine());
            Poligon poligon = new Poligon(n);
            for (int i = 0; i < n; i++) 
            {
                Console.WriteLine("Unestite koordinate temena {0}:", alpha[i]);
                string[] inputs = Console.ReadLine().Split();
                poligon.teme[i] = new Tacka(Convert.ToDouble(inputs[0]), Convert.ToDouble(inputs[1]));
            }
            poligon.toString();
            return poligon;
        } 
        public static Poligon FileLoad()
        {
            StreamReader fileload = new StreamReader("projekatpoligon.txt");
            int.TryParse(fileload.ReadLine(), out int br);
            Poligon poligon = new Poligon(br);

            for(int i = 0; i < br; i++)
            {
                string[] xy = fileload.ReadLine().Split(' ');
                poligon.teme[i] = new Tacka(Convert.ToDouble(xy[0]), Convert.ToDouble(xy[1]));
            }
            return poligon;

        }
        public static void FileSave(Poligon poligon)
        {
            StreamWriter filesave = new StreamWriter("projekatpoligon.txt");
            filesave.BaseStream.SetLength(0);
            filesave.WriteLine(poligon.teme.Length);
            
            for (int i = 0; i < poligon.teme.Length; i++)
            {
                filesave.WriteLine(poligon.teme[i].x + " " + poligon.teme[i].y);
            }
            
            filesave.Close();
        }
        public static int SIS(Vektor AB, Tacka C, Tacka D)
        {
            Vektor AC = new Vektor(AB.pocetak, C);
            Vektor AD = new Vektor(AB.pocetak, D);
            double k1 = Vektor.VP(AB, AC);
            double k2 = Vektor.VP(AB, AD);
            if (k1 * k2 > 0) return 0;
            else if (k1 * k2 == 0) return 1;
            else return 2;
        }
        public static bool Presek(Vektor AB, Vektor CD)
        {
            if (SIS(AB, CD.pocetak, CD.kraj) * SIS(CD, AB.pocetak, AB.kraj) > 0) return true;
            else return false;
        }
        public static bool Presek1(Vektor AB, Vektor CD)
        {   /*
            Vektor AC = new Vektor(AB.pocetak, CD.pocetak);
            Vektor AD = new Vektor(AB.pocetak, CD.kraj);
            Vektor CA = new Vektor(CD.pocetak, AB.pocetak);
            Vektor CB = new Vektor(CD.pocetak, AB.kraj);
            double k1 = Vektor.VP(AB, AC);
            double k2 = Vektor.VP(AB, AD);
            double k3 = Vektor.VP(CD, CA);
            double k4 = Vektor.VP(CD, CB);
            bool presek;
            if ((k1 * k2 <= 0) && (k3 * k4 <= 0)) presek = true;
            else return false;
            if (Math.Max(AB.pocetak.x, AB.kraj.x) >= Math.Min(CD.pocetak.x, CD.kraj.x) && Math.Max(AB.pocetak.y, AB.kraj.y) >= Math.Min(CD.pocetak.y, CD.kraj.y) &&
                Math.Max(CD.pocetak.x, CD.kraj.x) >= Math.Min(AB.pocetak.x, AB.kraj.x) && Math.Max(CD.pocetak.y, CD.kraj.y) >= Math.Min(AB.pocetak.y, AB.kraj.y)) presek = true;
            else presek = false;
            return presek;
            */
            bool presek;
            if (Presek(AB, CD)) presek = true;
            else return false;
            if (Math.Max(AB.pocetak.x, AB.kraj.x) >= Math.Min(CD.pocetak.x, CD.kraj.x) && Math.Max(AB.pocetak.y, AB.kraj.y) >= Math.Min(CD.pocetak.y, CD.kraj.y) &&
                Math.Max(CD.pocetak.x, CD.kraj.x) >= Math.Min(AB.pocetak.x, AB.kraj.x) && Math.Max(CD.pocetak.y, CD.kraj.y) >= Math.Min(AB.pocetak.y, AB.kraj.y)) presek = true;
            else presek = false;
            return presek;
        }
    }
}
