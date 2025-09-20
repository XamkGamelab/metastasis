using UnityEngine;

namespace SLC.RetroHorror.Core
{
    /// <summary>
    /// Generic MonoBehavior Singleton base class. Inherit from this slightly
    /// streamlined Singleton implementation.
    /// Usage: public class ExampleSingleton : MonoSingleton<ExampleSingleton>
    /// </summary>
    /// <typeparam name="T">Class to make singleton of.</typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public static T Instance { get; protected set; }

        protected virtual void Awake()
        {
            if (Instance == null) Instance = this as T;
            else
            {
                Destroy(gameObject);
                string warning = string.Concat($"Found multiple instances of {typeof(T).Name}\n",
                                                "Deleting extra instance, please fix this.");
                Debug.LogWarning(warning);
            }
        }
    }
}