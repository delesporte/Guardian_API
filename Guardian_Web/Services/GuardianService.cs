﻿using Guardian_Utility;
using Guardian_Web.Models.API;
using Guardian_Web.Models.DTO.Guardian;
using Guardian_Web.Services.IServices;

namespace Guardian_Web.Services
{
    public class GuardianService : BaseService, IGuardianService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string apiUrl;
        private string version;

        public GuardianService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            apiUrl = configuration.GetValue<string>("ServicesUrls:GuardianAPI");
            version = "v1";
        }

        public Task<T> CreateAsync<T>(GuardianCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIResquest()
            {
                ApiType=SD.ApiType.POST,
                Data=dto,
                Url=apiUrl+ $"/api/{version}/guardianAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIResquest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = apiUrl + $"/api/{version}/guardianAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIResquest()
            {
                ApiType = SD.ApiType.GET,
                Url = apiUrl + $"/api/{version}/guardianAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIResquest()
            {
                ApiType = SD.ApiType.GET,
                Url = apiUrl + $"/api/{version}/guardianAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdadeAsync<T>(GuardianUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIResquest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = apiUrl + $"/api/{version}/guardianAPI/" + dto.Id,
                Token = token
            });
        }
    }
}
