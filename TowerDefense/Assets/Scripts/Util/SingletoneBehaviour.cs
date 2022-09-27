using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SingletoneBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_Instance;

    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<T>();
                if (m_Instance == null)
                {
                    GameObject go = new GameObject($"@{nameof(T)}");
                    m_Instance = go.AddComponent<T>();
                }
                DontDestroyOnLoad(m_Instance.gameObject);
            }

            return m_Instance;
        }
    }

    private void Awake()
    {
        if (m_Instance != null)
        {
            if (m_Instance != this)
            {
                Destroy(gameObject);
            }

            return;
        }
        else
        {
            m_Instance = GetComponent<T>();
            DontDestroyOnLoad(gameObject);
        }
    }
}