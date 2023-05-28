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

namespace Hospital_Reservation_App.View
{
    /// <summary>
    /// Interaction logic for RegistrationWindowView.xaml
    /// </summary>
    public partial class RegistrationWindowView : Window
    {
        public RegistrationWindowView()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void btnMinimize_click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void btnClose_click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void TextBlockLogin_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var logWindow = new LoginWindowView();
            this.Close();
            logWindow.Show();
        }

        private void btnRegistrate_Click(object sender, RoutedEventArgs e)
        {
            this.IsVisibleChanged += (s, ev) =>
            {
                var mainView = new LoginWindowView();
                if (this.IsVisible == false)
                {
                    mainView.Show();
                    this.Close();
                }
            };
        }
    }
}
