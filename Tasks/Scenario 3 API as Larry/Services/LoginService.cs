using Newtonsoft.Json;
using RestSharp;
using Tasks.Scenario_3_API_as_Larry.ResponseModels;

namespace Tasks.Scenario_3_API_as_Larry.Services
{
    public class LoginService
    {
        private readonly RestClient _client;

        public LoginService(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        public async Task<GetLoginFailResponseModel> GetLoginFailTotal(string userName = null)
        {
            var request = new RestRequest(Path.Endpoints.LoginFailTotal, Method.Get);

            if (userName != null)
            {
                request.AddParameter(Path.LoginParameters.Get.UserName, userName);
            }

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var result = JsonConvert.DeserializeObject<GetLoginFailResponseModel>(response.Content);
                return result;
            }
            else
            {
                throw new Exception($"Request failed with status code {response.StatusCode}");
            }
        }

        public async Task<GetLoginFailResponseModel> GetLoginFailTotalWithFailCount(int failCount)
        {
            var request = new RestRequest(Path.Endpoints.LoginFailTotal, Method.Get);
            request.AddParameter(Path.LoginParameters.Get.FailCount, failCount);

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var result = JsonConvert.DeserializeObject<GetLoginFailResponseModel>(response.Content);
                return result;
            }
            else
            {
                throw new Exception($"Request failed with status code {response.StatusCode}");
            }
        }

        public async Task<GetLoginFailResponseModel> GetLoginFailTotalWithFetchLimit(int fetchLimit)
        {
            var request = new RestRequest(Path.Endpoints.LoginFailTotal, Method.Get);
            request.AddParameter(Path.LoginParameters.Get.FetchLimit, fetchLimit);

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var result = JsonConvert.DeserializeObject<GetLoginFailResponseModel>(response.Content);
                return result;
            }
            else
            {
                throw new Exception($"Request failed with status code {response.StatusCode}");
            }
        }

        public async Task<RestResponse> PutResetLoginFailTotal(string userName)
        {
            var request = new RestRequest(Path.Endpoints.ResetLoginFailTotal, Method.Put);
            request.AddParameter(Path.LoginParameters.Put.UserName, userName);

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return response;
            }
            else
            {
                throw new Exception($"Request failed with status code {response.StatusCode}");
            }
        }

        public async Task<RestResponse> PostLogin(string userName, string password)
        {
            var loginData = new
            {
                user_name = userName,
                password = password
            };

            var request = new RestRequest(Path.Endpoints.Login, Method.Post);
            request.AddJsonBody(loginData);

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return response;
            }
            else
            {
                throw new Exception($"Request failed with status code {response.StatusCode}");
            }
        }
    }
}
