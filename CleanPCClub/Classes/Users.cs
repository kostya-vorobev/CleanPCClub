using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanPCClub
{
    public class Users
    {
        private int id;
        private string login;
        private int role;


        public Users()
        {
        }

        public Users(int newId, string newLogin, int newRole)
        {
            id = newId;
            login = newLogin;
            role = newRole;
        }

        public  int Id { get => id; set => id = value; }
        public int Role { get => role; set => role = value; }
        public string Login { get => login; set => login = value; }

    }
}
