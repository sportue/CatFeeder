using System.Net.Mail;
using System.Text.RegularExpressions;

namespace CatFeeder.Helpers
{
    public static  class Extensions
    {
        public static bool IsValidEmailAddress(this string s)
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
