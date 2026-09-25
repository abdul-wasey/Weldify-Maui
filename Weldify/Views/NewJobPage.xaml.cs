using Weldify.ViewModels;

namespace Weldify.Views;

public partial class NewJobPage : ContentPage, IQueryAttributable
{
    private readonly NewJobViewModel _viewModel;

    public NewJobPage(
        NewJobViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadCustomersAsync();
    }

    public async void ApplyQueryAttributes(
        IDictionary<string, object> query)
    {
        if (query.TryGetValue("customerId", out var value) &&
            int.TryParse(value?.ToString(), out var customerId))
        {
            _viewModel.SetSelectedCustomer(customerId);
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current!.GoToAsync(nameof(AddCustomerPage));
    }
}