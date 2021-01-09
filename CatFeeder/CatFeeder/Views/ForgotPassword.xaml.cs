using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatFeeder.Animations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CatFeeder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPassword : ContentPage
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        protected void Back(object s, EventArgs e)
        {
            Navigation.PopAsync();
        }
        protected void Login(object s, EventArgs e)
        {
           // Navigation.PushAsync(new LoginPage15());
        }
    }
}