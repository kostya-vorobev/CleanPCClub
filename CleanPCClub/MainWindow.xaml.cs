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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CleanPCClub
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MySqlLib.MySqlData.MySqlExecuteData.MyResultData result = new MySqlLib.MySqlData.MySqlExecuteData.MyResultData();

            result = MySqlLib.MySqlData.MySqlExecuteData.SqlReturnDataset("select * from Users where Login='"+login_TB.Text+"' and Password='"+Pass_PB.Password+"'");

            if (result.ResultData.DefaultView.Table.Rows.Count == 1)
            {
                string buffLogin = result.ResultData.DefaultView.Table.Rows[0]["Login"].ToString();
                string buffRole = result.ResultData.DefaultView.Table.Rows[0]["Role"].ToString();
                int buffId = Convert.ToInt32(result.ResultData.DefaultView.Table.Rows[0]["Id"].ToString());
                Users user = new Users(buffId, buffLogin, Convert.ToInt32(buffRole));
                Menu menuW = new Menu(user);
                menuW.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show(result.ErrorText);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
