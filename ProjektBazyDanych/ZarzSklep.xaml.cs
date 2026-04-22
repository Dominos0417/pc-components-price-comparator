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
    /// Logika interakcji dla klasy ZarzSklep.xaml
    /// </summary>
    public partial class ZarzSklep : Window
    {
        public ZarzSklep()
        {
            InitializeComponent();
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ApkaDoCen;Integrated Security=True;TrustServerCertificate=True;";

            BtnDodaj.Click += BtnDodaj_Click;
            BtnWyszukajPoIDProduktu.Click += BtnWyszukajPoIDProduktu_Click;
            BtnPowrot.Click += BtnPowrot_Click;
            BtnEdytuj.Click += BtnEdytuj_Click;
            BtnUsun.Click += BtnUsun_Click;

            LoadAllPrices(); 
        }
        private void LoadAllPrices()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ApkaDoCen;Integrated Security=True;TrustServerCertificate=True;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    c.idProduktu, 
                    p.nazwa AS nazwaProduktu,
                    s.nazwa AS nazwaSklepu, 
                    c.cena, 
                    c.dostepnosc, 
                    c.dataAktualizacji 
                FROM CENY c
                INNER JOIN SKLEPY s ON c.idSklepu = s.idSklepu
                INNER JOIN PRODUKTY p ON c.idProduktu = p.idProduktu
                ORDER BY c.idProduktu";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        LbPozycje.Items.Clear();

                        while (reader.Read())
                        {
                            int idProduktu = reader.GetInt32(0);
                            string nazwaProduktu = reader.GetString(1);
                            string nazwaSklepu = reader.GetString(2);
                            decimal cena = reader.GetDecimal(3);
                            bool dostepnosc = reader.GetBoolean(4);
                            DateTime dataAkt = reader.GetDateTime(5);

                            string tekst = $"Produkt ID: {idProduktu} ({nazwaProduktu}), Sklep: {nazwaSklepu}, Cena: {cena:C}, Dostępny: {(dostepnosc ? "Tak" : "Nie")}, Data aktualizacji: {dataAkt:d}";
                            LbPozycje.Items.Add(tekst);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas ładowania danych: " + ex.Message);
            }
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ApkaDoCen;Integrated Security=True;TrustServerCertificate=True;";
            if (!int.TryParse(TbIDProduktu.Text, out int idProduktu))
            {
                MessageBox.Show("Niepoprawne ID Produktu.");
                return;
            }

            if (!(CbSklep.SelectedItem is ComboBoxItem sklepItem) || !int.TryParse(sklepItem.Tag.ToString(), out int idSklepu))
            {
                MessageBox.Show("Wybierz poprawny sklep.");
                return;
            }

            if (!decimal.TryParse(TbCena.Text, out decimal cena))
            {
                MessageBox.Show("Niepoprawna cena.");
                return;
            }

            if (!(CbDostepnosc.SelectedItem is ComboBoxItem dostepnoscItem))
            {
                MessageBox.Show("Wybierz dostępność.");
                return;
            }
            bool dostepnosc = dostepnoscItem.Content.ToString() == "Tak";

            DateTime dataAktualizacji = DateTime.Now;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Sprawdź czy istnieje rekord
                    string checkQuery = "SELECT COUNT(*) FROM CENY WHERE idProduktu = @idProduktu AND idSklepu = @idSklepu";
                    using (SqlCommand cmdCheck = new SqlCommand(checkQuery, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@idProduktu", idProduktu);
                        cmdCheck.Parameters.AddWithValue("@idSklepu", idSklepu);
                        int count = (int)cmdCheck.ExecuteScalar();

                        if (count > 0)
                        {
                            // Aktualizacja
                            string updateQuery = @"UPDATE CENY
                                               SET cena = @cena, dostepnosc = @dostepnosc, dataAktualizacji = @dataAktualizacji
                                               WHERE idProduktu = @idProduktu AND idSklepu = @idSklepu";
                            using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn))
                            {
                                cmdUpdate.Parameters.AddWithValue("@cena", cena);
                                cmdUpdate.Parameters.AddWithValue("@dostepnosc", dostepnosc);
                                cmdUpdate.Parameters.AddWithValue("@dataAktualizacji", dataAktualizacji);
                                cmdUpdate.Parameters.AddWithValue("@idProduktu", idProduktu);
                                cmdUpdate.Parameters.AddWithValue("@idSklepu", idSklepu);

                                cmdUpdate.ExecuteNonQuery();
                                MessageBox.Show("Cena została zaktualizowana.");
                            }
                        }
                        else
                        {
                            // Dodanie
                            string insertQuery = @"INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc, dataAktualizacji)
                                               VALUES (@idProduktu, @idSklepu, @cena, @dostepnosc, @dataAktualizacji)";
                            using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn))
                            {
                                cmdInsert.Parameters.AddWithValue("@idProduktu", idProduktu);
                                cmdInsert.Parameters.AddWithValue("@idSklepu", idSklepu);
                                cmdInsert.Parameters.AddWithValue("@cena", cena);
                                cmdInsert.Parameters.AddWithValue("@dostepnosc", dostepnosc);
                                cmdInsert.Parameters.AddWithValue("@dataAktualizacji", dataAktualizacji);

                                cmdInsert.ExecuteNonQuery();
                                MessageBox.Show("Cena została dodana.");
                            }
                        }
                    }
                }

                LoadAllPrices(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas zapisu do bazy: " + ex.Message);
            }
        }

        private void BtnWyszukajPoIDProduktu_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ApkaDoCen;Integrated Security=True;TrustServerCertificate=True;";
            if (!int.TryParse(TbIDProduktuDoWyświetlenia.Text, out int idProduktu))
            {
                MessageBox.Show("Niepoprawne ID Produktu.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    c.idProduktu, 
                    p.nazwa AS nazwaProduktu,
                    s.nazwa AS nazwaSklepu, 
                    c.cena, 
                    c.dostepnosc, 
                    c.dataAktualizacji
                FROM CENY c
                INNER JOIN SKLEPY s ON c.idSklepu = s.idSklepu
                INNER JOIN PRODUKTY p ON c.idProduktu = p.idProduktu
                WHERE c.idProduktu = @idProduktu";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idProduktu", idProduktu);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            LbPozycje.Items.Clear();

                            if (!reader.HasRows)
                            {
                                MessageBox.Show("Brak danych dla podanego ID produktu.");
                                return;
                            }

                            while (reader.Read())
                            {
                                int prodId = reader.GetInt32(0);
                                string nazwaProduktu = reader.GetString(1);
                                string sklep = reader.GetString(2);
                                decimal cena = reader.GetDecimal(3);
                                bool dostepnosc = reader.GetBoolean(4);
                                DateTime dataAkt = reader.GetDateTime(5);

                                string tekst = $"Produkt ID: {prodId} ({nazwaProduktu}), Sklep: {sklep}, Cena: {cena:C}, Dostępny: {(dostepnosc ? "Tak" : "Nie")}, Data: {dataAkt:d}";
                                LbPozycje.Items.Add(tekst);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas wyszukiwania: " + ex.Message);
            }
        }
        private void BtnEdytuj_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ApkaDoCen;Integrated Security=True;TrustServerCertificate=True;";
            if (!int.TryParse(TbIDProduktu.Text, out int idProduktu))
            {
                MessageBox.Show("Niepoprawne ID Produktu.");
                return;
            }

            if (!(CbSklep.SelectedItem is ComboBoxItem sklepItem) || !int.TryParse(sklepItem.Tag.ToString(), out int idSklepu))
            {
                MessageBox.Show("Wybierz poprawny sklep.");
                return;
            }

            if (!decimal.TryParse(TbCena.Text, out decimal cena))
            {
                MessageBox.Show("Niepoprawna cena.");
                return;
            }

            if (!(CbDostepnosc.SelectedItem is ComboBoxItem dostepnoscItem))
            {
                MessageBox.Show("Wybierz dostępność.");
                return;
            }
            bool dostepnosc = dostepnoscItem.Content.ToString() == "Tak";

            DateTime dataAktualizacji = DateTime.Now;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                  
                    string checkQuery = "SELECT COUNT(*) FROM CENY WHERE idProduktu = @idProduktu AND idSklepu = @idSklepu";
                    using (SqlCommand cmdCheck = new SqlCommand(checkQuery, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@idProduktu", idProduktu);
                        cmdCheck.Parameters.AddWithValue("@idSklepu", idSklepu);
                        int count = (int)cmdCheck.ExecuteScalar();

                        if (count == 0)
                        {
                            MessageBox.Show("Rekord do edycji nie istnieje.");
                            return;
                        }

                  
                        string updateQuery = @"UPDATE CENY
                                       SET cena = @cena, dostepnosc = @dostepnosc, dataAktualizacji = @dataAktualizacji
                                       WHERE idProduktu = @idProduktu AND idSklepu = @idSklepu";

                        using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn))
                        {
                            cmdUpdate.Parameters.AddWithValue("@cena", cena);
                            cmdUpdate.Parameters.AddWithValue("@dostepnosc", dostepnosc);
                            cmdUpdate.Parameters.AddWithValue("@dataAktualizacji", dataAktualizacji);
                            cmdUpdate.Parameters.AddWithValue("@idProduktu", idProduktu);
                            cmdUpdate.Parameters.AddWithValue("@idSklepu", idSklepu);

                            cmdUpdate.ExecuteNonQuery();
                            MessageBox.Show("Cena została zaktualizowana.");
                        }
                    }
                }

                LoadAllPrices(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas aktualizacji: " + ex.Message);
            }
        }
        private void BtnUsun_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ApkaDoCen;Integrated Security=True;TrustServerCertificate=True;";
            if (!int.TryParse(TbIDProduktu.Text, out int idProduktu))
            {
                MessageBox.Show("Niepoprawne ID Produktu.");
                return;
            }

            if (!(CbSklep.SelectedItem is ComboBoxItem sklepItem) || !int.TryParse(sklepItem.Tag.ToString(), out int idSklepu))
            {
                MessageBox.Show("Wybierz poprawny sklep.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    
                    string checkQuery = "SELECT COUNT(*) FROM CENY WHERE idProduktu = @idProduktu AND idSklepu = @idSklepu";
                    using (SqlCommand cmdCheck = new SqlCommand(checkQuery, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@idProduktu", idProduktu);
                        cmdCheck.Parameters.AddWithValue("@idSklepu", idSklepu);
                        int count = (int)cmdCheck.ExecuteScalar();

                        if (count == 0)
                        {
                            MessageBox.Show("Rekord do usunięcia nie istnieje.");
                            return;
                        }

                        // Usuń rekord
                        string deleteQuery = "DELETE FROM CENY WHERE idProduktu = @idProduktu AND idSklepu = @idSklepu";
                        using (SqlCommand cmdDelete = new SqlCommand(deleteQuery, conn))
                        {
                            cmdDelete.Parameters.AddWithValue("@idProduktu", idProduktu);
                            cmdDelete.Parameters.AddWithValue("@idSklepu", idSklepu);

                            cmdDelete.ExecuteNonQuery();
                            MessageBox.Show("Rekord został usunięty.");
                        }
                    }
                }

                LoadAllPrices(); // odśwież listę po usunięciu
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas usuwania: " + ex.Message);
            }
        }
        public class CenaPozycja
        {
            public int IdProduktu { get; set; }
            public int IdSklepu { get; set; }
            public string NazwaSklepu { get; set; }
            public decimal Cena { get; set; }
            public bool Dostepnosc { get; set; }

            public override string ToString()
            {
                return $"ID: {IdProduktu}, Sklep: {NazwaSklepu}, Cena: {Cena:C}, Dostępny: {(Dostepnosc ? "Tak" : "Nie")}";
            }
        }

        private void BtnPowrot_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void LbPozycje_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LbPozycje.SelectedItem is CenaPozycja wybranaCena)
            {
                TbIDProduktu.Text = wybranaCena.IdProduktu.ToString();

                // Ustawienie ComboBox Sklep wg IdSklepu
                foreach (ComboBoxItem item in CbSklep.Items)
                {
                    if (int.TryParse(item.Tag.ToString(), out int idSklepu) && idSklepu == wybranaCena.IdSklepu)
                    {
                        CbSklep.SelectedItem = item;
                        break;
                    }
                }

                TbCena.Text = wybranaCena.Cena.ToString("F2");

                // Ustawienie ComboBox Dostępność (Tak/Nie)
                foreach (ComboBoxItem item in CbDostepnosc.Items)
                {
                    if ((wybranaCena.Dostepnosc && item.Content.ToString() == "Tak") ||
                        (!wybranaCena.Dostepnosc && item.Content.ToString() == "Nie"))
                    {
                        CbDostepnosc.SelectedItem = item;
                        break;
                    }
                }
            }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
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
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.Text = GetPlaceholder(tb);
                    tb.Foreground = Brushes.Gray;
                }
            }
        }

        private string GetPlaceholder(TextBox tb)
        {
            if (tb == TbIDProduktu) return "Id Produktu";
            if (tb == TbCena) return "Cena";
            if (tb == TbIDProduktuDoWyświetlenia) return "Id produktu do wyświetlenia";
            return "";
        }

        private void BtnPokaWszystkie_Click(object sender, RoutedEventArgs e)
        {
            LoadAllPrices();
        }
    }
}
