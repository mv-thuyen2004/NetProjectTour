using CommunityToolkit.Mvvm.ComponentModel;
using DoAn.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microcharts;
using SkiaSharp;
using System.Linq;
using CommunityToolkit.Mvvm.Input;

namespace DoAn.ViewModels
{
    public partial class AdminReportViewModel : ObservableObject
    {
        private readonly DatabaseServices _db;

        [ObservableProperty]
        private ObservableCollection<KeyValuePair<string, decimal>> revenueByTour;

        [ObservableProperty]
        private ObservableCollection<KeyValuePair<int, Dictionary<int, decimal>>> revenueByMonthForAllYears;

        [ObservableProperty]
        private ObservableCollection<KeyValuePair<int, Dictionary<int, int>>> ticketsSoldByMonthForAllYears;

        [ObservableProperty]
        private Chart revenueByTourChart;

        [ObservableProperty]
        private Chart revenueByMonthChart;

        [ObservableProperty]
        private Chart ticketsSoldByMonthChart;

        [ObservableProperty]
        private string message;

        public AdminReportViewModel(DatabaseServices db)
        {
            _db = db;
            RevenueByTour = new ObservableCollection<KeyValuePair<string, decimal>>();
            RevenueByMonthForAllYears = new ObservableCollection<KeyValuePair<int, Dictionary<int, decimal>>>();
            TicketsSoldByMonthForAllYears = new ObservableCollection<KeyValuePair<int, Dictionary<int, int>>>();
            LoadReportDataAsync();
        }

        private async void LoadReportDataAsync()
        {
            try
            {
                // Doanh thu theo tour
                var tourData = await _db.GetRevenueByTour();
                RevenueByTour.Clear();
                foreach (var item in tourData)
                {
                    RevenueByTour.Add(item);
                }
                UpdateTourChart();

                // Doanh thu theo tháng cho tất cả các năm
                var monthRevenueData = await _db.GetRevenueByMonthForAllYears();
                RevenueByMonthForAllYears.Clear();
                foreach (var yearData in monthRevenueData)
                {
                    RevenueByMonthForAllYears.Add(new KeyValuePair<int, Dictionary<int, decimal>>(yearData.Key, yearData.Value));
                }
                UpdateMonthRevenueChart();

                // Số lượng vé bán ra theo tháng cho tất cả các năm
                var monthTicketsData = await _db.GetTicketsSoldByMonthForAllYears();
                TicketsSoldByMonthForAllYears.Clear();
                foreach (var yearData in monthTicketsData)
                {
                    TicketsSoldByMonthForAllYears.Add(new KeyValuePair<int, Dictionary<int, int>>(yearData.Key, yearData.Value));
                }
                UpdateMonthTicketsChart();

                Message = RevenueByTour.Any() || RevenueByMonthForAllYears.Any() || TicketsSoldByMonthForAllYears.Any()
                    ? "Báo cáo đã được tải thành công."
                    : "Không có dữ liệu để hiển thị báo cáo.";
                System.Diagnostics.Debug.WriteLine($"Report loaded - RevenueByTour: {string.Join(", ", RevenueByTour)}, RevenueByMonthForAllYears: {string.Join(", ", RevenueByMonthForAllYears.Select(kvp => $"{kvp.Key}: {string.Join(", ", kvp.Value.Select(m => $"{m.Key}: {m.Value}"))}"))}, TicketsSoldByMonthForAllYears: {string.Join(", ", TicketsSoldByMonthForAllYears.Select(kvp => $"{kvp.Key}: {string.Join(", ", kvp.Value.Select(m => $"{m.Key}: {m.Value}"))}"))}");
            }
            catch (Exception ex)
            {
                Message = $"Lỗi khi tải báo cáo: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"LoadReportDataAsync error: {ex.Message}, StackTrace: {ex.StackTrace}");
            }
        }

        private void UpdateTourChart()
        {
            if (!RevenueByTour.Any())
            {
                RevenueByTourChart = null;
                return;
            }

            var colors = new[] { "#FF6384", "#36A2EB", "#FFCE56", "#4BC0C0", "#9966FF", "#FF9F40" };
            var entries = RevenueByTour.Select((kvp, index) => new ChartEntry((float)kvp.Value)
            {
                Label = kvp.Key,
                ValueLabel = kvp.Value.ToString("N0"),
                Color = SKColor.Parse(colors[index % colors.Length])
            }).ToList();

            RevenueByTourChart = new BarChart
            {
                Entries = entries,
                LabelTextSize = 20,
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal
            };
        }

