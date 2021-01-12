using System;
using CatFeeder.Models.Google;

namespace CatFeeder.DependencyServices
{
    public interface IGoogleManager
    {
        void Login(Action<GoogleUser, string> OnLoginComplete);
        void Logout();
    }
}
