using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Sah.Helper;

namespace Sah.Klase
{
    public class SahovskaTabla
    {
        public Polje[,] matricaPolja;

        public SahovskaTabla()
        {
            matricaPolja = new Polje[8, 8];
        }

        public bool? pomeriIPojedi(Figura figura, Polje polje)
        {
            if (figura.mogucePomeranjeNaDatoPolje(polje))
            {
                figura.Polje.figuraNaPolju = null; //uklanja figuru sa polja na kom je trenutno

                if (matricaPolja[polje.brojReda,  SlovoUBroj.vrednostKolonePrekoSlova[polje.slovoKolone]] != null) //ako je pojeo nesto na tom polju
                {
                    figura.Polje = polje;
                    figura.Polje.figuraNaPolju = figura;
                    matricaPolja[polje.brojReda, SlovoUBroj.vrednostKolonePrekoSlova[polje.slovoKolone]] = figura.Polje;
                    MessageBox.Show("Pomerena figura, pojedena druga figura");
                    return true;
                }
                else {
                    figura.Polje = polje;
                    figura.Polje.figuraNaPolju = figura;
                    matricaPolja[polje.brojReda,  SlovoUBroj.vrednostKolonePrekoSlova[polje.slovoKolone]] = figura.Polje;
                    MessageBox.Show("Pomerena figura, nije nista pojedeno"); 
                    return false; 
                }
            }
            else { MessageBox.Show("Nije uspelo pomeranje figure. Trenutno sam u klasi Sahovska tabla"); return null; }
        }

        public Figura? vratiFiguruSaPolja(string slovoKolone, int brojReda)
        {
            return matricaPolja[brojReda, SlovoUBroj.vrednostKolonePrekoSlova[slovoKolone]].figuraNaPolju; //moze da vrati null
        }

        public string[,] tekstualniOpisTable()
        {
            string[,] matricaTekstualnihOznaka = new string[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (matricaPolja[i, j] == null) //ako je je polje null, tj samo napravljeno
                    {
                        matricaTekstualnihOznaka[i, j] =  "_";
                    } else if ( matricaPolja[i, j]!= null && matricaPolja[i, j].figuraNaPolju == null) //ako polje postoji, ali nema figuru (slucaj uklonjena figura)
                    {
                        matricaTekstualnihOznaka[i, j] = "_";
                    } else //postoji figura
                    {
                        matricaTekstualnihOznaka[i, j] = matricaPolja[i, j].figuraNaPolju.Oznaka;
                    }
                }
            }
            return matricaTekstualnihOznaka;
        }
    }
}
