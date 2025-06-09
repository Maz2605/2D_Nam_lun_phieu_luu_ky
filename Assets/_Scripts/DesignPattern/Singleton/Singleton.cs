using UnityEngine;

public class Singleton <T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _lock = new object();
    
    public static bool DontDestroyOnLoadEnabled { get; set; } = true;


    public static T Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("There is more than one singleton");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "[Singleton] " + typeof(T);
                        
                        if(DontDestroyOnLoadEnabled)
                            DontDestroyOnLoad(singleton);
                    }
                    
                }
            }

            return _instance;
        }

        set
        {
            if (_instance != null)
            {
                _instance = value;
            }
        }
    }

    
    public virtual void KeepAlive(bool alive)
    {
        DontDestroyOnLoadEnabled = alive;
    }
    protected virtual void Awake()
    {
        lock (_lock)
        {
            if (_instance == null)
            {
                _instance = this as T;

                if (DontDestroyOnLoadEnabled)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else if (_instance != this)
            {
                Debug.LogWarning("[Singleton] Duplicate instance of " + typeof(T) + " detected. Destroying new instance.");
                Destroy(gameObject);
            }
            
        }
    }

    private void OnDestroy()
    {
        if(_instance == this)
            _instance = null;
    }
    protected virtual void OnDisable()
    {
        if (!Application.isPlaying)
        {
            Instance = null;
        }
    }
}
