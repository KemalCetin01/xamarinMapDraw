using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinApp.Models;
namespace XamarinApp.Services.ApiServices
{
    public class UserApiService
    {
        private readonly HttpClient _httpClient;
        public UserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<UserModel>> getListAsyc()
        {
            IEnumerable<UserModel> userModels;
            var response = await _httpClient.GetAsync("https://localhost:44358/User/getListUser");
            if (response.IsSuccessStatusCode)
            {
                userModels = JsonConvert.DeserializeObject<IEnumerable<UserModel>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                userModels = null; 
            }
            return userModels;

        }

        public async Task<string> AddAsync(UserModel UserModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(UserModel), Encoding.UTF8, "application/json");


            var response = await _httpClient.PostAsync("https://localhost:44358/User/AddUser", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                //loglama yapılabilir
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public async Task<UserModel> getByIdAsycn(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44358/User/getById?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UserModel>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                return null;
            }
        }
        public async Task<string> UpdateAsync(UserModel UserModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(UserModel), Encoding.UTF8, "application/json");


            var response = await _httpClient.PutAsync("https://localhost:44358/User/UpdateUser", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                //loglama yapılabilir
                return response.Content.ReadAsStringAsync().Result;
            }
        }
        public async Task<string> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:44358/User/DeleteUser?id={id}");

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                //loglama yapılabilir
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}