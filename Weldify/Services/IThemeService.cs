namespace Weldify.Services
{
    public interface IThemeService
    {
        bool IsDarkMode { get; }

        void LoadTheme();

        void SetTheme(bool isDarkMode);
    }
}
