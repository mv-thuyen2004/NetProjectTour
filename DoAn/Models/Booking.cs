namespace DoAn.Models
{
    public class Booking
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; } // ID của người dùng
        public int TourSessionId { get; set; } // ID của phiên tour
        public int NumberOfPeople { get; set; } // Số người đặt
        public int CanReview { get; set; } // 0: Không thể đánh giá, 1: Có thể đánh giá
    }
}