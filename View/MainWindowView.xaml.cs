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
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
        }

        private void btnReserveVisit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMyAcc_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnMinimize_click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void btnClose_click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void My_reserevations_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Visits_to_doctor_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void My_account_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Exit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var logWindow = new LoginWindowView();
            //this.Close();
            var mainWindow = Window.GetWindow(this);
            mainWindow.Close();
            logWindow.Show();
        }
    }
}
