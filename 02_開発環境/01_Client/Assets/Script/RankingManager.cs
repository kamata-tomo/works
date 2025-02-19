using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class RankingManager : MonoBehaviour
{
    [SerializeField] GameObject rankingItemPrefab;
    [SerializeField] GameObject parentGameObject;
    [SerializeField] List<GameObject> ONRankingUI;
    [SerializeField] List<GameObject> OffRankingUI;
    // Start is called before the first frame update


    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "titleScene")
        {
            //ESCでメニュー表示
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OFFRanking();
            }
        }
    }
    public void OFFRanking()
    {
        for (int i = 0; i < ONRankingUI.Count; ++i)
        {
            ONRankingUI[i].SetActive(false);
        }
        for (int i = 0; i < OffRankingUI.Count; ++i)
        {
            OffRankingUI[i].SetActive(true);
        }
        foreach (Transform n in parentGameObject.transform)
        {
            GameObject.Destroy(n.gameObject);
        }
    }

    public void LoadRanking()
    {
        for (int i = 0; i < ONRankingUI.Count; ++i)
        {
            ONRankingUI[i].SetActive(true);
        }
        for (int i = 0;i < OffRankingUI.Count; ++i)
        {
            OffRankingUI[i].SetActive(false);
        }
        StartCoroutine(GetRanking());
    }

    public void UpdateRanking()
    {
        StartCoroutine(AddHiscore());
    }

    IEnumerator GetRanking()
    {

        UnityWebRequest request = UnityWebRequest.Get($"https://functionappge202405.azurewebsites.net/api/Rankings/get?id={GameManager.StageID}");

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("x-functions-key", "A0db6gVnMsfxsVm-JJc94QsuiEAyFYvsC2KOANMCZ0WIAzFuoKvXNQ==");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            List<HIscore> hIscoList = JsonConvert.DeserializeObject<List<HIscore>>(json);
            foreach (HIscore hIscore in hIscoList)
            {
                GameObject textObject = Instantiate(
                    rankingItemPrefab,
                    parentGameObject.transform.position,
                    Quaternion.identity,
                    parentGameObject.transform);

                textObject.GetComponent<Text>().text = hIscore.GetRankText();

            }
        }
        else
        {
            Debug.Log("Error: GetHiscore UnityWedRequest Failed.");
        }
    }

    IEnumerator AddHiscore()
    {
        HIscore hiscore = new HIscore(GameManager.ScoreTime);
        string json = JsonConvert.SerializeObject(hiscore);

        UnityWebRequest request = UnityWebRequest.Post(
            "https://functionappge202405.azurewebsites.net/api/Rankings/add",
            json,
            "application/json");
        request.SetRequestHeader("x-functions-key", "A0db6gVnMsfxsVm-JJc94QsuiEAyFYvsC2KOANMCZ0WIAzFuoKvXNQ==");
        Debug.Log("ハイスコア送信します");


        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error:AddHiscore UnityWedRequest Failed.");

        }
        else
        {
            Debug.Log("ハイスコア送信完了!");
        }
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void Retry()
    {
        SceneManager.LoadScene("GameScene");
    }
}