        private void UpdateMonthRevenueChart()
        {
            if (!RevenueByMonthForAllYears.Any())
            {
                RevenueByMonthChart = null;
                return;
            }

            var colors = new[] { "#FF6384", "#36A2EB", "#FFCE56", "#4BC0C0", "#9966FF", "#FF9F40" };
            var datasets = new List<ChartEntry[]>();
            int colorIndex = 0;

            foreach (var yearData in RevenueByMonthForAllYears)
            {
                var entries = Enumerable.Range(1, 12).Select(month => new ChartEntry((float)(yearData.Value.ContainsKey(month) ? yearData.Value[month] : 0))
                {
                    Label = $"T{month}",
                    ValueLabel = yearData.Value.ContainsKey(month) ? yearData.Value[month].ToString("N0") : "0",
                    Color = SKColor.Parse(colors[colorIndex % colors.Length])
                }).ToArray();
                datasets.Add(entries);
                colorIndex++;
            }

            var combinedEntries = datasets.SelectMany((entries, yearIndex) => entries.Select((entry, monthIndex) => new { Year = RevenueByMonthForAllYears[yearIndex].Key, Month = monthIndex + 1, Entry = entry })).ToList();

            var lineChart = new LineChart
            {
                LabelTextSize = 30,
                LineMode = LineMode.Straight,
                PointMode = PointMode.Circle,
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal
            };

            var groupedEntries = combinedEntries.GroupBy(x => x.Month).Select(g => g.Select(x => x.Entry).ToArray()).ToList();
            lineChart.Entries = groupedEntries.SelectMany((entries, index) => entries).ToList();

            RevenueByMonthChart = lineChart;
        }

        private void UpdateMonthTicketsChart()
        {
            if (!TicketsSoldByMonthForAllYears.Any())
            {
                TicketsSoldByMonthChart = null;
                return;
            }

            var colors = new[] { "#FF6384", "#36A2EB", "#FFCE56", "#4BC0C0", "#9966FF", "#FF9F40" };
            var datasets = new List<ChartEntry[]>();
            int colorIndex = 0;

            foreach (var yearData in TicketsSoldByMonthForAllYears)
            {
                var entries = Enumerable.Range(1, 12).Select(month => new ChartEntry(yearData.Value.ContainsKey(month) ? yearData.Value[month] : 0)
                {
                    Label = $"T{month}",
                    ValueLabel = yearData.Value.ContainsKey(month) ? yearData.Value[month].ToString() : "0",
                    Color = SKColor.Parse(colors[colorIndex % colors.Length])
                }).ToArray();
                datasets.Add(entries);
                colorIndex++;
            }

            var combinedEntries = datasets.SelectMany((entries, yearIndex) => entries.Select((entry, monthIndex) => new { Year = TicketsSoldByMonthForAllYears[yearIndex].Key, Month = monthIndex + 1, Entry = entry })).ToList();

            var lineChart = new LineChart
            {
                LabelTextSize = 30,
                LineMode = LineMode.Straight,
                PointMode = PointMode.Circle,
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal
            };

            var groupedEntries = combinedEntries.GroupBy(x => x.Month).Select(g => g.Select(x => x.Entry).ToArray()).ToList();
            lineChart.Entries = groupedEntries.SelectMany((entries, index) => entries).ToList();

            TicketsSoldByMonthChart = lineChart;
        }

        [RelayCommand]
        private async Task Tab1()
        {
            System.Diagnostics.Debug.WriteLine("Tab1: Staying on HomePage");
            await Shell.Current.GoToAsync("//adminhome");
        }

        [RelayCommand]
        private async Task Tab2()
        {
            System.Diagnostics.Debug.WriteLine("Tab2: Navigating to FavoritesPage");
        }

        [RelayCommand]
        private async Task Tab3()
        {
            System.Diagnostics.Debug.WriteLine("Tab3: Navigating to ProfilePage");
            await Shell.Current.GoToAsync("//profileAdmin");
        }
    }
}