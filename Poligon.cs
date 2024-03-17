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
            if (Konveksan())
            {
                Console.WriteLine("Indexi tacaka koje cine konveksni omotac su:");
                for (int i = 0; i < broj_temena; i++)
                {
                    Console.WriteLine(i);
                }
                return this;
            }
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
            for (int i = 0; i < broj_temena; i++)
            {
                if ((teme[i].x == pom_x) && (teme[i].y == pom_y)) {
                    index1 = i;
                }
            }
            //Console.WriteLine("Pobedio {0}", index1);
            List<Tacka> omotac = new List<Tacka>();
            omotac.Add(teme[index1]);
            Tacka[] SortTeme = new Tacka[broj_temena];
            for(int i = 0; i < broj_temena; i++)
            {
                SortTeme[i] = teme[(index1 + i) % broj_temena];
                //Console.WriteLine("Teme x: {0}, Teme y: {1}", SortTeme[i].x, SortTeme[i].y);
            }
            index1 = 0;
            double min_ugao = 90;
            int index2 = 0;
            List<int> indexi = new List<int>();
            for (int i = 1; i < broj_temena; i++)
            {
                double pomx = SortTeme[(index1 + i) % broj_temena].x - SortTeme[index1].x;
                double pomy = SortTeme[(index1 + i) % broj_temena].y - SortTeme[index1].y;
                double pomugao = Math.Atan2(pomy, pomx);
                if (pomugao < min_ugao)
                {
                    min_ugao = pomugao;
                    index2 = (index1 + i) % broj_temena;
                }

            }
            for(int i = index2; i > 0; i--)
            {
                indexi.Add(i);
            }
            //indexi.Add(index2);
            //Console.WriteLine("Izabrana {0}", Array.IndexOf(teme, SortTeme.ElementAt(index2)));
            omotac.Add(SortTeme[index2]);
            /*
            Tacka[] SortiranoTeme = new Tacka[broj_temena];
            for(int i = 0; i < broj_temena; i++)
            {
                SortiranoTeme[i] = teme[(index2 + i) % broj_temena];
            }
            index2 = 0;
            for (int i = 0; i < broj_temena; i++)
            {
                if ((SortiranoTeme[i].x == pom_x) && (SortiranoTeme[i].y == pom_y))
                {
                    index1 = i;
                   
                }
            }
            Console.WriteLine("Nakon sortiranog niza index prvog je " + index1);
            */
            
            /*if(index1 > index2)
            {
                for (int i = index2; i >= 0; i--)
                {
                    if (i == index1) continue;
                    indexi.Add(i);
                }
            }
            else if(index2 > index1)
            {
                for (int i = index2; i >= index1; i--)
                {
                    if (i == index1) continue;
                    indexi.Add(i);
                }
            }*/
            
            while (index2 != index1)
            {
                min_ugao = 180;
                int index3 = 0;
                Tacka pom = SortTeme[index2];
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
                for(int i = index2 + 1; i <= broj_temena; i++)
                {
                    if (indexi.Contains(i % broj_temena)) continue;
                    Tacka kraj = SortTeme[i % broj_temena];
                    double temp_ugao = 0;
                    double temp_x = kraj.x - pom.x;
                    double temp_y = kraj.y - pom.y;
                    if (kraj.y < pom.y && kraj.x < pom.x)
                    {
                        temp_ugao = 2 * Math.PI + Math.Atan2(temp_y, temp_x);
                    }
                    else
                    {
                        temp_ugao = Math.Atan2(temp_y, temp_x);
                    }
                    if (temp_ugao < min_ugao)
                    {
                        min_ugao = temp_ugao;
                        index3 = i % broj_temena;
                    }

                }
                for (int i = index3; i > 0; i--)
                {
                    indexi.Add(i);
                }
                //Console.WriteLine("Dodajem: {0}", Array.IndexOf(teme, SortTeme.ElementAt(index3)));
                omotac.Add(teme[index3]);
                /*if (index1 > index3)
                {
                    for (int i = index3; i >= 0; i--)
                    {
                        if (i == index1) continue;
                        indexi.Add(i);
                    }
                }
                else if (index3 > index1)
                {
                    for (int i = index3; i >= index1; i--)
                    {
                        if (i == index1) continue;
                        indexi.Add(i);
                    }
                }*/
                index2 = index3;
            }
            Console.WriteLine("Indexi tacaka koje cine konveksni omotac su:");
            foreach(var T in omotac)
            {
                Console.Write(Array.IndexOf(teme, T) + " ");
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
