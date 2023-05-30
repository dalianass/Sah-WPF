using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Sah.Helper;

namespace Sah.Klase
{
    public class Polje
    {
        public string slovoKolone { get; set; }
        public int brojReda { get; set; }
        public string Boja { get; set; }
        public Figura? figuraNaPolju { get; set; } //nullable
        public Polje(int red, string kolona, string boja)
        {
            this.slovoKolone = kolona;
            this.brojReda = red;
            this.Boja = boja;
            this.figuraNaPolju = null; //naknadno dodajem figuru

        }

        //public void dodajFiguruNaPolje(Figura figura) {
        //    this.figuraNaPolju = figura;
        //}

        //public void ukloniFiguruSaPolja()
        //{
        //    this.figuraNaPolju = null;
        //}

        public bool uIstomRedu(Polje polje)
        {
            if (brojReda == polje.brojReda) return true;
            else return false;
        }

        public bool uIstojKoloni(Polje polje)
        {
            if (slovoKolone == polje.slovoKolone) return true;
            else return false;
        }

        public bool uDijagonali(Polje polje)
        {
            if (!(slovoKolone == polje.slovoKolone && brojReda == polje.brojReda)) //ako nije to isto polje
            {
                if (Math.Abs(SlovoUBroj.vrednostKolonePrekoSlova[polje.slovoKolone] - SlovoUBroj.vrednostKolonePrekoSlova[slovoKolone]) == Math.Abs(polje.brojReda - brojReda)) //ako je razlika rastojanja izmedju redova i kolona ista, u dijagonali su
                {
                    MessageBox.Show("Jesu u dijagonali");
                    return true;
                }
                else
                {
                    MessageBox.Show("Nisu u dijagonali");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Ista polja");
                return false;
            }
        }

        public int racunajRastojanje(Polje polje)
        {
            var rastojanjeRedovi = Math.Abs(polje.brojReda - this.brojReda);
            var rastojanjeKolone = Math.Abs(SlovoUBroj.vrednostKolonePrekoSlova[polje.slovoKolone] - SlovoUBroj.vrednostKolonePrekoSlova[this.slovoKolone]);
            return rastojanjeKolone + rastojanjeRedovi;
        }

        public string tekstualniOpisPolja()
        {
            string rez = this.slovoKolone + this.brojReda; //posto je slovoKolone tipa string, vrsice konkatenaciju
            return rez.Length != 0 ? rez : "nema oznake";
        }
    }
}
