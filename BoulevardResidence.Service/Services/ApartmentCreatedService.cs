using BoulevardResidence.Service.DTOs.Aparments;
using BoulevardResidence.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Net;


namespace BoulevardResidence.Service.Services
{
    public class ApartmentCreatedService : IApartmentCreatedService
    {
        private readonly IApartmentService _service;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ApartmentCreatedService(IApartmentService service, HttpClient httpClient, IConfiguration configuration)
        {
            _service = service;

            _httpClient = httpClient;
            _configuration = configuration;
        }


        public async Task CreateApartmentWithRequest(int Id)
        {
            var loginurl = _configuration.GetSection("Apartment:LoginUrl").Value;
            var credentials = @"{
                  ""type"": ""api-app"",
                  ""credentials"": {
                    ""pb_api_key"": ""app-64f03f5fec48e""
                  }
                }";

            var content = new StringContent(credentials, Encoding.UTF8, "application/json");

            var responselogin = await _httpClient.PostAsync(loginurl, content);

            var responseContent = await responselogin.Content.ReadAsStringAsync();
            var responseDatatok = JsonConvert.DeserializeObject<ResponseTokenAuth>(responseContent);

            string accessToken = responseDatatok.access_token;

            string requestUrl = $"https://pb17132.profitbase.ru/api/v4/json/property?access_token={accessToken}&id={Id}";


            // GET isteği gönder
            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

            var responseBody = await response.Content.ReadAsStringAsync();
           
         
            var responsedata = JsonConvert.DeserializeObject<MyResponse>(responseBody);
          
            CreateApartmentDto createApartmentDto = new CreateApartmentDto()
            {
                Id = responsedata.Data[0].Id,
                HouseId = responsedata.Data[0].HouseId,
                HouseName = responsedata.Data[0].HouseName,
                ProjectName = responsedata.Data[0].ProjectName,
                Number = responsedata.Data[0].Number,
                RoomsAmount = responsedata.Data[0].RoomsAmount,
                Floor = responsedata.Data[0].Floor,
                SectionName = responsedata.Data[0].SectionName,
                LayoutType = responsedata.Data[0].LayoutType,
                WithoutLayout = responsedata.Data[0].WithoutLayout,
                Studio = responsedata.Data[0].Studio,
                FreeLayout = responsedata.Data[0].FreeLayout,
                EuroLayout = responsedata.Data[0].EuroLayout,
                TypePurpose = responsedata.Data[0].TypePurpose,
                AreaTotal = responsedata.Data[0].Area.AreaTotal,

                Status = responsedata.Data[0].Status,
                CustomStatusId = responsedata.Data[0].CustomStatusId,
                SpecialOffersIds = responsedata.Data[0].SpecialOffersIds
            };
            await _service.CreateApartment(createApartmentDto);

        }
        public async Task UpdatedApartmentsWithBackgroundService()
        {
            var loginurl = _configuration.GetSection("Apartment:LoginUrl").Value;
            var credentials = @"{
                  ""type"": ""api-app"",
                  ""credentials"": {
                    ""pb_api_key"": ""app-64f03f5fec48e""
                  }
                }";

            var content = new StringContent(credentials, Encoding.UTF8, "application/json");

            var responselogin = await _httpClient.PostAsync(loginurl, content);

            var responseContent = await responselogin.Content.ReadAsStringAsync();
            var responseDatatok = JsonConvert.DeserializeObject<ResponseTokenAuth>(responseContent);

            string accessToken = responseDatatok.access_token;

            string requestUrl = $"https://pb17132.profitbase.ru/api/v4/json/property?access_token={accessToken}&houseId[]=110449&full=false";


            // GET isteği gönder
            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

            var responseBody = await response.Content.ReadAsStringAsync();


            var responsedata = JsonConvert.DeserializeObject<MyResponse>(responseBody);

            foreach (var item in responsedata.Data)
            {
                var apartment = await _service.GetFindApartmentById(item.Id);

                if (apartment==null)
                {
                    await CreateApartmentWithRequest(item.Id);
                }
                else
                {
                    if (apartment.Status!= item.Status)
                    {
                        await _service.UpdateApartmentAsync(apartment.Id, item.Status);
                    }
                }
            }
        

        }
    }

    public class LoginCredentials
    {
        public string Type { get; set; }
        public Credentials Credentials { get; set; }
    }

    public class Credentials
    {
        public string pb_api_key { get; set; }
    }
}
