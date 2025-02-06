namespace Church_GIDS.Service
{
    public class MapService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public MapService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleMaps:ApiKey"];
        }

        public async Task<string> GetShortestPath(double originLat, double originLng, double destLat, double destLng)
        {
            var url = $"https://maps.googleapis.com/maps/api/directions/json?origin={originLat},{originLng}&destination={destLat},{destLng}&key={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
