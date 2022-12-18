using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CleanPCClub.Classes
{
    class Managers : Human
    {
        private int idUser;
        private string login;
        private string pass;
        private double salary;

        public Managers(DataRowView dataTable)
        {
            this.id = Convert.ToInt32(dataTable["Id"].ToString());
            this.name = dataTable["Name"].ToString();
            this.lastName = dataTable["Last_Name"].ToString();
            this.dateBrith = Convert.ToDateTime(dataTable["Date_brith"].ToString());
            this.phone = dataTable["Phone"].ToString();
            this.email = dataTable["Email"].ToString();
            this.idUser = Convert.ToInt32(dataTable["Id_User"].ToString());
            this.salary = Convert.ToDouble(dataTable["Salary"].ToString());
            SearchLoginPass();
        }
        public Managers(int id, string login, string pass, string name, string lastName, 
            DateTime dateBrith, string phone, string email, int salary, int idUser)
        {
            this.id = id;
            this.login = login;
            this.pass = pass;
            this.salary = salary;
            Name = name;
            LastName = lastName;
            DateBrith = dateBrith;
            Phone = phone;
            Email = email;
            this.IdUser = idUser;

        }
        public Managers(string login, string pass, string name, string lastName, 
            DateTime dateBrith, string phone, string email, int salary)
        {
            this.login = login;
            this.pass = pass;
            this.salary = salary;
            Name = name;
            LastName = lastName;
            DateBrith = dateBrith;
            Phone = phone;
            Email = email;

        }

        public Managers(string login, string pass, string name, string lastName, 
            DateTime dateBrith, string phone, string email, int salary, int idUser):
        this(login, pass, name, lastName, dateBrith, phone, email, salary)
        {
            this.idUser = idUser;
        }

        public Managers()
        {
        }


        public override int Id { get => id; set => id = value; }
        public override string Name { get => name; set => name = value; }
        public override string LastName { get => lastName; set => lastName = value; }
        public override DateTime DateBrith { get => dateBrith; set => dateBrith = value; }
        public override string Phone { get => phone; set => phone = value; }
        public override string Email { get => email; set => email = value; }
        public int IdUser { get => idUser; set => idUser = value; }
        public string Pass { get => pass; set => pass = value; }
        public string Login { get => login; set => login = value; }
        public double Salary { get => salary; set => salary = value; }

        public static void SearchAll(DataGrid _dgv)
        {
            MySqlLib.MySqlData.MySqlExecuteData.MyResultData result = new MySqlLib.MySqlData.MySqlExecuteData.MyResultData();

            result = MySqlLib.MySqlData.MySqlExecuteData.SqlReturnDataset("select * from manager");
            if (result.HasError == false)
            {
                _dgv.Columns.Clear();
                _dgv.ItemsSource = result.ResultData.DefaultView;
            }
        }

        public static void Search(DataGrid _dgv, string find)
        {
            MySqlLib.MySqlData.MySqlExecuteData.MyResultData result = new MySqlLib.MySqlData.MySqlExecuteData.MyResultData();
            string select = "select * from manager where " +
                "id Like '%" + find + "%' or " +
                "Id_User Like '%" + find + "%' or " +
                "Salary Like '%" + find + "%' or " +
                "Name Like '%" + find + "%' or " +
                "Last_name Like '%" + find + "%' or " +
                "Date_brith Like '%" + find + "%' or " +
                "Phone Like '%" + find + "%' or " +
                "Email Like '%" + find + "%'";

            result = MySqlLib.MySqlData.MySqlExecuteData.SqlReturnDataset(select);
            if (result.HasError == false)
            {
                _dgv.Columns.Clear();
                _dgv.ItemsSource = result.ResultData.DefaultView;
            }
        }

        public void SearchLoginPass()
        {
            MySqlLib.MySqlData.MySqlExecuteData.MyResultData result = new MySqlLib.MySqlData.MySqlExecuteData.MyResultData();
            string select = "select * from Users where id=" + this.idUser + "";

            result = MySqlLib.MySqlData.MySqlExecuteData.SqlReturnDataset(select);
            if (result.ResultData.DefaultView.Table.Rows.Count == 1)
            {
                this.login =  result.ResultData.DefaultView.Table.Rows[0]["Login"].ToString();
                this.pass = result.ResultData.DefaultView.Table.Rows[0]["Password"].ToString();
            }
            else return;
        }

        public static bool SearchLogin(string find)
        {
            MySqlLib.MySqlData.MySqlExecuteData.MyResultData result = new MySqlLib.MySqlData.MySqlExecuteData.MyResultData();
            string select = "select * from Users where Login='" + find + "'";

            result = MySqlLib.MySqlData.MySqlExecuteData.SqlReturnDataset(select);
            if (result.ResultData.DefaultView.Table.Rows.Count == 0)
            {
                return true;
            }
            else return false;
        }

        public override bool Insert()
        {
            MySqlLib.MySqlData.MySqlExecute.MyResult result = new MySqlLib.MySqlData.MySqlExecute.MyResult();
            string query = "INSERT INTO `pcclub`.`users` " +
                "(`Login`, `Password`) VALUES " +
                "('" + this.login + "','" + this.pass + "')";
            result = MySqlLib.MySqlData.MySqlExecute.SqlNoneQuery(query);

            if (result.HasError == false)
            {
            }
            else return false;

            MySqlLib.MySqlData.MySqlExecuteData.MyResultData idUser = new MySqlLib.MySqlData.MySqlExecuteData.MyResultData();
            string select = "select (`id`) from Users where Login='" + this.login + "'";

            idUser = MySqlLib.MySqlData.MySqlExecuteData.SqlReturnDataset(select);

            if (idUser.HasError == false)
            {
                this.idUser = Convert.ToInt32(idUser.ResultData.DefaultView.Table.Rows[0]["id"].ToString());
            }
            else return false;

            result = new MySqlLib.MySqlData.MySqlExecute.MyResult();
            query = "INSERT INTO `pcclub`.`manager` " +
                "(`Name`, `Last_name`, `Date_brith`, `Phone`, `Email`, `Salary`, `Id_User`) VALUES " +
                "('" + this.Name + "','" + this.LastName + "','" + 
                this.DateBrith.Year + "-" + this.DateBrith.Month + "-"
                + this.DateBrith.Day + "','" + this.Phone + "'," + 
                "'" + this.Email + "', " + this.salary + " , " + 
                this.idUser + ")";
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
            string query = "UPDATE `pcclub`.`users` SET " +
                "Login = '" + this.Login + "', " +
                "Password = '" + this.pass + "' " +
                "WHERE Id = " + this.idUser;
            result = MySqlLib.MySqlData.MySqlExecute.SqlNoneQuery(query);

            if (result.HasError == false)
            {
            }
            else return false;

            result = new MySqlLib.MySqlData.MySqlExecute.MyResult();
            query = "UPDATE `pcclub`.`manager` SET " +
                "Salary = " + this.salary + " , " +
                "Name = '" + this.Name + "'," +
                "Last_name = '" + this.LastName + "'," +
                "Date_brith = '" + this.DateBrith.Year + "-" + this.DateBrith.Month + "-" + this.DateBrith.Day + "'," +
                "Email = '" + this.Email + "' " +
                "WHERE Id = " + this.Id;
            result = MySqlLib.MySqlData.MySqlExecute.SqlNoneQuery(query);
            if (result.HasError == false)
            {
                return true;
            }
            else return false;
        }
    }
}
