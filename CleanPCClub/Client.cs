using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CleanPCClub
{
    class Client
    {
        private int id;
        private string login;
        private string password;
        private string name;
        private string lastName;
        private DateTime dateBrith;
        private string phone;
        private string email;

        public Client()
        {
        }
        public Client(string login, string password, string name, string lastName, DateTime dateBrith, string phone, string email)
        {
            this.login = login;
            this.password = password;
            this.name = name;
            this.lastName = lastName;
            this.dateBrith = dateBrith;
            this.phone = phone;
            this.email = email;
        }

        public Client(int id, string login, string password, string name, string lastName, DateTime dateBrith, string phone, string email)
        {
            this.id = id;
            this.login = login;
            this.password = password;
            this.name = name;
            this.lastName = lastName;
            this.dateBrith = dateBrith;
            this.phone = phone;
            this.email = email;
        }

        public int Id { get => id; set => id = value; }
        public string Login { get => login; set => login = value; }
        public string Password { get => password; set => password = value; }
        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime DateBrith { get => dateBrith; set => dateBrith = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Email { get => email; set => email = value; }

        public static void SearchAll(DataGrid _dgv)
        {
            MySqlLib.MySqlData.MySqlExecuteData.MyResultData result = new MySqlLib.MySqlData.MySqlExecuteData.MyResultData();

            result = MySqlLib.MySqlData.MySqlExecuteData.SqlReturnDataset("select * from Clients");
            if (result.HasError == false)
            {
                _dgv.Columns.Clear();
                _dgv.ItemsSource = result.ResultData.DefaultView;
            }
        }

        public bool Insert()
        {
            MySqlLib.MySqlData.MySqlExecuteData.MyResultData result = new MySqlLib.MySqlData.MySqlExecuteData.MyResultData();
            string query = "INSERT INTO `pcclub`.`clients` " +
                "(`Login`, `Password`, `Name`, `Last_name`, `Date_brith`, `Phone`, `Email`) VALUES " +
                "('" + this.Login + "', '" + this.Password + "'," +
                "'" + this.Name + "','" + this.LastName + "','" + this.DateBrith.Year + "-" + this.DateBrith.Month + "-"
                + this.DateBrith.Day + "','" + this.Phone + "'," + "'" + this.Email + "')";
            result = MySqlLib.MySqlData.MySqlExecuteData.SqlReturnDataset(query);
            if (result.HasError == false)
            {
                return true;
            }
            else return false;
        }

        public bool Update()
        {
            MySqlLib.MySqlData.MySqlExecuteData.MyResultData result = new MySqlLib.MySqlData.MySqlExecuteData.MyResultData();
            string query = "UPDATE Products SET " +
                "Login = '" + this.Login + "', " +
                "Password = '" + this.Password + "'," +
                "Name = '" + this.Name + "'," +
                "Last_name = '" + this.LastName + "'," +
                "Date_brith = '" + this.DateBrith + "'," +
                "Email = '" + this.Email + "'," +
                "WHERE Login = '" + this.Login + "'";
            result = MySqlLib.MySqlData.MySqlExecuteData.SqlReturnDataset(query);
            if (result.HasError == false)
            {
                return true;
            }
            else return false;
        }

        public static void Search(DataGrid _dgv, string find)
        {
            MySqlLib.MySqlData.MySqlExecuteData.MyResultData result = new MySqlLib.MySqlData.MySqlExecuteData.MyResultData();
            string select = "select * from Clients where " +
                "id Like '%" + find + "%' or" +
                " Login Like '%" + find + "%' or " +
                "Password Like '%" + find + "%' or " +
                " Name Like '%" + find + "%' or "+
                "Last_name Like '%" + find + "%' or " +
                " Date_brith Like '%" + find + "%' or " +
                "Phone Like '%" + find + "%' or " +
                " Email Like '%" + find + "%'";

            result = MySqlLib.MySqlData.MySqlExecuteData.SqlReturnDataset(select);
            if (result.HasError == false)
            {
                _dgv.Columns.Clear();
                _dgv.ItemsSource = result.ResultData.DefaultView;
            }
        }

        public static bool SearchLogin(string find)
        {
            MySqlLib.MySqlData.MySqlExecuteData.MyResultData result = new MySqlLib.MySqlData.MySqlExecuteData.MyResultData();
            string select = "select * from Clients where Login='" + find + "'";

            result = MySqlLib.MySqlData.MySqlExecuteData.SqlReturnDataset(select);
            if (result.ResultData.DefaultView.Table.Rows.Count == 0)
            {
                return true;
            }else return false;
        }


    }
}
