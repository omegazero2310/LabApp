using CommonClass.Models;
using CommonClass.Models.Request;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MobileAppLab.ApiServices
{
    public class AdminPartServices : BaseApiService, IService<AdminParts>
    {
        public AdminPartServices(HttpClient httpClient) : base(httpClient, "AdminParts", true)
        {
        }

        public Task<ServerRespone> CreateNew(AdminParts entity)
        {
            throw new NotImplementedException();
        }

        public Task<ServerRespone> Delete(object key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AdminParts>> GetAll(int skip = 0, int take = 0, bool isforceRefresh = false)
        {
            try
            {
                if ((Connectivity.NetworkAccess != NetworkAccess.Internet &&
                    !Barrel.Current.IsExpired(key: this.BaseUrl + "/Get")) || isforceRefresh == false)
                {
                    return Barrel.Current.Get<IEnumerable<AdminParts>>(key: this.BaseUrl + "/Get");
                }
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, this.BaseUrl);
                //Get Token from SecureStorage
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UserToken.Token);
                var respone = await HttpClient.SendAsync(message);
                respone.EnsureSuccessStatusCode();
                ServerRespone serverRespone = JsonConvert.DeserializeObject<ServerRespone>(respone.Content.ReadAsStringAsync().Result);
                var listStaff = JsonConvert.DeserializeObject<IEnumerable<AdminParts>>(serverRespone.Result.ToString());
                Barrel.Current.Add(key: this.BaseUrl + "/Get", data: listStaff, expireIn: TimeSpan.FromDays(1));
                return listStaff;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new List<AdminParts>();
            }
        }
        public async Task<IReadOnlyDictionary<int, string>> GetAllAsDictionary()
        {
            var res = await this.GetAll();
            return res.ToDictionary(objKey => objKey.PartID, objValue => objValue.PartName);
        }

        public Task<AdminParts> GetByID(object key)
        {
            throw new NotImplementedException();
        }

        public Task<ServerRespone> Update(AdminParts entity)
        {
            throw new NotImplementedException();
        }
    }
}
