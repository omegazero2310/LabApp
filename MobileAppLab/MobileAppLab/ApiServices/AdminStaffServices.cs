using CommonClass.Enums;
using CommonClass.Models;
using CommonClass.Models.Request;
using MobileAppLab.Properties;
using MobileAppLab.Utilities;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MobileAppLab.ApiServices
{
    public class AdminStaffService : BaseApiService, IService<AdminStaff>
    {
        private static readonly Dictionary<string, PositionOptions> _staffPositions = new Dictionary<string, PositionOptions>
                {
                    {LocalizationResourceManager.Instance[nameof(AppResource.Position_NV)],PositionOptions.NV },
                    {LocalizationResourceManager.Instance[nameof(AppResource.Position_TP)],PositionOptions.TP },
                    {LocalizationResourceManager.Instance[nameof(AppResource.Position_GD)],PositionOptions.GD },
                };
        public AdminStaffService(HttpClient httpClient) : base(httpClient, "AdminStaffs", true)
        {

        }

        public async Task<HttpResponseMessage> GetProfilePicture(int id)
        {
            try
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, this.BaseUrl + $"/GetProfilePicture?id={id}");
                //Get Token from SecureStorage
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UserToken.Token);
                var respone = await HttpClient.SendAsync(message);
                respone.EnsureSuccessStatusCode();
                return respone;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            }
        }
        public async Task<HttpResponseMessage> UploadProfilePicture(int id, string filePath)
        {
            try
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, this.BaseUrl + $"/UploadProfilePicture?id={id}");
                //Get Token from SecureStorage
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UserToken.Token);

                using (var multipartFormContent = new MultipartFormDataContent())
                {
                    //Load the file and set the file's Content-Type header
                    var fileStreamContent = new StreamContent(File.OpenRead(filePath));
                    fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");

                    //Add the file
                    multipartFormContent.Add(fileStreamContent, name: "file", fileName: "profile.png");
                    message.Content = multipartFormContent;
                    //Send it
                    var response = await HttpClient.SendAsync(message);
                    response.EnsureSuccessStatusCode();
                    return response;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }
        public async Task<HttpResponseMessage> CreateNew(AdminStaff entity)
        {
            try
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, this.BaseUrl + "/Create");
                message.Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
                //Get Token from SecureStorage
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UserToken.Token);
                var respone = await HttpClient.SendAsync(message);
                respone.EnsureSuccessStatusCode();
                return respone;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<HttpResponseMessage> Delete(object key)
        {
            try
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, this.BaseUrl + $"/Delete?id={key}");
                //Get Token from SecureStorage
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UserToken.Token);
                var respone = await HttpClient.SendAsync(message);
                respone.EnsureSuccessStatusCode();
                return respone;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<IEnumerable<AdminStaff>> GetAll(int skip = 0, int take = 0, bool isForceRefresh = false)
        {
            try
            {
                if ((Connectivity.NetworkAccess != NetworkAccess.Internet &&
                    !Barrel.Current.IsExpired(key: this.BaseUrl + $"/GetAll")) || isForceRefresh == false)
                {
                    return Barrel.Current.Get<IEnumerable<AdminStaff>>(key: this.BaseUrl + $"/GetAll");
                }
                var asm = this.GetType().Assembly;
                //ảnh mặc định
                System.IO.Stream stream = asm.GetManifestResourceStream("MobileAppLab.AssetImages.icon_default_profile_pic.png");
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, (int)stream.Length);

                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, this.BaseUrl + $"/GetAll");
                //Get Token from SecureStorage
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UserToken.Token);
                var respone = await HttpClient.SendAsync(message);
                respone.EnsureSuccessStatusCode();
                ServerRespone serverRespone = JsonConvert.DeserializeObject<ServerRespone>(respone.Content.ReadAsStringAsync().Result);
                var listStaff = JsonConvert.DeserializeObject<IEnumerable<AdminStaff>>(serverRespone.Result.ToString());
                foreach (var staff in listStaff)
                {
                    //staff.PositionName = _staffPositions.Where(row => row.Value == staff.PositionID).FirstOrDefault().Key ?? "";
                    var res = await this.GetProfilePicture(staff.ID);
                    if (res.IsSuccessStatusCode)
                    {
                        ServerRespone serverResponeImg = JsonConvert.DeserializeObject<ServerRespone>(res.Content.ReadAsStringAsync().Result);
                        var imgData = (byte[])serverResponeImg.Result;
                        if (imgData?.Length > 0)

                            staff.ProfilePicture = await res.Content.ReadAsByteArrayAsync();
                        else
                            staff.ProfilePicture = data;
                    }
                }
                Barrel.Current.Add(key: this.BaseUrl + $"/GetAll", data: listStaff, expireIn: TimeSpan.FromDays(1));
                return listStaff;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new List<AdminStaff>();

            }
        }

        public async Task<AdminStaff> GetByID(object key)
        {
            try
            {
                if ((Connectivity.NetworkAccess != NetworkAccess.Internet &&
                    !Barrel.Current.IsExpired(key: this.BaseUrl + $"/Get/{key}")))
                {
                    return Barrel.Current.Get<AdminStaff>(key: this.BaseUrl + $"/Get/{key}");
                }
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, this.BaseUrl + $"/Get?id={key}");
                //Get Token from SecureStorage
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UserToken.Token);
                var respone = await HttpClient.SendAsync(message);
                respone.EnsureSuccessStatusCode();
                ServerRespone serverRespone = JsonConvert.DeserializeObject<ServerRespone>(respone.Content.ReadAsStringAsync().Result);
                var staff = JsonConvert.DeserializeObject<AdminStaff>(serverRespone.Result.ToString());
                var profilePic = await this.GetProfilePicture(staff.ID);

                if (profilePic.IsSuccessStatusCode)
                {
                    staff.ProfilePicture = await profilePic.Content.ReadAsByteArrayAsync();
                }

                Barrel.Current.Add(key: this.BaseUrl + $"/Get/{key}", data: staff, expireIn: TimeSpan.FromDays(1));
                return staff;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<HttpResponseMessage> Update(AdminStaff entity)
        {
            try
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Put, this.BaseUrl + "/Update");
                message.Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
                //Get Token from SecureStorage
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UserToken.Token);
                var respone = await HttpClient.SendAsync(message);
                respone.EnsureSuccessStatusCode();
                return respone;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
