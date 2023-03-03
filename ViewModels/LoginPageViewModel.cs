using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Maui.ViewModels
{
    public partial class LoginPageViewmodel:BaseViewModel
    {
        private readonly CarApiService carApiService;
        public LoginPageViewmodel(CarApiService carApiService)
        {
            this.carApiService = carApiService;
        }
        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;
       

        [RelayCommand]
        async Task Login()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await DisplayLoginMessage("Please fill all fields");
               
            }
            else
            {
                //call Api to Login Attempt
                var loginModel =new LoginModel(username, password);
                var response=await carApiService.Login(loginModel);
                //display  message
                await DisplayLoginMessage(carApiService.StatusMessage);
                if(!string.IsNullOrEmpty(response.Token))
                {
                    //store token in secure storage 
                    await SecureStorage.SetAsync("Token", response.Token);
                    

                    //build a menu on the fly ....based on the user role
                    var jsonToken=new JwtSecurityTokenHandler().ReadToken(response.Token) as 
                        JwtSecurityToken;
                    var role=jsonToken.Claims.FirstOrDefault(q=>q.Type.Equals(ClaimTypes.Role))?.Value;
                    var userInfo = new UserInfo
                    {
                        UserName = Username,
                        Role=role

                    };
                    
                    //navigate to app's mainpage
                    await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                }
                else
                {
                await DisplayLoginMessage("Invalid Login Attempt");
                } 
            }
        }
        async Task DisplayLoginMessage(string message)
        {
            await Shell.Current.DisplayAlert("Login Attempt Result", message, "OK");
            Password = string.Empty;

        }
    }
}
