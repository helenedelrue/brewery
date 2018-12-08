using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Breweries.Models.DB;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration; 

namespace Breweries.Utils
{
    //Class that is responsible for feeding the data from the given API into the created database. 
    //Ensure that data is only pushed into the database once. 
    public static class DataSeeder
    { 
        public static string APIKey { get; set; }

        public static void SeedBreweriesAsync(BreweryDatabaseContext context)
        {
            if (!context.Brewery.Any())
            {
                Task<string> breweryTask = GetBreweryAsync("https://guides.unibooker.com/api/packages/Filter?apiKey=" + APIKey + "&language=en");

                var json = JObject.Parse(breweryTask.Result);

                var json_responses = json["Response"];

                foreach (JObject brewery in json_responses)
                {
                    Brewery newBrewery = new Brewery();
                    newBrewery.Id = brewery["ID"].ToString();
                    newBrewery.Title = brewery["Title"].ToString();
                    newBrewery.Email = brewery["ContactCard"]["Email"].ToString();
                    newBrewery.Website = brewery["ContactCard"]["Website"].ToString();
                    newBrewery.Telephone = brewery["ContactCard"]["Telephone"].ToString();
                    if (newBrewery != null)
                    {
                        context.AddRange(newBrewery);
                        context.SaveChanges();
                    }
                }
            }
        }


        static async Task<string> GetBreweryAsync(string path)
        {
            using (var httpClientHandler = new HttpClientHandler()) {
                //fix for SSL certificate being expired, override SSL certificate check on a HTTP call with the anonymous callback function 
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(httpClientHandler)) {
                    HttpResponseMessage response = await client.GetAsync(path);

                    if (response.IsSuccessStatusCode)
                    {
                        var response_string = await response.Content.ReadAsStringAsync();
                        return response_string;
                    }
                    return "Error";
                }
            }
        }
    }
}
