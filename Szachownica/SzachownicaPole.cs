namespace Szachy
{
    /// <summary>
    /// Ta klasa przechowuje informacje o polu na szachownicy -> trzyma figurę, kolor pola i czy ruch na pole jest możliwy
    /// </summary>
    public class SzachownicaPole
    {
        /// <summary>
        /// Pokazuje czy pole na planszy ma bialy czy czarny kolor (TO NIE JEST KOLOR FIGRUKI)
        /// </summary>
        
        public int biale_pole_planszy { get; set; }
        /// <summary>
        /// Pokazuje, jaka bierka stoi na planszy
        /// </summary>
        
        public Bierka figurka { get; set; }

        /// <summary>
        /// Konstruktor przyjmujacy bierkę
        /// </summary>
        /// <param name="bierka">Figura, która stoi na polu </param>
        
        public SzachownicaPole(Bierka bierka)
        {
            this.figurka = bierka;
            this.biale_pole_planszy = 0;
        }
        
        /// <summary>
        /// Konstruktor kopiujący -> taki, który tworzy nowe figurki
        /// </summary>
        /// <param name="inp">Pole do skopiowania</param>
        
        public SzachownicaPole(SzachownicaPole inp)
        {
            this.biale_pole_planszy = inp.biale_pole_planszy;

            Bierka pom = inp.figurka;
            if (pom is Pionek)
                this.figurka = new Pionek((Pionek)pom);
            else if (pom is Krol)
                this.figurka = new Krol((Krol)pom);
            else if (pom is Goniec)
                this.figurka = new Goniec((Goniec)pom);
            else if (pom is Krolowa)
                this.figurka = new Krolowa((Krolowa)pom);
            else if (pom is Skoczek)
                this.figurka = new Skoczek((Skoczek)pom);
            else if (pom is Wieza)
                this.figurka = new Wieza((Wieza)pom);
            else
                this.figurka = null;

        }
    }
}
