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
        /// <summary>
        /// Action for collapse the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMinimize_click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        /// <summary>
        /// Action for close the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        /// <summary>
        /// Action closing registration window and showing a login window after clicking on textblock.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlockLogin_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var logWindow = new LoginWindowView();
            this.Close();
            logWindow.Show();
        }
        /// <summary>
        /// Action showing login window and close registration window after clicking on registraion button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegistrate_Click(object sender, RoutedEventArgs e)
        {
            var LoginWindow = new LoginWindowView();
            LoginWindow.Show();
            this.Close();
        }
    }
}
