using System.Collections;
using UnityEngine;

public sealed class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager m_instance;
    private static CoroutineManager instance
    {
        get
        {
            if (m_instance is null)
            {
                var go = new GameObject("[COROUTINE MANAGER]");
                m_instance = go.AddComponent<CoroutineManager>();
                DontDestroyOnLoad(go);
            }
            return m_instance;
        }
    }

    public static Coroutine Start(IEnumerator enumerator)
    {
        return instance.StartCoroutine(enumerator);
    }

    public static void Stop(Coroutine routine)
    {
        instance.StopCoroutine(routine);
    }
}