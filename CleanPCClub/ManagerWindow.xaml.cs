using CleanPCClub.Classes;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Manager.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
            Managers.SearchAll(dataGridManagers);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ManagerEditor managerEditor = new ManagerEditor();
            managerEditor.ShowDialog();
        }
        private void ChangText(object sender, RoutedEventArgs e)
        {
            if (search_TB.Text != "")
            {
                Managers.Search(dataGridManagers, search_TB.Text);
            }
            else
            {
                Managers.SearchAll(dataGridManagers);
            }
        }

        private void EditRecord(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dataRow = (DataRowView)dataGridManagers.SelectedItem;
                ManagerEditor clientEditor = new ManagerEditor(dataRow);
                clientEditor.ShowDialog();
            }
            catch (Exception ex)
            {
                ManagerEditor clientEditor = new ManagerEditor();
                clientEditor.ShowDialog();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateTable(object sender, RoutedEventArgs e)
        {
            Managers.SearchAll(dataGridManagers);
        }
    }
}
