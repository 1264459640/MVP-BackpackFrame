using UnityEngine;

namespace Manager
{
    public class Singleton<T> where T : new()
    {
        protected static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                    (instance as IInitable)?.Init();

                    return instance;
                }

                return instance;
            }
        }
    }
    public class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        var obj = new GameObject(typeof(T).Name, typeof(T));
                        DontDestroyOnLoad(obj);
                        instance = obj.GetComponent<T>();
                        (instance as IInitable)?.Init();
                    }
                    else
                    {
                        Debug.LogWarning("Instance is already exist!");
                    }
                }

                return instance;
            }
        }
        
        protected virtual void Awake()
        {
            instance = this as T;
            DontDestroyOnLoad(this);
        }
    }
    public interface IInitable
    {
        void Init();
    }
}