using FluToDoApp.Configuration;
using FluToDoApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FluToDoApp.Services
{
    public class TodoApiService : IApiService<TodoItem>
    {
        HttpClient client;
        string urlBase;
        IConfiguration _configuration;
        
        public TodoApiService()
        {
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            client = new HttpClient(clientHandler);
            //_configuration = new ConfigurationApp();
            urlBase = "/api/todo";//_configuration.ApiUrlBase;
            client.BaseAddress = new Uri("http://192.168.1.102:8080");
        }
        public async Task<bool> AddItemAsync(TodoItem item)
        {
            string json = JsonConvert.SerializeObject(item);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(urlBase, httpContent);
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
                HttpResponseMessage response = await client.DeleteAsync($"{urlBase}/{id}");
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
                HttpResponseMessage response = await client.GetAsync($"{urlBase}/{id}");
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
                HttpResponseMessage response = await client.GetAsync(urlBase);
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
                HttpResponseMessage response = await client.PutAsync($"{urlBase}/{id}", httpContent);
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
