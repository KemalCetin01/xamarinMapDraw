using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinApp.Models;

namespace XamarinApp.Services.ApiServices
{
    public class KonumApiService
    {
        private static HttpClient _httpClient;
        private string baseUrl = $"http://192.168.1.33:45455/api/Konum/";
        public KonumApiService()
        {
            _httpClient = new HttpClient();
        }
 
        public async Task<IEnumerable<LocationModel>> getListAsyc()
        {
     
            IEnumerable<LocationModel> konumModels;


            var response = await _httpClient.GetAsync(baseUrl+ "getListKonum");
            if (response.IsSuccessStatusCode)
            {
                konumModels = JsonConvert.DeserializeObject<IEnumerable<LocationModel>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                konumModels = null;
            }
            return konumModels;

        }
        public async Task<IEnumerable<LocationModel>> getTodayByUserId(int userId)
        {
     
            IEnumerable<LocationModel> konumModels;
            var usrId = userId;

            var response = await _httpClient.GetAsync(baseUrl+ "getTodayByUserId?id="+ usrId);
            if (response.IsSuccessStatusCode)
            {
                konumModels = JsonConvert.DeserializeObject<IEnumerable<LocationModel>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                konumModels = null;
            }

            return konumModels;

        }

        public async Task<string> AddAsync(LocationModel LocationModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(LocationModel), Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync(baseUrl+ "AddKonum", stringContent);

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

        public async Task<LocationModel> getByIdAsycn(int id)
        {
            var response = await _httpClient.GetAsync(baseUrl+"getById?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<LocationModel>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                return null;
            }
        }
        public async Task<string> UpdateAsync(LocationModel LocationModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(LocationModel), Encoding.UTF8, "application/json");


            var response = await _httpClient.PutAsync(baseUrl+"UpdateKonum", stringContent);

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
            var response = await _httpClient.DeleteAsync(baseUrl+"DeleteKonum?id={id}");

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
