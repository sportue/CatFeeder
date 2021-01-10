using CatFeeder.Renderers;
using Xamarin.Forms;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace CatFeeder.Helpers
{
    public class EmailValidationBehavior : Behavior<BorderlessEntry>
    {
        protected override void OnAttachedTo(BorderlessEntry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += BindingContextChanged;

        }

        protected override void OnDetachingFrom(BorderlessEntry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged += BindingContextChanged;
        }

        private void BindingContextChanged(object sender, TextChangedEventArgs e)
        {
            var email = e.NewTextValue;

            var emailEntry = sender as BorderlessEntry;

            if (IsValidEmailAddress(email))
            {
                emailEntry.BackgroundColor = Color.Transparent;
            }
            else
            {
                emailEntry.BackgroundColor = Color.LightBlue;
            }
        }

        public static bool IsValidEmailAddress(string s)
        {
            try
            {
                var temp = new MailAddress(s);

                string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
                var regex = new Regex(validEmailPattern, RegexOptions.IgnoreCase);

                if (!regex.IsMatch(s))
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
