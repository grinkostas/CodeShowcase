using Core.Utilities;
using UnityEngine;

namespace Core.Settings
{
    public abstract class Settings : ScriptableObject { }
    public abstract class Settings<T> : Settings where T : ScriptableObject
    {
        protected static T _cachedDefault;

        public static T data
        {
            get
            {
                if (_cachedDefault == null)
                {
                    _cachedDefault = Resources.Load<T>($"{CorePaths.ResourcesSettingsFolder}/{typeof(T).Name}");
                }
                return _cachedDefault;
            }
        }
    }
}