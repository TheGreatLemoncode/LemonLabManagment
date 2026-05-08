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
using BackEnd.Models;
using System.IO;
using System.Media;
using System.Security.RightsManagement;

namespace FrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Notifier _notifier { get; set; }
        private MediaPlayer mediaPlayer { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            API.Initialisation();
            CreationCompte.ControlUsed += LoadMainMenu;
            Connection.ControlUsed += LoadMainMenu;
            mediaPlayer = new();
            MainView.Content = new Connection();
            _notifier = new(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    Application.Current.MainWindow,
                    Corner.BottomLeft,
                    10, 
                    10);
                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(TimeSpan.FromSeconds(1), MaximumNotificationCount.FromCount(2));
                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }

        /// <summary>
        /// method that change the content of the mainview
        /// for the accountcreation view
        /// </summary>
        /// <param name="sender">The instance that called the event or method</param>
        /// <param name="e">the arguments passed to the function</param>
        public void LoadAccountCreation(object sender, EventArgs e)
        {
            MainView.Content = new CreationCompte();
        }

        /// <summary>
        /// method that change the content of the mainview
        /// for the MainMenu view
        /// </summary>
        /// <param name="sender">The instance that called the event or method</param>
        /// <param name="e">the arguments passed to the function</param>
        public void LoadMainMenu(object sender, EventArgs e)
        {
            MainView.Content = new MainMenu();
        }

        private void LoadAllMachineList()
        {
            MainView.Content = new DefaultMachineLIst();
        }

        /// <summary>
        /// method that change the content of the mainview
        /// for the accountconnexion view
        /// </summary>
        /// <param name="sender">The instance that called the event or method</param>
        /// <param name="e">the arguments passed to the function</param>
        public void LoadAccountConnexion()
        {
            MainView.Content = new Connection();
        }

        /// <summary>
        /// method that play the sound of a window from the assets
        /// using a mediaplayer
        /// </summary>
        public void PlayWindowSound()
        {
            mediaPlayer.Open(new Uri(@"Assets/Sounds/NotificationSound.mp3", UriKind.RelativeOrAbsolute));
            mediaPlayer.Play();
        }

        /// <summary>
        /// method that open a separate window to play the 
        /// tutorial video of the app
        /// </summary>
        public void PlayInformationVideo()
        {
            Dialogs.VideoPlayer box = new();
            box.Owner = Application.Current.MainWindow;
            box.Show();
            PlayWindowSound();
        }

        /// <summary>
        /// Method that is called when the window is about to close. Used to 
        /// save the user's information using the API
        /// </summary>
        /// <param name="sender">the instance that called the method</param>
        /// <param name="e">the arguments given by the instance</param>
        private void Window_Closed(object sender, EventArgs e)
        {
            API.SaveUserInformation();
        }
    }
}