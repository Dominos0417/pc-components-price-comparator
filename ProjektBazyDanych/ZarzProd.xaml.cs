using Microsoft.Data.SqlClient;
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
using System.Windows.Shapes;

namespace ProjektBazyDanych
{
    /// <summary>
    /// Logika interakcji dla klasy ZarzProd.xaml
    /// </summary>
    public partial class ZarzProd : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ApkaDoCen;Integrated Security=True;TrustServerCertificate=True;";
        public ZarzProd()
        {
            InitializeComponent();
            btnDodaj.Click += BtnDodaj_Click;
            btnUsuń.Click += BtnUsuń_Click;
            btnProduktyWys.Click += BtnProduktyWys_Click;
            btnPowrót.Click += BtnPowrót_Click;

            // Od razu ładujemy produkty po starcie
            BtnProduktyWys_Click(null, null);
        }
        private void BtnProduktyWys_Click(object sender, RoutedEventArgs e)
        {
            LbProdukty.Items.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT idProduktu, nazwa FROM Produkty ORDER BY idProduktu";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string nazwa = reader.GetString(1);
                        LbProdukty.Items.Add($"{id} - {nazwa}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd podczas pobierania produktów: " + ex.Message);
                }
            }
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (CbKategoria.SelectedValue == null)
            {
                MessageBox.Show("Wybierz kategorię produktu.");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbNazwa.Text) || tbNazwa.Text == "Podaj nazwę produktu")
            {
                MessageBox.Show("Podaj nazwę produktu.");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbOpis.Text) || tbOpis.Text == "Podaj krótki opis produktu")
            {
                MessageBox.Show("Podaj opis produktu.");
                return;
            }

            int idKategorii = Convert.ToInt32(CbKategoria.SelectedValue);
            string nazwa = tbNazwa.Text.Trim();
            string opis = tbOpis.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string insertSql = "INSERT INTO Produkty (idKategorii, nazwa, opis) VALUES (@idKategorii, @nazwa, @opis)";
                    SqlCommand cmd = new SqlCommand(insertSql, conn);
                    cmd.Parameters.AddWithValue("@idKategorii", idKategorii);
                    cmd.Parameters.AddWithValue("@nazwa", nazwa);
                    cmd.Parameters.AddWithValue("@opis", opis);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Produkt dodany pomyślnie.");
                        ClearInputFields();
                        BtnProduktyWys_Click(null, null); // odśwież listę
                    }
                    else
                    {
                        MessageBox.Show("Nie udało się dodać produktu.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd podczas dodawania produktu: " + ex.Message);
                }
            }
        }

        private void ClearInputFields()
        {
            CbKategoria.SelectedIndex = -1;
            tbNazwa.Text = "";
            tbOpis.Text = "";
        }

        private void BtnUsuń_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbProduktID.Text.Trim(), out int idProduktu))
            {
                MessageBox.Show("Podaj poprawne ID produktu (liczba całkowita).");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string deleteSql = "DELETE FROM Produkty WHERE idProduktu = @idProduktu";
                    SqlCommand cmd = new SqlCommand(deleteSql, conn);
                    cmd.Parameters.AddWithValue("@idProduktu", idProduktu);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Produkt usunięty pomyślnie.");
                        BtnProduktyWys_Click(null, null);
                        tbProduktID.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Nie znaleziono produktu o podanym ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd podczas usuwania produktu: " + ex.Message);
                }
            }
        }

        private void BtnPowrót_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void tbProduktID_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbProduktID.Text == "Podaj ID produktu")
            {
                tbProduktID.Text = string.Empty;
            }
        }
        private void tbProduktID_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbProduktID.Text))
            {
                tbProduktID.Text = "Podaj ID produktu";
            }
        }
        private void tbNazwa_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbNazwa.Text == "Podaj nazwę produktu")
            {
                tbNazwa.Text = string.Empty;
            }
        }
        private void tbNazwa_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNazwa.Text))
            {
                tbNazwa.Text = "Podaj nazwę produktu";
            }
        }
        private void tbOpis_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbOpis.Text == "Podaj krótki opis produktu")
            {
                tbOpis.Text = string.Empty;
            }
        }
        private void tbOpis_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbOpis.Text))
            {
                tbOpis.Text = "Podaj krótki opis produktu";
            }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                // Jeśli pole zawiera placeholder, wyczyść je
                if (tb.Text == GetPlaceholder(tb))
                {
                    tb.Text = "";
                    tb.Foreground = Brushes.Black;
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                // Jeśli użytkownik nic nie wpisał, przywróć placeholder
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.Text = GetPlaceholder(tb);
                    tb.Foreground = Brushes.Gray;
                }
            }
        }

        // Zwraca placeholder w zależności od pola
        private string GetPlaceholder(TextBox tb)
        {
            if (tb == tbNazwa) return "Podaj nazwę produktu";
            if (tb == tbOpis) return "Podaj krótki opis produktu";
            if (tb == tbProduktID) return "Podaj ID produktu";
            return "";
        }
    }
}
