﻿using CommonClass.Models;
using CommonClass.Models.Request;
using Newtonsoft.Json;
using Org.Apache.Http.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static Android.Content.ClipData;
using static Android.Resource;

namespace MobileAppLab.ApiServices
{
    public class AdminStaffService : BaseApiService, IService<AdminStaff>
    {
        public AdminStaffService(HttpClient httpClient) : base(httpClient, "AdminStaffs", true)
        {
        }

        public async Task<HttpResponseMessage> GetProfilePicture(int id)
        {
            try
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, $"/GetProfilePicture?id={id}");
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
        public async Task<HttpResponseMessage> UploadProfilePicture(int id, string filePath)
        {
            try
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, $"/UploadProfilePicture?id={id}");
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
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "/Create");
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
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AdminStaff>> GetAll(int skip = 0, int take = 0)
        {
            throw new NotImplementedException();
        }

        public async Task<AdminStaff> GetByID(object key)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> Update(AdminStaff entity)
        {
            throw new NotImplementedException();
        }
    }
}
