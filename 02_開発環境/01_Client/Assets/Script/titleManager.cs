using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms;



public class titleManager : MonoBehaviour
{
    [SerializeField] GameObject UPButton;
    [SerializeField] GameObject DOWNButton;

    public Text stageText;

    public void Start()
    {
        GameManager.StageID = 0;
        DOWNButton.SetActive(false);
        stageText.GetComponent<Text>().text = GameManager.StageID.ToString();

    }

    public void StartGame()
    {
        Initiate.Fade("gameScene", Color.black, 1.0f);

    }

    public void UpStage()
    {
        if (3 != GameManager.StageID)
        {
            GameManager.StageID++;
            stageText.GetComponent<Text>().text = GameManager.StageID.ToString();
            DOWNButton.SetActive(true);
            if (1 == GameManager.StageID)
            {
                UPButton.SetActive(false);
            }
        }
        else
        {
            UPButton.SetActive(false);
        }
    }

    public void downStage()
    {
        if (0 != GameManager.StageID)
        {
            GameManager.StageID--;
            stageText.GetComponent<Text>().text = GameManager.StageID.ToString();
            UPButton.SetActive(true);
            if (0 == GameManager.StageID)
            {
                DOWNButton.SetActive(false);
            }
        }
        else
        {
            DOWNButton.SetActive(false);
        }

    }

    public void EndGame()
    {
#if UNITY_EDITOR // Unity 工デイタの場合
       UnityEditor.EditorApplication.isPlaying = false;
#else 
            //ビルドの場合
        Application.Quit();
#endif
    }
}
