using CatFeeder.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CatFeeder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        public SignUp()
        {
            InitializeComponent();
            buttonRegister.IsEnabled = false;
        }

        void BorderlessEntryUsername_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length < 4)
                labelUsernameError.HeightRequest = 30;
            else
                labelUsernameError.HeightRequest = 0;
            ValidateForm();
        }

        void BorderlessEntryMail_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Email.Text) || !Email.Text.IsValidEmailAddress())
                labelEmailError.HeightRequest = 30;
            else
                labelEmailError.HeightRequest = 0;
            ValidateForm();
        }

        void BorderlessEntryPassword_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length < 6)
                labelPasswordError.HeightRequest = 30;
            else
                labelPasswordError.HeightRequest = 0;
            ValidateForm();
        }

        void BorderlessEntryRePassword_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (Password.Text != RePassword.Text)
                labelRePasswordError.HeightRequest = 30;
            else
                labelRePasswordError.HeightRequest = 0;
            ValidateForm();
        }

        void ValidateForm()
        {
            bool validation = true;
            if (string.IsNullOrWhiteSpace(Email.Text) || !Email.Text.IsValidEmailAddress())
            {
                validation = false;
            }
            if (string.IsNullOrWhiteSpace(entryUsername.Text) || entryUsername.Text.Length < 3)
            {
                validation = false;
            }
            if (string.IsNullOrWhiteSpace(Password.Text) || (Password.Text != RePassword.Text) || Password.Text.Length < 6)
            {
                validation = false;
            }
            buttonRegister.IsEnabled = validation;
        }
    }
}