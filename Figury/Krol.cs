using System.Collections.Generic;
using System.Windows.Markup;

namespace Szachy
{
    /// <summary>
    /// Reprezentacja szachowego króla
    /// </summary>
    public class Krol : Bierka
    {
        /// <summary>
        /// Sprawdza czy król został ruszony (używane przy roszadzie)
        /// </summary>
        public bool ruszono { get; set; }  
        /// <summary>
        /// Pole pierwszej wieży
        /// </summary>
        public int pozycja_pierwszej_wiezy;
        /// <summary>
        /// Pole drugiej wieży
        /// </summary>
        public int pozycja_drugiej_wiezy;
        /// <summary>
        /// Zwykły konstruktor
        /// </summary>
        /// <param name="kolor">Kolor gracza, którego jest to król</param>
        public Krol(int kolor)
        {
            bialy_gracz = kolor;
        }

        /// <summary>
        /// Konstruktor kopiujący
        /// </summary>
        /// <param name="kop">Król którego chcemy skopiować</param>
        public Krol(Krol kop)
        {
            this.ruszono = kop.ruszono;
            this.pozycja_pierwszej_wiezy = kop.pozycja_pierwszej_wiezy;
            this.pozycja_drugiej_wiezy = kop.pozycja_drugiej_wiezy;
            this.bialy_gracz = kop.ktory_gracz();
        }
        /// <summary>
        /// Wszystkie możliwe pola na które może ruszyć się król (z wyłączeniem szachowania)
        /// </summary>
        /// <param name="pole">Pole króla</param>
        /// <param name="pomocnicza_szachownica">Szachownica na której sprawdzamy</param>
        /// <returns>Lista pól na które można się ruszyć</returns>
        public override List<int> zobacz_mozliwosci(int pole, Szachownica pomocnicza_szachownica)
        {
            List<int> zwrot = new List<int>();

            void mozliwa_roszada(int temp_pole)
            {

                int temp_kolumna = temp_pole % 8;
                int temp_wiersz = (temp_pole - temp_kolumna) / 8;
                int dokad;

                for (int i = temp_kolumna - 1; i >= 0; i--)
                {
                    dokad = temp_wiersz * 8 + i;
                    if (pomocnicza_szachownica.figura_na_polu(dokad) is null)
                        continue;

                    if (pomocnicza_szachownica.figura_na_polu(dokad).ktory_gracz() == this.bialy_gracz &&
                            pomocnicza_szachownica.figura_na_polu(dokad) is Krol)
                        zwrot.Add(temp_pole);
                    break;

                }

                for (int i = temp_kolumna + 1; i <= 7; i++)
                {
                    dokad = temp_wiersz * 8 + i;
                    if (pomocnicza_szachownica.figura_na_polu(dokad) is null)
                        continue;

                    if (pomocnicza_szachownica.figura_na_polu(dokad).ktory_gracz() == this.bialy_gracz &&
                            pomocnicza_szachownica.figura_na_polu(dokad) is Krol)
                        zwrot.Add(temp_pole);
                    break;
                }
            }

            ///<summary>
            ///Funkcja, ktora sprawdza czy krol moze ruszyc sie na dane pole, zdecydowałem sie uzyc funkcji zeby poprawic czytelnosc kodu
            ///</summary>

            void pom_ruch_krola(int pom_wiersz, int kolumna_pom)
            {
                int dokad = pom_wiersz * 8 + kolumna_pom;

                if (kolumna_pom <= 7 && kolumna_pom >= 0 && pom_wiersz <= 7 && pom_wiersz >= 0)
                {
                    if (pomocnicza_szachownica.figura_na_polu(dokad) is null)
                    {
                        zwrot.Add(dokad);
                    }

                    else
                    {
                        if (pomocnicza_szachownica.figura_na_polu(dokad).ktory_gracz() != this.bialy_gracz)
                            zwrot.Add(dokad);
                    }
                }
            }

            int kolumna = pole % 8;
            int wiersz = (pole - kolumna) / 8;

            pom_ruch_krola(wiersz + 1, kolumna + 1);

            pom_ruch_krola(wiersz + 1, kolumna);

            pom_ruch_krola(wiersz + 1, kolumna - 1);

            pom_ruch_krola(wiersz, kolumna + 1);

            pom_ruch_krola(wiersz, kolumna - 1);

            pom_ruch_krola(wiersz - 1, kolumna - 1);

            pom_ruch_krola(wiersz - 1, kolumna);

            pom_ruch_krola(wiersz - 1, kolumna + 1);

            if (!ruszono && !pomocnicza_szachownica.czy_szach(bialy_gracz))
            {
                if (pozycja_pierwszej_wiezy != -1)
                    mozliwa_roszada(pozycja_pierwszej_wiezy);
                if (pozycja_drugiej_wiezy != -1)
                    mozliwa_roszada(pozycja_drugiej_wiezy);
            }
            
            return zwrot;
        }
    }
}