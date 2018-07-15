using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BotApplication.Dialogs
{
    public class GetQnAAPI
    {
        // NOTE: Replace this with a valid host name.
        static string host = "https://qn.azurewebsites.net/qnamaker";


        // NOTE: Replace this with a valid endpoint key.
        // This is not your subscription key.
        // To get your endpoint keys, call the GET /endpointkeys method.
        static string endpoint_key = "302a6a9f-205a-4c0e-ab64-bd1bb782f8f1";

        // NOTE: Replace this with a valid knowledge base ID.
        // Make sure you have published the knowledge base with the
        // POST /knowledgebases/{knowledge base ID} method.
        static string kb = "c65e1ff3-a025-4df5-b73b-51f85b109486";
        static string key = "9e07c8d00adc45c884561918f5a57a2d";

        static string service = "/qnamaker";
        static string method = "/knowledgebases/" + kb + "/generateAnswer/";


        public async static Task<JObject> Post(string quiz)
        {

            System.Object[] body = new System.Object[] { new { question = quiz } };
            var requestBody = JsonConvert.SerializeObject(body);
            
            var uri = "https://qnamsc.azurewebsites.net/qnamaker/knowledgebases/b7fdf5fa-fd02-4eb4-b4b3-469221550658/generateAnswer";
                       
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);
                request.Content = new StringContent("{\"question\": \"" + quiz + "\"}", Encoding.UTF8, "application/json");
                request.Headers.Add("Authorization", "EndpointKey " + endpoint_key);

                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();
                

                return JObject.Parse(responseBody);
            }
        }



    }
}