using Hospital_Reservation_App.ViewModel;
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
    /// Interaction logic for LoginWindowView.xaml
    /// </summary>
    public partial class LoginWindowView : Window
    {

        public LoginWindowView()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        /// <summary>
        /// Action for the minimize size of window.
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
        private void TextBlockForgotPass_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            /*var forWin = new forgotPasswordWindow();
            this.Close();
            forWin.Show();*/
            MessageBox.Show("In working process!");
        }
        /// <summary>
        /// Action for close login window and open registration window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlockRegister_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var regWindow = new RegistrationWindowView();
            this.Close();
            regWindow.Show();
        }

    }
}
