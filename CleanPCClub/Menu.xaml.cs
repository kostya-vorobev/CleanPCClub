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

namespace CleanPCClub
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private Users session;
        public Menu(Users user)
        {
            InitializeComponent();
            name.Content = "Пользователь: " + user.Login;
            if (user.Role == 2)
            {
                managerReg.Visibility = Visibility.Visible;
            }
            session = user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow();
            this.Hide();
            clientWindow.Show();
            this.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PCWindow pC = new PCWindow();
            this.Hide();
            pC.Show();
            this.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow(session);
            this.Hide();
            orderWindow.Show();
            this.Show();
        }
    }
}
