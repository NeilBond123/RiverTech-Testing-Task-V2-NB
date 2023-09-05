using Newtonsoft.Json;
using RiverTech_Testing_Task_V2.Models;

namespace RiverTech_Testing_Task_V2
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
        }

        public async Task<ApiResponseModel> GetUserById(int userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/users/{userId}");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            ApiResponseModel apiResponse = JsonConvert.DeserializeObject<ApiResponseModel>(responseBody);

            return apiResponse;
        }
    }
}
