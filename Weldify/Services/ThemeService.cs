using Weldify.Helpers;

namespace Weldify.Services
{
    public class ThemeService : IThemeService
    {
        public bool IsDarkMode
        {
            get => GetSavedTheme() == AppTheme.Dark;
        }

        public void LoadTheme()
        {
            Application.Current!.UserAppTheme = GetSavedTheme();
        }

        public void SetTheme(bool isDarkMode)
        {
            var theme = isDarkMode
            ? AppTheme.Dark
            : AppTheme.Light;

            Application.Current!.UserAppTheme = theme;

            Preferences.Default.Set(
                PreferencesKeys.ThemeMode,
                isDarkMode
                ? (int)AppTheme.Dark
                : (int)AppTheme.Light);
        }

        private static AppTheme GetSavedTheme()
        {
            var savedTheme = Preferences.Default.Get(
                PreferencesKeys.ThemeMode,
                (int)AppTheme.Light);

            return savedTheme == (int)AppTheme.Dark
                ? AppTheme.Dark
                : AppTheme.Light;
        }
    }
}
