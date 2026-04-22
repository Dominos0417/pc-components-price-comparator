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
using System.Data;
using Microsoft.Data.SqlClient;

namespace ProjektBazyDanych
{
    /// <summary>
    /// Logika interakcji dla klasy WyszukiwanieProduktów.xaml
    /// </summary>
    public partial class WyszukiwanieProduktów : Window
    {
        public WyszukiwanieProduktów()
        {
            InitializeComponent();
     

        }

        private void btnPowrót_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnWyszukaj_Click(object sender, RoutedEventArgs e)
        {
            LbWyszukaneProdukty.Items.Clear();


            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ApkaDoCen;Integrated Security=True;TrustServerCertificate=True;";

            // Walidacja pola Cena od
            if (!decimal.TryParse(TbOD.Text, out decimal cenaOd))
            {
                MessageBox.Show("Wprowadź poprawną wartość liczbową w polu 'Cena od'.");
                TbOD.Focus();
                return;
            }

            // Walidacja pola Cena do
            if (!decimal.TryParse(TbDO.Text, out decimal cenaDo))
            {
                MessageBox.Show("Wprowadź poprawną wartość liczbową w polu 'Cena do'.");
                TbDO.Focus();
                return;
            }

            // Sprawdzenie, czy cenaOd <= cenaDo
            if (cenaOd > cenaDo)
            {
                MessageBox.Show("Wartość 'Cena od' nie może być większa niż 'Cena do'.");
                TbOD.Focus();
                return;
            }

            // Walidacja wyboru kategorii
            if (CbKategoria.SelectedValue == null)
            {
                MessageBox.Show("Wybierz kategorię.");
                CbKategoria.Focus();
                return;
            }

            int idKategorii = Convert.ToInt32(CbKategoria.SelectedValue);

            // Połączenie i zapytanie do bazy
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
            SELECT 
                P.nazwa, 
                P.opis, 
                C.cena, 
                S.nazwa AS nazwaSklepu
            FROM Produkty P
            JOIN (
                SELECT 
                    idProduktu, 
                    MIN(cena) AS minCena
                FROM Ceny
                WHERE dostepnosc = 1
                GROUP BY idProduktu
            ) MinCeny ON P.idProduktu = MinCeny.idProduktu
            JOIN Ceny C ON C.idProduktu = MinCeny.idProduktu AND C.cena = MinCeny.minCena AND C.dostepnosc = 1
            JOIN Sklepy S ON C.idSklepu = S.idSklepu
            WHERE P.idKategorii = @idKategorii
              AND C.cena BETWEEN @cenaOd AND @cenaDo
            ORDER BY C.cena ASC;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idKategorii", idKategorii);
                        cmd.Parameters.AddWithValue("@cenaOd", cenaOd);
                        cmd.Parameters.AddWithValue("@cenaDo", cenaDo);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                LbWyszukaneProdukty.Items.Add("Brak wyników.");
                                return;
                            }

                            while (reader.Read())
                            {
                                string nazwa = reader.GetString(0);
                                string opis = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                decimal cena = reader.GetDecimal(2);
                                string nazwaSklepu = reader.GetString(3); 

                                LbWyszukaneProdukty.Items.Add($"{nazwa} - {opis} | Cena: {cena} zł | Sklep: {nazwaSklepu}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd podczas pobierania danych: " + ex.Message);
                }
            }
        }
    }
}
