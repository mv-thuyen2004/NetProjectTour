using SQLite;

namespace DoAn.Models
{
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
        public string Phonenumber { get; set; }
        public string Role { get; set; }
    }
}