using System;
using System.Collections.Generic;

namespace Szachy
{
    /// <summary>
    /// Reprezentacja szachowego gońca
    /// </summary>
    class Goniec : Bierka
    {
        /// <summary>
        /// Kolor figury zależy od tego, co podamy w parametrze konstruktora
        /// </summary>
        public Goniec(int kolor)
        {
            bialy_gracz = kolor;
        }
        /// <summary>
        /// Konstruktor kopiujący
        /// </summary>
        /// <param name="pom">Goniec do skopiowania</param>
        public Goniec(Goniec pom)
        {
            this.bialy_gracz = pom.ktory_gracz();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pole"></param>
        /// <param name="pomocnicza_szachownica"></param>
        /// <returns></returns>
        public override List<int> zobacz_mozliwosci(int pole, Szachownica pomocnicza_szachownica)
        {
            List<int> zwrot = new List<int>();
            int temp_kolumna = pole % 8;
            int temp_wiersz  = (pole - temp_kolumna) / 8;

            while (temp_wiersz > 0 && temp_wiersz <= 7 && temp_kolumna > 0 && temp_kolumna <= 7)
            {
                temp_kolumna--; temp_wiersz--;
                if (temp_wiersz * 8 + temp_kolumna < 0 || temp_wiersz * 8 + temp_kolumna > 63)
                    break;
                if (pomocnicza_szachownica.figura_na_polu(temp_wiersz * 8 + temp_kolumna) is null)
                    zwrot.Add(temp_wiersz * 8 + temp_kolumna);
                else if (pomocnicza_szachownica.figura_na_polu(temp_wiersz * 8 + temp_kolumna).ktory_gracz() != this.bialy_gracz)
                {
                    zwrot.Add(temp_wiersz * 8 + temp_kolumna);
                    break;
                }
                else if (pomocnicza_szachownica.figura_na_polu(temp_wiersz * 8 + temp_kolumna).ktory_gracz() == this.bialy_gracz)
                    break;
                if (temp_wiersz == 0 || temp_wiersz == 7 || temp_kolumna == 0 || temp_kolumna == 7)
                    break;
            }

            temp_kolumna = pole % 8; temp_wiersz = (pole - temp_kolumna) / 8;

            while (temp_wiersz >= 0 && temp_wiersz < 7 && temp_kolumna >= 0 && temp_kolumna < 7)
            {
                temp_kolumna++; temp_wiersz++;
                if (temp_wiersz * 8 + temp_kolumna < 0 || temp_wiersz * 8 + temp_kolumna > 63)
                    break;
                if (pomocnicza_szachownica.figura_na_polu(temp_wiersz * 8 + temp_kolumna) is null)
                    zwrot.Add(temp_wiersz * 8 + temp_kolumna);
                else if (pomocnicza_szachownica.figura_na_polu(temp_wiersz * 8 + temp_kolumna).ktory_gracz() != this.bialy_gracz)
                {
                    zwrot.Add(temp_wiersz * 8 + temp_kolumna);
                    break;
                }
                else if (pomocnicza_szachownica.figura_na_polu(temp_wiersz * 8 + temp_kolumna).ktory_gracz() == this.bialy_gracz)
                    break;
                if (temp_wiersz == 0 || temp_wiersz == 7 || temp_kolumna == 0 || temp_kolumna == 7)
                    break;
            }

            temp_kolumna = pole % 8; temp_wiersz = (pole - temp_kolumna) / 8;

            while (temp_wiersz >= 0 && temp_wiersz < 7 && temp_kolumna > 0 && temp_kolumna <= 7)
            {
                temp_kolumna--; temp_wiersz++;
                if (temp_wiersz * 8 + temp_kolumna < 0 || temp_wiersz * 8 + temp_kolumna > 63)
                    break;
                if (pomocnicza_szachownica.figura_na_polu(temp_wiersz * 8 + temp_kolumna) is null)
                    zwrot.Add(temp_wiersz * 8 + temp_kolumna);
                else if (pomocnicza_szachownica.figura_na_polu(temp_wiersz * 8 + temp_kolumna).ktory_gracz() != this.bialy_gracz)
                {
                    zwrot.Add(temp_wiersz * 8 + temp_kolumna);
                    break;
                }
                else if (pomocnicza_szachownica.figura_na_polu(temp_wiersz * 8 + temp_kolumna).ktory_gracz() == this.bialy_gracz)
                    break;
                if (temp_wiersz == 0 || temp_wiersz == 7 || temp_kolumna == 0 || temp_kolumna == 7)
                    break;
            }

            temp_kolumna = pole % 8; temp_wiersz = (pole - temp_kolumna) / 8;

            while (temp_wiersz > 0 && temp_wiersz <= 7 && temp_kolumna >= 0 && temp_kolumna < 7)
            {
                temp_kolumna++; temp_wiersz--;
                if (temp_wiersz * 8 + temp_kolumna < 0 || temp_wiersz * 8 + temp_kolumna > 63)
                    break;
                if (pomocnicza_szachownica.figura_na_polu(temp_wiersz * 8 + temp_kolumna) is null)
                    zwrot.Add(temp_wiersz * 8 + temp_kolumna);
                else if (pomocnicza_szachownica.figura_na_polu(temp_wiersz * 8 + temp_kolumna).ktory_gracz() != this.bialy_gracz)
                {
                    zwrot.Add(temp_wiersz * 8 + temp_kolumna);
                    break;
                }
                else if (pomocnicza_szachownica.figura_na_polu(temp_wiersz * 8 + temp_kolumna).ktory_gracz() == this.bialy_gracz)
                    break;
                if (temp_wiersz == 0 || temp_wiersz == 7 || temp_kolumna == 0 || temp_kolumna == 7)
                    break;
            }

            return zwrot;
        }

        /// <summary>
        /// Wylicza najkrótszą ścieżkę od króla do końca
        /// </summary>
        /// <param name="krol">Pole króla</param>
        /// <param name="goniec">Pole gońca</param>
        /// <returns>Lista pól składowych najkrótszej ścieżki</returns>
        public List<int> najkrotsza_sciezka(int krol, int goniec)
        {
            var goniec_kolumna = goniec % 8;
            var goniec_wiersz = (goniec - goniec_kolumna) / 8;

            List<int> sciezka = new List<int>();

            var krol_kolumna = krol % 8;
            var krol_wiersz = (krol - krol_kolumna) / 8;

            var start_wiersz = Math.Min(krol_wiersz, goniec_wiersz) == goniec_wiersz ? goniec_wiersz : krol_wiersz;
            var start_kolumna = start_wiersz == goniec_wiersz ? goniec_kolumna : krol_kolumna;

            var stop_kolumna = start_kolumna == goniec_kolumna ? krol_kolumna : goniec_kolumna;
            var stop_wiersz = start_kolumna == goniec_kolumna ? krol_wiersz : goniec_wiersz;

            for (int i = start_wiersz + 1; i < stop_wiersz; i++)
            {
                if (start_kolumna < stop_kolumna)
                {
                    start_kolumna++;
                    sciezka.Add(i * 8 + start_kolumna);
                }
                else
                {
                    start_kolumna--;
                    sciezka.Add(i * 8 + start_kolumna);
                }
            }
            sciezka.Add(goniec);
            return sciezka;
        }
    }
}
