using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjektBazyDanych;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Wyszukiwanie_Click(object sender, RoutedEventArgs e)
    {
        WyszukiwanieProduktów wyszukiwanieProduktów = new WyszukiwanieProduktów();
        wyszukiwanieProduktów.Show();
    }

    private void ZarzProd_Click(object sender, RoutedEventArgs e)
    {
        ZarzProd zarzProd = new ZarzProd();
        zarzProd.Show();
    }

    private void ZarzSklep_Click(object sender, RoutedEventArgs e)
    {
        ZarzSklep zarzSklep = new ZarzSklep();
        zarzSklep.Show();
    }

    private void btnWyłącz_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}