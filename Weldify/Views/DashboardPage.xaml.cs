using Weldify.ViewModels;

namespace Weldify.Views;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}