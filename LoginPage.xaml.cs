using CarListApp.Maui.ViewModels;

namespace CarListApp.Maui;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewmodel _loginPageViewmodel)
	{
		InitializeComponent();
	    BindingContext= _loginPageViewmodel; 
	}
}