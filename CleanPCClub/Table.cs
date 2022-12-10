using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanPCClub
{
    class Table
    {
        private static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=PCClub.mdb;";
        private OleDbConnection myConnection;

        public void MySelect() { }
        public void MyUpdate() { }
        public void MyDelete() { }
        public void MyInsert() { }

    }
}
