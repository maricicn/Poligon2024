using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Poligon2024
{
    internal class Poligon
    {
        public int broj_temena;
        public Tacka[] teme;
        public Poligon()
        {
            teme = new Tacka[broj_temena];
        }
        public Poligon(int n)
        {
            broj_temena = n;
            teme = new Tacka[broj_temena];
        }
        public Boolean Konveksan()
        {
            int plusevi = 0;
            for (int i = 0; i < teme.Length; i++)
            {
                Vektor prvi = new Vektor(teme[i], teme[(i + 1) % broj_temena]);
                Vektor drugi = new Vektor(teme[(i + 1) % broj_temena], teme[(i + 2) % broj_temena]);
                if (Vektor.VP(prvi, drugi) > 0) plusevi++;
            }
            if ((plusevi == 0) || plusevi == broj_temena) return true;
            else return false;
        }
        public bool Prost()
        {
            Vektor[] nizV = new Vektor[broj_temena];
            for(int i = 0; i < broj_temena; i++)
            {
                Vektor v = new Vektor(teme[i], teme[(i+1) % broj_temena]);
                nizV[i] = v;
            }
            for (int i = 0; i < broj_temena - 1; i++)
            {
                for (int j = i + 1; j < broj_temena; j++)
                {
                    if (Tacka.Jednake(teme[i], teme[j]))
                    {
                        return false;
                    }
                }
            }
            bool prost = true;
            for(int i = 0; i < broj_temena - 2; i++)
            {
                for(int j = 0; j < broj_temena - 3; j++)
                {
                    if (Funkcije.Presek1(nizV[i], nizV[(i + j + 2) % broj_temena])) prost = false;
                }
            }
            return prost;

        }
        public bool TPripadaPoligonu(Tacka N)
        {
            double xmax = teme[0].x;
            for(int i = 1; i < broj_temena; i++)
            {
                if (teme[i].x > xmax) xmax = teme[i].x;
            }
            Tacka M = new Tacka(xmax + 1, N.y);
            Vektor ispit1 = new Vektor(N, M);
            int br_preseka = 0;
            for(int i = 0; i < broj_temena; i++)
            {
                Vektor ispit2 = new Vektor(teme[i], teme[(i+1) %  broj_temena]);
                if (Funkcije.Presek(ispit1, ispit2)) br_preseka++;
                if(Vektor.PresekUTemenu(ispit1, teme[(i + 1) % broj_temena]))
                {
                    Vektor pom = new Vektor(teme[i], teme[(i+2) % broj_temena]);
                    if (Funkcije.SIS(pom, N, M) == 2) br_preseka++;
                }
            }
            if (br_preseka % 2 == 0) return false;
            else return true;
            
        }
        public Poligon KonveksniOmotac()
        {
            if (Konveksan()) return this;

            double pom_x = teme[0].x;
            double pom_y = teme[0].y;
            for (int i = 1; i < broj_temena; i++)
            {
                if (teme[i].x < pom_x)
                {
                    pom_x = teme[i].x;
                    pom_y = teme[i].y;
                }
                else
                {
                    if (teme[i].x == pom_x)
                    {
                        if (teme[i].y < pom_y)
                        {
                            pom_y = teme[i].y;
                        }
                    }
                }
            }
            
            Tacka prva = new Tacka(pom_x, pom_y);
            int index1 = 0;
            for(int i = 0; i < broj_temena; i++)
            {
                if ((teme[i].x == pom_x) && (teme[i].y == pom_y)){
                    index1 = i;
                }
            }
            Console.WriteLine("Pobedio {0}", index1);
            List<Tacka> omotac = new List<Tacka>();
            omotac.Add(teme[index1]);
            double min_ugao = 90;
            int index2 = 0;
            for (int i = 1; i < broj_temena; i++)
            {
                double pomx = teme[(index1 + i) % broj_temena].x - teme[index1].x;
                double pomy = teme[(index1 + i) % broj_temena].y - teme[index1].y;
                double pomugao = Math.Atan2(pomy, pomx);
                if (pomugao < min_ugao)
                {
                    min_ugao = pomugao;
                    index2 = (index1 + i) % broj_temena;
                }
            }
            Console.WriteLine("Izabrana {0}", index2);
            omotac.Add(teme[index2]);
            List<int> indexi = new List<int>();
            indexi.Add(index2);
            
            while (index2 != index1)
            {
                min_ugao = 180;
                int index3 = 0;
                Tacka pom = teme[index2];
                /*
                for (int brojac = index2 + 1; brojac != index1; brojac = ((brojac + 1) % broj_temena))
                {
                    Tacka kraj = teme[brojac];
                    double temp_x = kraj.x - pom.x;
                    double temp_y = kraj.y - pom.y;
                    double temp_ugao = Math.Atan2(temp_y, temp_x);
                    if (temp_ugao < min_ugao)
                    {
                        min_ugao = temp_ugao;
                        index3 = brojac % broj_temena;
                    }
                }
                Console.WriteLine("Dodajem: {0}", index3);
                omotac.Add(teme[index3]);
                index2 = index3;
                */
                for(int i = index2 + 1; i < broj_temena; i++)
                {
                    if (indexi.Contains(i)) continue;
                    Tacka kraj = teme[(i) % broj_temena];
                    double temp_ugao = 0;
                    double temp_x = kraj.x - pom.x;
                    double temp_y = kraj.y - pom.y;
                    if (kraj.y < pom.y)
                    {
                        temp_ugao = Math.PI - Math.Atan2(temp_y, temp_x);
                    }
                    else
                    {
                        temp_ugao = Math.Atan2(temp_y, temp_x);
                    }
                    if (temp_ugao < min_ugao)
                    {
                        min_ugao = temp_ugao;
                        index3 = (i) % broj_temena;
                    }
                    
                }
                Console.WriteLine("Dodajem: {0}", index3);
                omotac.Add(teme[index3]);
                indexi.Add(index3);
                index2 = index3;
            }
            return null;

        }
        public double Obim()
        {
            double obim = 0;
            for(int i = 0; i < teme.Length; i++)
            {
                Vektor pom = new Vektor(teme[i], teme[(i + 1) % broj_temena]);
                obim = obim + Vektor.Intenzitet(pom);
            }
            return obim;
        }
        public double Povrsina()
        {
            double povrsina = 0;
            for (int i = 0; i < teme.Length; i++)
            {
                Tacka A = teme[i];
                Tacka B = teme[(i + 1) % broj_temena];
                povrsina = povrsina + (A.x * B.y - B.x * A.y);
            }
            return Math.Abs(povrsina)/2;

        }
        public void toString()
        {
            for(int i = 0; i < teme.Length; i++)
            {
                Console.WriteLine("Koordinate temena A{0} su: {1} {2}", i, teme[i].x, teme[i].y);
            }
        }
    }
}
