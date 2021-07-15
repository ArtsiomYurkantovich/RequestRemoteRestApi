using ConsoleTables;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TestTaskRemoteRestApi.Model;

namespace TestTaskRemoteRestApi
{
    class GetDataFromRemoteApi
    {

        private readonly string url;

        public GetDataFromRemoteApi(string url)
        {
            this.url = url;
        }

        public async Task DeserelizationAsync()
        {
            var result = await GetJsonStringAsync();
            DataObjects product = JsonSerializer.Deserialize<DataObjects>(result);
            var table = new ConsoleTable("Product name", "Category product");

            foreach (var prod in product.Products)
            {
                foreach (var cat in product.Categories)
                {
                    if (prod.CategoryId == cat.Id)
                    {
                        table.AddRow($"{prod.Name}", $"{cat.Name}");
                    }
                }
            }

            table.Write();
        }

        private async Task<string> GetUriAsync(Uri uri)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage result = await client.GetAsync(uri);
                if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadAsStringAsync();
                }
            }

            return string.Empty;
        }

        private async Task<string> GetJsonStringAsync()
        {
            var result = await GetUriAsync(new Uri(url));
            return JObject.Parse(result).ToString();
        }
    }
}
