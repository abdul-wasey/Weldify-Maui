using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Weldify.Services;

namespace Weldify.ViewModels;

public partial class AddCustomerViewModel : ObservableObject
{

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _phone = string.Empty;

    [ObservableProperty]
    private string _address = string.Empty;

    [ObservableProperty]
    private bool _isBusy;

    private readonly ICustomerService _customerService;

    public AddCustomerViewModel(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [RelayCommand]
    private async Task SaveCustomerAsync()
    {
        if (IsBusy)
            return;

        if (string.IsNullOrWhiteSpace(Name))
        {
            await Shell.Current.DisplayAlertAsync(
                "Required",
                "Please enter customer name.",
                "OK");

            return;
        }

        if (string.IsNullOrWhiteSpace(Phone))
        {
            await Shell.Current.DisplayAlertAsync(
                "Required",
                "Please enter customer phone number.",
                "OK");

            return;
        }

        try
        {
            IsBusy = true;

            var customer = await _customerService.CreateAsync(
                Name,
                Phone,
                Address);

            await Shell.Current.GoToAsync(
                $"..?customerId={customer.Id}");
        }
        catch (Exception)
        {
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Unable to save customer. Try again later.",
                "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}