using Weldify.ViewModels;

namespace Weldify.Views;

public partial class AddCustomerPage : ContentPage
{
    public AddCustomerPage(AddCustomerViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}