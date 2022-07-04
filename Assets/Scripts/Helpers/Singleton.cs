using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected void InitInstance(T instance)
    {
        if (Instance == null)
        {
            Instance = instance;
        }
    }
}
