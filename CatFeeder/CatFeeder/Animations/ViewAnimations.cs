using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CatFeeder.Animations
{
    public static class ViewAnimations
    {
        public static void FadeAnimY(View view)
        {
            Animation animation = new Animation();
            animation.Add(0, 0.1, new Animation(v => view.Opacity = v, view.Opacity, 1));
            animation.Add(0, 0.1, new Animation(v => view.TranslationY = v, view.TranslationY, 0));
            animation.Add(0, 0.1, new Animation(v => view.TranslationX = v, view.TranslationX, 0));
            animation.Commit(view, "StartAnim", 16, 2000, null, null);

        }
    }

}
