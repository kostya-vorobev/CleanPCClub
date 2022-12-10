using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        

        public ClientWindow()
        {
            InitializeComponent();
            Client.SearchAll(dataGridClients);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            

        }

        private void ChangeText(object sender, RoutedEventArgs e)
        {
            if (search_TB.Text != "")
            {
                Client.Search(dataGridClients, search_TB.Text);
            }
            else
            {
                Client.SearchAll(dataGridClients);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ClientEditor clientEditor = new ClientEditor();
            clientEditor.Show();
        }
    }
}
