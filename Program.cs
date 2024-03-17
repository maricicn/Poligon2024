using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poligon2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("Pritisni:");
            Console.WriteLine("1: Unos");
            Console.WriteLine("2: SAVE");
            Console.WriteLine("3: LOAD");
            Console.WriteLine("0: EXIT");
            int izbor = Convert.ToInt32(Console.ReadLine());
            if (izbor == 1)
            {
                Console.WriteLine("koliko temena?");
                int br_temena = Convert.ToInt32(Console.ReadLine());
                poligon prvi = new poligon(br_temena);
                for (int i = 0; i < br_temena; i++)
                {
                       // Unesite x za teme A(i)
                       // Unesite y za teme A(i)
                    
                }
            }
            
            tacka A = new tacka(0, -1);
            tacka B = new tacka(5.866, 6.5);
            tacka C = new tacka(2, 0);
            tacka D = new tacka(5, 6);

            vektor prvi = new vektor(C, A);
            vektor drugi = new vektor(D, B);
            double stampa = vektor.ugao(prvi, drugi);
            Console.WriteLine("ugao = "+stampa.ToString());
            */
            /*
            Poligon novi = new Poligon(8);
            Tacka A = new Tacka(0,0);
            Tacka B = new Tacka(2,0);
            Tacka C = new Tacka(3,2);
            Tacka D = new Tacka(2,4);
            Tacka E = new Tacka(0,4);
            Tacka F = new Tacka(-1,2);
            Tacka G = new Tacka(-2,1);
            Tacka H = new Tacka(-1,-1);
            novi.teme[0] = A;
            novi.teme[1] = B;
            novi.teme[2] = C;
            novi.teme[3] = D;
            novi.teme[4] = E;
            novi.teme[5] = F;
            novi.teme[6] = G;
            novi.teme[7] = H;
            */

            string input = "123";
            Poligon poligon = new Poligon();
            while(input != "0")
            { 
                Console.WriteLine("Izaberite jednu od ponudjenih opcija: \n 1 - Unos Poligona \n 2 - FileSave \n 3 - FileLoad");
                input = Console.ReadLine();
                if (input == "1")
                {
                    poligon = Funkcije.Unos();
                }
                else if(input == "2")
                {
                    Funkcije.FileSave(poligon);
                }
                else if(input == "3")
                {
                    poligon = Funkcije.FileLoad();
                }
                else if(input == "4")
                {
                    if (poligon.Prost()) Console.WriteLine("Poligon je prost");
                    else Console.WriteLine("Poligon nije prost");
                }
                else if (input == "5")
                {
                    Console.WriteLine("Unesite tacku koju zelite da proverite");
                    string[] inputs1 = Console.ReadLine().Split();
                    Tacka T = new Tacka(Convert.ToDouble(inputs1[0]), Convert.ToDouble(inputs1[1]));
                    if (poligon.TPripadaPoligonu(T)) Console.WriteLine("da");
                    else Console.WriteLine("ne");
                }
                else if (input == "6")
                {
                    poligon.KonveksniOmotac();
                }
            }

            /*Tacka kraj = new Tacka(-8, 2);
            Tacka pom = new Tacka(1, -1);
            double temp_x = kraj.x - pom.x;
            double temp_y = kraj.y - pom.y;
            double temp_ugao = 0;
            if (kraj.x < pom.x)
            {
                temp_ugao = Math.Atan2(temp_y, temp_x);
            }
            else
            {
                temp_ugao = Math.Atan2(temp_y, temp_x);
            }
            Console.WriteLine(temp_ugao);*/








        }
    }
}
