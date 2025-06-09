using SQLite;
using DoAn.Models;
using System.Diagnostics;

namespace DoAn.Services
{
    public class DatabaseServices
    {
        private readonly SQLiteAsyncConnection _database;
        private bool _isInitialized;

        public DatabaseServices()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mydatabase.db3");
            System.Diagnostics.Debug.WriteLine($"Database path: {dbPath}");
            _database = new SQLiteAsyncConnection(dbPath);
            _isInitialized = false;
        }

        private async Task InitializeAsync()
        {
            if (_isInitialized)
                return;

            try
            {
                // tạo bảng Tour 
                var tableInfo = await _database.GetTableInfoAsync("Tour");
                if (tableInfo.Count == 0)
                {
                    await _database.CreateTableAsync<Tour>();
                    System.Diagnostics.Debug.WriteLine("Tour table created");
                    
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Tour table already exists");
                }
                // tạo bảng TourSessions
                tableInfo = await _database.GetTableInfoAsync("TourSessions");
                if (tableInfo.Count == 0)
                {
                    await _database.CreateTableAsync<TourSessions>();
                    Debug.WriteLine("Table 'TourSessions' created successfully.");
                }

                // tạo bảng Booking
                tableInfo = await _database.GetTableInfoAsync("Booking");
                if (tableInfo.Count == 0)
                {
                    await _database.CreateTableAsync<Booking>();
                    Debug.WriteLine("Table 'booking' created successfully.");
                }
                tableInfo = await _database.GetTableInfoAsync("Review");
                if (tableInfo.Count == 0)
                {
                    await _database.CreateTableAsync<Review>();
                    Debug.WriteLine("Table 'Review' created successfully.");
                }

                // tạo bảng users
                tableInfo = await _database.GetTableInfoAsync("Users");
                if (tableInfo.Count == 0)
                {
                    await _database.CreateTableAsync<Users>();
                    System.Diagnostics.Debug.WriteLine("Users table created");
                    await SeedAdminUser(); // Thêm tài khoản admin khi tạo bảng Users
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Users table already exists");
                }

                //tạo bảng favorite
                tableInfo = await _database.GetTableInfoAsync("FavoriteTour");
                if (tableInfo.Count == 0)
                {
                    await _database.CreateTableAsync<FavoriteTour>();
                    System.Diagnostics.Debug.WriteLine("FavoriteTour table created");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("FavoriteTour table already exists");
                }

                _isInitialized = true;
                System.Diagnostics.Debug.WriteLine("Database initialization completed");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database initialization error: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        private async Task EnsureInitializedAsync()
        {
            if (!_isInitialized)
            {
                await InitializeAsync();
            }
        }

        private async Task SeedAdminUser()
        {
            try
            {
                var admin = await _database.Table<Users>().FirstOrDefaultAsync(u => u.Role == "Admin");
                if (admin == null)
                {
                    var newAdmin = new Users
                    {
                        Name = "Administrator",
                        Username = "admin",
                        Pass = "admin",
                        Phonenumber = "0123456789",
                        Role = "Admin"
                    };
                    await _database.InsertAsync(newAdmin);
                    Debug.WriteLine("Admin user created: Username = admin, Password = admin123");
                }
                else
                {
                    Debug.WriteLine("Admin user already exists");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error seeding admin user: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Failed to seed admin user", ex);
            }
        }

        public async Task CloseAsync()
        {
            await _database.CloseAsync();
            System.Diagnostics.Debug.WriteLine("Database connection closed");
        }

        // Quản lý Tour
        public async Task<bool> AddTour(string tourName, string location, string description, string imageUrl, decimal price, float avgRate = 0, int totalBooked = 0)
        {
            await InitializeAsync();
            try
            {
                var tour = new Tour
                {
                    TourName = tourName,
                    Location = location,
                    Description = description,
                    ImageUrl = imageUrl,
                    Price = price,
                    AvgRate = avgRate,
                    TotalBooked = totalBooked
                };
                Debug.WriteLine($"Adding tour: {tourName}, ImageUrl: {imageUrl}");
                return await _database.InsertAsync(tour) > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in AddTour: {ex.Message}, StackTrace: {ex.StackTrace}");
                return false;
            }
        }

        public async Task<bool> UpdateTour(Tour tour)
        {
            await InitializeAsync();
            try
            {
                var rowsAffected = await _database.UpdateAsync(tour);
                Debug.WriteLine($"Updated tour with ID {tour.TourId}, rows affected: {rowsAffected}");
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UpdateTour error: {ex.Message}, StackTrace: {ex.StackTrace}");
                return false;
            }
        }

        public async Task<int> DeleteTour(int tourId)
        {
            await InitializeAsync();
            try
            {
                var rowsAffected = await _database.DeleteAsync<Tour>(tourId);
                Debug.WriteLine($"Deleted tour with ID {tourId}, rows affected: {rowsAffected}");
                return rowsAffected;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DeleteTour error: {ex.Message}, StackTrace: {ex.StackTrace}");
                return 0;
            }
        }

        public async Task<List<Tour>> GetTours(string search = "")
        {
            await InitializeAsync();
            System.Diagnostics.Debug.WriteLine($"Getting tours with search: {search}");
            try
            {
                var query = _database.Table<Tour>();
                if (!string.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(t => t.TourName.Contains(search) || t.Location.Contains(search));
                }
                query = query.OrderByDescending(t => t.TotalBooked);

                var tours = await query.ToListAsync();
                System.Diagnostics.Debug.WriteLine($"Found {tours.Count} tours");
                foreach (var tour in tours)
                {
                    System.Diagnostics.Debug.WriteLine($"Tour: {tour.TourName}, Location: {tour.Location}, Price: {tour.Price}, ImageUrl: {tour.ImageUrl}");
                }
                return tours;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetTours error: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<Tour> GetTourById(int tourId)
        {
            await InitializeAsync();
            try
            {
                var tour = await _database.Table<Tour>().Where(t => t.TourId == tourId).FirstOrDefaultAsync();
                Debug.WriteLine($"Found tour with ID {tourId}: {tour?.TourName}");
                return tour;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetTourById error: {ex.Message}, StackTrace: {ex.StackTrace}");
                return null;
            }
        }

        public async Task InsertSampleTours()
        {
            await InitializeAsync();
            try
            {
                var existingTours = await _database.Table<Tour>().CountAsync();
                System.Diagnostics.Debug.WriteLine($"Existing tours count: {existingTours}");
                if (existingTours == 0)
                {
                    var sampleTours = new List<Tour>
                    {
                        new Tour
                        {
                            TourName = "Hà Nội City Tour",
                            Location = "Hà Nội",
                            Description = "Khám phá thủ đô Việt Nam với các địa điểm nổi tiếng như Hồ Gươm, Văn Miếu.",
                            ImageUrl = "thudo.jpg",
                            Price = 500000,
                            AvgRate = 0f,
                            TotalBooked = 0
                        },
                        new Tour
                        {
                            TourName = "Vịnh Hạ Long",
                            Location = "Quảng Ninh",
                            Description = "Du thuyền trên vịnh Hạ Long, ngắm cảnh núi đá vôi và hang động.",
                            ImageUrl = "vinhhalong.jpg",
                            Price = 1500000,
                            AvgRate = 0f,
                            TotalBooked = 0
                        },
                        new Tour
                        {
                            TourName = "Đà Lạt Mộng Mơ",
                            Location = "Đà Lạt",
                            Description = "Tham quan thành phố ngàn hoa với khí hậu mát mẻ và cảnh sắc thơ mộng.",
                            ImageUrl = "dalat.jpg",
                            Price = 1200000,
                            AvgRate = 0f,
                            TotalBooked = 0
                        }
                    };
                    await _database.InsertAllAsync(sampleTours);
                    System.Diagnostics.Debug.WriteLine("Inserted sample tours successfully");
                    var backupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mydatabase_backup.db3");
                    File.Copy(_database.DatabasePath, backupPath, true);
                    System.Diagnostics.Debug.WriteLine($"Backed up database to: {backupPath}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Sample tours already exist, skipping insertion");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"InsertSampleTours error: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task UpdateTotalBooked(int tourId)
        {
            await EnsureInitializedAsync();
            try
            {
                // Lấy tất cả các phiên tour đã hoàn thành của tour
                var completedSessions = await _database.Table<TourSessions>()
                    .Where(s => s.TourId == tourId && s.Status == 1)
                    .ToListAsync();

                int totalBooked = 0;
                foreach (var session in completedSessions)
                {
                    var bookings = await _database.Table<Booking>()
                        .Where(b => b.TourSessionId == session.Id)
                        .ToListAsync();
                    totalBooked += bookings.Sum(b => b.NumberOfPeople);
                }

                // Cập nhật TotalBooked trong bảng Tour
                var tour = await _database.Table<Tour>()
                    .Where(t => t.TourId == tourId)
                    .FirstOrDefaultAsync();
                if (tour != null)
                {
                    tour.TotalBooked = totalBooked;
                    await _database.UpdateAsync(tour);
                    Debug.WriteLine($"Updated TotalBooked for TourId {tourId} to {totalBooked}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UpdateTotalBooked error: {ex.Message}, StackTrace: {ex.StackTrace}");
            }
        }

        // Quản lý Đặt Tour
        public Task<int> AddTourSession(int tourId, DateTime startDate, int totalSeats, int remainingSeats, int status)
        {
            var session = new TourSessions
            {
                TourId = tourId,
                StartDate = startDate,
                TotalSeats = totalSeats,
                RemainingSeats = remainingSeats,
                Status = status // Status giờ là int (0 hoặc 1)
            };
            return _database.InsertAsync(session);
        }

        public Task<int> UpdateTourSession(TourSessions session)
        {
            return _database.UpdateAsync(session);
        }

        public Task<int> DeleteTourSession(int sessionId)
        {
            return _database.DeleteAsync<TourSessions>(sessionId);
        }

        public async Task<TourSessions> GetTourSessionById(int sessionId)
        {
            return await _database.Table<TourSessions>().Where(ts => ts.Id == sessionId).FirstOrDefaultAsync();
        }

        public async Task<List<TourSessions>> GetTourSessionsByTourId(int tourId)
        {
            await EnsureInitializedAsync();
            return await _database.Table<TourSessions>()
                .Where(s => s.TourId == tourId && s.Status == 0) // Lọc chỉ các phiên chưa hoàn thành
                .ToListAsync();
        }

        public async Task<List<TourSessions>> GetAllTourSessionsByTourId(int tourId)
        {
            await EnsureInitializedAsync();
            return await _database.Table<TourSessions>()
                .Where(s => s.TourId == tourId) // Lấy tất cả phiên tour
                .ToListAsync();
        }

        public async Task<List<TourSessions>> GetAllTourSessionsWithStatusZero()
        {
            await EnsureInitializedAsync();
            return await _database.Table<TourSessions>().Where(s => s.Status == 0).ToListAsync();
        }

        public async Task<Dictionary<Tour, List<TourSessions>>> GetToursWithSessionsStatusZero()
        {
            await EnsureInitializedAsync();
            try
            {
                var tours = await GetTours();
                var sessions = await GetAllTourSessionsWithStatusZero();

                var result = new Dictionary<Tour, List<TourSessions>>();
                foreach (var tour in tours)
                {
                    var tourSessions = sessions.Where(s => s.TourId == tour.TourId).ToList();
                    if (tourSessions.Any())
                    {
                        result.Add(tour, tourSessions);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetToursWithSessionsStatusZero error: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<int> AddBooking(Booking booking)
        {
            await EnsureInitializedAsync();
            return await _database.InsertAsync(booking);
        }

        public async Task<int> UpdateBooking(Booking booking)
        {
            await EnsureInitializedAsync();
            try
            {
                // Lấy bản ghi hiện tại từ cơ sở dữ liệu
                var existingBooking = await _database.Table<Booking>()
                    .Where(b => b.Id == booking.Id)
                    .FirstOrDefaultAsync();

                if (existingBooking != null)
                {
                    // Cập nhật chỉ các giá trị cần thiết, giữ nguyên các giá trị khác
                    existingBooking.CanReview = booking.CanReview; // Chỉ cập nhật CanReview
                    var rowsAffected = await _database.UpdateAsync(existingBooking);
                    System.Diagnostics.Debug.WriteLine($"Updated booking with ID {booking.Id}, rows affected: {rowsAffected}");
                    return rowsAffected;
                }
                return 0; // Không tìm thấy bản ghi để cập nhật
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateBooking error: {ex.Message}, StackTrace: {ex.StackTrace}");
                return 0;
            }
        }

        public async Task<List<Booking>> GetBookingsByUserId(int userId)
        {
            await EnsureInitializedAsync();
            return await _database.Table<Booking>().Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<Booking> GetBookingById(int bookingId)
        {
            await EnsureInitializedAsync();
            return await _database.Table<Booking>().Where(b => b.Id == bookingId).FirstOrDefaultAsync();
        }

        public async Task<List<Booking>> GetBookingsByTourSessionId(int tourSessionId)
        {
            try
            {
                return await _database.Table<Booking>()
                    .Where(b => b.TourSessionId == tourSessionId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetBookingsByTourSessionId error: {ex.Message}, StackTrace: {ex.StackTrace}");
                return new List<Booking>(); // Trả về danh sách rỗng nếu có lỗi
            }
        }

        // Quản lý Đánh giá
        public async Task<int> AddReview(Review review)
        {
            return await _database.InsertAsync(review);
        }

        public async Task<List<Review>> GetReviewsByToursId(int tourSessionId)
        {
            return await _database.Table<Review>()
                .Where(r => r.TourSessionId == tourSessionId)
                .ToListAsync();
        }

        public async Task UpdateAvgRate(int tourId)
        {
            await EnsureInitializedAsync();
            try
            {
                // Lấy tất cả các phiên tour đã hoàn thành
                var completedSessions = await _database.Table<TourSessions>()
                    .Where(s => s.TourId == tourId && s.Status == 1)
                    .ToListAsync();

                if (!completedSessions.Any())
                {
                    Debug.WriteLine($"No completed sessions found for TourId {tourId}");
                    return;
                }

                // Lấy tất cả đánh giá từ các phiên đã hoàn thành
                var reviews = new List<int>();
                foreach (var session in completedSessions)
                {
                    var sessionReviews = await _database.Table<Review>()
                        .Where(r => r.TourSessionId == session.Id)
                        .ToListAsync();
                    reviews.AddRange(sessionReviews.Select(r => r.Rating));
                }

                if (reviews.Any())
                {
                    float avgRate = (float)reviews.Average();
                    var tour = await _database.Table<Tour>()
                        .Where(t => t.TourId == tourId)
                        .FirstOrDefaultAsync();
                    if (tour != null)
                    {
                        tour.AvgRate = avgRate;
                        await _database.UpdateAsync(tour);
                        Debug.WriteLine($"Updated AvgRate for TourId {tourId} to {avgRate}");
                    }
                }
                else
                {
                    Debug.WriteLine($"No reviews found for TourId {tourId}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UpdateAvgRate error: {ex.Message}, StackTrace: {ex.StackTrace}");
            }
        }

        // Quản lý Yêu thích
        public async Task<bool> IsTourFavorite(int userId, int tourId)
        {
            await InitializeAsync();
            try
            {
                var count = await _database.Table<FavoriteTour>()
                                         .Where(f => f.UserId == userId && f.TourId == tourId)
                                         .CountAsync();
                System.Diagnostics.Debug.WriteLine($"IsTourFavorite: UserId={userId}, TourId={tourId}, IsFavorite={count > 0}");
                return count > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"IsTourFavorite error: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> ToggleFavoriteTour(int userId, int tourId)
        {
            await InitializeAsync();
            try
            {
                var existing = await _database.Table<FavoriteTour>()
                                            .Where(f => f.UserId == userId && f.TourId == tourId)
                                            .FirstOrDefaultAsync();
                if (existing != null)
                {
                    await _database.DeleteAsync(existing);
                    System.Diagnostics.Debug.WriteLine($"Removed tour from favorites: UserId={userId}, TourId={tourId}");
                    return false; // Not favorite anymore
                }
                else
                {
                    var favorite = new FavoriteTour
                    {
                        UserId = userId,
                        TourId = tourId
                    };
                    await _database.InsertAsync(favorite);
                    System.Diagnostics.Debug.WriteLine($"Added tour to favorites: UserId={userId}, TourId={tourId}");
                    return true; // Now favorite
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ToggleFavoriteTour error: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<List<Tour>> GetFavoriteTours(int userId)
        {
            await InitializeAsync();
            try
            {
                var favoriteTours = await _database.Table<FavoriteTour>()
                                                  .Where(f => f.UserId == userId)
                                                  .ToListAsync();
                var tours = new List<Tour>();
                foreach (var favorite in favoriteTours)
                {
                    var tour = await _database.Table<Tour>()
                                             .Where(t => t.TourId == favorite.TourId)
                                             .FirstOrDefaultAsync();
                    if (tour != null)
                    {
                        tours.Add(tour);
                    }
                }
                System.Diagnostics.Debug.WriteLine($"Found {tours.Count} favorite tours for UserId: {userId}");
                return tours;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetFavoriteTours error: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        // Admin Quản lý Tài khoản
        public async Task<bool> IsUsernameExists(string username)
        {
            await InitializeAsync();
            System.Diagnostics.Debug.WriteLine($"Checking if username exists: {username}");
            return await _database.Table<Users>().Where(u => u.Username == username).CountAsync() > 0;
        }

        public async Task<bool> RegisterUser(string name, string username, string password, string phonenumber)
        {
            await InitializeAsync();
            var user = new Users
            {
                Name = name,
                Username = username,
                Pass = password,
                Phonenumber = phonenumber,
                Role = "User"
            };
            System.Diagnostics.Debug.WriteLine($"Registering user: {username}");
            return await _database.InsertAsync(user) > 0;
        }

        public async Task<Users> GetUser(string username, string password)
        {
            await InitializeAsync();
            System.Diagnostics.Debug.WriteLine($"Attempting to get user: {username}");
            try
            {
                var users = await _database.Table<Users>().ToListAsync();
                System.Diagnostics.Debug.WriteLine($"Total users in database: {users.Count}");
                foreach (var u in users)
                {
                    System.Diagnostics.Debug.WriteLine($"Found user: {u.Username}, Pass: {u.Pass}, Role: {u.Role}");
                }

                var user = await _database.Table<Users>()
                                        .Where(u => u.Username == username && u.Pass == password)
                                        .FirstOrDefaultAsync();
                System.Diagnostics.Debug.WriteLine(user != null ? "User matched" : "No matching user found");
                return user;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetUser error: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<List<Users>> GetAllUsers()
        {
            await InitializeAsync();
            try
            {
                var users = await _database.Table<Users>().ToListAsync();
                Debug.WriteLine($"Found {users.Count} users");
                return users;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetAllUsers error: {ex.Message}, StackTrace: {ex.StackTrace}");
                return new List<Users>();
            }
        }

        public async Task<int> DeleteUser(int userId)
        {
            await InitializeAsync();
            try
            {
                var rowsAffected = await _database.DeleteAsync<Users>(userId);
                Debug.WriteLine($"Deleted user with ID {userId}, rows affected: {rowsAffected}");
                return rowsAffected;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DeleteUser error: {ex.Message}, StackTrace: {ex.StackTrace}");
                return 0;
            }
        }

        // Báo cáo và Thống kê
        public async Task<Dictionary<string, decimal>> GetRevenueByTour()
        {
            await EnsureInitializedAsync();
            try
            {
                var bookings = await _database.Table<Booking>().ToListAsync();
                var revenueByTour = new Dictionary<string, decimal>();
                foreach (var booking in bookings)
                {
                    var tourSession = await GetTourSessionById(booking.TourSessionId);
                    if (tourSession != null)
                    {
                        var tour = await GetTourById(tourSession.TourId);
                        if (tour != null)
                        {
                            string tourName = tour.TourName;
                            decimal revenue = booking.NumberOfPeople * tour.Price;
                            if (revenueByTour.ContainsKey(tourName))
                                revenueByTour[tourName] += revenue;
                            else
                                revenueByTour[tourName] = revenue;
                        }
                    }
                }
                System.Diagnostics.Debug.WriteLine($"Revenue by tour: {string.Join(", ", revenueByTour.Select(kvp => $"{kvp.Key}: {kvp.Value}"))}");
                return revenueByTour;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetRevenueByTour error: {ex.Message}, StackTrace: {ex.StackTrace}");
                return new Dictionary<string, decimal>();
            }
        }

        public async Task<Dictionary<int, Dictionary<int, decimal>>> GetRevenueByMonthForAllYears()
        {
            await EnsureInitializedAsync();
            try
            {
                var bookings = await _database.Table<Booking>().ToListAsync();
                var revenueByYearAndMonth = new Dictionary<int, Dictionary<int, decimal>>();
                foreach (var booking in bookings)
                {
                    var tourSession = await GetTourSessionById(booking.TourSessionId);
                    if (tourSession != null)
                    {
                        var tour = await GetTourById(tourSession.TourId);
                        if (tour != null)
                        {
                            int year = tourSession.StartDate.Year;
                            int month = tourSession.StartDate.Month;
                            decimal revenue = booking.NumberOfPeople * tour.Price;

                            if (!revenueByYearAndMonth.ContainsKey(year))
                                revenueByYearAndMonth[year] = new Dictionary<int, decimal>();
                            if (revenueByYearAndMonth[year].ContainsKey(month))
                                revenueByYearAndMonth[year][month] += revenue;
                            else
                                revenueByYearAndMonth[year][month] = revenue;
                        }
                    }
                }
                System.Diagnostics.Debug.WriteLine($"Revenue by month for all years: {string.Join(", ", revenueByYearAndMonth.Select(kvp => $"{kvp.Key}: {string.Join(", ", kvp.Value.Select(m => $"{m.Key}: {m.Value}"))}"))}");
                return revenueByYearAndMonth;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetRevenueByMonthForAllYears error: {ex.Message}, StackTrace: {ex.StackTrace}");
                return new Dictionary<int, Dictionary<int, decimal>>();
            }
        }

        public async Task<Dictionary<int, Dictionary<int, int>>> GetTicketsSoldByMonthForAllYears()
        {
            await EnsureInitializedAsync();
            try
            {
                var bookings = await _database.Table<Booking>().ToListAsync();
                var ticketsByYearAndMonth = new Dictionary<int, Dictionary<int, int>>();
                foreach (var booking in bookings)
                {
                    var tourSession = await GetTourSessionById(booking.TourSessionId);
                    if (tourSession != null)
                    {
                        int year = tourSession.StartDate.Year;
                        int month = tourSession.StartDate.Month;
                        int tickets = booking.NumberOfPeople;

                        if (!ticketsByYearAndMonth.ContainsKey(year))
                            ticketsByYearAndMonth[year] = new Dictionary<int, int>();
                        if (ticketsByYearAndMonth[year].ContainsKey(month))
                            ticketsByYearAndMonth[year][month] += tickets;
                        else
                            ticketsByYearAndMonth[year][month] = tickets;
                    }
                }
                System.Diagnostics.Debug.WriteLine($"Tickets sold by month for all years: {string.Join(", ", ticketsByYearAndMonth.Select(kvp => $"{kvp.Key}: {string.Join(", ", kvp.Value.Select(m => $"{m.Key}: {m.Value}"))}"))}");
                return ticketsByYearAndMonth;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetTicketsSoldByMonthForAllYears error: {ex.Message}, StackTrace: {ex.StackTrace}");
                return new Dictionary<int, Dictionary<int, int>>();
            }
        }
    }
}