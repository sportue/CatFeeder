using CatFeeder.Models;
using CatFeeder.Models.Request.LoginRequest;
using CatFeeder.Models.Response;
using CatFeeder.Models.Response.LoginResponse;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CatFeeder.Services
{
    public class UserRestService
    {

        public UserRestService()
        {

        }
      

        public async Task<User> getUserByMailAndPassword(string email, string password)
        {
            var data = new BaseResponse<SignInResponse>();
            User user = new User();
            HttpClient client = new HttpClient();

            var uri = new Uri(Constants.SignIn);
            var request = new SignInRequest();
            request.Email = email;
            request.Password = password;
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(uri, content);
                HttpStatusCode code = response.StatusCode;
                if (code == HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    data = JsonConvert.DeserializeObject<BaseResponse<SignInResponse>>(result);

                    var stream = data.Data.Token;
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(stream);
                    var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

                    if (!string.IsNullOrWhiteSpace(tokenS.Claims.First(claim => claim.Type == "userID").Value))
                    {
                        user.Id = new Guid(tokenS.Claims.First(claim => claim.Type == "userID").Value);
                    }
                    user.FirstName = tokenS.Claims.First(claim => claim.Type == "firstName").Value;
                    user.LastName = tokenS.Claims.First(claim => claim.Type == "lastName").Value;
                    user.Username = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
                }

            }
            catch (Exception)
            {

                throw;
            }

            return user;
        }

        public async Task<User> getUserByFacebookToken(string tokenAccess)
        {
            var data = new BaseResponse<SignInResponse>();
            User user = new User();
            HttpClient client = new HttpClient();

            var uri = new Uri(Constants.SignInWithFacebook);
            var request = new FacebookRequest();
            request.FacebookToken = tokenAccess;
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(uri, content);
                HttpStatusCode code = response.StatusCode;
                if (code == HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    data = JsonConvert.DeserializeObject<BaseResponse<SignInResponse>>(result);

                    var stream = data.Data.Token;
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(stream);
                    var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

                    if (!string.IsNullOrWhiteSpace(tokenS.Claims.First(claim => claim.Type == "userID").Value))
                    {
                        user.Id = new Guid(tokenS.Claims.First(claim => claim.Type == "userID").Value);
                    }
                    user.FirstName = tokenS.Claims.First(claim => claim.Type == "firstName").Value;
                    user.LastName = tokenS.Claims.First(claim => claim.Type == "lastName").Value;
                    user.Username = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
                }

            }
            catch (Exception)
            {

                throw;
            }

            return user;
        }

        public async Task<User> addUser(SignUpRequest request)
        {
            var data = new BaseResponse<SignInResponse>();
            User user = new User();
            HttpClient client = new HttpClient();
            var uri = new Uri(Constants.SignUp);
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await client.PostAsync(uri, content);
                HttpStatusCode code = response.StatusCode;
                if (code == HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<BaseResponse<SignInResponse>>(result);

                    var stream = data.Data.Token;
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(stream);
                    var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
                    if (!string.IsNullOrWhiteSpace(tokenS.Claims.First(claim => claim.Type == "userID").Value))
                    {
                        user.Id = new Guid(tokenS.Claims.First(claim => claim.Type == "userID").Value);
                    }
                    user.FirstName = tokenS.Claims.First(claim => claim.Type == "firstName").Value;
                    user.LastName = tokenS.Claims.First(claim => claim.Type == "lastName").Value;
                    user.Username = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
                }

            }
            catch (Exception)
            {
                throw;
            }

            return user;

        }
    }
}
