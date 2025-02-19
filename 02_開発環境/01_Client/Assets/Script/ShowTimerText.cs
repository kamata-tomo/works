
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTimerText : MonoBehaviour
{
    public Text m_txtTimer;
    public GameTimer m_gameTimer;
    private void Update()
    {
        m_txtTimer.text = string.Format("{0:D2}:{1:D2}:{2:D2}",
            (int)m_gameTimer.CurrentTime / 60,
            (int)m_gameTimer.CurrentTime % 60,
            (int)(m_gameTimer.CurrentTime * 100) % 60
            );

    }
}
