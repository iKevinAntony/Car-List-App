using Android.OS;
using AndroidX.AppCompat.View.Menu;
using CarListApp.Maui.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Maui.ViewModels
{
    public partial class LoadingPageViewModel:BaseViewModel
    {
        public LoadingPageViewModel()
        {
            CheckUserLoginDetails();
        }

        private async void CheckUserLoginDetails()
        {
            //Retrieve token from internel storage
            var token = await SecureStorage.GetAsync("Token");
            if(string.IsNullOrEmpty(token))
            {
              await GoToLoginPage();
            }
            else
            {
                var jsonToken=new JwtSecurityTokenHandler().ReadToken(token)as JwtSecurityToken;
                if(jsonToken.ValidTo<DateTime.UtcNow)
                {
                    SecureStorage.Remove("Token");
                    await GoToLoginPage();
                }
                else
                {
                    var role = jsonToken.Claims.FirstOrDefault(q => q.Type.Equals(ClaimTypes.Role))?.Value;
                    App.UserInfo = new UserInfo()
                    {
                        UserName = jsonToken.Claims.FirstOrDefault(q => q.Type.Equals(ClaimTypes.Email))?.Value,
                        Role = role
                    };
                   
                    await GoToMainPage();
                }
            }
                //evaluate token and devcide if valid
        }
        private async Task GoToLoginPage()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }
        private async Task GoToMainPage()
        {
            await Shell.Current.GoToAsync($"{nameof(MainPage)}");
        }
    }
}
