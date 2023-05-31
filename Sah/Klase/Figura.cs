using Sah.Klase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Sah.Klase
{
    public class Figura
    {
        public string Boja { get; set; }
        public Polje Polje { get; set; }
        public string Oznaka { get; set; }
        
        public Figura(string boja, Polje polje, string oznaka)
        {
            this.Boja = boja; 
            this.Polje = polje;
            this.Oznaka = oznaka;
            this.Polje.figuraNaPolju = this; //odgovarajucem polju zadajem figuru
        }
        public bool mogucePomeranjeNaDatoPolje(Polje polje)
        {
        if (this.Oznaka == "T") //Top se krece samo vertikalno i horizontalno
        {
            if (this.Polje.slovoKolone == polje.slovoKolone) { return true; }
            if (this.Polje.brojReda == polje.brojReda) { return true; }
            else { return false; }
        }
        else if (this.Oznaka == "S") //Skakac se pomera za 2 kolone i 1 red ili za 1 kolonu i 2 reda - rastojanje je u tom slucaju 3
        {
            if (this.Polje.racunajRastojanje(polje) == 3) { return true; } //metoda koja racuna rastojanje
            else { return false;}
        }
            //Kralj se krece jedno polje vertikalno (po koloni), horizontalno (po redu) ili dijagonalno. Kralja nije moguće pomeriti na
            //polje koje je napadnuto od strane bilo koje figure druge boje. 
            else if (this.Oznaka == "K" && !(polje.figuraNaPolju != null && polje.figuraNaPolju.Boja != this.Boja)) //ako je u pitanju kralj i ako na tom polju ne postoji vec neka figura druge boje od kralja
        {
            if (this.Polje.uIstojKoloni(polje) && this.Polje.racunajRastojanje(polje) == 1) { return true; } //moze se pomeriti samo za jedno mesto u koloni
            if (this.Polje.uIstomRedu(polje) && this.Polje.racunajRastojanje(polje) == 1) { return true;} //moze se pomeriti samo za jedno mesto u redu
            if (this.Polje.uDijagonali(polje) && this.Polje.racunajRastojanje(polje) == 1) { return true; } //moze se pomeriti samo za jedno mesto dijagonalno
            else { return false; }

        }
        else if (this.Oznaka == "D") //Dama se moze kretati proizvoljan broj polja vertikalno (po koloni), horizontalno (po redu) ili dijagonalno. 
        {
            if (this.Polje.uIstojKoloni(polje)) { return true;}
            if (this.Polje.uIstomRedu(polje)) { return true; }
            if (this.Polje.uDijagonali(polje)) { return true; }
            else { return false;}
        }
        else { return false; }
        }

        public string FormatirajOznakuFigure(Figura figura)
        {
            if(figura.Boja == "bela")
            {
                return figura.Oznaka.ToLower();
            } else if(figura.Boja == "crna")
            {
                return figura.Oznaka.ToUpper();
            } else
            {
               return "Doslo je do greske. Ne mogu da prepoznam oznaku figure.";
            }
        }

        public string FormatirajOznakuFigure()
        {
            if (this.Boja == "bela")
            {
                return this.Oznaka.ToLower();
            }
            else if (this.Boja == "crna")
            {
                return this.Oznaka.ToUpper();
            }
            else
            {
                return "Doslo je do greske. Ne mogu da prepoznam oznaku figure.";
            }
        }

        public string tekstualniOpisFiguraPolje()
        {
            if(this.FormatirajOznakuFigure().Length < 4) //ako vraca oznaku, jer najduza ima 4 slova
            {
                return this.Polje.tekstualniOpisPolja() + this.FormatirajOznakuFigure();

            } else
            {
                return "Greska u oznaci figure";
            }
        }
    }
}
