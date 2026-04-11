using BackEnd.Models;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using ToastNotifications.Messages;

namespace FrontEnd.Dialogs
{
    /// <summary>
    /// Logique d'interaction pour CanvasWindow.xaml
    /// </summary>
    public partial class CanvasWindow : Window
    {
        private Border _dragged;
        private Point _dragStart;
        private Border _drFrom;
        private bool _drawing = false;
        public CanvasWindow(List<Machine> Machines)
        {
            InitializeComponent();
            foreach (Machine machine in Machines)
            {
                PopulateMachineList(machine);
            }
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragged == null)
                return;
            Point pos = e.GetPosition(MainCanvas);
            Canvas.SetTop(_dragged, pos.Y - _dragStart.Y);
            Canvas.SetLeft(_dragged,  pos.X - _dragStart.X);

        }

        private void MainCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_dragged == null)
                return;
            _dragged.ReleaseMouseCapture();
            _dragged = null;
        }

        private void node_LeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_drawing)
            { 
                if(_drFrom == null)
                {
                    _drFrom = (Border)sender;
                    _drFrom.BorderBrush = Brushes.YellowGreen;
                    _drFrom.BorderThickness = new Thickness(2);
                }
                else
                {
                    DrawLine(_drFrom, (Border)sender);
                    _drFrom.BorderBrush = null;
                    _drFrom.BorderThickness = new Thickness(0);
                    _drFrom = null;
                }
                e.Handled = true;
                return;
            }
            _dragged = (Border)sender;
            _dragged.CaptureMouse();
            _dragStart = e.GetPosition(_dragged);
        }
        private void node_RightMouseDown(object sender, MouseButtonEventArgs e)
        {
            Machine mac = (Machine)((Border)sender).Tag;
            MainCanvas.Children.Remove((Border)sender);
            MachineList.Items.Add((Border)sender);
        }

        private void Mode_dessin_Click(object sender, RoutedEventArgs e)
        {
            _drawing = !_drawing;
            if (_drawing)
                ((MainWindow)Application.Current.MainWindow)._notifier.ShowInformation("Mode dessin activé");
            else
                ((MainWindow)Application.Current.MainWindow)._notifier.ShowInformation("Mode dessin désactivé");
            //MessageBox.Show(_drawing.ToString());
        }

        private void PopulateCanvas(Machine pMAchine, double x, double y)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            string icon = "";
            switch (pMAchine.GetType().Name)
            {
                case "Server":
                    icon = "▣";
                    break;
                case "Routeur":
                    icon = "⬡";
                    break;
                case "Computer":
                    icon = "▤";
                    break;
                case "Imprimante":
                    icon = "⊟";
                    break;
                default:
                    icon = "▪";
                    break;
            }

            stackPanel.Children.Add(new TextBlock
            {
                Text = icon,
                FontSize = 12,
                Foreground = new SolidColorBrush(Color.FromRgb(0x7A, 0x8A, 0x7A)),
                VerticalAlignment = VerticalAlignment.Center
            });

            stackPanel.Children.Add(new TextBlock
            {
                Text = pMAchine.Name,
                FontSize = 12,
                Foreground = new SolidColorBrush(Color.FromRgb(0x7A, 0x8A, 0x7A)),
                VerticalAlignment = VerticalAlignment.Center
            });

            Border node = new();
            node.Child = stackPanel;
            node.Tag = pMAchine;
            node.MouseLeftButtonDown += node_LeftMouseDown;
            node.MouseRightButtonDown += node_RightMouseDown;
            Canvas.SetTop(node,y);
            Canvas.SetLeft(node,x);
            MainCanvas.Children.Add(node);
        }
        private void PopulateMachineList(Machine pMAchine)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            string icon = "";
            switch (pMAchine.GetType().Name)
            {
                case "Server":
                    icon = "▣";
                    break;
                case "Routeur":
                    icon = "⬡";
                    break;
                case "Computer":
                    icon = "▤";
                    break;
                case "Imprimante":
                    icon = "⊟";
                    break;
                default:
                    icon = "▪";
                    break;
            }

            stackPanel.Children.Add(new TextBlock
            {
                Text = icon,
                FontSize = 12,
                Foreground = new SolidColorBrush(Color.FromRgb(0x7A, 0x8A, 0x7A)),
                VerticalAlignment = VerticalAlignment.Center
            });

            stackPanel.Children.Add(new TextBlock
            {
                Text = pMAchine.Name,
                FontSize = 12,
                Foreground = new SolidColorBrush(Color.FromRgb(0x7A, 0x8A, 0x7A)),
                VerticalAlignment = VerticalAlignment.Center
            });

            Border node = new();
            node.Child = stackPanel;
            node.Tag = pMAchine;
            node.MouseLeftButtonDown += ListItem_doubleclick;
            MachineList.Items.Add(node);
        }


        private void ListItem_doubleclick(object s, MouseButtonEventArgs e)
        {
            if (e.ClickCount < 2)
                return;
            Border sender = (Border)s;
            Machine nMachine = (Machine)sender.Tag;
            MachineList.Items.Remove(sender);
            double y = (MainCanvas.ActualHeight / 2) - 30;
            double x = (MainCanvas.ActualWidth / 2) - 30;
            PopulateCanvas(nMachine, x, y);
        }

        private void DrawLine(Border from, Border to)
        {
            var p1 = GetCenter(from);
            var p2 = GetCenter(to);

            var line = new Line
            {
                X1 = p1.X,
                Y1 = p1.Y,
                X2 = p2.X,
                Y2 = p2.Y,
                Stroke = new SolidColorBrush(Color.FromRgb(0x7C, 0xAA, 0x00)),
                StrokeThickness = 1.5,
            };
            line.MouseLeftButtonDown += line_Left_clk;

            Panel.SetZIndex(line, -1); // behind the nodes
            MainCanvas.Children.Insert(0, line);
        }

        private void line_Left_clk(object sender, MouseButtonEventArgs args)
        {
            MainCanvas.Children.Remove((Line?)sender);
        }

        private Point GetCenter(Border node)
        {
            double x = Canvas.GetLeft(node) + node.ActualWidth / 2;
            double y = Canvas.GetTop(node) + node.ActualHeight / 2;
            return new Point(x, y);
        }
    }
}
