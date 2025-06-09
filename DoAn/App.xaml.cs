namespace DoAn
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            //MainPage = new NavigationPage(new Views.HomePage());

        }
        public static int GetUserId()
        {
            string idStr = Preferences.Get("IdNguoiDung", "-1");
            if (int.TryParse(idStr, out int id))
            {
                return id;
            }
            return -1; // Giá trị mặc định nếu không có
        }



    }
}