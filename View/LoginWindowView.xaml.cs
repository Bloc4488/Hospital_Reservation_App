﻿using System;
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
        private void btnMinimize_click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void btnClose_click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void TextBlockForgotPass_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            /*var forWin = new forgotPasswordWindow();
            this.Close();
            forWin.Show();*/
        }

        private void TextBlockRegister_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var regWindow = new RegistrationWindowView();
            this.Close();
            regWindow.Show();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            this.IsVisibleChanged += (s, ev) =>
            {
                var mainView = new MainWindowView();
                if (this.IsVisible == false)
                {
                    mainView.Show();
                    this.Close();
                }
            };
        }
    }
}