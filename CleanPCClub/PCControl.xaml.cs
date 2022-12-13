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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CleanPCClub
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class PCControl : UserControl
    {
        public PCControl()
        {
            InitializeComponent();
        }
        PC pc;
        public PCControl(DataRowView data)
        {
            InitializeComponent();
            pcId.Content = "ПК №" + data["Id"].ToString();
            pcLoginClient.Content = "Пользователь: " + Client.SearchLogin(Convert.ToInt32(data["Id_Client"].ToString()));
            if (data["Status"].ToString() == "1")
            {
                pcStatus.Content = "Арендован";
                pcButton.Content = "Заблокировать";
            }
            else
            {
                pcStatus.Content = "Свободен";
                pcButton.Content = "Разблокировать";
            }
            pc = new PC(data);

        }

        private void pcButton_Click(object sender, RoutedEventArgs e)
        {
            if (pc.StatusPC != 1)
            {
                pc.StatusPC = 1;
                pcLoginClient.Content = "Пользователь: None";
                pc.UpdateStatus(1);
                pcStatus.Content = "Арендован";
                pcButton.Content = "Заблокировать";
            }
            else
            {
                pc.StatusPC = 0;
                pcLoginClient.Content = "Пользователь: None";
                pc.UpdateStatus(0);
                pcStatus.Content = "Свободен";
                pcButton.Content = "Разблокировать";
            }
        }
    }
}
