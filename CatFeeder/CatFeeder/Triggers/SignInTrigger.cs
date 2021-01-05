using Xamarin.Forms;

namespace CatFeeder.Triggers
{
    public class SignInTrigger : TriggerAction<Label>
    {
        Label signInInfo;

        public Label SignInChange { get => signInInfo; set => signInInfo = value; }

        protected override void Invoke(Label sender)
        {
            if (sender.Text.Equals("Oturum Açma Başarılı"))
            {
                sender.IsVisible = false;
            }
            else
            {
                sender.IsVisible = true;
            }
        }



    }
}
