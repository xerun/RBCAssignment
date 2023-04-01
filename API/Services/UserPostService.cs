using API.Helper;
using API.Models;
using API.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Services
{
    public class UserPostService
    {
        private IHttpClientWrapper _httpClientWrapper;

        public UserPostService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<List<UserPost>> GetAllUserPost()
        {
            try
            {
                var response = await _httpClientWrapper.GetAsync(ApiUrls.PostsUrl);
                response.EnsureSuccessStatusCode();

                using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<List<UserPost>>(stream) ?? new List<UserPost>();
            }
            catch(Exception ex)
            {
                // the exception can be logged as well
                _ = ex;
                return new List<UserPost>();
            }
        }
    }
}
