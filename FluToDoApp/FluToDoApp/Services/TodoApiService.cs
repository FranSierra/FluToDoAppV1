using FluToDoApp.Configuration;
using FluToDoApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace FluToDoApp.Services
{
    public class TodoApiService : IApiService<TodoItem>
    {
        HttpClient client;
        string apiUrl;
        private IConfiguration _configuration => DependencyService.Get<IConfiguration>();

        public TodoApiService()
        {
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri(_configuration.UrlBase);
            apiUrl = _configuration.ApiUrl;

        }
        public async Task<bool> AddItemAsync(TodoItem item)
        {
            string json = JsonConvert.SerializeObject(item);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(apiUrl, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error al crear item: {e.Message}");
            }

            return false;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"{apiUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error al eliminar el item: {e.Message}");
            }
            return false;
        }

        public async Task<TodoItem> GetItemAsync(string id)
        {
            var item = new TodoItem();
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{apiUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<TodoItem>(content);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error al obtener el item: {e.Message}");
            }

            return item;
        }

        public async Task<IEnumerable<TodoItem>> GetItemsAsync(bool forceRefresh = false)
        {
            List<TodoItem> items = new List<TodoItem>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<List<TodoItem>>(content);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error al obtener items: {e.Message}");
            }


            return items;
        }

        public async Task<bool> UpdateItemAsync(TodoItem item)
        {
            var id = item.Key;
            string json = JsonConvert.SerializeObject(item);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await client.PutAsync($"{apiUrl}/{id}", httpContent);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Ha ocurrido el siguiente error al actuaizar: {e.Message}");
            }

            return false;
        }
    }
}
