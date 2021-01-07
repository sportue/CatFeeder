using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CatFeeder.Views
{
    public partial class Map : ContentPage
    {
        public Map()
        {
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            SecureStorage.Remove("user_token");
            Navigation.InsertPageBefore(new MainPage(true), this);
            Navigation.PopAsync();

        }
    }
}