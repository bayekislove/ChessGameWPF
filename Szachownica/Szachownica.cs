using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Windows.Controls;

// konwencja nazewnicza - 1 -> gracz biały; 0 -> gracz czarny;
namespace Szachy
{
    /// <summary>
    /// Klasa jest reprezentacją szachownicy w grze -> zawiera pola i figury na nich 
    /// </summary>
    
    public class Szachownica
    {
        /// <summary>
        /// Info o polach
        /// </summary>
        private SzachownicaPole[] plansza;
        /// <summary>
        /// Info o tym, figury z których pól szachują dane pole
        /// </summary>
        private List<int>[] zaszachowane_pola;
        /// <summary>
        /// Pozycja białego króla na tej szachownicy
        /// </summary>
        private int bialy_krol;
        /// <summary>
        /// Pozycja czarnego króla na tej szachownicy
        /// </summary>
        private int czarny_krol;

        /// <summary>
        /// Tworzymy obiekt klasy, która przechowuje informacje o wszystkich figurach na planszy
        /// </summary>

        public Szachownica()
        {
            plansza = new SzachownicaPole[64]; ///tablica jednowymiarowa - tyle miejsc - ile pol
            bialy_krol = 60;
            czarny_krol = 4;
            zaszachowane_pola = new List<int>[64];
            for (int i = 0; i < 64; i++)
            {
                zaszachowane_pola[i] = new List<int>();
            }

            zainicjuj_gracza(1);
            zainicjuj_gracza(0);
            zainicjuj_kolory();
        }

        /// <summary>
        /// Zwraca tablicę z szachującymi polami dla danej szachownicy
        /// </summary>
        /// <returns>Tablicę list na której dla każdego pola są pola, które je szachują</returns>
        
        public List<int>[] sprawdz_szachowane()
        {
            return zaszachowane_pola;
        }

        /// <summary>
        /// Ustawiamy wszystkie bierki na odpowiednich miejscach na planszy
        /// </summary>

        private void zainicjuj_gracza(int kolor)
        {
            int wiersz = kolor == 0 ? 1 : 6; // czarny kolor -> ustawiamy pionki w 2gimi rzedzie, jeśli nie, to w 7
            for (int i = 0; i < 8; i++)
            {
                plansza[wiersz * 8 + i] = new SzachownicaPole(new Pionek(kolor));
            }
            wiersz = kolor == 0 ? 0 : 7; //jeśli kolor == 0-> czarny gracz -> ustawiamy na górze, otherwise na dole
            //startowo ustawiamy kolor kazdego pola na bialy, pozniej przejdziemy sie po calej tablict i pozmieniamy jak trzeba
            plansza[wiersz * 8] = new SzachownicaPole(new Wieza(kolor));
            plansza[wiersz * 8 + 7] = new SzachownicaPole(new Wieza(kolor));
            plansza[wiersz * 8 + 1] = new SzachownicaPole(new Skoczek(kolor));
            plansza[wiersz * 8 + 6] = new SzachownicaPole(new Skoczek(kolor));
            plansza[wiersz * 8 + 2] = new SzachownicaPole(new Goniec(kolor));
            plansza[wiersz * 8 + 5] = new SzachownicaPole(new Goniec(kolor));
            plansza[wiersz * 8 + 4] = new SzachownicaPole(new Krol(kolor));
            plansza[wiersz * 8 + 3] = new SzachownicaPole(new Krolowa(kolor));
        }

        /// <summary>
        /// Tworzy szachownicę z pól -> każdemu polu ustawia kolor
        /// </summary>

        private void zainicjuj_kolory()
        {
            int kolor = 1;

            for (int i = 0; i < 64; i++)
            {
                if (plansza[i] is null)
                    plansza[i] = new SzachownicaPole((Bierka)null);

                plansza[i].biale_pole_planszy = kolor;

                if (i % 8 == 7) //jeśli jesteśmy na końcu wiersza, to kolor się nie zmienia - tak, jak w szachownicy 
                    continue;
                else
                    kolor = kolor == 1 ? 0 : 1;
            }
        }
        
        /// <summary>
        /// Sprawdza jaki obiekt klasy PoleSzachownicy (czyli info o kolorze pola i jakiego koloru jest pole)
        /// </summary>
        /// <param name="i">Nr pola które chcemy sprawdzić</param>
        /// <returns>Reprezentację pola</returns>

