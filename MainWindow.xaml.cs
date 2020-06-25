using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Windows.Media.Imaging;

namespace Szachy
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region PolaOkna

        private Szachownica plansza; //mamy jedna szachownice i to w klasie szachownica wykonywane sa odpowiednie dzialania na planszy z bierkami
        private bool klikniete; //jeśli tak, to znaczy, że gracz chce wykonać ruch
        private int czyja_kolej; //jesli = 1, to znaczy, ze ruch wykonuje bialy gracz
        private int start_ruchu; //miejsce z ktorego rusza sie gracz
        private int gracz_zaszachowany; //ktory gracz jest zaszachowany
        private List<Button> przyciski; //tablica z każdym polem, żeby łatwiej było je zmieniać
        private List<int> mozliwe_ruchy; //tutaj będą przechowywane wszystkie pola, na które można się ruszyć

        #endregion PolaOkna

        /// <summary>
        /// Tworzy okienko, zapisuje wszystkie przyciski w tablicy (czyli elementy okienka bezpośrednio) i robi grę
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            przyciski = Field.Children.Cast<Button>().ToList(); //H1- 0; A9 - 63 pole w tablicy;
            NowaGra();
        }

        /// <summary>
        /// Funckcja bierze pole a zwraca jego graficzną reprezentację w UI
        /// </summary>
        /// <param name="value">Pole które chcemy przekonwertować na obrazek</param>
        /// <returns>Zwraca obrazek, który reprezentuje stan pola</returns>

        public BitmapImage Convert(SzachownicaPole value)
        {

            SzachownicaPole do_konwersji = (SzachownicaPole)value;
            string lokalizacja = "";
            lokalizacja = do_konwersji.biale_pole_planszy == 1 ? "Biale" : "Czarne";

            Bierka figuraWobrazek = do_konwersji.figurka;
            if (!(figuraWobrazek is null))
            {
                if (figuraWobrazek is Pionek)
                    lokalizacja += "Pion";
                else if (figuraWobrazek is Krol)
                {
                    lokalizacja += "Krol";
                    if (figuraWobrazek.ktory_gracz() == gracz_zaszachowany)
                        lokalizacja += "Szach";
                }
                else if (figuraWobrazek is Krolowa)
                    lokalizacja += "Krolowa";
                else if (figuraWobrazek is Goniec)
                    lokalizacja += "Goniec";
                else if (figuraWobrazek is Wieza)
                    lokalizacja += "Wieza";
                else if (figuraWobrazek is Skoczek)
                    lokalizacja += "Skoczek";

                lokalizacja += figuraWobrazek.ktory_gracz() == 1 ? "Biale" : "Czarne";
            }

            return new BitmapImage(new Uri($"pack://application:,,,/Images/{lokalizacja}.png"));
        }

        /// <summary>
        /// Funkcja ustawia figury na planszy i resetuję grę
        /// </summary>

        private void NowaGra()
        {
            plansza = new Szachownica();
            czyja_kolej = 1;
            klikniete = false;
            gracz_zaszachowany = -1;
            aktualizuj_plansze();
        }

        /// <summary>
        /// Funkcja aktualizuje w UI pozycje pionków -> przechodzi po tablicy i ustawia nowe pionki
        /// Wynajduje też i aktualizuje informacje gdzie znajdują się dane wieże (to jest niezbędne do wykonania roszady)
        /// </summary>
        private void aktualizuj_plansze()
        {
            List<int> biale_wieze = new List<int>(2);
            List<int> czarne_wieze = new List<int>(2);
            int pozycja_bialego_krola = 0;
            int pozycja_czarnego_krola = 0;

            for (int i = 0; i < 64; i++)
            {
                przyciski[i].BorderThickness = new Thickness(0);

                var brush = new ImageBrush();
                //jeśli_gracz zasachowany to znaczy ze zaden gracz niezaszachowany ergo nie ma szacha
                brush.ImageSource = Convert(plansza.poleNaSzachownicy(i));
                brush.Stretch = Stretch.Fill;
                przyciski[i].Background = brush;                                                                    
                if (plansza.figura_na_polu(i) is Krol)
                {
                    if (plansza.figura_na_polu(i).ktory_gracz() == 1)
                        pozycja_bialego_krola = i;
                    else
                        pozycja_czarnego_krola = i;
                }

                if (plansza.figura_na_polu(i) is Wieza)
                {
                    if (plansza.figura_na_polu(i).ktory_gracz() == 1)
                        biale_wieze.Add(i);
                    else
                        czarne_wieze.Add(i);
                }
            }
            //aktualizacja pozycji wież dla białego króla
            if (biale_wieze.Count == 0)
                ((Krol)plansza.figura_na_polu(pozycja_bialego_krola)).pozycja_pierwszej_wiezy = -1;
            else if (biale_wieze.Count == 1)
                ((Krol)plansza.figura_na_polu(pozycja_bialego_krola)).pozycja_pierwszej_wiezy = biale_wieze[0];
            else if (biale_wieze.Count == 2)
                ((Krol)plansza.figura_na_polu(pozycja_bialego_krola)).pozycja_drugiej_wiezy = biale_wieze[1];
            else
                ((Krol)plansza.figura_na_polu(pozycja_bialego_krola)).pozycja_drugiej_wiezy = -1;
            //aktualizacja pozycji wież dla czarnego króla
            if (czarne_wieze.Count == 0)
                ((Krol)plansza.figura_na_polu(pozycja_czarnego_krola)).pozycja_pierwszej_wiezy = -1;
            else if (czarne_wieze.Count == 1)
                ((Krol)plansza.figura_na_polu(pozycja_czarnego_krola)).pozycja_pierwszej_wiezy = czarne_wieze[0];
            else if (czarne_wieze.Count == 2)
                ((Krol)plansza.figura_na_polu(pozycja_czarnego_krola)).pozycja_drugiej_wiezy = czarne_wieze[1];
            else
                ((Krol)plansza.figura_na_polu(pozycja_czarnego_krola)).pozycja_drugiej_wiezy = -1;
        }

        /// <summary>
        /// Funkcja przechodzi po kazdym polu, na ktore mozna wykonac ruch i zaznacza, ze ruch na to pole jest mozliwy
        /// </summary>
        /// <param name="sugerowane_pola">Pola, na które można się ruzsyć</param>
        private void pokaz_mozliwosci_na_planszy(List<int> sugerowane_pola)
        {
            foreach(int i in sugerowane_pola)
            {
                przyciski[i].BorderThickness = new Thickness(5);
                przyciski[i].BorderBrush = Brushes.Green;
            }
        }

        /// <summary>
        /// Funkcja, w której będą odbywać się wszystkie ruchy na planszy
        /// </summary>
        /// <param name="sender">W tym przypadku to kliknięty przycisk w aplikacji</param>

        private void Field_Click(object sender, RoutedEventArgs e)
        {
            var skadklikniete = (Button)sender;
            int pole_klikniete = Grid.GetRow(skadklikniete) * 8 + Grid.GetColumn(skadklikniete); //wzor na pole w tablicy -> nr pola = wiersz * 8 + kolumna 

            #region Start Ruchu
            //w tym przypadku pole_klikniete to miejsce, z ktorego chcemy wykonac ruch
            if (!klikniete)
            {
                if (plansza.figura_na_polu(pole_klikniete) is null)
                    return;
                if (plansza.figura_na_polu(pole_klikniete).ktory_gracz() != czyja_kolej)
                    return;

                klikniete = true;
                start_ruchu = pole_klikniete;

                mozliwe_ruchy = (new Szachownica(plansza)).figura_na_polu(start_ruchu).ruchy_bez_szacha(pole_klikniete, plansza);

                pokaz_mozliwosci_na_planszy(mozliwe_ruchy);
                return;
            }

            #endregion Start Ruchu

            #region PotwierdzenieRuchu

            //w tym przypadku pole_klikniete to miejsce, na ktore chcemy wykonac ruch
            if (mozliwe_ruchy.Contains(pole_klikniete))
            {
                Bierka ruszona = plansza.figura_na_polu(start_ruchu);
                if (ruszona is Pionek)
                    ((Pionek)ruszona).ruszono = true;

                if (ruszona is Krol)
                {
                    ((Krol)ruszona).ruszono = true;
                    int ktora_strona;
                    if (plansza.figura_na_polu(pole_klikniete) is Wieza)
                    {
                        ktora_strona = start_ruchu < pole_klikniete ? 1 : -1; //zaleznie od tego, gdzie znajduje sie wieza idziemy w inna strone
                        plansza.ustaw_na_polu(new Wieza(ruszona.ktory_gracz()), start_ruchu + ktora_strona * 2 + -ktora_strona); 
                        plansza.wykonaj_ruch(start_ruchu, start_ruchu + ktora_strona * 2);
                    }                          
                }

                plansza.wykonaj_ruch(start_ruchu, pole_klikniete);
                plansza.aktualizuj_szachowanie();
                czyja_kolej = czyja_kolej == 1 ? 0 : 1; //zaznaczamy, że ruch odbył się poprawnie i zmieniamy obecnego gracza

                if (plansza.czy_szach(czyja_kolej))
                {
                    gracz_zaszachowany = czyja_kolej;
                    if (plansza.czy_mat(czyja_kolej)) //jeśli jest mat to pytamy gracza czy chce zagrać jeszcze raz
                    {
                        aktualizuj_plansze();
                        string kto_wygral = czyja_kolej == 0 ? "White's" : "Black's";
                        MessageBoxResult wynik = MessageBox.Show(kto_wygral + " have won!\nDo you want to play again?", "New game box",  MessageBoxButton.YesNo);
                        switch (wynik)
                        {
                            case MessageBoxResult.Yes:
                                NowaGra();
                                break;
                            case MessageBoxResult.No:
                                Close();
                                break;
                        }
                    }
                }
                    
                else
                    gracz_zaszachowany = -1;
            }

            else
            {
                mozliwe_ruchy = new List<int>(); //zerujemy wszystkie poprzednie możliwe ruchy, bo anulowaliśmy ruch
            }

            aktualizuj_plansze();
            klikniete = false; //wracamy sprzed kliknięcia, jest to swoiste anulowanie poprzedniego kliknięcia

            #endregion PotwierdzenieRuchu
        }
    }
}
