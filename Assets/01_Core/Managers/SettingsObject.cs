namespace Game.Settings
{
    public abstract class SettingsObject
    {
        internal SettingsObject()
        {
            CameraShake = 1;
        }

        public float CameraShake { get; }
    }
}