using Core;
using Game.Settings;

namespace Game
{
    public class SettingsManager : AbstractManager<SettingsManager>
    {
        private SettingsObject settingsObject;

        public float GetCameraShake()
        {
            return settingsObject.CameraShake;
        }
    }
}