using UnityEngine;

namespace RogueTactics
{
    public class Singletone<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        private const string MultipleInstancesError =
            "There's more than one ";
        private const string Delimiter = " - ";

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError(MultipleInstancesError +
                    typeof(T).Name + Delimiter + Instance);
                Destroy(gameObject);

                return;
            }

            Instance = this as T;
        }
    }
}
