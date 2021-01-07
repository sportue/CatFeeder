using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CatFeeder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        bool isLoggerOut;
        public MainPage(bool isLoggerOut)
        {
            InitializeComponent();
            this.IsLoggerOut = isLoggerOut;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (!isLoggerOut)
            {
                Navigation.RemovePage(this);
            }

        }

        protected void ClickLoginPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignIn());
        }


        public bool IsLoggerOut { get => isLoggerOut; set => isLoggerOut = value; }


    }
}