        public SzachownicaPole poleNaSzachownicy(int i)
        {
            return plansza[i];
        }

        /// <summary>
        /// Konstruktor kopiujący
        /// </summary>
        /// <param name="pom"></param>
        public Szachownica(Szachownica pom)
        {
            plansza = new SzachownicaPole[64];
            for (int i = 0; i < 64; i++)
            {
                plansza[i] = new SzachownicaPole(pom.poleNaSzachownicy(i));
            }
            this.czarny_krol = pom.czarny_krol;
            this.bialy_krol = pom.bialy_krol;
        }

        /// <summary>
        /// Funkcja sprawdza, jaka figurka stoi na inputowanym polu
        /// </summary>
        /// <param name="pole">Pole które chcemy sprawdzić</param>
        /// <returns>Bierka, która stoi na polu</returns>

        public Bierka figura_na_polu(int pole)
        {
  
            return plansza[pole].figurka;
        }

        /// <summary>
        /// Funkcja zamienia figurę danego pola na inputowaną bierkę
        /// </summary>
        /// <param name="figura">Figurka, którą chcemy ustawić</param>
        /// <param name="pole">Pole na którym chcemy ustawić obiekt klasy bierka</param>

        public void ustaw_na_polu(Bierka figura, int pole)
        {
            plansza[pole].figurka = figura;
        }

        /// <summary>
        /// Funkcja, która wykonuje ruch w tablicy Bierek -> kiedy ruch możliwy, to przenosi bierkę z pola startowego na końcowe 
        /// </summary>
        /// <param name="pole_startowe">Miejsce, z ktorego sie ruszamy</param>
        /// <param name="pole_docelowe">Miejsce, do ktorego sie ruszamy</param>

        public void wykonaj_ruch(int pole_startowe, int pole_docelowe)
        {
            Bierka ruszona = this.figura_na_polu(pole_startowe);
            if(ruszona is Krol)
            {
                czarny_krol = ruszona.ktory_gracz() == 0 ? pole_docelowe : czarny_krol;
                bialy_krol = ruszona.ktory_gracz() == 1 ? pole_docelowe : bialy_krol;
            }
            plansza[pole_startowe].figurka = null;
            plansza[pole_docelowe].figurka = ruszona;
        }

        /////////////////////////////////// LOGIKA SZACHOWANIA //////////////////////////////////////////////////
 
        /// <summary>
        /// Funkcja sprawdza które pole jest przez jakie figury atakowane
        /// </summary>
        
        public void aktualizuj_szachowanie()
        {
            zaszachowane_pola = new List<int>[64];

            for (int i = 0; i < 64; i++)
                zaszachowane_pola[i] = new List<int>();

            for (int it = 0; it < 64; it++)
            {
                if (plansza[it] is null || plansza[it].figurka is null)
                    continue;

                List<int> co_szachowane_przez_figure;

                if (plansza[it].figurka is Pionek)
                {
                    co_szachowane_przez_figure = ((Pionek)plansza[it].figurka).zobacz_szachowane(it, this);
                }
                else
                {
                    co_szachowane_przez_figure = plansza[it].figurka.zobacz_mozliwosci(it, this);
                }

                foreach (int essa in co_szachowane_przez_figure)
                    zaszachowane_pola[essa].Add(it);

            }
        }

        /// <summary>
        /// Sprawdza czy jest szach
        /// </summary>
        /// <param name="gracz">Którego gracza sprawdzić</param>
        /// <returns>P jeśli król jest atakowany przez figurę drugiego gracza, F wpp</returns>
        
