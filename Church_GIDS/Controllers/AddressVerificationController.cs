using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Church_GIDS.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressVerificationController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public AddressVerificationController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("verify")]
        public async Task<IActionResult> VerifyAddress([FromQuery] string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                return BadRequest("Address parameter is required.");
            }

            // Set up the request
            string requestUri = $"https://nominatim.openstreetmap.org/search?format=json&q={address}";
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.UserAgent.ParseAdd("Church_GIDS/1.0 (nikhiluprety456@gmail.com)");

            // Send the request
            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Error retrieving address information.");
            }

            string responseBody = await response.Content.ReadAsStringAsync();
            JArray jsonResponse = JArray.Parse(responseBody);

            if (jsonResponse.Count == 0)
            {
                return NotFound("Address not found.");
            }

            var result = jsonResponse[0];
            string formattedAddress = result["display_name"].ToString();
            double latitude = (double)result["lat"];
            double longitude = (double)result["lon"];

            return Ok(new
            {
                Address = formattedAddress,
                Latitude = latitude,
                Longitude = longitude
            });
        }
    }
}
