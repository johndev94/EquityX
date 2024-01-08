using EquityX.Services;
using EquityX.ViewModel;

namespace EquityX.View;

public partial class RegisterPage : ContentPage
{

    public RegisterPage() //MainViewModel viewModel
    {
        InitializeComponent();
        //BindingContext = viewModel;
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var email = emailEntry.Text;
        var password = passwordEntry.Text;
        var confirmPassword = confirmPasswordEntry.Text;

        if (IsValidRegistration(email, password, confirmPassword))
        {
            // Proceed with registration
            try
            {
                // Assuming you have a method to register the user
                await RegisterUser(email, password);
                await DisplayAlert("Success", "Registration successful", "OK");

                // Navigate to login or main page after successful registration
                // Navigation logic here...
            }
            catch (Exception ex)
            {
                // Handle any exceptions during registration
                await DisplayAlert("Error", $"Registration failed: {ex.Message}", "OK");
            }
        }
        else
        {
            // Show error message
            await DisplayAlert("Error", "Invalid registration details", "OK");
        }
    }

    private bool IsValidRegistration(string email, string password, string confirmPassword)
    {
        // Check if the email is in a valid format
        if (!IsValidEmail(email))
        {
            return false;
        }

        // Check if the password meets your criteria (length, complexity, etc.)
        if (password.Length < 8) // Example: minimum 8 characters
        {
            return false;
        }

        // Check if password and confirm password match
        if (password != confirmPassword)
        {
            return false;
        }

        return true;
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private async Task RegisterUser(string email, string password)
    {
        
        User newUser = new User
        {
            Email = email,
            Password = password,
            Balance = 0,  
            Portfolio = 0,
            
        };
        await DatabaseContext.AddUserAsync(newUser);

        // Implement the logic to register the user
        // This could involve sending data to a server, saving it in a database, etc.
        // For example:
        // await myAuthService.RegisterNewUser(email, password);
    }
}
