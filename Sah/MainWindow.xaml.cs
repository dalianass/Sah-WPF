using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sah.Klase;
using Sah.Helper;
using static System.Net.Mime.MediaTypeNames;

namespace Sah
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Polje polje;
        Polje polje2;
        Polje polje3;
        Figura figura;
        Figura figura2;
        Figura figura3;
        Grid grid;
        Ellipse selektovanaElipsa;
        SahovskaTabla tabla;

        public MainWindow()
        {
            InitializeComponent();

            tabla = new SahovskaTabla();
            polje = new Polje(2, "G", "crna");
            figura = new Figura("crna", polje, "K");

            polje2 = new Polje(5, "D", "bela");
            figura2 = new Figura("bela", polje2, "D");


            polje3 = new Polje(6, "E", "bela");

            figura3 = new Figura("bela", polje3, "S");

            //tabla.matricaPolja[1, 1] = polje;
            //tabla.matricaPolja[3, 3] = polje2;
            //tabla.matricaPolja[3, 4] = polje3;
            kreirajGridIPolja();
            prikaziTabluTekstualno();



            ////polje3.figuraNaPolju = null;

        }

        private void kreirajGridIPolja()
        {
            grid = new Grid();
            for (int i = 0; i < 10; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            Rectangle rectangle;
            TextBox tb;
            bool neparanRed = true;

            //dodavanje polja
            for (int row = 1; row < 9; row++) //prvi red i poslednji red i prva i poslednja kolona grida su rezervisani za oznake, a ne za sah, zato petlja ide 1-9
            {
                for (int col = 1; col < 9; col++)
                {
                    if (neparanRed && col % 2 == 0 || !neparanRed && col % 2 == 1) //neparan red i parna kolona ili paran red i neparna kolona
                    {
                        tabla.matricaPolja[row - 1, col - 1] = new Polje(row - 1, BrojUSlovo.vrednostSlovaPrekoBrojaKolone[col - 1], "bela");
                        rectangle = new Rectangle
                        {
                            Fill = Brushes.Beige,
                            Tag = tabla.matricaPolja[row-1, col-1]
                        };

                        rectangle.MouseLeftButtonDown += Polje_MouseLeftButtonDown;

                        //OZNAKE POLJA
                        tb = new TextBox
                        {
                            Text = tabla.matricaPolja[row-1, col-1].tekstualniOpisPolja(),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Foreground = Brushes.LightGray ,
                            Background = Brushes.Transparent,
                            BorderBrush = Brushes.Transparent,
                            IsReadOnly = true
                        };

                    } 
                    else
                    {
                        tabla.matricaPolja[row - 1, col - 1] = new Polje(row - 1, BrojUSlovo.vrednostSlovaPrekoBrojaKolone[col - 1], "crna"); 
                        rectangle = new Rectangle
                        {
                            Fill = Brushes.Gray,
                            Tag = tabla.matricaPolja[row - 1, col - 1]
                        };
                        rectangle.MouseDown += Polje_MouseLeftButtonDown;

                        //OZNAKE POLJA
                        tb = new TextBox
                        {
                            Text = tabla.matricaPolja[row - 1, col - 1].tekstualniOpisPolja(),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Foreground = Brushes.LightGray,
                            Background = Brushes.Transparent,
                            BorderBrush = Brushes.Transparent,
                            IsReadOnly = true
                        };
                    }

                    Grid.SetRow(rectangle, row);
                    Grid.SetColumn(rectangle, col);
                    grid.Children.Add(rectangle);

                    Grid.SetRow(tb, row); //prvi red
                    Grid.SetColumn(tb, col);
                    grid.Children.Add(tb);
                }
                neparanRed = !neparanRed;
            }
            dodajOznakeKolonaIRedova(grid);

            dodajFiguruNaGrid(grid, figura);
            dodajFiguruNaGrid(grid, figura2);
            mainGrid.Children.Add(grid);
        }

        public void dodajOznakeKolonaIRedova(Grid grid)
        {
            string[] nizSlova = new string[8] { "A", "B", "C", "D", "E", "F", "G", "H" };

            //SLOVNE OZNAKE - HORIZONTALNE
            for(int i=1; i<9; i++) //jer tako krecu i polja
            {
                TextBlock tb = new TextBlock
                {
                    Text = nizSlova[i-1],
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = Brushes.Red,
                    FontWeight = FontWeights.Bold
                };

                Grid.SetRow(tb, 0); //prvi red
                Grid.SetColumn(tb, i); 
                grid.Children.Add(tb);
            }

            //BROJNE OZNAKE - VERTIKALNE
            for (int i = 1; i < 9; i++) //jer tako krecu i polja
            {
                TextBlock tb = new TextBlock
                {
                    Text = (i-1).ToString(),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = Brushes.Red,
                    FontWeight = FontWeights.Bold
                };

                Grid.SetRow(tb, i); 
                Grid.SetColumn(tb, 0); //prva kolona
                grid.Children.Add(tb);
            }

        }
        public void dodajFiguruNaGrid(Grid grid, Figura figura)
        {
            Ellipse elipsa = new Ellipse
            {
                Fill = figura.Boja == "bela" ? Brushes.White : Brushes.Black,
                Tag = figura.Polje
            };

            TextBlock tb = new TextBlock
            {
                Text = figura.FormatirajOznakuFigure(),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = Brushes.Red,
                FontWeight = FontWeights.Bold
            };

            elipsa.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;


            Grid.SetRow(elipsa, figura.Polje.brojReda+1);
            Grid.SetColumn(elipsa, SlovoUBroj.vrednostKolonePrekoSlova[figura.Polje.slovoKolone]+1); //kolona figure
            grid.Children.Add(elipsa);

            Grid.SetRow(tb, figura.Polje.brojReda+1);
            Grid.SetColumn(tb, SlovoUBroj.vrednostKolonePrekoSlova[figura.Polje.slovoKolone]+1); //kolona figure
            grid.Children.Add(tb);

            //dodaj polje figure (i figuru tom polju) u matricuPolja unutar sahovske table
            tabla.matricaPolja[figura.Polje.brojReda, SlovoUBroj.vrednostKolonePrekoSlova[figura.Polje.slovoKolone]] = figura.Polje;
            tabla.matricaPolja[figura.Polje.brojReda, SlovoUBroj.vrednostKolonePrekoSlova[figura.Polje.slovoKolone]].figuraNaPolju = figura;

        }

        private TextBlock nadjiTextBlock(Grid grid, int row, int column)
        {
            foreach (var child in grid.Children)
            {
                if (child is TextBlock textBlock && Grid.GetRow(textBlock) == row && Grid.GetColumn(textBlock) == column)
                {
                    return textBlock;
                }
            }

            return null;
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selektovanaElipsa = (Ellipse)sender;
        }

        //Polje na koje treba da se stavi figura - handling
        private void Polje_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectPolje = (Rectangle)sender;
            Polje poljeNaKojeSePremesta= rectPolje.Tag as Polje;

            if(selektovanaElipsa != null) {
                Polje poljeElipse = selektovanaElipsa.Tag as Polje;
                TextBlock tekstNaElipsi = nadjiTextBlock(grid, poljeElipse.brojReda + 1, SlovoUBroj.vrednostKolonePrekoSlova[poljeElipse.slovoKolone] + 1);

                //premestanje elipse
                Grid.SetRow(selektovanaElipsa, poljeNaKojeSePremesta.brojReda + 1);
                Grid.SetColumn(selektovanaElipsa, SlovoUBroj.vrednostKolonePrekoSlova[poljeNaKojeSePremesta.slovoKolone] + 1);

                //premestanje oznake elipse
                Grid.SetRow(tekstNaElipsi, poljeNaKojeSePremesta.brojReda + 1);
                Grid.SetColumn(tekstNaElipsi, SlovoUBroj.vrednostKolonePrekoSlova[poljeNaKojeSePremesta.slovoKolone] + 1);

                //uklanjanje figure sa starog mesta - update matricePolja
                //tabla.matricaPolja[poljeElipse.brojReda, SlovoUBroj.vrednostKolonePrekoSlova[poljeElipse.slovoKolone]].figuraNaPolju = null;
                tabla.matricaPolja[poljeElipse.brojReda, SlovoUBroj.vrednostKolonePrekoSlova[poljeElipse.slovoKolone]] = null;

                //dodavanje figure na novo mesto - update matricePolja
                tabla.matricaPolja[poljeNaKojeSePremesta.brojReda, SlovoUBroj.vrednostKolonePrekoSlova[poljeNaKojeSePremesta.slovoKolone]] = poljeNaKojeSePremesta;
                tabla.matricaPolja[poljeNaKojeSePremesta.brojReda, SlovoUBroj.vrednostKolonePrekoSlova[poljeNaKojeSePremesta.slovoKolone]].figuraNaPolju = poljeElipse.figuraNaPolju;

                poljeNaKojeSePremesta.figuraNaPolju = poljeElipse.figuraNaPolju; //eksplicitna dodela figure novom polju
                poljeElipse.figuraNaPolju = null; //uklanjanje figure sa starog polja
                selektovanaElipsa.Tag = poljeNaKojeSePremesta;
                prikaziTabluTekstualno();
            } else
            {
                MessageBox.Show("Najpre odaberite koju figuru zelite da pomerite, a potom kliknite polje gde zelite da pomerite figuru");
            }
            selektovanaElipsa = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(polje.uDijagonali(polje2).ToString());
            //polje.uDijagonali(polje2);

            //figura.mogucePomeranjeNaDatoPolje(polje2); //ona je na polju, a hoce na polje2
            var podaci = tabla.tekstualniOpisTable();
            string output = "";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    output += podaci[i, j] + "\t"; // Separate elements by a tab or any other delimiter
                }

                output += Environment.NewLine; // Move to the next line
            }
            //MessageBox.Show(tabla.tekstualniOpisTable().ToString());
            //MessageBox.Show(polje2.tekstualniOpisPolja().ToString());
            MessageBox.Show(output);
        }

        public void prikaziTabluTekstualno()
        {
            var podaci = tabla.tekstualniOpisTable();
            string output = "";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    output += podaci[i, j] + "\t"; // Separate elements by a tab or any other delimiter
                }

                output += Environment.NewLine; // Move to the next line
            }
            MessageBox.Show(output);
        }
    }
}
