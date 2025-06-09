using DoAn.Services;
using DoAn.Models;
namespace DoAn.Models
{
    public class Review
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TourSessionId { get; set; }

        public int Rating { get; set; } // Đánh giá từ 1 đến 5 sao
        public string Comment { get; set; } // Bình luận, có thể null
    }
}