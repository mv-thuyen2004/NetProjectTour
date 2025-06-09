using SQLite;

namespace DoAn.Models
{
    public class FavoriteTour
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TourId { get; set; }
    }
}