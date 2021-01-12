using System;
namespace CatFeeder.Models.Google
{
    public class GoogleUser
    {
        public string Name { get; set; }
        public string AccessToken { get; set; }
        public string ExpirationDate { get; set; }
        public string Email { get; set; }
        public Uri Picture { get; set; }
    }
}
