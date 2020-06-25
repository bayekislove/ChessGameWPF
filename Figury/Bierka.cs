using System.Collections.Generic;

namespace Szachy
{
    /// <summary>
    /// Abstrakcyjna klasa reprezentująca wszystkie figurki
    /// </summary>
    abstract public class Bierka
    {
        /// <summary>
        /// kolor gracza, którego jest dana figura
        /// </summary>
        protected int bialy_gracz;

        /// <summary>
        /// Funkcja pokazuje, na ktore pola moze ruszyc sie gracz  </summary>
        /// <returns> Tablicę pól, na którą można się ruszyć   </returns>
        public abstract List<int> zobacz_mozliwosci(int pole, Szachownica pomocnicza_szachownica);

        /// <summary>
        /// Jakiego koloru jest figurka?
        /// </summary>
        /// <returns>Kolor bierki</returns>
        public int ktory_gracz() 
        {
            return bialy_gracz; 
        }

        /// <summary>
        /// Sprawdza czy ruch jest możliwy, tj czy ruch nie spowoduje szacha
        /// </summary>
        /// <param name="start">Skąd ma być ruch</param>
        /// <param name="koniec">Dokąd ma być ruch</param>
        /// <param name="inp_szachownica">Szachownica na której sprawdzamy możliwość</param>
        /// <returns>P jeśli można się ruszyć bez szacha, F wpp</returns>
   
        public bool ruch_mozliwy(int start, int koniec, Szachownica inp_szachownica)
        {
            int ktory_gracz = inp_szachownica.figura_na_polu(start).ktory_gracz();
            inp_szachownica.wykonaj_ruch(start, koniec);
            inp_szachownica.aktualizuj_szachowanie();
            if (inp_szachownica.czy_szach(ktory_gracz)) //jeśli po ruchu figury jest szach na przeciwnym królu, to wtedy nie można wykonać tego ruchu
                return false;
            return true;
        }

        /// <summary>
        /// Sprawdza na które można się ruszyć (zgodnie z szachowymi zasadami)
        /// </summary>
        /// <param name="pole">Pole figury którą chcemy się ruszyć</param>
        /// <param name="inp_szachownica">Szachownica na której sprawdzamy</param>
        /// <returns>Pola, na które ruch nie spowoduje szacha</returns>

        public List<int> ruchy_bez_szacha(int pole, Szachownica inp_szachownica)
        {
            List<int> strzaly = inp_szachownica.figura_na_polu(pole).zobacz_mozliwosci(pole, inp_szachownica);
            List<int> zwrot = new List<int>();
            foreach (int x in strzaly)
            {
                if (ruch_mozliwy(pole, x, new Szachownica(inp_szachownica)))
                    zwrot.Add(x);
            }
            return zwrot;
        }
    }
}
