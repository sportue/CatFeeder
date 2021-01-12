using CatFeeder.Services;
using CatFeeder.Views;
using Xamarin.Forms;

namespace CatFeeder
{
    public partial class App : Application
    {

        private static UserRestService userService;
        public static UserRestService UserService { get => userService; set => userService = value; }
        public App()
        {
            InitializeComponent();
            UserService = new UserRestService();
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
