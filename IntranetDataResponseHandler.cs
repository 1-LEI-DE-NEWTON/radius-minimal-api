using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace radius_minimal_api
{
    public class IntranetDataResponseHandler
    {
        public static bool ConnectionsHandler(string response)
        {
            if (response == null)
            {
                return false;
            }

            //Deserialize response
            Token responseObj = JsonConvert.DeserializeObject<Token>(response);
            string tokenString = responseObj.data.token;

            //Check if user is allowed to connect
            if (DecodeToken(tokenString) != "3")
            {
                return false;
            }
            
            else
            {
                return true;
            }

        }

        private static string DecodeToken(string response)
        {            
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(response) as JwtSecurityToken;

            return jsonToken.Claims.First(
                claim => claim.Type == "pessoa_tipo_id").Value;
        }
    }

    public class DataToken
    {
        public string token { get; set; }
    }

    public class Token
    {
        public DataToken data { get; set; }
    }
}
