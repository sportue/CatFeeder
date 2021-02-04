using CatFeeder.Models.Request.MapRequest;
using CatFeeder.Models.Response;
using CatFeeder.Models.Response.MapResponse;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CatFeeder.Services
{
    public class MapRestService
    {
        public MapRestService()
        {

        }

        public async Task<BaseResponse<MapCoordinates>> TargetLocations(MapRequest request)
        {

            var data = new BaseResponse<MapCoordinates>();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer" , Constants.CURRENT_USER.Token);
            var uri = new Uri(Constants.TargetLocations);
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await client.PostAsync(uri, content);
                HttpStatusCode code = response.StatusCode;
                if (code == HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<BaseResponse<MapCoordinates>>(result);
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
    }
}
