using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_Instance;

    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance
    {
        get
        {
            // Create new instance if one doesn't already exist.
            if (m_Instance == null)
            {
                var existing = FindObjectOfType<T>();
                if (existing != null)
                {
                    m_Instance = existing;
                }
                else
                {
                    // Need to create a new GameObject to attach the singleton to.
                    var singletonObject = new GameObject();
                    m_Instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";
                }

                // Make instance persistent.
                DontDestroyOnLoad(m_Instance.gameObject);
            }

            return m_Instance;
        }
    }

    public static bool IsInstanceValid { get => m_Instance != null; }
}
