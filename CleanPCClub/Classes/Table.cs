namespace CleanPCClub
{
    internal abstract class Table
    {
        protected int id;

        public abstract int Id { get; set; }
        public abstract bool Insert();
        public abstract bool Update();
    }
}