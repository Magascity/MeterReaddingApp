

using MeterReaddingApp.Models;

namespace MeterReaddingApp.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        BindingContext = new HomePageViewModel();

        // Retrieve and display the user name and SBU
        var userName = Preferences.Get("username", string.Empty);
        var sbu = Preferences.Get("sbu", string.Empty);
        UserNameLabel.Text = $"Hi {userName}";
        SbuLabel.Text = $"SBU: {sbu}";
    }
}