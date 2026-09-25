using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Weldify.Data.Entities;
using Weldify.Services;

namespace Weldify.ViewModels;

public partial class NewJobViewModel : ObservableObject
{
    private int? _pendingCustomerId;

    private readonly ICustomerService _customerService;

    public ObservableCollection<Customer> Customers { get; } = [];

    [ObservableProperty]
    private Customer? _selectedCustomer;

    [ObservableProperty]
    private bool isBusy;

    public NewJobViewModel(
        ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task LoadCustomersAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            var customers = await _customerService.GetAllAsync();

            Customers.Clear();

            foreach (var customer in customers)
            {
                Customers.Add(customer);
            }

            // Select customer after ItemsSource is populated
            if (_pendingCustomerId.HasValue)
            {
                SelectCustomer(_pendingCustomerId.Value);
                _pendingCustomerId = null;
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    public void SetSelectedCustomer(int customerId)
    {
        _pendingCustomerId = customerId;
    }

    private void SelectCustomer(int customerId)
    {
        SelectedCustomer = Customers
            .FirstOrDefault(x => x.Id == customerId);
    }

    partial void OnSelectedCustomerChanged(Customer? value)
    {
        
    }
}