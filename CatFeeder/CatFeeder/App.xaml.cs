using CatFeeder.Services;
using CatFeeder.Views;
using Xamarin.Forms;

namespace CatFeeder
{
    public partial class App : Application
    {

        private static UserRestService userService;
        public static UserRestService UserService { get => userService; set => userService = value; }
        private static MapRestService mapService;
        public static MapRestService MapService { get => mapService; set => mapService = value; }
        public App()
        {
            InitializeComponent();
            UserService = new UserRestService();
            MapService = new MapRestService();
            MainPage = new NavigationPage(new SplashScreen());
           
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
