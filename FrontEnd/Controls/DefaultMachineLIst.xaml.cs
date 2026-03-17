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

namespace FrontEnd.Controls
{
    /// <summary>
    /// Logique d'interaction pour DefaultMachineLIst.xaml
    /// </summary>
    public partial class DefaultMachineLIst : UserControl
    {
        public LayoutMode Layout { get; set; } = LayoutMode.Welcome;
        public DefaultMachineLIst()
        {
            InitializeComponent();
            ((TemplateSelector)this.Resources["MySelector"]).CurrentLayout = LayoutMode.All;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((TemplateSelector)this.Resources["MySelector"]).CurrentLayout = LayoutMode.Welcome;
        }
    }
}
