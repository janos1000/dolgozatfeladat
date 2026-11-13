using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace DolgozatFeladat
{
    public class Dolgozat
    {
        public string Nev { get; set; } = string.Empty;
        public int Eletkor { get; set; }
        public int Pontszam { get; set; }
    }

    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Dolgozat> dolgozatokLista = new ObservableCollection<Dolgozat>();

        public MainWindow()
        {
            InitializeComponent();
            AlapAdatokBetoltese();
        }

        private void AlapAdatokBetoltese()
        {
            dolgozatokLista.Add(new Dolgozat { Nev = "Kiss Péter", Eletkor = 16, Pontszam = 87 });
            dolgozatokLista.Add(new Dolgozat { Nev = "Nagy Anna", Eletkor = 17, Pontszam = 92 });
            dolgozatokLista.Add(new Dolgozat { Nev = "Szabó Tamás", Eletkor = 16, Pontszam = 78 });
            dolgozatokLista.Add(new Dolgozat { Nev = "Tóth Éva", Eletkor = 18, Pontszam = 95 });
            dolgozatokLista.Add(new Dolgozat { Nev = "Varga Bence", Eletkor = 17, Pontszam = 81 });
            dolgozatokLista.Add(new Dolgozat { Nev = "Horváth Zsolt", Eletkor = 16, Pontszam = 65 });
            dolgozatokLista.Add(new Dolgozat { Nev = "Molnár Réka", Eletkor = 18, Pontszam = 89 });
            dolgozatokLista.Add(new Dolgozat { Nev = "Lakatos Dániel", Eletkor = 17, Pontszam = 72 });

            dgDolgozatok.ItemsSource = dolgozatokLista;
        }

        private void btnHozzaadas_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNev.Text))
            {
                MessageBox.Show("Kérlek, add meg a nevet!", "Hiányzó adat", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtEletkor.Text, out int eletkor) || !int.TryParse(txtPontszam.Text, out int pontszam))
            {
                MessageBox.Show("Az életkornak és a pontszámnak egész számnak kell lennie!", "Hibás formátum", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            dolgozatokLista.Add(new Dolgozat
            {
                Nev = txtNev.Text.Trim(),
                Eletkor = eletkor,
                Pontszam = pontszam
            });

            txtNev.Clear();
            txtEletkor.Clear();
            txtPontszam.Clear();
            txtNev.Focus();
        }

        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("dolgozatok.txt"))
                {
                    sw.WriteLine("Nev;Eletkor;Pontszam");
                    foreach (var item in dolgozatokLista)
                    {
                        sw.WriteLine($"{item.Nev};{item.Eletkor};{item.Pontszam}");
                    }
                }
                MessageBox.Show("Az adatok sikeresen elmentve a dolgozatok.txt fájlba!", "Mentés kész", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a mentés során: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
