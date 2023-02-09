using System.Text;

namespace radius_minimal_api
{
    public class AuthService
    {
        private static HttpClient client = new HttpClient();

        public static async Task<string> Authenticate(string username, string password)
        {
            var response = await client.PostAsync("https://api.intranet.maracanau.ifce.edu.br/auth/login",
                new StringContent(
                    $"{{\"identificacao\":\"{username}\",\"senha\":\"{password}\"}}",
                    Encoding.UTF8,
                    "application/json"));
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
                
            }
            else
            {
                return null;
            }
        }
    }
}
