using System;
using System.Collections.Generic;
using System.Linq;

namespace Szachy
{
    /// <summary>
    /// Reprezentacja szachowego pionka
    /// </summary>
    public class Pionek : Bierka
    {   
        /// <value>
        /// Czy pionek został ruszony?
        /// </value>
        public bool ruszono { get; set; } 
        /// <summary>
        /// Kolor figury zależy od tego, co podamy w parametrze konstruktora
        /// </summary>
        public Pionek(int kolor)
        {
            bialy_gracz = kolor;
            ruszono = false;
        }
        /// <summary>
        /// Konstruktor kopiujący
        /// </summary>
        /// <param name="nowy">Pionek do skopiowania</param>
        public Pionek(Pionek nowy)
        {
            this.bialy_gracz = nowy.ktory_gracz();
            this.ruszono = nowy.ruszono;
        }

        /// <summary>
        /// Funkcja pokazuje, na ktore pola moze ruszyc sie gracz  </summary>
        /// <returns> Tablicę pól, na którą można się ruszyć   </returns>

        public override List<int> zobacz_mozliwosci(int pole, Szachownica pomocnicza_szachownica)
        {

            var kolumna_pionka = pole % 8;
            var gdzie_patrzec = bialy_gracz == 1 ? -1 : 1;
            List<int> zwrot = new List<int>();

            if(kolumna_pionka == 0)
            {
                if (!(pomocnicza_szachownica.figura_na_polu(pole + gdzie_patrzec * 8 - 1) is null))
                    if (pomocnicza_szachownica.figura_na_polu(pole + gdzie_patrzec * 8 - 1).ktory_gracz() != this.bialy_gracz)
                        zwrot.Add(pole + gdzie_patrzec * 8 + 1);
            }

            if (kolumna_pionka == 7)
            {
                if (!(pomocnicza_szachownica.figura_na_polu(pole + gdzie_patrzec * 8 - 1) is null))
                    if (pomocnicza_szachownica.figura_na_polu(pole + gdzie_patrzec * 8 - 1).ktory_gracz() != this.bialy_gracz)
                        zwrot.Add(pole + gdzie_patrzec * 8 - 1);
            }
            if (pomocnicza_szachownica.figura_na_polu(pole + gdzie_patrzec * 8) is null)
            {
                zwrot.Add(pole + gdzie_patrzec * 8);
                if(!ruszono)
                {
                    var gdzie_patrzec_temp = gdzie_patrzec * 2;
                    if (pomocnicza_szachownica.figura_na_polu(pole + gdzie_patrzec_temp * 8) is null)
                        zwrot.Add(pole + gdzie_patrzec_temp * 8);
                }
            }

            if (!(pomocnicza_szachownica.figura_na_polu(pole + gdzie_patrzec * 8 + 1) is null)) //sprawdza czy może wykonać bicie po skosie
                if (pomocnicza_szachownica.figura_na_polu(pole + gdzie_patrzec * 8 + 1).ktory_gracz() != this.bialy_gracz)
                    zwrot.Add(pole + gdzie_patrzec * 8 + 1);

            if (!(pomocnicza_szachownica.figura_na_polu(pole + gdzie_patrzec * 8 - 1) is null))
                if (pomocnicza_szachownica.figura_na_polu(pole + gdzie_patrzec * 8 - 1).ktory_gracz() != this.bialy_gracz)
                    zwrot.Add(pole + gdzie_patrzec * 8 - 1);

            return zwrot;
        }

        /// <summary>
        /// Sprawdza które pola szachuje dany pionek (to różni się od pól, na które może się ruszyć)
        /// </summary>
        /// <param name="pole">Pole piona</param>
        /// <param name="pomocnicza_szachownica">Szachownica sprawdzana</param>
        /// <returns>Pola które szachuje sprawdzany pionek</returns>
        public List<int> zobacz_szachowane(int pole, Szachownica pomocnicza_szachownica)
        {
            var kolumna_pionka = pole % 8;
            var gdzie_patrzec = bialy_gracz == 1 ? -1 : 1;
            List<int> zwrot = new List<int>();
            if (kolumna_pionka != 7)
            {
                if (pomocnicza_szachownica.figura_na_polu(pole + gdzie_patrzec * 8 + 1) is null)
                    zwrot.Add(pole + gdzie_patrzec * 8 + 1);
                else if(pomocnicza_szachownica.figura_na_polu(pole + gdzie_patrzec * 8 + 1).ktory_gracz() != this.bialy_gracz)
                    zwrot.Add(pole + gdzie_patrzec * 8 + 1);
            }

            if (kolumna_pionka != 0)
            {
                if (pomocnicza_szachownica.figura_na_polu(pole + gdzie_patrzec * 8 - 1) is null)
                {
                    zwrot.Add(pole + gdzie_patrzec * 8 - 1);
                }
                    
                else if (pomocnicza_szachownica.figura_na_polu(pole + gdzie_patrzec * 8 - 1).ktory_gracz() != this.bialy_gracz)
                    zwrot.Add(pole + gdzie_patrzec * 8 - 1);
            }

            return zwrot;
        }

    }
}
