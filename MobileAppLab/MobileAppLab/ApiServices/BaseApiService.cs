﻿using CommonClass.Models.Request;
using Newtonsoft.Json;
using System.Net.Http;
using Xamarin.Essentials;

namespace MobileAppLab.ApiServices
{
    public class BaseApiService
    {
        protected HttpClient HttpClient;
        protected string BaseUrl = "";
        protected UserTokens UserToken;
        public BaseApiService(HttpClient httpClient, string baseUrl, bool isAuthorize = false)
        {
            HttpClient = httpClient;
            this.BaseUrl = App.API_URL + baseUrl;
            if (isAuthorize)
                this.GetToken();
        }
        public virtual bool GetToken()
        {
            if (UserToken == null)
            {
                var getJwtString = SecureStorage.GetAsync("JWT")?.Result ?? "";
                if (!string.IsNullOrEmpty(getJwtString))
                {
                    UserToken = JsonConvert.DeserializeObject<UserTokens>(getJwtString);
                    return true;
                }
                else
                {
                    throw new System.Exception("MSG_NOT_LOGINED");
                }
            }
            else
                return true;
        }
    }
}