using MeterReaddingApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Security.Claims;
using MeterReaddingApp.Helpers;
using System.Diagnostics;


namespace MeterReaddingApp.Services
{
    public class ApiService
    {

        public static async Task<bool> Login(string email, string password, string sbu)
        {
            try
            {
                var loginModel = new LoginModel()
                {
                    Email = email,
                    Password = password,
                    Sbu = sbu
                };

                var httpClient = new HttpClient();
                var json = JsonConvert.SerializeObject(loginModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://walkroute.kadswac.org/api/Accounts/login", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
                    return false;
                }

                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Token>(jsonResult);
                Preferences.Set("accessToken", result.access_token);
                Preferences.Set("userId", result.user_Id);
                Preferences.Set("tokenExpirationTime", result.expiration_Time);
                Preferences.Set("currentTime", (int)UnixTimeHelper.GetCurrentTime());
                Preferences.Set("username", email); // Save the username here
                Preferences.Set("sbu", result.Sbu); // Save the SBU here from the response
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

        public static async Task<List<CustomersByDistrictCode>> GetCustomersByDistrictCode(string code)
        {
            //await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync($"https://walkroute.kadswac.org/api/Customers/{code}");
            return JsonConvert.DeserializeObject<List<CustomersByDistrictCode>>(response);
        }

        public static class TokenValidator
        {
            public static async Task CheckTokenValidity()
            {
                var expirationTime = Preferences.Get("tokenExpirationTime", 0);
                Preferences.Set("currentTime", (int)UnixTimeHelper.GetCurrentTime());
               
                var currentTime = Preferences.Get("currentTime", 0);
                if (expirationTime < currentTime)
                {
                    var email = Preferences.Get("email", string.Empty);
                    var password = Preferences.Get("password", string.Empty);
                    var sbu = Preferences.Get("Sbu", string.Empty);
                    await ApiService.Login(email, password, sbu);
                }

            }
        }

        //public static async Task<bool> Login(string email, string password)
        //{
        //    var loginModel = new LoginModel()
        //    {
        //        Email = email,
        //        Password = password
        //    };

        //    var httpClient = new HttpClient();
        //    var json = JsonConvert.SerializeObject(loginModel);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    var response = await httpClient.PostAsync("https://walkroute.kadswac.org/api/Accounts/login", content);
        //    if (!response.IsSuccessStatusCode) return false;
        //    var jsonResult = await response.Content.ReadAsStringAsync();
        //    var result = JsonConvert.DeserializeObject<Token>(jsonResult);
        //    Preferences.Set("accessToken", result.access_token);
        //    Preferences.Set("userId", result.user_Id);
        //    Preferences.Set("tokenExpirationTime", result.expiration_Time);
        //    //Preferences.Set("currentTime", (int)UnixTime.GetCurrentTime());
        //    Preferences.Set("currentTime", (int)UnixTimeHelper.GetCurrentTime());
        //    return true;
        //}



        //public static async Task<bool> Login(string email, string password)
        //{
        //    var login = new Login()
        //    {
        //        Email = email,
        //        Password = password
        //    };

        //    var httpClient = new HttpClient();
        //    var json = JsonConvert.SerializeObject(login);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/Accounts/login", content);
        //    if (!response.IsSuccessStatusCode) return false;
        //    var jsonResult = await response.Content.ReadAsStringAsync();
        //    var result = JsonConvert.DeserializeObject<Token>(jsonResult);
        //    Preferences.Set("accesstoken", result.AccessToken);
        //    Preferences.Set("user_id", result.UserId);
        //    Preferences.Set("username", result.UserName);
        //    Preferences.Set("sbu", result.Sbu);         
        //    return true;
        //}

    }
}
