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
using Xamarin.Essentials;

namespace CatFeeder.Services
{
    public class UserRestService
    {

        public UserRestService()
        {

        }
      
        public async Task<bool> LoginWithToken()
        {
            try
            {
                var token = await SecureStorage.GetAsync("user_token");
                if (!String.IsNullOrWhiteSpace(token))
                {
                    
                    User user = await getUserByToken(token);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }

        }
        public async Task<BaseResponse<SignInResponse>> getUserByMailAndPassword(string email, string password)
        {
            var data = new BaseResponse<SignInResponse>();
            
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
                    if (data.HasError || string.IsNullOrWhiteSpace(stream))
                    {
                        return data;
                    }
                    await getUserByToken(stream);
                }
            }
            catch (Exception ex)
            {
                data.HasError = true;
                data.Message = ex.Message;
                return data;
            }

            return data;
        }

        public async Task<User> getUserByToken(string stream)
        {
            User user = new User();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            if (!string.IsNullOrWhiteSpace(tokenS.Claims.First(claim => claim.Type == "userID").Value))
            {
                user.Id = new Guid(tokenS.Claims.First(claim => claim.Type == "userID").Value);
            }
            user.FirstName = tokenS.Claims.First(claim => claim.Type == "firstName").Value;
            user.LastName = tokenS.Claims.First(claim => claim.Type == "lastName").Value;
            user.Email = tokenS.Claims.First(claim => claim.Type == "email").Value;
            user.Username = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
            user.Token = stream;
            Constants.CURRENT_USER = user;
            await SecureStorage.SetAsync("user_token", user.Token);
            return user;

        }

        public async Task<BaseResponse<SignInResponse>> getUserByFacebookToken(string tokenAccess)
        {
            var data = new BaseResponse<SignInResponse>();
            
            HttpClient client = new HttpClient();

            var uri = new Uri(Constants.SignInWithFacebook);
            FacebookRequest request = new FacebookRequest();
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
                    if (data.HasError || string.IsNullOrWhiteSpace(stream))
                    {
                        return data;
                    }
                    await getUserByToken(stream);
                }
            }
            catch (Exception ex)
            {
                data.HasError = true;
                data.Message = ex.Message;
                return data;
            }

            return data;
        }

        public async Task<BaseResponse<SignInResponse>> addUser(SignUpRequest request)
        {

            var data = new BaseResponse<SignInResponse>();
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
                    if (data.HasError || string.IsNullOrWhiteSpace(stream))
                    {
                        return data;
                    }
                    await getUserByToken(stream);
                }
            }
            catch (Exception ex)
            {
                data.HasError = true;
                data.Message = ex.Message;
                return data;
            }

            return data;

        }

        public async Task<BaseResponse<bool>> ForgotPassword(string email, string username)
        {
            var data = new BaseResponse<bool>();

            HttpClient client = new HttpClient();

            var uri = new Uri(Constants.ForgotPassword);
            var request = new ForgetPasswordRequest();
            request.Email = email;
            request.Username = username;
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(uri, content);
                HttpStatusCode code = response.StatusCode;
                if (code == HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<BaseResponse<bool>>(result);
                }

            }
            catch (Exception)
            {

                throw;
            }

            return data; 
        }
    }
}