        public bool czy_szach(int gracz) 
        {
            int krol_gracza = gracz == 1 ? bialy_krol : czarny_krol;
            foreach (int it in zaszachowane_pola[krol_gracza])
            {
                if (plansza[it].figurka.ktory_gracz() != gracz) //jeśli pole z królem atakuje figura gracza przeciwnego, to szach
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Sprawdza czy król gracza może się obronić przed szachem
        /// </summary>
        /// <param name="gracz">Którego gracza sprawdzić</param>
        /// <returns>P jeśli mat, F wpp</returns>

        public bool czy_mat(int gracz)
        {
            bool pionek_blokujacy(int gdzie_zablokowac)
            {
                var gdzie_patrzec = gracz == 1 ? -1 : 1;

                if (gdzie_zablokowac + gdzie_patrzec * 8 < 0 ||
                    gdzie_zablokowac + gdzie_patrzec * 8 > 63)
                    return false;

                if (!(this.figura_na_polu(gdzie_zablokowac + gdzie_patrzec * 8) is null))
                    if (figura_na_polu(gdzie_zablokowac + gdzie_patrzec * 8) is Pionek && 
                        ((Pionek)figura_na_polu(gdzie_zablokowac + gdzie_patrzec * 8)).ruch_mozliwy(
                        gdzie_zablokowac + gdzie_patrzec * 8, gdzie_zablokowac, new Szachownica(this)) &&
                        ((Pionek)figura_na_polu(gdzie_zablokowac + gdzie_patrzec * 8)).ktory_gracz() == gracz)
                        return true;

                gdzie_patrzec *= 2;

                if (gdzie_zablokowac + gdzie_patrzec * 8 < 0 ||
                    gdzie_zablokowac + gdzie_patrzec * 8 > 63)
                    return false;

                if (!(this.figura_na_polu(gdzie_zablokowac + gdzie_patrzec * 8) is null))
                    if (figura_na_polu(gdzie_zablokowac + gdzie_patrzec * 8) is Pionek &&
                        ((Pionek)figura_na_polu(gdzie_zablokowac + gdzie_patrzec * 8)).ruch_mozliwy(
                        gdzie_zablokowac + gdzie_patrzec * 8, gdzie_zablokowac, new Szachownica(this)) &&
                        ((Pionek)figura_na_polu(gdzie_zablokowac + gdzie_patrzec * 8)).ktory_gracz() == gracz)
                        return true;

                return false;
            }

            int krol_gracza = gracz == 1 ? bialy_krol : czarny_krol;

            if (plansza[krol_gracza].figurka.ruchy_bez_szacha(krol_gracza, this).Count != 0) //krol moze umknąć, więc mata nie ma
                return false;
            // a jeśli król nie może uciec...
            foreach (int it in zaszachowane_pola[krol_gracza]) //plansza[it] to figura atakujaca krola
            { 
                bool uratowany = false;

                if (plansza[it].figurka.ktory_gracz() != gracz) //jeśli pole króla atakuje jakaś figura przeciwnego gracza
                {
                    if ((plansza[it].figurka is Skoczek)     //skoczka lub pionka nie da się zasłonić,
                        || (plansza[it].figurka is Pionek))  //więc jedynie da się go zabić (jedyna opcja na obronienie się przed szachem)
                    {
                        foreach (int atak in zaszachowane_pola[it])
                            if (plansza[atak].figurka.ktory_gracz() == gracz &&
                                plansza[atak].figurka.ruch_mozliwy(atak, it, new Szachownica(this)))
                                uratowany = true;
                        if (!uratowany) //jeśli nie da się uratować króla, to jest mat
                            return true;
                    }

                    else //wpp musimy sprawdzić, czy da się zablokować figurę atakującą (ale przy tym nie odsłonić króla!!!), bierzemy ścieżkę od figury atakującej
                         // do figury króla i sprawdzamy dla każdego pola czy na nim da się zablokować gońca
                    {
                        List<int> droga = new List<int>();
                        if (plansza[it].figurka is Krolowa)
                            droga = ((Krolowa)plansza[it].figurka).najkrotsza_sciezka(krol_gracza, it);

                        else if (plansza[it].figurka is Goniec)
                            droga = ((Goniec)plansza[it].figurka).najkrotsza_sciezka(krol_gracza, it);

                        else
                            droga = ((Wieza)plansza[it].figurka).najkrotsza_sciezka(krol_gracza, it);

                        foreach (int pole in droga) //dla kazdego pola sprawdzamy, ktore figury go blokuja i czy bez szacha moga wykonac ruch
                        {
                            if (pionek_blokujacy(pole)) //jesli istnieje taki pionek, że można uratować
                                uratowany = true;

                            foreach (int atak in zaszachowane_pola[pole])
                                if (plansza[atak].figurka.ktory_gracz() == gracz && //jeśli możemy zabkokować, to wtedy 
                                    plansza[atak].figurka.ruch_mozliwy(atak, pole, new Szachownica(this)))
                                    uratowany = true;

                            if (!uratowany)
                                return true;
                        }
                    }
                }
            }
            return false; //jeśli wszystkie pola się obroniły, to znaczy, że mata nie ma
        }
    }
}
