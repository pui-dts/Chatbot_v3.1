using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
// NOTE: Install the Newtonsoft.Json NuGet package.
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BotApplication
{
    public class TranslateAPI
    {
        public static async Task<JArray> GetTranslate(string contentText,string lang)
        {
            var host = "https://api.cognitive.microsofttranslator.com";
            var path = "/translate?api-version=3.0";
            // Translate to German and Italian.
            var params_ = "";


            if (lang == "TH")
            {
              params_ = "&to=th";
            }
            else if(lang == "EN")
            {
               params_ = "&to=en";
            }

                var uri = host + path + params_;

            // NOTE: Replace this example key with a valid subscription key.
            var key = "AIzaSyBdXeVmsW6QkC3sJz9QZi5hhPjNz1sgjeg";



            System.Object[] body = new System.Object[] { new { Text = contentText } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);

                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(responseBody), Formatting.Indented);


                return JArray.Parse(responseBody);
            }

        }

    }
}