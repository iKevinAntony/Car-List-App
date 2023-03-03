using CarListApp.Maui.ViewModels;

namespace CarListApp.Maui.Views;

public partial class CarDetailPage : ContentPage
{
    private readonly CarDetailViewModel carDetailViewModel;

    public CarDetailPage(CarDetailViewModel carDetailViewModel)
	{
		InitializeComponent();
		BindingContext= carDetailViewModel;
        this.carDetailViewModel = carDetailViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await carDetailViewModel.GetCarData();
    }
}