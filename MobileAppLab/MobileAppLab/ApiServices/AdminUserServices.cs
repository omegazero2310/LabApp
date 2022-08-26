using CommonClass.Models;
using CommonClass.Models.Request;
using MonkeyCache.FileStore;
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
            string contentRespone = "";
            try
            {  
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, this.BaseUrl + "/Login");
                LoginRequest userLogin = new LoginRequest();
                userLogin.UserName = userName;
                userLogin.Password = password;
                message.Content = new StringContent(JsonConvert.SerializeObject(userLogin), Encoding.UTF8, "application/json");
                var respone = await HttpClient.SendAsync(message);
                contentRespone = respone.Content.ReadAsStringAsync().Result;
                respone.EnsureSuccessStatusCode();
                //lấy token lưu tạm để dùng cho các lần sau
                ServerRespone serverRespone = JsonConvert.DeserializeObject<ServerRespone>(respone.Content.ReadAsStringAsync().Result);
                await SecureStorage.SetAsync("JWT", serverRespone.Result.ToString());
                return (true,"");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return (false, contentRespone);
            }

        }
        public async static void Logout()
        {
            Barrel.Current.EmptyAll();
            SecureStorage.Remove("JWT");
        }

        public Task<HttpResponseMessage> CreateNew(AdminUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> Delete(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminUser>> GetAll(int skip = 0, int take = 0, bool forceRefresh =false)
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
