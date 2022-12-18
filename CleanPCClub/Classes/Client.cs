using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CleanPCClub
{
    class Client : Human
    {
        private string login;
        private string password;

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

        public Client(DataRowView dataTable)
        {
            this.id = Convert.ToInt32(dataTable["Id"].ToString());
            this.login = dataTable["Login"].ToString();
            this.password = dataTable["Password"].ToString();
            this.name = dataTable["Name"].ToString();
            this.lastName = dataTable["Last_Name"].ToString();
            this.dateBrith = Convert.ToDateTime(dataTable["Date_brith"].ToString());
            this.phone = dataTable["Phone"].ToString();
            this.email = dataTable["Email"].ToString();
        }

        public Client(string login, string password, string name, string lastName, DateTime dateBrith, string phone, string email, object p) : this(login, password, name, lastName, dateBrith, phone, email)
        {
        }

        public override int Id { get => id; set => id = value; }
        public string Login { get => login; set => login = value; }
        public string Password { get => password; set => password = value; }
        public override string Name { get => name; set => name = value; }
        public override string LastName { get => lastName; set => lastName = value; }
        public override DateTime DateBrith { get => dateBrith; set => dateBrith = value; }
        public override string Phone { get => phone; set => phone = value; }
        public override string Email { get => email; set => email = value; }

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

        public override bool Insert()
        {
            MySqlLib.MySqlData.MySqlExecute.MyResult result = new MySqlLib.MySqlData.MySqlExecute.MyResult();
            string query = "INSERT INTO `pcclub`.`clients` " +
                "(`Login`, `Password`, `Name`, `Last_name`, `Date_brith`, `Phone`, `Email`) VALUES " +
                "('" + this.Login + "', '" + this.Password + "'," +
                "'" + this.Name + "','" + this.LastName + "','" + this.DateBrith.Year + "-" + this.DateBrith.Month + "-"
                + this.DateBrith.Day + "','" + this.Phone + "'," + "'" + this.Email + "')";
            result = MySqlLib.MySqlData.MySqlExecute.SqlNoneQuery(query);
            if (result.HasError == false)
            {
                return true;
            }
            else return false;
        }

        public override bool Update()
        {
            MySqlLib.MySqlData.MySqlExecute.MyResult result = new MySqlLib.MySqlData.MySqlExecute.MyResult();
            string query = "UPDATE clients SET " +
                "Login = '" + this.Login + "', " +
                "Password = '" + this.Password + "'," +
                "Name = '" + this.Name + "'," +
                "Last_name = '" + this.LastName + "'," +
                "Date_brith = '" + this.DateBrith.Year + "-" + this.DateBrith.Month + "-" + this.DateBrith.Day + "',"+
                "Email = '" + this.Email + "' " +
                "WHERE Id = " + this.Id;
            result = MySqlLib.MySqlData.MySqlExecute.SqlNoneQuery(query);
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
        public static string SearchLogin(int find)
        {
            MySqlLib.MySqlData.MySqlExecuteData.MyResultData result = new MySqlLib.MySqlData.MySqlExecuteData.MyResultData();
            string select = "select * from Clients where Id=" + find;

            result = MySqlLib.MySqlData.MySqlExecuteData.SqlReturnDataset(select);
            if (result.ResultData.DefaultView.Table.Rows.Count == 1)
            {
                return result.ResultData.DefaultView.Table.Rows[0]["Login"].ToString();
            }
            else return "None";
        }


    }
}
