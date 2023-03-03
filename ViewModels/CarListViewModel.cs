using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using CarListApp.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;


namespace CarListApp.Maui.ViewModels
{
    public partial class CarListViewModel : BaseViewModel
    {

        const string editButtonText = "Update Car";
        const string createButtonText = "Add Car";
        private readonly CarApiService CarApiService;
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;
        string message=string.Empty;

        public ObservableCollection<Car> Cars { get; private set; } = new();
        public CarListViewModel(CarApiService carApiService)
        {
            Title = "Car List";
            addEditButtonText = createButtonText;
            this.CarApiService = carApiService;

            //GetCarList().Wait();
        }
        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        string make;
        [ObservableProperty]
        string model;
        [ObservableProperty]
        string vin;
        [ObservableProperty]
        int carId;
        [ObservableProperty]
        string addEditButtonText;
        [RelayCommand]
        async Task GetCarList()
        {
            if(IsLoading) return;
            try
            {
                IsLoading= true;
               if(Cars.Any())
                {
                    Cars.Clear(); 
                }
                var cars=new List<Car>();
               if (accessType==NetworkAccess.Internet)
                {
                 cars = await this.CarApiService.GetCars();
                }
                else
                {
                 cars =App.CarService.GetCars();
                }
                foreach (var car in cars)
                {
                    Cars.Add(car);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable To Get Cars:{ex.Message}");
                await ShowAlert("Failed to retreive list of car");
            }
            finally 
            {
              IsLoading = false;
                IsRefreshing = false;
            }
        }
        [RelayCommand]
        async Task GetCarDetails(int id)
         {
            if(id== 0) return;
            await Shell.Current.GoToAsync($"{nameof(CarDetailPage)}?Id={id}", true);
            
         }
        [RelayCommand]
        async Task SaveCar()
        {
            if(string.IsNullOrEmpty(Make)||string.IsNullOrEmpty(Model)||string.IsNullOrEmpty(Vin))
            {
                await ShowAlert("Please fill all fields");
                return;
            }
            var car = new Car
            {
                Make= Make,
                Model= Model,
                Vin= Vin,
            };
            if (CarId != 0)
            {
                car.Id = CarId;
                if(accessType == NetworkAccess.Internet)
                {
                    await CarApiService.UpdateCar(CarId, car);
                    message = CarApiService.StatusMessage;
                }
                else
                {
                    App.CarService.UpdateCar(car);
                    message = App.CarService.StatusMessage;
                }
              
            }
            else
            {
                if (accessType == NetworkAccess.Internet)
                {
                    await CarApiService.AddCar(car);
                    message= CarApiService.StatusMessage;
                }
                else
                {
                    App.CarService.AddCar(car);
                    message = App.CarService.StatusMessage;
                } 
            }
            await ShowAlert(message);
            await GetCarList();
            await ClearForm();
        }
        [RelayCommand]
        async Task DeleteCar(int id)
        {
            if(id== 0)
            {
                await Shell.Current.DisplayAlert("Invalid Record", "Please try again", "Ok");
                return;
            }
            if (accessType == NetworkAccess.Internet)
            {
               await CarApiService.DeleteCar(id);
               message= CarApiService.StatusMessage;
            }
            else
            {
                var result = App.CarService.DeleteCar(id);
                message= App.CarService.StatusMessage;
            }
            await ShowAlert(message);
            await GetCarList();
        }
        [RelayCommand]
        async Task UpdateCar()
        {
            addEditButtonText = editButtonText;
            return;
        }
        [RelayCommand]
        async Task SetEditMode(int id)
        {
            AddEditButtonText = editButtonText;
            CarId= id;
            var car = new Car();
            if (accessType == NetworkAccess.Internet)
            {
                car = await CarApiService.GetCar(id);
            }
            else
            {
                car = App.CarService.GetCar(id);
            }

            Make =car.Make;
            Model = car.Model;  
            Vin=car.Vin;
        }
        [RelayCommand]
        async Task ClearForm()
        {
            AddEditButtonText = createButtonText;
            CarId = 0;
            Make=string.Empty;
            Model = string.Empty;
            Vin=string.Empty;
        }
        private async Task ShowAlert(string message)
        {
            await Shell.Current.DisplayAlert("Info", message, "Ok");
        }
    }
}
