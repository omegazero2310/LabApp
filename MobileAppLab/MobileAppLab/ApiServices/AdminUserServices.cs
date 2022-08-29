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
    /// <summary>
    /// Service tương tác với API đăng nhập
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="MobileAppLab.ApiServices.BaseApiService" />
    /// <seealso cref="MobileAppLab.ApiServices.IService&lt;CommonClass.Models.AdminUser&gt;" />
    public class AdminUserServices : BaseApiService, IService<AdminUser>
    {
        public AdminUserServices(HttpClient httpClient) : base(httpClient, "AdminUsers")
        {
        }
        /// <summary>
        /// Đăng nhập vào hệ thống
        /// </summary>
        /// <param name="userName">tên đăng nhập.</param>
        /// <param name="password">mật khẩu.</param>
        /// <returns>Lưu lại JWT và trả kết quả <c>true</c> và chuỗi trống, trả về false và thông báo lỗi nếu có lỗi xảy ra </returns>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
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
        /// <summary>
        /// Đăng xuất, xóa tất cả cache và JWT
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public async static void Logout()
        {
            Barrel.Current.EmptyAll();
            SecureStorage.Remove("JWT");
        }

        public Task<ServerRespone> CreateNew(AdminUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<ServerRespone> Delete(object key)
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

        public Task<ServerRespone> Update(AdminUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
