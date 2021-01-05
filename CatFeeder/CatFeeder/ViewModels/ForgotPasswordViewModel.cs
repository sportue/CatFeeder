using System;
using System.Collections.Generic;
using System.Text;

namespace CatFeeder.ViewModels
{
    public class ForgotPasswordViewModel
    {
        string email;

        public ForgotPasswordViewModel()
        {

        }

        public string Email { get => email; set => email = value; }
    }
}
