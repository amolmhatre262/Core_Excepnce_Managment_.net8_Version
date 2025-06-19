using Expence_Managment_Core_WebApplication.Models;
using System.Net.Http;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Expence_Managment_Core_WebApplication.Models;

namespace Expence_Managment_Core_WebApplication.Services
{
    public class UserServices
    {
        private readonly HttpClient _httpClient;

        public UserServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<object> GetUsersAsync(int? id = null)
        {
            if (id.HasValue)
                return await _httpClient.GetFromJsonAsync<Users>($"https://localhost:44327/api/Authenticate/{id.Value}");
            else
                return await _httpClient.GetFromJsonAsync<List<Users>>("https://localhost:44327/api/Authenticate");
        }

        public async Task<bool> UpdateUserAsync(int id, Users updatedUser)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:44327/api/Authenticate/{id}", updatedUser);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> AddUser(Users updatedUser)
        {
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:44327/api/Authenticate/",updatedUser);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:44327/api/Authenticate/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
