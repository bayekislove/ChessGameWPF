using System.Collections.Generic;

namespace Szachy
{
    /// <summary>
    /// Klasa odpowiada szachowej królowej
    /// </summary>
    public class Krolowa : Bierka
    {
        /// <summary>
        /// Konstruktor 
        /// </summary>
        /// <param name="kolor">Kolor gracza, którego jest ta bierka (biały lub czarny)</param>
        public Krolowa(int kolor)
        {
            bialy_gracz = kolor;
        }

        /// <summary>
        /// Konstruktor kopiujący
        /// </summary>
        /// <param name="inp">Figura którą chcemy przekopiować</param>
        public Krolowa(Krolowa inp)
        {
            bialy_gracz = inp.ktory_gracz();
        }

        /// <summary>
        /// Z racji, że ruch królowej to kombinacja możliwości ruchu gońca i wieży, to sumuję pola w które mogą ruszyć się te figury, 
        /// jakby żyły na miejscu królowej
        /// </summary>

        public override List<int> zobacz_mozliwosci(int pole, Szachownica pomocnicza_szachownica)
        {

            Goniec goniec_pom = new Goniec(bialy_gracz);
            pomocnicza_szachownica.ustaw_na_polu(goniec_pom, pole);
            List<int> mozliwosci_gonca = goniec_pom.zobacz_mozliwosci(pole, pomocnicza_szachownica);

            Wieza wieza_pom = new Wieza(bialy_gracz);
            pomocnicza_szachownica.ustaw_na_polu(wieza_pom, pole);
            List<int> mozliwosci_wiezy = wieza_pom.zobacz_mozliwosci(pole, pomocnicza_szachownica);
            mozliwosci_gonca.AddRange(mozliwosci_wiezy);
            pomocnicza_szachownica.ustaw_na_polu(new Krolowa(bialy_gracz), pole); //królowa wraca na pole

            return mozliwosci_gonca;
        }

        /// <summary>
        /// Wylicza najkrótszą ścieżkę, którą musi pokonać figura żeby dostać się do króla
        /// </summary>
        /// <param name="pole_krola">Pole króla</param>
        /// <param name="pole_krolowej">Pole królowej</param>
        /// <returns>Najkrótszą ścieżkę, którą musi pokonać królowa żeby dostać się do króla</returns>

        public List<int> najkrotsza_sciezka(int pole_krola, int pole_krolowej)
        {
            var krolowa_kolumna = pole_krolowej % 8;
            var krolowa_wiersz = (pole_krolowej - krolowa_kolumna) / 8;

            var krol_kolumna = pole_krola % 8;
            var krol_wiersz = (pole_krola - krol_kolumna) / 8;

            if (krol_kolumna == krolowa_kolumna ||
                krol_wiersz == krolowa_wiersz)
                return (new Wieza(1)).najkrotsza_sciezka(pole_krola, pole_krolowej);
            else
                return (new Goniec(1)).najkrotsza_sciezka(pole_krola, pole_krolowej);
        }
    }
}
