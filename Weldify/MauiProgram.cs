using CommunityToolkit.Maui;
using MauiIcons.Material;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;
using Weldify.Data;
using Weldify.Services;
using Weldify.ViewModels;
using Weldify.Views;

namespace Weldify
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionToolkit()
                .UseMaterialMauiIcons()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            AddSqliteDb(builder);
            AddViews(builder);
            AddViewModels(builder);
            AddServices(builder);

            builder.Services.AddSingleton<AppShell>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void AddSqliteDb(MauiAppBuilder builder)
        {
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, $"{nameof(Weldify)}Db.db3");

            builder.Services.AddDbContextFactory<AppDbContext>(options =>
            {
                options.UseSqlite($"Data Source={databasePath}");
            });

            builder.Services.AddSingleton<DatabaseInitializer>();
        }

        private static void AddViews(MauiAppBuilder builder)
        {
            builder.Services.AddTransient<DashboardPage>();
            builder.Services.AddTransient<NewJobPage>();
            builder.Services.AddTransient<JobsPage>();
            builder.Services.AddTransient<CustomersPage>();
            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<AddCustomerPage>();
        }

        private static void AddViewModels(MauiAppBuilder builder)
        {
            builder.Services.AddTransient<DashboardPageViewModel>();
            builder.Services.AddTransient<AddCustomerViewModel>();
            builder.Services.AddTransient<NewJobViewModel>();
        }

        private static void AddServices(MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<IThemeService, ThemeService>();
            builder.Services.AddTransient<ICustomerService, CustomerService>();
        }
    }
}
