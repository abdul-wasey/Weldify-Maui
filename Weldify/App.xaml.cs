using Weldify.Data;
using Weldify.Services;

namespace Weldify;

public partial class App : Application
{
    private readonly AppShell _appShell;
    private readonly DatabaseInitializer _databaseInitializer;

    public App(
        AppShell appShell,
        IThemeService themeService,
        DatabaseInitializer databaseInitializer)
    {
        InitializeComponent();

        _appShell = appShell;
        _databaseInitializer = databaseInitializer;

        themeService.LoadTheme();
    }

    protected override Window CreateWindow(
        IActivationState? activationState)
    {
        return new Window(_appShell);
    }

    protected override async void OnStart()
    {
        base.OnStart();

        await _databaseInitializer.InitializeAsync();
    }
}