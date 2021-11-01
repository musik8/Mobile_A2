using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
//using Newtonsoft.Json;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace sourceTree
{
    class NetworkManager
    {

        private string url = "https://api.imgur.com/3/upload";
        private string ClientId = "b5244252c7f5bff";
       
        HttpClient client = new HttpClient();
      
         
        public NetworkManager()
        {
            client.DefaultRequestHeaders.Add("Authorization", "Client-ID " + ClientId);
        }


        public async Task<string> ImageUpload(ImageModel img)
        {

 
            string ResultLink = "Try Again";
            ImgUrl urlObj;
            Uri uri = new Uri(string.Format(url, string.Empty));

                try
                {
                    string json = JsonSerializer.Serialize<ImageModel>(img);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = null;
   
                        response = await client.PostAsync(uri, content);
          
                    if (response.IsSuccessStatusCode)
                    {
                    Console.WriteLine(@"\tTodoItem successfully saved.");
                        var jsonString = await response.Content.ReadAsStringAsync();

                        var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);

                        var array = dic.ElementAt(2).Value;
                        var dic2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(array.ToString());

                        urlObj = JsonConvert.DeserializeObject<ImgUrl>(array.ToString());
                   
                        return urlObj.link;


                }

                }
                catch (Exception ex)
                {
                     Console.WriteLine(@"\tERROR {0}", ex.Message);
                }

            return ResultLink;
         
        }
    }
}

