using Game.Settings;

namespace _04_UI.Screens.Settings
{
    public interface ILoadListener
    {
        void OnLoadSettings(SettingsObject settings);
    }
}