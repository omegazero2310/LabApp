using CommonClass.Models;
using CommonClass.Models.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MobileAppLab.ApiServices
{
    public class AdminUserServices : BaseApiService, IService<AdminUser>
    {
        public AdminUserServices(HttpClient httpClient) : base(httpClient, "AdminUsers")
        {
        }

        public async Task<(bool,string)> Login(string userName, string password)
        {
            try
            {  
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, this.BaseUrl + "/Login");
                LoginRequest userLogin = new LoginRequest();
                userLogin.UserName = userName;
                userLogin.Password = password;
                message.Content = new StringContent(JsonConvert.SerializeObject(userLogin), Encoding.UTF8, "application/json");
                var respone = await HttpClient.SendAsync(message);
                respone.EnsureSuccessStatusCode();
                //lấy token lưu tạm để dùng cho các lần sau
                await SecureStorage.SetAsync("JWT", respone.Content.ReadAsStringAsync().Result);
                return (true,"");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return (false,ex.Message);
            }

        }

        public Task<HttpResponseMessage> CreateNew(AdminUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> Delete(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminUser>> GetAll(int skip = 0, int take = 0)
        {
            throw new NotImplementedException();
        }

        public Task<AdminUser> GetByID(object key)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> Update(AdminUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
