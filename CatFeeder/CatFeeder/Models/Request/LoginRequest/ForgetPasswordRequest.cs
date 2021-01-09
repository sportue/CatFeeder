using System;
using System.Collections.Generic;
using System.Text;

namespace CatFeeder.Models.Request.LoginRequest
{
    public class ForgetPasswordRequest
    {
        public string Username { get; set; }
       
        public string Email { get; set; }

    }
}
