using FrontEnd.Controls;
using System.Text;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BackEnd.API;
using System.Security.Policy;
using FrontEnd.Controls.MainMenuControls;

namespace FrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Notifier _notifier { get; set; }
        public UserControl CurrentView;
        public MainWindow()
        {
            InitializeComponent();
            API.Initialisation();
            CreationCompte.ControlUsed += LoadMainMenu;
            Connection.ControlUsed += LoadMainMenu;
            MainView.Content = new MainMenu();
            //Window1 test = new();
            //test.Show();

            _notifier = new(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    Application.Current.MainWindow,
                    Corner.BottomLeft,
                    10, 
                    10);
                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(TimeSpan.FromSeconds(2), MaximumNotificationCount.FromCount(2));
                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }

        public void LoadAccountCreation(object sender, EventArgs e)
        {
            MainView.Content = new CreationCompte();
        }

        public void LoadMainMenu(object sender, EventArgs e)
        {
            MainView.Content = new MainMenu();
        }

        public void LoadAccountConnexion()
        {
            MainView.Content = new Connection();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            API.SaveUserInformation();
        }
    }
}