using BloodConnect.Domain.DTOs;
using BloodConnect.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Infrastructure.Services
{
    public class AddressService : IAddressService
    {
        private readonly string _baseUri = "http://viacep.com.br/ws/{0}/json";

        public async Task<AddressDto?> GetAddressByCEP(string cep)
        {
            HttpClient httpClient = new HttpClient();
            var url = string.Format(_baseUri, cep);
            httpClient.BaseAddress = new Uri(url);

            HttpResponseMessage response = httpClient.GetAsync("").Result;
            if (response.IsSuccessStatusCode)
            {
                AddressDto? parseObject = response.Content.ReadFromJsonAsync<AddressDto?>().Result;
                return parseObject;
            }

            return null;
        }
    }
}
