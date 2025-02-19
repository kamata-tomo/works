using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    public static bool Pause;
    [SerializeField] List<GameObject> UIOnPause;
    [SerializeField] List<GameObject> UIOffPause;
    [SerializeField] List<GameObject> UIClear;
    [SerializeField] List<GameObject> UIOnGame;

    // Start is called before the first frame update
    void Start()
    {
        Pause = false;

        GameTimer.OnReset();
        GameTimer.OnStart();
        Pause = false;
        for (int i = 0; i < UIOnPause.Count; i++)
        {
            UIOnPause[i].SetActive(Pause);
        }
        for (int i = 0; i < UIOffPause.Count; i++)
        {
            UIOffPause[i].SetActive(!Pause);
        }
    }
    private void Update()
    {
        //ESC�Ń��j���[�\��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Pause)
            {
                InPause();
            }else
            {
                OutPause();
            }
        }

    }
    // Update is called once per frame
    public void InPause()//�|�[�Y�����j���[�\������\��
    {
        Pause = true;
        for (int i = 0; i < UIOnPause.Count; i++)
        {
            UIOnPause[i].SetActive(Pause);
        }
        for (int i = 0; i < UIOffPause.Count; i++)
        {
            UIOffPause[i].SetActive(!Pause);
        }
        GameTimer.OnStop();
    }
    public void OutPause()//�|�[�Y���������j���[�\������\��
    {
        Pause = false;
        for (int i = 0; i < UIOnPause.Count; i++)
        {
            UIOnPause[i].SetActive(Pause);
        }
        for (int i = 0; i < UIOffPause.Count; i++)
        {
            UIOffPause[i].SetActive(!Pause);
        }
        GameTimer.OnStart();

    }

    public void ClearUI()//�N���AUI�\��
    {
        for (int i = 0; i < UIClear.Count; i++)
        {
            UIClear[i].SetActive(GameManager.gameClear);
        }
        for (int i = 0; i < UIOnGame.Count; i++)
        {
            UIOnGame[i].SetActive(!GameManager.gameClear);
        }
    }
}
