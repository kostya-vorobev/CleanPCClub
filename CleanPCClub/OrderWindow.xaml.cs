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
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    
    public partial class OrderWindow : Window
    {
        DataRowView dataRow;
        Users user;
        public OrderWindow(Users users)
        {
            InitializeComponent();
            Client.SearchAll(dataGridClient);
            Order.SearchAll(dataGridOrder);
            PC.SearchId(pc_CMB);
            for (int i = 1; i <= 5; i++)
                hour_CMB.Items.Add(i);
            user = users;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (searchClient_TB.Text != "")
                {
                    Client.Search(dataGridClient, searchClient_TB.Text);
                }
                else
                {
                    Client.SearchAll(dataGridClient);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (searchOrder_TB.Text != "")
                {
                    Order.Search(dataGridOrder, searchOrder_TB.Text);
                }
                else
                {
                    Order.SearchAll(dataGridOrder);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SelectClient_Click(object sender, RoutedEventArgs e)
        {
            dataRow = (DataRowView)dataGridClient.SelectedItem;
            selectClient_TB.Text = dataRow.Row["Login"].ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Order order = new Order(Convert.ToInt32(pc_CMB.SelectedItem.ToString()),
                Convert.ToInt32(dataRow.Row["Id"].ToString()), Convert.ToInt32(cost_TB.Text),
                DateTime.Now, DateTime.Now.AddHours(hour_CMB.SelectedIndex+1), user.Id);
            order.Insert();
            PC pc = new PC(Convert.ToInt32(pc_CMB.SelectedItem.ToString()));
            pc.UpdateStatus(1);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
