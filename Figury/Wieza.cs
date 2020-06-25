using System.Collections.Generic;

namespace Szachy
{
    /// <summary>
    /// Reprezentacja szachowej wieży
    /// </summary>
    public class Wieza : Bierka
    {
        /// <summary>
        /// Zwykły konstruktor
        /// </summary>
        /// <param name="kolor">Kolor gracza którego jest wieża</param>
        public Wieza(int kolor)
        {
            bialy_gracz = kolor;
        }
        /// <summary>
        /// Konstruktor kopiujący
        /// </summary>
        /// <param name="pom">Wieża, którą chcemy skopiować</param>
        public Wieza(Wieza pom)
        {
            bialy_gracz = pom.ktory_gracz();
        }

        /// <summary>
        /// Funkcja pokazuje, na ktore pola moze ruszyc sie gracz  </summary>
        /// <returns> Tablicę pól, na którą można się ruszyć </returns>

        public override List<int> zobacz_mozliwosci(int pole, Szachownica pomocnicza_szachownica)
        {
            List<int> zwrot = new List<int>();
            int temp_kolumna = pole % 8;
            int temp_wiersz = (pole - temp_kolumna) / 8;

            for(int i = temp_wiersz + 1; i <= 7; i++)
            {
                if (pomocnicza_szachownica.figura_na_polu(i * 8 + temp_kolumna) is null) {
                    zwrot.Add(i * 8 + temp_kolumna);
                }
                else if (pomocnicza_szachownica.figura_na_polu(i * 8 + temp_kolumna).ktory_gracz() != this.bialy_gracz)
                {
                    zwrot.Add(i * 8 + temp_kolumna);
                    break;
                }
                else if (pomocnicza_szachownica.figura_na_polu(i * 8 + temp_kolumna).ktory_gracz() == this.bialy_gracz)
                    break;
            }

            for (int i = temp_wiersz - 1; i >= 0; i--)
            {
                if (pomocnicza_szachownica.figura_na_polu(i * 8 + temp_kolumna) is null)
                {
                    zwrot.Add(i * 8 + temp_kolumna);
                }
                else if (pomocnicza_szachownica.figura_na_polu(i * 8 + temp_kolumna).ktory_gracz() != this.bialy_gracz)
                {
                    zwrot.Add(i * 8 + temp_kolumna);
                    break;
                }
                else if (pomocnicza_szachownica.figura_na_polu(i * 8 + temp_kolumna).ktory_gracz() == this.bialy_gracz)
                    break;
            }

            for (int i = temp_kolumna - 1; i >= 0; i--)
            {
                if (pomocnicza_szachownica.figura_na_polu(temp_wiersz * 8 + i) is null)
                    zwrot.Add(temp_wiersz * 8 + i);
                else if (pomocnicza_szachownica.figura_na_polu(temp_wiersz * 8 + i).ktory_gracz() != this.bialy_gracz)
                {
                    zwrot.Add(temp_wiersz * 8 + i);
                    break;
                }
                else if (pomocnicza_szachownica.figura_na_polu(temp_wiersz * 8 + i).ktory_gracz() == this.bialy_gracz)
                    break;
            }

            for (int i = temp_kolumna + 1; i <= 7; i++)
            {
                if (pomocnicza_szachownica.figura_na_polu(temp_wiersz * 8 + i) is null)
                    zwrot.Add(temp_wiersz * 8 + i);
                else if (pomocnicza_szachownica.figura_na_polu(temp_wiersz * 8 + i).ktory_gracz() != this.bialy_gracz)
                {
                    zwrot.Add(temp_wiersz * 8 + i);
                    break;
                }
                else if (pomocnicza_szachownica.figura_na_polu(temp_wiersz * 8 + i).ktory_gracz() == this.bialy_gracz)
                    break;
            }

            return zwrot;
        }
        /// <summary>
        /// Wylicza najkrótszą ścieżkę, którą musi pokonać figura żeby dostać się do króla
        /// </summary>
        /// <param name="pole_krola">Pole króla</param>
        /// <param name="pole_wiezy">Pole wieży</param>
        /// <returns></returns>
        public List<int> najkrotsza_sciezka(int pole_krola, int pole_wiezy) //wiemy, ze ten ruch da sie wykonac, wiec nie trzeba szachownicy
        {
            var wieza_kolumna = pole_wiezy % 8;
            var wieza_wiersz = (pole_wiezy - wieza_kolumna) / 8;
            List<int> sciezka = new List<int>();
            var krol_kolumna = pole_krola % 8;
            var krol_wiersz = (pole_krola - krol_kolumna) / 8;

            if(krol_wiersz == wieza_wiersz) //jeśli mają takie same wiersze, to znaczy, że idziemy po kolumnach
                for(int i = pole_wiezy; i < krol_wiersz; i++)
                    sciezka.Add(i);

            else
                for(int i = wieza_kolumna; i != krol_kolumna; i++) //a jak takie same kolumny, to po wierszach
                    sciezka.Add(i * 8 + wieza_wiersz);

            return sciezka;
        }
    }
}
