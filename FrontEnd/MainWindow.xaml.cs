using FrontEnd.Vues;
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
using BackEnd.API;

namespace FrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            API.Initialisation();
            Connexion.CreationEvent += ChargerCreationCompte;
            CreationCompte.CreationCompteComplete += ChargerMenuPrincipal;
            Main_Content.Content = new Connexion();
        }

        public void ChargerCreationCompte(object sender, EventArgs e)
        {
            Main_Content.Content = new CreationCompte();
        }

        public void ChargerMenuPrincipal(object sender, EventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            API.SaveUserInformation();
        }
    }
}