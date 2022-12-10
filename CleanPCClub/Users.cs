using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanPCClub
{
    public class Users
    {
        private string login;
        private int role;

        public Users()
        {
        }

        public Users(string newLogin, int newRole)
        {
            login = newLogin;
            role = newRole;
        }

        public int Role { get => role; set => role = value; }
        public string Login { get => login; set => login = value; }

        

    }
}
