namespace ApiGateway.Models
{
    public class RespuetaHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RespuetaHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<String> GetId(string id)
        {
            try
            {
                var resp=await _httpClientFactory.CreateClient("TrabajadorService").GetAsync($"api/Usuarios/{id}");
                if (!resp.IsSuccessStatusCode) return "nop";
                return await resp.Content.ReadAsStringAsync();
            }
            catch
            {
                return "nop";
            }
        }
    }
}
