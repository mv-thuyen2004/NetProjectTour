using System.ComponentModel.DataAnnotations.Schema;
using SQLite;

namespace DoAn.Models
{
    public class TourSessions
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int TourId { get; set; }

        public DateTime StartDate { get; set; }

        public int TotalSeats { get; set; }

        public int RemainingSeats { get; set; }

        public int Status { get; set; } // 0: Chưa bắt đầu, 1: Đã hoàn thành
    }
}