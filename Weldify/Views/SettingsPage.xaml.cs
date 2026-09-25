using Weldify.Services;

namespace Weldify.Views;

public partial class SettingsPage : ContentPage
{
    private readonly IThemeService _themeService;

    public SettingsPage(IThemeService themeService)
	{
		InitializeComponent();

        _themeService = themeService;
        ThemeSwitch.IsToggled = _themeService.IsDarkMode;
    }

    private void ThemeSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        _themeService.SetTheme(e.Value);
    }
}