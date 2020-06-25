using System.Collections.Generic;

namespace Szachy
{
    /// <summary>
    /// Reprezentacja szachowego skoczka
    /// </summary>
    public class Skoczek : Bierka
    {
        /// <summary>
        /// Kolor figury zależy od tego, co podamy w parametrze konstruktora
        /// </summary>
        public Skoczek(int kolor)
        {
            bialy_gracz = kolor;
        }

        /// <summary>
        /// Konstruktor kopiujący
        /// </summary>
        /// <param name="pom">Skoczek do skopiowania</param>

        public Skoczek(Skoczek pom)
        {
            this.bialy_gracz = pom.ktory_gracz();
        }

        /// <summary>
        /// Funkcja pokazuje, na ktore pola moze ruszyc sie gracz  </summary>
        /// <returns> Tablicę pól, na którą można się ruszyć   </returns>
        public override List<int> zobacz_mozliwosci(int pole, Szachownica szachownica)
        {
            Szachownica pomocnicza_szachownica = new Szachownica(szachownica);
            List<int> zwrot = new List<int>();
            int kolumna = pole % 8;
            int wiersz = (pole - kolumna) / 8;

            if(wiersz + 2 <= 7 && kolumna - 1 >= 0)
            {
                if ((pomocnicza_szachownica.figura_na_polu((wiersz + 2) * 8 + kolumna - 1) is null))
                    zwrot.Add((wiersz + 2) * 8 + kolumna - 1);

                if (!(pomocnicza_szachownica.figura_na_polu((wiersz + 2) * 8 + kolumna - 1) is null))
                {
                    if (pomocnicza_szachownica.figura_na_polu((wiersz + 2) * 8 + kolumna - 1).ktory_gracz() != this.bialy_gracz)
                        zwrot.Add((wiersz + 2) * 8 + kolumna - 1);
                }
            }

            if (wiersz + 1 <= 7 && kolumna - 2 >= 0)
            {
                if ((pomocnicza_szachownica.figura_na_polu((wiersz + 1) * 8 + kolumna - 2) is null))
                {
                    zwrot.Add((wiersz + 1) * 8 + kolumna - 2);
                }
                    
                if (!(pomocnicza_szachownica.figura_na_polu((wiersz + 1) * 8 + kolumna - 2) is null))
                {
                    if (pomocnicza_szachownica.figura_na_polu((wiersz + 1) * 8 + kolumna - 2).ktory_gracz() != this.bialy_gracz)
                        zwrot.Add((wiersz + 1) * 8 + kolumna - 2);
                }
            }

            if (wiersz - 2 >= 0 && kolumna - 1 >= 0)
            {
                if ((pomocnicza_szachownica.figura_na_polu((wiersz - 2) * 8 + kolumna - 1) is null))
                    zwrot.Add((wiersz - 2) * 8 + kolumna - 1);

                if (!(pomocnicza_szachownica.figura_na_polu((wiersz - 2) * 8 + kolumna - 1) is null))
                {
                    if (pomocnicza_szachownica.figura_na_polu((wiersz - 2) * 8 + kolumna - 1).ktory_gracz() != this.bialy_gracz)
                        zwrot.Add((wiersz - 2) * 8 + kolumna - 1);
                }
            }

            if (wiersz - 1 >= 0 && kolumna - 2 >= 0)
            {
                if ((pomocnicza_szachownica.figura_na_polu((wiersz - 1) * 8 + kolumna - 2) is null))
                    zwrot.Add((wiersz - 1) * 8 + kolumna - 2);

                if (!(pomocnicza_szachownica.figura_na_polu((wiersz - 1) * 8 + kolumna - 2) is null))
                {
                    if (pomocnicza_szachownica.figura_na_polu((wiersz - 1) * 8 + kolumna - 2).ktory_gracz() != this.bialy_gracz)
                        zwrot.Add((wiersz - 1) * 8 + kolumna - 2);
                }
            }

            if (wiersz + 2 <= 7 && kolumna + 1 <= 7)
            {
                if ((pomocnicza_szachownica.figura_na_polu((wiersz + 2) * 8 + kolumna + 1) is null))
                    zwrot.Add((wiersz + 2) * 8 + kolumna + 1);

                if (!(pomocnicza_szachownica.figura_na_polu((wiersz + 2) * 8 + kolumna + 1) is null))
                {
                    if (pomocnicza_szachownica.figura_na_polu((wiersz + 2) * 8 + kolumna + 1).ktory_gracz() != this.bialy_gracz)
                        zwrot.Add((wiersz + 2) * 8 + kolumna + 1);
                }
            }

            if (wiersz + 1 <= 7 && kolumna + 2 <= 7)
            {
                if ((pomocnicza_szachownica.figura_na_polu((wiersz + 1) * 8 + kolumna + 2) is null))
                    zwrot.Add((wiersz + 1) * 8 + kolumna + 2);

                if (!(pomocnicza_szachownica.figura_na_polu((wiersz + 1) * 8 + kolumna + 2) is null))
                {
                    if (pomocnicza_szachownica.figura_na_polu((wiersz + 1) * 8 + kolumna + 2).ktory_gracz() != this.bialy_gracz)
                        zwrot.Add((wiersz + 1) * 8 + kolumna + 2);
                }
            }

            if (wiersz - 2 >= 0 && kolumna + 1 <= 7)
            {
                if ((pomocnicza_szachownica.figura_na_polu((wiersz - 2) * 8 + kolumna + 1) is null))
                    zwrot.Add((wiersz - 2) * 8 + kolumna + 1);

                if (!(pomocnicza_szachownica.figura_na_polu((wiersz - 2) * 8 + kolumna + 1) is null))
                {
                    if (pomocnicza_szachownica.figura_na_polu((wiersz - 2) * 8 + kolumna + 1).ktory_gracz() != this.bialy_gracz)
                        zwrot.Add((wiersz - 2) * 8 + kolumna + 1);
                }
            }

            if (wiersz - 1 >= 0 && kolumna + 2 <= 7)
            {
                if ((pomocnicza_szachownica.figura_na_polu((wiersz - 1) * 8 + kolumna + 2) is null))
                    zwrot.Add((wiersz - 1) * 8 + kolumna + 2);

                if (!(pomocnicza_szachownica.figura_na_polu((wiersz - 1) * 8 + kolumna + 2) is null))
                {
                    if (pomocnicza_szachownica.figura_na_polu((wiersz - 1) * 8 + kolumna + 2).ktory_gracz() != this.bialy_gracz)
                        zwrot.Add((wiersz - 1) * 8 + kolumna + 2);
                }
            }

            return zwrot;
        }
    }
}
