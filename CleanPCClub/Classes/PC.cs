using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CleanPCClub
{
    class PC : Table
    {
        int statusPC;
        int id_Client;
        string oS;
        string mac;

        public override int Id { get => id; set => id = value; }
        public int StatusPC { get => statusPC; set => statusPC = value; }
        public int Id_Client { get => id_Client; set => id_Client = value; }
        public string OS { get => oS; set => oS = value; }

        public PC(int id)
        {
            Id = id;
        }

        public void SelectPCID()
        {
            MySqlLib.MySqlData.MySqlExecuteData.MyResultData result = new MySqlLib.MySqlData.MySqlExecuteData.MyResultData();
            string macAddr = MAC();
            result = MySqlLib.MySqlData.MySqlExecuteData.SqlReturnDataset("select * from PC where MAC='" + macAddr.ToString() + "'");
            if (result.HasError == true)
            {
                throw new Exception("Ошибка подключения к базу данных");
            }
            this.id = Convert.ToInt32(result.ResultData.Rows[0]["id"].ToString());
        }
        public PC()
        {
            this.mac = MAC();
        }

        public static string MAC()
        {
            var macAddr =
                (
                    from nic in NetworkInterface.GetAllNetworkInterfaces()
                    where nic.OperationalStatus == OperationalStatus.Up
                    select nic.GetPhysicalAddress().ToString()
                ).FirstOrDefault();
            return macAddr.ToString();
        }
        public bool CheckStatus()
        {
            MySqlLib.MySqlData.MySqlExecuteData.MyResultData result = new MySqlLib.MySqlData.MySqlExecuteData.MyResultData();
            string macAddr = MAC();
            result = MySqlLib.MySqlData.MySqlExecuteData.SqlReturnDataset("select status from PC where MAC='" + macAddr.ToString() + "'");
            if (result.HasError == false)
            {
                if (result.ResultData.Rows[0]["Status"].ToString() == "0")
                {
                    this.statusPC = 0;
                    return false;
                }
                else
                {
                    this.statusPC = 1;
                    return true;
                }
            }
            return false;
        }
        public static void CheckMAC()
        {
            MySqlLib.MySqlData.MySqlExecuteData.MyResultData result = new MySqlLib.MySqlData.MySqlExecuteData.MyResultData();
            string macAddr = MAC();
            result = MySqlLib.MySqlData.MySqlExecuteData.SqlReturnDataset("select * from PC where MAC='" + macAddr.ToString() + "'");
            if (result.HasError == true)
            {
                throw new Exception("Ошибка подключения к базу данных");
            }
            if (result.ResultData.Rows.Count == 0)
            {
                MySqlLib.MySqlData.MySqlExecute.MyResult insertPC = new MySqlLib.MySqlData.MySqlExecute.MyResult();
                string query = "INSERT INTO `pcclub`.`pc` " +
                    "(`Status`, `Id_Client`, `OS`, `MAC`) " +
                    "VALUES ('0', 1,'Windows','" + macAddr.ToString() + "')";
                insertPC = MySqlLib.MySqlData.MySqlExecute.SqlNoneQuery(query);
                if (result.HasError == true)
                {
                    throw new Exception("Ошибка подключения к базу данных");
                }
            }
        }

        public PC(int status, int id_Client, string oS, int id)
        {
            this.statusPC = status;
            this.id_Client = id_Client;
            this.oS = oS;
            this.id = id;
            this.mac = MAC();
        }
        public PC(DataRowView data)
        {
            this.id = Convert.ToInt32(data["Id"].ToString());
            this.statusPC = Convert.ToInt32(data["Status"].ToString());
            this.id_Client = Convert.ToInt32(data["Id_Client"].ToString());
            this.oS = data["OS"].ToString();
            this.mac = MAC();

        }
        public static void SearchAll(DataGrid _dgv)
        {
            MySqlLib.MySqlData.MySqlExecuteData.MyResultData result = new MySqlLib.MySqlData.MySqlExecuteData.MyResultData();

            result = MySqlLib.MySqlData.MySqlExecuteData.SqlReturnDataset("select * from PC");
            if (result.HasError == false)
            {
                _dgv.Columns.Clear();
                _dgv.ItemsSource = result.ResultData.DefaultView;
            }
        }

        public static void SearchId(ComboBox _cmb)
        {
            MySqlLib.MySqlData.MySqlExecuteData.MyResultData result = new MySqlLib.MySqlData.MySqlExecuteData.MyResultData();

            result = MySqlLib.MySqlData.MySqlExecuteData.SqlReturnDataset("select id from PC where STATUS=0");
            if (result.HasError == false)
            {

                _cmb.Items.Clear();
                if (result.ResultData.Rows.Count == 0)
                {
                    _cmb.Items.Add("Сейчас нет доспутных");
                }
                else
                {
                    for (int i = 0; i < result.ResultData.Rows.Count; i++)
                        _cmb.Items.Add(result.ResultData.Rows[i][0]);
                }
            }
        }

        public override bool Insert()
        {
            throw new NotImplementedException();
        }

        public override bool Update()
        {
            throw new NotImplementedException();
        }

        public bool UpdateStatus(int status)
        {
            MySqlLib.MySqlData.MySqlExecute.MyResult result = new MySqlLib.MySqlData.MySqlExecute.MyResult();
            string query = "UPDATE pc SET " +
                "Status = " + status + ", " +
                "Id_Client = " + 1 + " " +
                "WHERE MAC = '" + this.mac+"'";
            result = MySqlLib.MySqlData.MySqlExecute.SqlNoneQuery(query);
            if (result.HasError == false)
            {
                return true;
            }
            else return false;
        }
    }
}
