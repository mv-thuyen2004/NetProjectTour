using SQLite;

namespace DoAn.Models
{
    public class Tour
    {
        [PrimaryKey, AutoIncrement]
        public int TourId { get; set; }
        public string TourName { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public float AvgRate { get; set; }
        public int TotalBooked { get; set; }
    }
}