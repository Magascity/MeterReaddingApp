using MeterReaddingApp.Services;
using System.Diagnostics;

namespace MeterReaddingApp.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    async void BtnLogin_Clicked(System.Object sender, System.EventArgs e)
    {

        try
        {
            var response = await ApiService.Login(EntEmail.Text, EntPassword.Text, "");
            if (response)
            {
                Application.Current.MainPage = new HomePage();
            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid email or password.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            Debug.WriteLine($"Exception in BtnLogin_Clicked: {ex.Message}");
        }


        //var response = await ApiService.Login(EntEmail.Text, EntPassword.Text);
        //if (response)
        //{
        //    Application.Current.MainPage = new HomePage();
        //}
        //else
        //{
        //    await DisplayAlert("", "Oops something went wrong.", "Cancel");
        //}

    }

    //async void TapJoinNow_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    //{
    //    await Navigation.PushModalAsync(new RegisterPage());
    //}

}