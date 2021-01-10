using CatFeeder.Renderers;
using Xamarin.Forms;

namespace CatFeeder.Helpers
{
    class ConfirmPasswordBehavior : Behavior<BorderlessEntry>
    {
        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(ConfirmPasswordBehavior), false);
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public static readonly BindableProperty CompareToEntryProperty = BindableProperty.Create("CompareToEntry", typeof(BorderlessEntry), typeof(ConfirmPasswordBehavior), null);

        public Entry CompareToEntry
        {
            get { return (Entry)base.GetValue(CompareToEntryProperty); }
            set { base.SetValue(CompareToEntryProperty, value); }
        }
        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }
        protected override void OnAttachedTo(BorderlessEntry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }
        protected override void OnDetachingFrom(BorderlessEntry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            var password = CompareToEntry.Text;
            var confirmPassword = e.NewTextValue;
            IsValid = password.Equals(confirmPassword);
        }
    }
}
