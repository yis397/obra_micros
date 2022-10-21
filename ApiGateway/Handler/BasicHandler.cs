using ApiGateway.Models;
using System.Net.Http;
using System.Text.Json;

namespace ApiGateway.Handler
{
    public class BasicHandler: DelegatingHandler
    {
        private readonly RespuetaHandler _respuetaHandler;

        public BasicHandler(RespuetaHandler respuetaHandler)
        {
            _respuetaHandler = respuetaHandler;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage req,CancellationToken cancellationToken)
        {
            var respons = await base.SendAsync(req, cancellationToken);
            if (!respons.IsSuccessStatusCode) return respons;
            var msg=await respons.Content.ReadAsStringAsync();
            var option=new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var msgR=JsonSerializer.Deserialize<string>(msg);
            var idTraba=await _respuetaHandler.GetId(msgR??"nope");
            if(idTraba==null)return respons;
            respons.Content = new StringContent(idTraba, System.Text.Encoding.UTF8, "application/json");
            return respons;
        }
    }
}
