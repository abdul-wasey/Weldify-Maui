using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Weldify.Views;

namespace Weldify.ViewModels
{
    public partial class DashboardPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _greeting = string.Empty;

        [ObservableProperty]
        private int _todaysJobs;

        [ObservableProperty]
        private int _activeJobs;

        [ObservableProperty]
        private int _pendingQuotations;

        public DashboardPageViewModel()
        {
            Refresh();
        }

        public void Refresh()
        {
            Greeting = GetGreeting();

            // Temporary values.
            // These will come from SQLite.
            TodaysJobs = 0;
            ActiveJobs = 0;
            PendingQuotations = 0;
        }

        private static string GetGreeting()
        {
            var hour = DateTime.Now.Hour;

            return hour switch
            {
                >= 5 and < 12 => "Good morning",
                >= 12 and < 17 => "Good afternoon",
                >= 17 and < 22 => "Good evening",
                _ => "Good night"
            };
        }

        [RelayCommand]
        private async Task NewJobAsync()
        {
            await Shell.Current.GoToAsync(nameof(NewJobPage));
        }
    }
}
