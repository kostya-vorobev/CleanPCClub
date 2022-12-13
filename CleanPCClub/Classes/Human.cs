using System;
using System.Windows.Controls;

namespace CleanPCClub
{
    internal abstract class Human : Table
    {
        protected string name;
        protected string lastName;
        protected DateTime dateBrith;
        protected string phone;
        protected string email;

        public abstract string Name { get; set; }
        public abstract string LastName { get; set; }
        public abstract DateTime DateBrith { get; set; }
        public abstract string Phone { get; set; }
        public abstract string Email { get; set; }
    }
}