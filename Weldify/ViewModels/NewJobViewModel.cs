using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Weldify.Data.Entities;
using Weldify.Enums;
using Weldify.Services;

namespace Weldify.ViewModels;

public partial class NewJobViewModel : ObservableObject
{
    private int _selectedCustomerId;

    private readonly ICustomerService _customerService;
    private readonly IJobService _jobService;

    public NewJobViewModel(
        ICustomerService customerService,
        IJobService jobService)
    {
        _customerService = customerService;
        _jobService = jobService;
    }

    [ObservableProperty]
    private ObservableCollection<Customer> customers = [];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasSelectedCustomer))]
    private Customer? selectedCustomer;

    public bool HasSelectedCustomer => SelectedCustomer is not null;

    [ObservableProperty]
    private string jobNumber = string.Empty;

    [ObservableProperty]
    private string selectedJobStatus = "Draft";

    public List<string> JobStatuses =>
    [
        "Draft", "Quotation", "Approved", "In Production",
        "Ready for Installation", "Completed", "Cancelled"
    ];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowHeightWidth))]
    [NotifyPropertyChangedFor(nameof(ShowDepth))]
    [NotifyPropertyChangedFor(nameof(ShowLength))]
    [NotifyPropertyChangedFor(nameof(ShowLeafCount))]
    [NotifyPropertyChangedFor(nameof(WidthLabel))]
    private string selectedProductType = string.Empty;

    [ObservableProperty]
    private string selectedMaterial = string.Empty;

    [ObservableProperty]
    private string selectedGauge = string.Empty;

    public List<string> ProductTypes =>
    [
        "Safety Grill", "Main Door", "Iron Window", "Balcony Grill",
        "Main Gate", "Stair Railing", "Balcony Railing", "Other"
    ];

    public List<string> MaterialTypes =>
    [
        "Square Pipe", "Rectangular Pipe", "Round Pipe", "Angle Iron",
        "Flat Bar", "Round Bar", "Sheet", "Channel", "Other"
    ];

    public List<string> Gauges => ["12", "14", "16", "18", "20", "22", "24"];

    private bool IsRailing =>
        SelectedProductType is "Stair Railing" or "Balcony Railing";

    public bool ShowHeightWidth => !IsRailing;
    public bool ShowLength => IsRailing;
    public bool ShowDepth => SelectedProductType == "Balcony Grill";
    public bool ShowLeafCount => SelectedProductType == "Main Gate";

    public string WidthLabel =>
        SelectedProductType == "Main Gate" ? "Width per Leaf" : "Width";

    [ObservableProperty] private int heightFeet;
    [ObservableProperty] private int heightInches;
    [ObservableProperty] private int heightSuttar;

    [ObservableProperty] private int widthFeet;
    [ObservableProperty] private int widthInches;
    [ObservableProperty] private int widthSuttar;

    [ObservableProperty] private int depthFeet;
    [ObservableProperty] private int depthInches;
    [ObservableProperty] private int depthSuttar;

    [ObservableProperty] private int lengthFeet;
    [ObservableProperty] private int lengthInches;
    [ObservableProperty] private int lengthSuttar;

    [ObservableProperty] private int quantity = 1;
    [ObservableProperty] private int leafCount = 2;

    [ObservableProperty] private string selectedFinish = string.Empty;
    [ObservableProperty] private string color = string.Empty;

    public List<string> FinishTypes =>
        ["None", "Red Oxide", "Enamel Paint", "Powder Coating", "Other"];

    [ObservableProperty] private bool installationRequired;
    [ObservableProperty] private string siteAddress = string.Empty;
    [ObservableProperty] private string notes = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private bool isSaving;

    private bool CanSave => !IsSaving;

    [RelayCommand(CanExecute = nameof(CanSave))]
    private async Task SaveAsync()
    {
        if (SelectedCustomer is null)
        {
            await ShowErrorAsync("Please select a customer.");
            return;
        }

        if (string.IsNullOrWhiteSpace(JobNumber))
        {
            await ShowErrorAsync("Please enter a job number.");
            return;
        }

        if (string.IsNullOrWhiteSpace(SelectedProductType))
        {
            await ShowErrorAsync("Please select a product type.");
            return;
        }

        if (string.IsNullOrWhiteSpace(SelectedMaterial))
        {
            await ShowErrorAsync("Please select a material type.");
            return;
        }

        if (string.IsNullOrWhiteSpace(SelectedGauge) ||
            !int.TryParse(SelectedGauge, out var gauge))
        {
            await ShowErrorAsync("Please select a gauge.");
            return;
        }

        if (Quantity < 1)
        {
            await ShowErrorAsync("Quantity must be at least 1.");
            return;
        }

        if (ShowLeafCount && LeafCount < 1)
        {
            await ShowErrorAsync("Leaf count must be at least 1.");
            return;
        }

        if (InstallationRequired && string.IsNullOrWhiteSpace(SiteAddress))
        {
            await ShowErrorAsync("Please enter the site address.");
            return;
        }

        if (!TryParseEnum(SelectedProductType, out ProductType productType) ||
            !TryParseEnum(SelectedMaterial, out MaterialType materialType))
        {
            await ShowErrorAsync("The selected product or material is invalid.");
            return;
        }

        if (!Enum.TryParse<JobStatus>(NormalizeEnumName(SelectedJobStatus), out var status))
        {
            await ShowErrorAsync("The selected job status is invalid.");
            return;
        }

        if (!TryParseFinish(SelectedFinish, out var finish))
        {
            await ShowErrorAsync("The selected finish is invalid.");
            return;
        }

        try
        {
            IsSaving = true;

            var job = new Job
            {
                JobNumber = JobNumber.Trim(),
                CustomerId = SelectedCustomer.Id,
                Status = status,
                InstallationRequired = InstallationRequired,
                SiteAddress = string.IsNullOrWhiteSpace(SiteAddress)
                    ? null
                    : SiteAddress.Trim(),
                Finish = finish,
                Color = string.IsNullOrWhiteSpace(Color) ? null : Color.Trim(),
                Notes = string.IsNullOrWhiteSpace(Notes) ? null : Notes.Trim(),
                Items =
                [
                    new JobItem
                    {
                        ProductType = productType,
                        MaterialType = materialType,
                        Gauge = (IronGauge)gauge,
                        HeightMeasurement = BuildMeasurement(HeightFeet, HeightInches, HeightSuttar),
                        WidthMeasurement = ShowHeightWidth
                            ? BuildMeasurement(WidthFeet, WidthInches, WidthSuttar)
                            : null,
                        DepthMeasurement = ShowDepth
                            ? BuildMeasurement(DepthFeet, DepthInches, DepthSuttar)
                            : null,
                        LengthMeasurement = ShowLength
                            ? BuildMeasurement(LengthFeet, LengthInches, LengthSuttar)
                            : null,
                        LeafCount = ShowLeafCount ? LeafCount : 0,
                        Quantity = Quantity,
                        Unit = MeasurementUnit.Feet,
                        DesignNotes = string.IsNullOrWhiteSpace(Notes) ? null : Notes.Trim()
                    }
                ]
            };

            await _jobService.CreateAsync(job);

            await Shell.Current.GoToAsync("..");
        }
        catch (InvalidOperationException ex)
        {
            await ShowErrorAsync(ex.Message);
        }
        catch (Exception)
        {
            await ShowErrorAsync("The job could not be saved. Please try again.");
        }
        finally
        {
            IsSaving = false;
        }
    }

    [RelayCommand]
    private async Task AddCustomerAsync()
    {
        await Shell.Current.GoToAsync(nameof(Views.AddCustomerPage));
    }

    public async Task LoadCustomersAsync()
    {
        var list = await _customerService.GetAllAsync();
        Customers = new ObservableCollection<Customer>(list);
        SelectedCustomer = Customers.FirstOrDefault(c => c.Id == _selectedCustomerId);
    }

    public Task SetSelectedCustomer(int customerId)
    {
        _selectedCustomerId = customerId;
        return Task.CompletedTask;
    }

    private static string BuildMeasurement(int feet, int inches, int suttar)
    {
        return $"{feet} ft {inches} in {suttar} su";
    }

    private static bool TryParseEnum<TEnum>(string value, out TEnum result)
        where TEnum : struct, Enum
    {
        return Enum.TryParse(NormalizeEnumName(value), out result);
    }

    private static bool TryParseFinish(string value, out FinishType finish)
    {
        return Enum.TryParse(NormalizeEnumName(value), out finish);
    }

    private static string NormalizeEnumName(string value)
    {
        return value.Replace(" ", string.Empty);
    }

    private static async Task ShowErrorAsync(string message)
    {
        if (Shell.Current is not null)
            await Shell.Current.DisplayAlert("Save Job", message, "OK");
    }
}
