using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private static float m_fTimer;
    public float CurrentTime { get { return m_fTimer; } }

    public static bool m_bActive = false;

    private void Update()
    {
        if (m_bActive)
        {
            m_fTimer += Time.deltaTime;
        }
    }

    public static void OnStart()
    {
        m_bActive = true;
    }
    public static void OnStop()
    {
        m_bActive = false;
    }
    public static void OnReset()
    {
        m_fTimer = 0f;
        OnStop();
    }
}
