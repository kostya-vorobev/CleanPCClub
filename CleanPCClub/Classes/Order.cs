using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CleanPCClub
{
    class Order : Table
    {
        int idPC;
        int idClient;
        int cost;
        DateTime startTime;
        DateTime finishTime;
        int idManager;

        public Order(int idPC, int idClient, int cost, DateTime startTime, DateTime finishTime, int idManager)
        {
            this.idPC = idPC;
            this.idClient = idClient;
            this.cost = cost;
            this.startTime = startTime;
            this.finishTime = finishTime;
            this.idManager = idManager;
        }

        public Order()
        {
        }

        public Order(int id, int idPC, int idClient, int cost, DateTime startTime, DateTime finishTime, int idManager)
        {
            Id = id;
            this.idPC = idPC;
            this.idClient = idClient;
            this.cost = cost;
            this.startTime = startTime;
            this.finishTime = finishTime;
            this.idManager = idManager;
        }

        public override int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int IdPC { get => idPC; set => idPC = value; }
        public int IdClient { get => idClient; set => idClient = value; }
        public int Cost { get => cost; set => cost = value; }
        public DateTime StartTime { get => startTime; set => startTime = value; }
        public DateTime FinishTime { get => finishTime; set => finishTime = value; }
        public int IdManager { get => idManager; set => idManager = value; }

        public static void SearchAll(DataGrid _dgv)
        {
            MySqlLib.MySqlData.MySqlExecuteData.MyResultData result = new MySqlLib.MySqlData.MySqlExecuteData.MyResultData();
            string selectQuery = "SELECT `order`.Id, pc.Id  as PC, clients.Login, " +
                "`order`.cost,`order`.Time_Start, `order`.Time_Finish, " +
                "manager.Name, manager.Last_Name FROM `order`, clients, " +
                "pc, manager WHERE `order`.Id_Client = clients.id " +
                "AND `order`.Id_PC = pc.Id AND `order`.Id_Manager = manager.Id";
            result = MySqlLib.MySqlData.MySqlExecuteData.SqlReturnDataset(selectQuery);
            if (result.HasError == false)
            {
                _dgv.Columns.Clear();
                _dgv.ItemsSource = result.ResultData.DefaultView;
            }
        }

        public static void Search(DataGrid _dgv, string find)
        {
            MySqlLib.MySqlData.MySqlExecuteData.MyResultData result = new MySqlLib.MySqlData.MySqlExecuteData.MyResultData();
            string select = "select * from `pcclub`.`order` where " +
                "Id Like '%" + find + "%' or" +
                " Id_PC Like '%" + find + "%' or " +
                "Cost Like '%" + find + "%' or " +
                " Time_Start Like '%" + find + "%' or " +
                "Time_Finish Like '%" + find + "%' or " +
                " Id_Manager Like '%" + find + "%'";

            result = MySqlLib.MySqlData.MySqlExecuteData.SqlReturnDataset(select);
            if (result.HasError == false)
            {
                _dgv.Columns.Clear();
                _dgv.ItemsSource = result.ResultData.DefaultView;
            }
        }

        public override bool Insert()
        {
            MySqlLib.MySqlData.MySqlExecute.MyResult result = new MySqlLib.MySqlData.MySqlExecute.MyResult();
            string query = "INSERT INTO `pcclub`.`order` " +
                "(`Id_PC`, `Id_Client`, `Cost`, `Time_Start`, `Time_Finish`, `Id_Manager`) VALUES " +
                "('" + this.idPC + "', '" + this.idClient + "'," +
                "'" + this.cost + "','" + this.startTime.ToString("hh:mm:ss") 
                + "','" + finishTime.ToString("hh:mm:ss") + "','" + this.idManager + "')";
            result = MySqlLib.MySqlData.MySqlExecute.SqlScalar(query);
            if (result.HasError == false)
            {
                return true;
            }
            else return false;
        }

        public override bool Update()
        {
            throw new NotImplementedException();
        }
    }
}
