using CatFeeder.Renderers;
using Xamarin.Forms;


namespace CatFeeder.Helpers
{
    class MinLenghtValidationBehavior : Behavior<BorderlessEntry>
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
            var password = e.NewTextValue;

            var passwordEntry = sender as BorderlessEntry;

            if (password.Length > 5)
            {
                passwordEntry.BackgroundColor = Color.Transparent;
            }
            else
            {
                passwordEntry.BackgroundColor = Color.LightBlue;
            }
        }
    
    }
}
