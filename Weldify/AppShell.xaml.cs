using Weldify.Views;

namespace Weldify
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(NewJobPage), typeof(NewJobPage));
            Routing.RegisterRoute(nameof(AddCustomerPage), typeof(AddCustomerPage));
        }
    }
}
