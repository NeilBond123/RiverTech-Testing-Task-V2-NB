using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using RiverTech_Testing_Task_V2.Models;
using Xunit;

namespace RiverTech_Testing_Task_V2
{
    [FeatureDescription("API Verification")]
    public class ApiTests : FeatureFixture
    {
        private ApiClient? _apiClient;
        private ApiResponseModel? _apiResponse;
        private int _userID = 1;

        [Scenario]
        [Label("API-1")]
        [InlineData(1)]
        public async Task VerifyApiResponse(int _userID)
        {
            // Using "Given...When...Then" standard for BDD
            await Runner.RunScenarioAsync(
                Given_a_valid_user_id,
                When_calling_API_with_user_id,
                Then_API_response_should_contain_valid_data);
        }

        private Task Given_a_valid_user_id()
        {
            // No additional action is needed in this case
            return Task.CompletedTask;
        }

        private async Task When_calling_API_with_user_id()
        {
            _apiClient = new ApiClient();
            _apiResponse = await _apiClient.GetUserById(_userID);
        }

        private async Task Then_API_response_should_contain_valid_data()
        {
            // Verify the response status code
            Assert.NotNull(_apiResponse);
            Assert.Equal(_userID, _apiResponse.Id);

            // Verify the status code returned by the API
            Assert.True(_apiResponse.Id > 0);

            // Verify that the internal contents are not null
            Assert.False(string.IsNullOrEmpty(_apiResponse.Name));
            Assert.False(string.IsNullOrEmpty(_apiResponse.Username));
            Assert.False(string.IsNullOrEmpty(_apiResponse.Email));

            // Verify that Address model and internal contents are not null
            Assert.NotNull(_apiResponse.Address);
            Assert.False(string.IsNullOrEmpty(_apiResponse.Address.Street));
            Assert.False(string.IsNullOrEmpty(_apiResponse.Address.Suite));
            Assert.False(string.IsNullOrEmpty(_apiResponse.Address.City));
            Assert.False(string.IsNullOrEmpty(_apiResponse.Address.ZipCode));

            // Verify that Geo model and internal contents are not null
            Assert.NotNull(_apiResponse.Address.Geo);
            Assert.False(string.IsNullOrEmpty(_apiResponse.Address.Geo.Lat));
            Assert.False(string.IsNullOrEmpty(_apiResponse.Address.Geo.Lng));

            Assert.False(string.IsNullOrEmpty(_apiResponse.Phone));
            Assert.False(string.IsNullOrEmpty(_apiResponse.Website));

            // Verify that Company model and internal contents are not null
            Assert.NotNull(_apiResponse.Company);
            Assert.False(string.IsNullOrEmpty(_apiResponse.Company.Name));
            Assert.False(string.IsNullOrEmpty(_apiResponse.Company.CatchPhrase));
            Assert.False(string.IsNullOrEmpty(_apiResponse.Company.BS));

            await Task.CompletedTask;
        }
    }
}