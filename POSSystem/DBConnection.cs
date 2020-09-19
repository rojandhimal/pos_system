
namespace POSSystem
{
    class DBConnection
    {
        public string MyConnection()
        {
            string con = @"Data Source=ROJAN\ROJAN;Initial Catalog=pos_system;Integrated Security=True";
            return con;
        }
    }
}
