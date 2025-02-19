using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEditor.SearchService;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject gameClearText;
    [SerializeField] AudioClip gameClearSE;
    AudioSource AudioSource;
    [SerializeField]  List<GameObject> Stage;
    [SerializeField] UIManager uiManager;
    [SerializeField] GameTimer m_gameTimer;
    [SerializeField] RankingManager rankingManager;




    public static int StageID;
    public static int ScoreTime;
    public static bool gameClear;

    private void Start()
    {

        for (int i = 0; i < Stage.Count; i++)
        {
            Stage[i].gameObject.SetActive(false);
        }
        gameClearText.SetActive(false);
        Stage[StageID].gameObject.SetActive(true);
        AudioSource = GetComponent<AudioSource>();
        gameClear=false;
    }



    public void GameClear()
    {
        ScoreTime = (((int)m_gameTimer.CurrentTime / 60) * 10000) + (((int)m_gameTimer.CurrentTime % 60) * 100) + ((int)(m_gameTimer.CurrentTime * 100) % 60);
        rankingManager.UpdateRanking();
        GameTimer.OnStop();
        gameClear = true;
        gameClearText.SetActive(true);
        AudioSource.PlayOneShot(gameClearSE);
        uiManager.ClearUI();
        //Invoke("ReturnTitle", 3.0f);

    }

    public void RestartScene()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        Initiate.Fade(thisScene.name, Color.black, 1.0f);

    }

    public void ReturnTitle()
    {
        Initiate.Fade("titleScene", Color.black, 1.0f);

    }
}
